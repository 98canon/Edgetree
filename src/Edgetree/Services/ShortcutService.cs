using System.Text;
using System.Windows.Input;
using SidebarExplorer.App.Models;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using Keyboard = System.Windows.Input.Keyboard;

namespace SidebarExplorer.App.Services;

internal sealed record ShortcutSpec(string Id, Key DefaultKey, ModifierKeys DefaultModifiers);

internal static class ShortcutService
{
    public const string Search = "Search";
    public const string Help = "Help";
    public const string ClosePanel = "ClosePanel";
    public const string CollapseAll = "CollapseAll";
    public const string NextItem = "NextItem";
    public const string HistoryBack = "HistoryBack";
    public const string HistoryForward = "HistoryForward";
    public const string Slideshow = "Slideshow";
    public const string Clock = "Clock";
    public const string SavePreset = "SavePreset";

    public static readonly ShortcutSpec[] Catalog =
    {
        new(Search, Key.F, ModifierKeys.Control),
        new(Help, Key.F1, ModifierKeys.None),
        new(SavePreset, Key.S, ModifierKeys.Control | ModifierKeys.Shift),
        new(ClosePanel, Key.None, ModifierKeys.None),
        new(CollapseAll, Key.None, ModifierKeys.None),
        new(NextItem, Key.None, ModifierKeys.None),
        new(HistoryBack, Key.None, ModifierKeys.None),
        new(HistoryForward, Key.None, ModifierKeys.None),
        new(Slideshow, Key.None, ModifierKeys.None),
        new(Clock, Key.None, ModifierKeys.None),
    };

    public static string DisplayName(string id) => id switch
    {
        Search => Strings.ShortcutSearch,
        Help => Strings.ShortcutHelp,
        ClosePanel => Strings.ShortcutClosePanel,
        CollapseAll => Strings.ShortcutCollapseAll,
        NextItem => Strings.ShortcutNextItem,
        HistoryBack => Strings.ShortcutHistoryBack,
        HistoryForward => Strings.ShortcutHistoryForward,
        Slideshow => Strings.ShortcutSlideshow,
        Clock => Strings.ShortcutClock,
        SavePreset => Strings.ShortcutSavePreset,
        _ => id,
    };

    public static bool Matches(AppSettings settings, string id, KeyEventArgs e)
    {
        if (!TryResolve(settings, id, out var key, out var modifiers) || key == Key.None)
        {
            return false;
        }

        Key eventKey = e.Key == Key.System ? e.SystemKey : e.Key;
        return eventKey == key && Keyboard.Modifiers == modifiers;
    }

    public static string Format(AppSettings settings, string id)
    {
        if (!TryResolve(settings, id, out var key, out var modifiers) || key == Key.None)
        {
            return Strings.ShortcutUnbound;
        }

        return Format(key, modifiers);
    }

    public static string Format(Key key, ModifierKeys modifiers)
    {
        if (key == Key.None)
        {
            return Strings.ShortcutUnbound;
        }

        var text = new StringBuilder();
        if (modifiers.HasFlag(ModifierKeys.Control)) text.Append("Ctrl+");
        if (modifiers.HasFlag(ModifierKeys.Shift)) text.Append("Shift+");
        if (modifiers.HasFlag(ModifierKeys.Alt)) text.Append("Alt+");
        if (modifiers.HasFlag(ModifierKeys.Windows)) text.Append("Win+");
        text.Append(key switch
        {
            Key.Left => "←",
            Key.Right => "→",
            Key.Up => "↑",
            Key.Down => "↓",
            Key.OemPlus or Key.Add => "+",
            Key.OemMinus or Key.Subtract => "−",
            Key.Back => "Backspace",
            Key.Space => "Space",
            _ => key.ToString(),
        });
        return text.ToString();
    }

    public static void Set(AppSettings settings, string id, Key key, ModifierKeys modifiers)
    {
        settings.Shortcuts[id] = key == Key.None ? "" : FormatForStorage(key, modifiers);
    }

    private static string FormatForStorage(Key key, ModifierKeys modifiers)
    {
        var text = new StringBuilder();
        if (modifiers.HasFlag(ModifierKeys.Control)) text.Append("Ctrl+");
        if (modifiers.HasFlag(ModifierKeys.Shift)) text.Append("Shift+");
        if (modifiers.HasFlag(ModifierKeys.Alt)) text.Append("Alt+");
        if (modifiers.HasFlag(ModifierKeys.Windows)) text.Append("Win+");
        text.Append(key);
        return text.ToString();
    }

    public static void Clear(AppSettings settings, string id)
        => settings.Shortcuts[id] = "";

    public static void Reset(AppSettings settings, string id)
        => settings.Shortcuts.Remove(id);

    private static bool TryResolve(AppSettings settings, string id, out Key key, out ModifierKeys modifiers)
    {
        key = Key.None;
        modifiers = ModifierKeys.None;
        var spec = Catalog.FirstOrDefault(item => item.Id == id);
        if (spec is null)
        {
            return false;
        }

        if (settings.Shortcuts.TryGetValue(id, out var stored))
        {
            if (string.IsNullOrWhiteSpace(stored))
            {
                return true;
            }

            return TryParse(stored, out key, out modifiers);
        }

        key = spec.DefaultKey;
        modifiers = spec.DefaultModifiers;
        return true;
    }

    private static bool TryParse(string text, out Key key, out ModifierKeys modifiers)
    {
        key = Key.None;
        modifiers = ModifierKeys.None;
        text = text.Trim();
        if (text == "+")
        {
            key = Key.OemPlus;
            return true;
        }

        // Older builds stored the display form Ctrl++ rather than a stable key name.
        // Accept it while writing all new values as Ctrl+OemPlus.
        if (text.EndsWith("++", StringComparison.Ordinal))
        {
            text = text[..^2] + "+OemPlus";
        }

        var parts = text.Split('+', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (parts.Length == 0)
        {
            return false;
        }

        for (int i = 0; i < parts.Length - 1; i++)
        {
            switch (parts[i].ToLowerInvariant())
            {
                case "ctrl":
                case "control":
                    modifiers |= ModifierKeys.Control;
                    break;
                case "shift":
                    modifiers |= ModifierKeys.Shift;
                    break;
                case "alt":
                    modifiers |= ModifierKeys.Alt;
                    break;
                case "win":
                case "windows":
                    modifiers |= ModifierKeys.Windows;
                    break;
                default:
                    key = Key.None;
                    modifiers = ModifierKeys.None;
                    return false;
            }
        }

        string last = parts[^1];
        key = last switch
        {
            "←" => Key.Left,
            "→" => Key.Right,
            "↑" => Key.Up,
            "↓" => Key.Down,
            "+" => Key.OemPlus,
            "−" or "-" => Key.OemMinus,
            "Backspace" => Key.Back,
            "Space" => Key.Space,
            _ => Enum.TryParse(last, true, out Key parsed) ? parsed : Key.None,
        };
        return key != Key.None;
    }
}
