using System.Drawing;
using SidebarExplorer.App.Native;

namespace SidebarExplorer.App.Services;

// Registers this window as a Windows appbar so maximized windows use the
// leftover work area instead of covering it. Unregistering restores the
// work area. The window handle must outlive the registration: ABM_REMOVE
// before the hwnd goes away, or explorer can keep the inset until restart.
internal sealed class AppBarService : IDisposable
{
    private IntPtr _hwnd;
    private bool _registered;
    private bool _disposed;

    public AppBarService()
    {
        CallbackMessage = NativeMethods.RegisterWindowMessage("Edgetree.AppBar");
    }

    public uint CallbackMessage { get; }

    public bool IsRegistered => _registered;

    // Last strip handed back by SETPOS, in physical pixels. Used to put this
    // window's own reservation back into WorkingArea so layout does not treat
    // the leftover region as the dock target and shrink itself.
    public Rectangle LastReservedPx { get; private set; }

    public bool Register(IntPtr hwnd)
    {
        if (_registered && _hwnd == hwnd)
        {
            return true;
        }

        if (_registered)
        {
            Unregister();
        }

        if (hwnd == IntPtr.Zero || !NativeMethods.AppBarNew(hwnd, CallbackMessage))
        {
            return false;
        }

        _hwnd = hwnd;
        _registered = true;
        return true;
    }

    public void Unregister()
    {
        if (!_registered)
        {
            return;
        }

        NativeMethods.AppBarRemove(_hwnd);
        _registered = false;
        LastReservedPx = Rectangle.Empty;
        _hwnd = IntPtr.Zero;
    }

    public Rectangle SetPos(uint edge, int left, int top, int right, int bottom)
    {
        if (!_registered)
        {
            return Rectangle.Empty;
        }

        var result = NativeMethods.AppBarSetPos(_hwnd, edge, left, top, right, bottom);
        int width = Math.Max(0, result.Right - result.Left);
        int height = Math.Max(0, result.Bottom - result.Top);
        LastReservedPx = new Rectangle(result.Left, result.Top, width, height);
        return LastReservedPx;
    }

    public void NotifyActivated()
    {
        if (_registered)
        {
            NativeMethods.AppBarActivate(_hwnd);
        }
    }

    public void NotifyWindowPosChanged()
    {
        if (_registered)
        {
            NativeMethods.AppBarWindowPosChanged(_hwnd);
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        Unregister();
    }
}
