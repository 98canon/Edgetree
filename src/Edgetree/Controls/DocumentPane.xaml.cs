using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit.Highlighting;
using SidebarExplorer.App.Services;
using Brush = System.Windows.Media.Brush;

namespace SidebarExplorer.App.Controls;

public partial class DocumentPane : System.Windows.Controls.UserControl
{
    public const long MaxBytes = 2 * 1024 * 1024;

    private readonly DispatcherTimer _saveTimer;
    private FileSystemWatcher? _watcher;
    private bool _loading;
    private bool _previewMode;
    private bool _isMarkdown;
    private string? _lastSavedText;
    private string? _pendingSaveText;

    public DocumentPane()
    {
        InitializeComponent();
        _saveTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1000) };
        _saveTimer.Tick += (_, _) => FlushSave();
        Loaded += (_, _) => ApplyEditorColors();
        PreviewMouseDown += DocumentPane_PreviewMouseDown;
    }

    public string? CurrentPath { get; private set; }

    public bool ShowPopOut
    {
        get => PopOutButton.Visibility == Visibility.Visible;
        set => PopOutButton.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
    }

    public event EventHandler? PopOutRequested;

    public void Load(string path)
    {
        bool samePath = string.Equals(CurrentPath, path, StringComparison.OrdinalIgnoreCase);
        if (samePath && MessageText.Visibility != Visibility.Visible)
        {
            return;
        }

        FlushSave();
        UnhookWatcher();
        CurrentPath = path;
        TitleText.Text = Path.GetFileName(path);

        if (!File.Exists(path))
        {
            ShowMessage(Strings.DocumentLoadFailed);
            return;
        }

        var info = new FileInfo(path);
        if (info.Length > MaxBytes)
        {
            ShowMessage(Strings.DocumentTooLarge);
            return;
        }

        string text;
        try
        {
            text = File.ReadAllText(path);
        }
        catch
        {
            ShowMessage(Strings.DocumentLoadFailed);
            return;
        }

        _lastSavedText = text;
        _pendingSaveText = null;
        _isMarkdown = FileTypeFilter.IsMarkdown(path);
        _previewMode = _isMarkdown;
        ModeRow.Visibility = _isMarkdown ? Visibility.Visible : Visibility.Collapsed;

        _loading = true;
        try
        {
            Editor.Text = text;
            Editor.SyntaxHighlighting = HighlightingFor(path);
            Preview.AssetPathRoot = Path.GetDirectoryName(path) ?? string.Empty;
            if (_isMarkdown)
            {
                Preview.Markdown = text;
            }
        }
        finally
        {
            _loading = false;
        }

        ApplyMode();
        HookWatcher(path);
    }

    public void Unload()
    {
        FlushSave();
        UnhookWatcher();
        CurrentPath = null;
        _lastSavedText = null;
        _pendingSaveText = null;
        TitleText.Text = string.Empty;
        _loading = true;
        try
        {
            Editor.Text = string.Empty;
            Preview.Markdown = string.Empty;
        }
        finally
        {
            _loading = false;
        }

        Preview.Visibility = Visibility.Collapsed;
        Editor.Visibility = Visibility.Collapsed;
        MessageText.Visibility = Visibility.Collapsed;
    }

    public void FlushSave()
    {
        _saveTimer.Stop();
        if (CurrentPath is null || _pendingSaveText is null)
        {
            return;
        }

        if (string.Equals(_pendingSaveText, _lastSavedText, StringComparison.Ordinal))
        {
            _pendingSaveText = null;
            return;
        }

        try
        {
            File.WriteAllText(CurrentPath, _pendingSaveText, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            _lastSavedText = _pendingSaveText;
            _pendingSaveText = null;
        }
        catch
        {
            MessageText.Text = Strings.DocumentSaveFailed;
            MessageText.Visibility = Visibility.Visible;
        }
    }

    private void ShowMessage(string text)
    {
        Preview.Visibility = Visibility.Collapsed;
        Editor.Visibility = Visibility.Collapsed;
        MessageText.Text = text;
        MessageText.Visibility = Visibility.Visible;
        ModeRow.Visibility = Visibility.Collapsed;
    }

    private void ApplyMode()
    {
        MessageText.Visibility = Visibility.Collapsed;
        PreviewToggle.IsChecked = _previewMode;
        EditToggle.IsChecked = !_previewMode;
        if (_previewMode && _isMarkdown)
        {
            Preview.Markdown = Editor.Text;
            Preview.Visibility = Visibility.Visible;
            Editor.Visibility = Visibility.Collapsed;
            Dispatcher.BeginInvoke(FitPreviewWidth, DispatcherPriority.Loaded);
        }
        else
        {
            Preview.Visibility = Visibility.Collapsed;
            Editor.Visibility = Visibility.Visible;
        }
    }

    private void Preview_SizeChanged(object sender, SizeChangedEventArgs e)
        => FitPreviewWidth();

    private void FitPreviewWidth()
    {
        double width = Preview.ActualWidth;
        if (width <= 1)
        {
            return;
        }

        if (Preview.Document is FlowDocument document)
        {
            document.PageWidth = width;
            document.ColumnWidth = width;
            return;
        }

        if (FindFlowDocument(Preview) is { } found)
        {
            found.PageWidth = width;
            found.ColumnWidth = width;
        }
    }

    private static FlowDocument? FindFlowDocument(DependencyObject root)
    {
        if (root is FlowDocumentScrollViewer { Document: { } document })
        {
            return document;
        }

        int count = VisualTreeHelper.GetChildrenCount(root);
        for (int i = 0; i < count; i++)
        {
            if (FindFlowDocument(VisualTreeHelper.GetChild(root, i)) is { } nested)
            {
                return nested;
            }
        }

        return null;
    }

    private void PreviewToggle_Click(object sender, RoutedEventArgs e)
    {
        _previewMode = true;
        ApplyMode();
    }

    private void EditToggle_Click(object sender, RoutedEventArgs e)
    {
        _previewMode = false;
        ApplyMode();
    }

    private void PopOutButton_Click(object sender, RoutedEventArgs e)
        => PopOutRequested?.Invoke(this, EventArgs.Empty);

    private void DocumentPane_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Middle && CurrentPath is not null)
        {
            PopOutRequested?.Invoke(this, EventArgs.Empty);
            e.Handled = true;
        }
    }

    private void Editor_TextChanged(object? sender, EventArgs e)
    {
        if (_loading || CurrentPath is null)
        {
            return;
        }

        _pendingSaveText = Editor.Text;
        _saveTimer.Stop();
        _saveTimer.Start();
    }

    private void HookWatcher(string path)
    {
        string? dir = Path.GetDirectoryName(path);
        if (string.IsNullOrEmpty(dir))
        {
            return;
        }

        try
        {
            _watcher = new FileSystemWatcher(dir, Path.GetFileName(path))
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName,
            };
            _watcher.Changed += OnDiskChanged;
            _watcher.Renamed += OnDiskChanged;
            _watcher.EnableRaisingEvents = true;
        }
        catch
        {
        }
    }

    private void UnhookWatcher()
    {
        if (_watcher is null)
        {
            return;
        }

        _watcher.EnableRaisingEvents = false;
        _watcher.Changed -= OnDiskChanged;
        _watcher.Renamed -= OnDiskChanged;
        _watcher.Dispose();
        _watcher = null;
    }

    private void OnDiskChanged(object sender, FileSystemEventArgs e)
    {
        Dispatcher.BeginInvoke(() =>
        {
            if (CurrentPath is null || _pendingSaveText is not null)
            {
                return;
            }

            try
            {
                string text = File.ReadAllText(CurrentPath);
                if (string.Equals(text, _lastSavedText, StringComparison.Ordinal)
                    || string.Equals(text, Editor.Text, StringComparison.Ordinal))
                {
                    return;
                }

                _loading = true;
                Editor.Text = text;
                _lastSavedText = text;
                if (_isMarkdown && _previewMode)
                {
                    Preview.Markdown = text;
                }
            }
            catch
            {
            }
            finally
            {
                _loading = false;
            }
        });
    }

    public void ApplyEditorColors()
    {
        if (TryFindResource("ViewerBackground") is Brush background)
        {
            Editor.Background = background;
            Preview.Background = background;
        }

        if (TryFindResource("ForegroundText") is Brush foreground)
        {
            Editor.Foreground = foreground;
            Editor.LineNumbersForeground = foreground;
            Preview.Foreground = foreground;
        }
    }

    private static IHighlightingDefinition? HighlightingFor(string path)
    {
        string? name = Path.GetExtension(path).TrimStart('.').ToLowerInvariant() switch
        {
            "cs" or "csx" => "C#",
            "xml" or "xaml" or "axaml" or "csproj" or "vbproj" or "fsproj" or "sln"
                or "vcxproj" or "props" or "targets" or "nuspec" or "resx"
                or "config" or "manifest" or "cshtml" or "vbhtml" => "XML",
            "html" or "htm" or "xhtml" or "shtml" or "aspx" or "ascx" => "HTML",
            "js" or "jsx" or "mjs" or "cjs" or "ts" or "tsx" or "json" or "jsonc" or "json5" => "JavaScript",
            "java" => "Java",
            "c" or "h" or "cpp" or "cc" or "cxx" or "hpp" or "hh" or "hxx" => "C++",
            "php" or "phtml" => "PHP",
            "py" or "pyw" or "pyi" => "Python",
            "rb" or "rbw" => "Ruby",
            "ps1" or "psm1" or "psd1" => "PowerShell",
            "sql" or "psql" => "TSQL",
            "vb" => "VB",
            "md" or "markdown" or "mdown" or "mkd" => "MarkDown",
            "patch" or "diff" => "Patch",
            "tex" or "latex" => "TeX",
            _ => null,
        };
        if (name is null)
        {
            return null;
        }

        return HighlightingManager.Instance.GetDefinition(name)
            ?? HighlightingManager.Instance.HighlightingDefinitions
                .FirstOrDefault(d => string.Equals(d.Name, name, StringComparison.OrdinalIgnoreCase));
    }
}
