using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using SidebarExplorer.App.Models;

namespace SidebarExplorer.App.Services;

public class SettingsService
{
    private const int SplitSettingsSchemaVersion = 1;
    private const string GlobalNodeName = "Global";
    private const string GlobalWriteMutexName = "Local\\Edgetree-SettingsWrite";
    private const string InstanceStateFileName = "state.json";

    private static readonly string SettingsDir = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "Edgetree");

    private static readonly string SettingsPath = Path.Combine(SettingsDir, "settings.json");

    // Pre-rebrand location (app was named SidebarExplorer) - kept here only so
    // Load() can pull an existing install's settings forward the first time
    // it runs under the new folder name.
    private static readonly string OldSettingsDir = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "SidebarExplorer");

    private static readonly string OldSettingsPath = Path.Combine(OldSettingsDir, "settings.json");

    // These are values that describe one visible Edgetree window rather than a
    // user's application-wide preferences. The list is explicit on purpose:
    // adding a new AppSettings property must not silently choose a persistence
    // scope just because its name happens to contain a particular word.
    private static readonly HashSet<string> InstancePropertyNames = new(StringComparer.OrdinalIgnoreCase)
    {
        nameof(AppSettings.ExpandedWidth),
        nameof(AppSettings.ViewerOpen),
        nameof(AppSettings.ViewerWidth),
        nameof(AppSettings.ViewerSideSwapped),
        nameof(AppSettings.ViewerNavigator),
        nameof(AppSettings.ViewerFilmstrip),
        nameof(AppSettings.ViewerFilmstripCellHeight),
        nameof(AppSettings.ViewerFilmstripGrid),
        nameof(AppSettings.ViewerFilmstripGridCellSize),
        nameof(AppSettings.ViewerFilmstripGridHeight),
        nameof(AppSettings.HelpWindowWidth),
        nameof(AppSettings.HelpWindowHeight),
        nameof(AppSettings.IsAutoHidden),
        nameof(AppSettings.DockedHeightRatio),
        nameof(AppSettings.DockedTopRatio),
        nameof(AppSettings.DockOnRight),
        nameof(AppSettings.ExpandedFolderPaths),
        nameof(AppSettings.LastSelectedPath),
        nameof(AppSettings.ViewerFullscreen),
        nameof(AppSettings.ViewerRest),
        nameof(AppSettings.IsFloating),
        nameof(AppSettings.FloatingLeft),
        nameof(AppSettings.FloatingTop),
        nameof(AppSettings.FloatingWidth),
        nameof(AppSettings.FloatingHeight),
        nameof(AppSettings.SidePanelMode),
        nameof(AppSettings.LastSearchFolder),
    };

    // Comments, trailing commas and case-insensitive property names remain
    // accepted, as they were before the settings split.
    private static readonly JsonSerializerOptions ReadOptions = new()
    {
        AllowTrailingCommas = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        PropertyNameCaseInsensitive = true,
    };

    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        WriteIndented = true,
    };

    private readonly int? _instanceSlotId;
    private readonly string? _launchMonitorDeviceName;
    private readonly string? _instanceStatePath;
    private string? _currentMonitorDeviceName;
    private string? _persistedMonitorDeviceName;
    private bool _loadedLegacyInstanceState;
    private readonly object _saveGate = new();

    public SettingsService(int? instanceSlotId = null, string? launchMonitorDeviceName = null)
    {
        if (instanceSlotId is < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(instanceSlotId));
        }

        _instanceSlotId = instanceSlotId;
        _launchMonitorDeviceName = string.IsNullOrWhiteSpace(launchMonitorDeviceName)
            ? null
            : launchMonitorDeviceName;
        _currentMonitorDeviceName = _launchMonitorDeviceName;
        _instanceStatePath = instanceSlotId.HasValue
            ? InstanceCoordinator.GetInstanceStateFilePath(instanceSlotId.Value, InstanceStateFileName)
            : null;
    }

    // True when this service has been created for a particular independent
    // window. It is intentionally true even before state.json exists, because
    // a new slot still needs to save its first state to that path.
    public bool HasInstanceState => _instanceSlotId.HasValue;

    public int? InstanceSlotId => _instanceSlotId;

    public string? LaunchMonitorDeviceName => _launchMonitorDeviceName;

    public string? InstanceStatePath => _instanceStatePath;

    public bool InstanceStateExists => _instanceStatePath is not null && File.Exists(_instanceStatePath);

    // A legacy flat settings file is still an instance snapshot for slot 0.
    // Treating it as persisted state prevents a restart from unexpectedly
    // moving an existing user to the launch cursor's monitor.
    public bool HasPersistedInstanceState => InstanceStateExists || _loadedLegacyInstanceState;

    public string? PersistedMonitorDeviceName => _persistedMonitorDeviceName;

    public string? EffectiveMonitorDeviceName => _persistedMonitorDeviceName ?? _currentMonitorDeviceName ?? _launchMonitorDeviceName;

    public void SetCurrentMonitorDeviceName(string? deviceName)
    {
        _currentMonitorDeviceName = string.IsNullOrWhiteSpace(deviceName) ? null : deviceName;
    }

    public string GlobalSettingsPath => SettingsPath;

    public AppSettings Load()
    {
        _persistedMonitorDeviceName = null;
        _currentMonitorDeviceName = _launchMonitorDeviceName;
        _loadedLegacyInstanceState = false;

        try
        {
            EnsureCurrentSettingsFile();

            return HasInstanceState
                ? LoadSplitSettings()
                : LoadLegacySettings();
        }
        catch (IOException)
        {
            return new AppSettings();
        }
        catch (JsonException)
        {
            KeepUnreadableFile(SettingsPath);
            return new AppSettings();
        }
    }

    private AppSettings LoadLegacySettings()
    {
        if (!File.Exists(SettingsPath))
        {
            return AppSettings.ForFirstRun();
        }

        var root = ReadObject(SettingsPath);
        if (TryGetObject(root, GlobalNodeName, out var globalNode))
        {
            var combined = SerializeToObject(new AppSettings());
            Overlay(combined, globalNode!);
            var splitSettings = JsonSerializer.Deserialize<AppSettings>(combined.ToJsonString(), ReadOptions);
            return splitSettings is null ? new AppSettings() : NormalizeAndMerge(splitSettings);
        }

        var settings = JsonSerializer.Deserialize<AppSettings>(root.ToJsonString(), ReadOptions);
        if (settings is null)
        {
            return new AppSettings();
        }

        return NormalizeAndMerge(settings);
    }

    private AppSettings LoadSplitSettings()
    {
        var hasGlobalFile = File.Exists(SettingsPath);
        var baseSettings = hasGlobalFile ? new AppSettings() : AppSettings.ForFirstRun();
        var globalRoot = hasGlobalFile
            ? ReadObject(SettingsPath)
            : new JsonObject();

        var combined = SerializeToObject(baseSettings);
        var isSplit = TryGetObject(globalRoot, GlobalNodeName, out var globalNode);

        if (isSplit)
        {
            Overlay(combined, globalNode!);
        }
        else
        {
            // A legacy flat settings.json is treated as a complete first-slot
            // snapshot. Other slots receive only the global portion so their
            // window geometry and current folder do not leak from slot 0.
            Overlay(combined, globalRoot, includeInstanceProperties: _instanceSlotId == 0);
            _loadedLegacyInstanceState = _instanceSlotId == 0;
        }

        if (_instanceStatePath is not null && File.Exists(_instanceStatePath))
        {
            try
            {
                var instanceRoot = ReadObject(_instanceStatePath);
                if (instanceRoot["LaunchMonitorDeviceName"] is JsonValue monitorValue &&
                    monitorValue.TryGetValue<string>(out var monitorName))
                {
                    _persistedMonitorDeviceName = monitorName;
                    _currentMonitorDeviceName = monitorName;
                }
                var instanceNode = TryGetObject(instanceRoot, "Instance", out var wrappedInstance)
                    ? wrappedInstance!
                    : instanceRoot;
                Overlay(combined, instanceNode, includeOnlyInstanceProperties: true);
            }
            catch (JsonException)
            {
                KeepUnreadableFile(_instanceStatePath);
            }
            catch (IOException)
            {
                // A locked or temporarily unavailable instance file should not
                // hide usable global preferences; the slot falls back to defaults.
            }
        }

        var settings = JsonSerializer.Deserialize<AppSettings>(combined.ToJsonString(), ReadOptions)
            ?? new AppSettings();
        return NormalizeAndMerge(settings);
    }

    private static AppSettings NormalizeAndMerge(AppSettings settings)
    {
        settings.Normalize();
        settings.MergeFavoritesIntoBookmarks();
        return settings;
    }

    private static void EnsureCurrentSettingsFile()
    {
        if (File.Exists(SettingsPath) || !File.Exists(OldSettingsPath))
        {
            return;
        }

        Directory.CreateDirectory(SettingsDir);
        File.Copy(OldSettingsPath, SettingsPath);
    }

    private static JsonObject ReadObject(string path)
    {
        var node = JsonNode.Parse(
            File.ReadAllText(path),
            nodeOptions: null,
            documentOptions: new JsonDocumentOptions
            {
                AllowTrailingCommas = true,
                CommentHandling = JsonCommentHandling.Skip,
            });

        return node as JsonObject
            ?? throw new JsonException($"Settings file '{path}' must contain a JSON object.");
    }

    private static bool TryGetObject(JsonObject source, string name, out JsonObject? value)
    {
        foreach (var property in source)
        {
            if (property.Key.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                value = property.Value as JsonObject
                    ?? throw new JsonException($"Settings node '{name}' must contain a JSON object.");
                return true;
            }
        }

        value = null;
        return false;
    }

    private static JsonObject SerializeToObject(AppSettings settings)
    {
        return JsonSerializer.SerializeToNode(settings, WriteOptions) as JsonObject
            ?? throw new JsonException("AppSettings did not serialize to a JSON object.");
    }

    private static void Overlay(
        JsonObject destination,
        JsonObject source,
        bool includeInstanceProperties = true,
        bool includeOnlyInstanceProperties = false)
    {
        foreach (var property in source)
        {
            var isInstanceProperty = InstancePropertyNames.Contains(property.Key);
            if (includeOnlyInstanceProperties && !isInstanceProperty)
            {
                continue;
            }

            if (!includeInstanceProperties && isInstanceProperty)
            {
                continue;
            }

            var existingName = FindPropertyName(destination, property.Key);
            destination[existingName ?? property.Key] = property.Value?.DeepClone();
        }
    }

    private static string? FindPropertyName(JsonObject source, string name)
    {
        foreach (var property in source)
        {
            if (property.Key.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return property.Key;
            }
        }

        return null;
    }

    // ----- Save failure reporting ----------------------------------------------
    public event Action<Exception>? SaveFailed;

    public event Action? SaveRecovered;

    private bool _lastSaveFailed;

    public bool Save(AppSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        lock (_saveGate)
        {
            try
            {
                settings.Normalize();

                if (!HasInstanceState)
                {
                    SaveWithoutInstanceSlot(settings);
                    return NoteSaved();
                }

                SaveSplit(settings);
                return NoteSaved();
            }
            catch (IOException ex)
            {
                return NoteFailed(ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                return NoteFailed(ex);
            }
            catch (Exception ex) when (
                ex is JsonException or ArgumentException or NotSupportedException)
            {
                return NoteFailed(ex);
            }
        }
    }

    private void SaveWithoutInstanceSlot(AppSettings settings)
    {
        var serialized = SerializeToObject(settings);
        WithGlobalWriteMutex(() =>
        {
            var existing = TryReadExistingSettingsForSave();
            if (TryGetObject(existing, GlobalNodeName, out var wrappedGlobal))
            {
                var globalNode = new JsonObject();
                Overlay(globalNode, wrappedGlobal!, includeInstanceProperties: false);
                Overlay(globalNode, serialized, includeInstanceProperties: false);
                var document = new JsonObject
                {
                    ["SchemaVersion"] = SplitSettingsSchemaVersion,
                    [GlobalNodeName] = globalNode,
                };
                WriteAtomic(SettingsPath, document.ToJsonString(WriteOptions));
                return;
            }

            WriteAtomic(SettingsPath, JsonSerializer.Serialize(settings, WriteOptions));
        });
    }

    private void SaveSplit(AppSettings settings)
    {
        var serialized = SerializeToObject(settings);
        var existingInstance = _instanceStatePath is not null && File.Exists(_instanceStatePath)
            ? ReadObject(_instanceStatePath)
            : new JsonObject();
        var existingInstanceNode = TryGetObject(existingInstance, "Instance", out var wrappedInstance)
            ? wrappedInstance!
            : existingInstance;
        var instanceNode = new JsonObject();
        Overlay(instanceNode, existingInstanceNode, includeOnlyInstanceProperties: true);
        Overlay(instanceNode, serialized, includeOnlyInstanceProperties: true);

        WithGlobalWriteMutex(() =>
        {
            var existing = TryReadExistingSettingsForSave();
            var existingGlobal = TryGetObject(existing, GlobalNodeName, out var wrappedGlobal)
                ? wrappedGlobal!
                : existing;

            var globalNode = new JsonObject();
            Overlay(globalNode, existingGlobal, includeInstanceProperties: false);
            Overlay(globalNode, serialized, includeInstanceProperties: false);

            var globalDocument = new JsonObject
            {
                ["SchemaVersion"] = SplitSettingsSchemaVersion,
                [GlobalNodeName] = globalNode,
            };

            WriteAtomic(SettingsPath, globalDocument.ToJsonString(WriteOptions));
        });

        var instanceDocument = new JsonObject
        {
            ["SchemaVersion"] = SplitSettingsSchemaVersion,
            ["InstanceSlotId"] = _instanceSlotId!.Value,
            ["LaunchMonitorDeviceName"] = _currentMonitorDeviceName ?? _launchMonitorDeviceName,
            ["Instance"] = instanceNode,
        };

        WriteAtomic(_instanceStatePath!, instanceDocument.ToJsonString(WriteOptions));
    }

    private static JsonObject TryReadExistingSettingsForSave()
    {
        if (File.Exists(SettingsPath))
        {
            return ReadObject(SettingsPath);
        }

        if (File.Exists(OldSettingsPath))
        {
            return ReadObject(OldSettingsPath);
        }

        return new JsonObject();
    }

    private static void WithGlobalWriteMutex(Action action)
    {
        using var mutex = new Mutex(initiallyOwned: false, GlobalWriteMutexName);
        var ownsMutex = false;

        try
        {
            try
            {
                ownsMutex = mutex.WaitOne(TimeSpan.FromSeconds(10));
            }
            catch (AbandonedMutexException)
            {
                ownsMutex = true;
            }

            if (!ownsMutex)
            {
                throw new IOException("Timed out waiting for the Edgetree settings write lock.");
            }

            action();
        }
        finally
        {
            if (ownsMutex)
            {
                mutex.ReleaseMutex();
            }
        }
    }

    private static void WriteAtomic(string path, string contents)
    {
        var directory = Path.GetDirectoryName(path);
        if (string.IsNullOrEmpty(directory))
        {
            throw new IOException($"Settings path '{path}' has no parent directory.");
        }

        Directory.CreateDirectory(directory);
        var tempPath = $"{path}.{Environment.ProcessId}.{Guid.NewGuid():N}.tmp";

        try
        {
            File.WriteAllText(tempPath, contents, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            if (File.Exists(path))
            {
                File.Replace(tempPath, path, path + ".bak", ignoreMetadataErrors: true);
            }
            else
            {
                File.Move(tempPath, path);
            }
        }
        finally
        {
            try
            {
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }
            catch (IOException)
            {
                // The successful replacement is more important than cleanup of
                // an orphaned temporary file; the next save uses a new name.
            }
        }
    }

    private bool NoteSaved()
    {
        if (_lastSaveFailed)
        {
            _lastSaveFailed = false;
            SaveRecovered?.Invoke();
        }

        return true;
    }

    private bool NoteFailed(Exception error)
    {
        _lastSaveFailed = true;
        SaveFailed?.Invoke(error);
        return false;
    }

    private static void KeepUnreadableFile(string path)
    {
        try
        {
            if (!File.Exists(path))
            {
                return;
            }

            var directory = Path.GetDirectoryName(path) ?? SettingsDir;
            var fileName = Path.GetFileNameWithoutExtension(path);
            var extension = Path.GetExtension(path);
            var backup = Path.Combine(
                directory,
                $"{fileName}.unreadable-{DateTime.Now:yyyyMMdd-HHmmssfff}{extension}");
            File.Copy(path, backup);
        }
        catch (IOException)
        {
            // Recovery must never turn a malformed settings file into a startup
            // failure of its own.
        }
        catch (UnauthorizedAccessException)
        {
        }
    }

    // Existing callers use this static path in the save-failure message.
    public static string PathForMessages => SettingsPath;
}
