using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace SidebarExplorer.App.Services;

public sealed class InstanceCoordinator : IDisposable
{
    private static readonly string AppDataRoot = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "Edgetree");

    private static readonly string InstancesRoot = Path.Combine(AppDataRoot, "instances");

    private static readonly HashSet<string> ReservedDeviceNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "CON",
        "PRN",
        "AUX",
        "NUL",
        "COM1",
        "COM2",
        "COM3",
        "COM4",
        "COM5",
        "COM6",
        "COM7",
        "COM8",
        "COM9",
        "LPT1",
        "LPT2",
        "LPT3",
        "LPT4",
        "LPT5",
        "LPT6",
        "LPT7",
        "LPT8",
        "LPT9",
    };

    private static readonly HashSet<char> InvalidFileNameChars = new(Path.GetInvalidFileNameChars());

    private readonly Mutex _mutex;
    private bool _disposed;

    private InstanceCoordinator(int slotId, Mutex mutex)
    {
        SlotId = slotId;
        _mutex = mutex;
        InstanceDirectory = GetInstanceDirectory(slotId);
    }

    public static string InstancesRootDirectory => InstancesRoot;

    public int SlotId { get; }

    public string InstanceDirectory { get; }

    public static InstanceCoordinator? TryAcquire()
    {
        for (var slotId = 0; slotId < int.MaxValue; slotId++)
        {
            if (TryAcquireSlot(slotId, out var coordinator))
            {
                return coordinator;
            }
        }

        return null;
    }

    public static InstanceCoordinator Acquire()
        => TryAcquire() ?? throw new InvalidOperationException("No free Edgetree instance slot was available.");

    public static string GetInstanceDirectory(int slotId)
    {
        ValidateSlotId(slotId);
        return Path.Combine(InstancesRootDirectory, GetSlotDirectoryName(slotId));
    }

    public static string GetInstanceStateFilePath(int slotId, string fileName)
    {
        return Path.Combine(GetInstanceDirectory(slotId), SanitizeFileName(fileName));
    }

    public string GetInstanceStateFilePath(string fileName)
        => Path.Combine(InstanceDirectory, SanitizeFileName(fileName));

    public static string SanitizeFileName(string fileName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);

        var builder = new System.Text.StringBuilder(fileName.Length);

        foreach (var ch in fileName)
        {
            if (InvalidFileNameChars.Contains(ch) || ch == Path.DirectorySeparatorChar || ch == Path.AltDirectorySeparatorChar)
            {
                builder.Append('_');
            }
            else
            {
                builder.Append(ch);
            }
        }

        var sanitized = builder.ToString().Trim();
        sanitized = sanitized.TrimEnd('.');

        if (sanitized.Length == 0)
        {
            sanitized = "state";
        }

        var baseName = Path.GetFileNameWithoutExtension(sanitized);
        if (baseName.Length == 0)
        {
            sanitized = "state" + Path.GetExtension(sanitized);
            baseName = Path.GetFileNameWithoutExtension(sanitized);
        }

        if (ReservedDeviceNames.Contains(baseName))
        {
            sanitized = "_" + sanitized;
        }

        return sanitized;
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;

        try
        {
            _mutex.ReleaseMutex();
        }
        catch (ApplicationException)
        {
        }
        catch (ObjectDisposedException)
        {
        }
        finally
        {
            _mutex.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    private static bool TryAcquireSlot(int slotId, out InstanceCoordinator? coordinator)
    {
        ValidateSlotId(slotId);

        coordinator = null;
        var mutexName = GetSlotMutexName(slotId);
        Mutex? mutex = null;

        try
        {
            mutex = new Mutex(initiallyOwned: true, mutexName, out var createdNew);
            if (createdNew)
            {
                coordinator = new InstanceCoordinator(slotId, mutex);
                return true;
            }

            try
            {
                if (mutex.WaitOne(0))
                {
                    coordinator = new InstanceCoordinator(slotId, mutex);
                    return true;
                }
            }
            catch (AbandonedMutexException)
            {
                coordinator = new InstanceCoordinator(slotId, mutex);
                return true;
            }

            mutex.Dispose();
            mutex = null;
            return false;
        }
        catch
        {
            mutex?.Dispose();
            throw;
        }
    }

    private static void ValidateSlotId(int slotId)
    {
        if (slotId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(slotId));
        }
    }

    private static string GetSlotDirectoryName(int slotId)
        => $"slot-{slotId.ToString("D4", CultureInfo.InvariantCulture)}";

    private static string GetSlotMutexName(int slotId)
        => $"Local\\Edgetree-InstanceSlot-{slotId.ToString(CultureInfo.InvariantCulture)}";
}


