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
    private bool _fittingPreview;
    private DispatcherOperation? _previewFitOperation;
    private double _lastPreviewWidth;
    private bool _previewMode;
    private bool _isMarkdown;
    private string? _lastSavedText;
    private string? _pendingSaveText;
    private bool _diskReloadPending;
    private int _previewGeneration;
    private FlowDocument? _preparedPreviewDocument;
    private string? _lastRenderedMarkdown;

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
        _previewGeneration++;
        _preparedPreviewDocument = null;
        _lastRenderedMarkdown = null;
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

        Exception? renderError = null;
        _loading = true;
        try
        {
            Editor.Text = text;
            Editor.SyntaxHighlighting = HighlightingFor(path);
            Preview.AssetPathRoot = Path.GetDirectoryName(path) ?? string.Empty;
            if (_isMarkdown)
            {
                Preview.Markdown = text;
                _lastRenderedMarkdown = text;
            }
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or FormatException)
        {
            renderError = ex;
        }
        finally
        {
            _loading = false;
        }

        if (renderError is not null)
        {
            ShowMessage(Strings.DocumentLoadFailed);
            return;
        }

        ApplyMode();
        HookWatcher(path);
    }

    public void Unload()
    {
        FlushSave();
        UnhookWatcher();
        _previewGeneration++;
        _preparedPreviewDocument = null;
        _lastRenderedMarkdown = null;
        CurrentPath = null;
        _lastSavedText = null;
        _pendingSaveText = null;
        TitleText.Text = string.Empty;
        _loading = true;
        try
        {
            Editor.Text = string.Empty;
            try
            {
                Preview.Markdown = string.Empty;
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
            }
        }
        finally
        {
            _loading = false;
        }

        PreviewHost.Visibility = Visibility.Collapsed;
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
        PreviewHost.Visibility = Visibility.Collapsed;
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
            try
            {
                string text = Editor.Text;
                if (!string.Equals(_lastRenderedMarkdown, text, StringComparison.Ordinal))
                {
                    Preview.Markdown = text;
                    _lastRenderedMarkdown = text;
                    _preparedPreviewDocument = null;
                    _lastPreviewWidth = 0;
                }

                PreviewHost.Visibility = Visibility.Visible;
                Editor.Visibility = Visibility.Collapsed;
                ScheduleFitPreviewWidth();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or FormatException)
            {
                ShowMessage(Strings.DocumentLoadFailed);
            }
        }
        else
        {
            PreviewHost.Visibility = Visibility.Collapsed;
            Editor.Visibility = Visibility.Visible;
        }
    }

    private void PreviewHost_SizeChanged(object sender, SizeChangedEventArgs e)
        => ScheduleFitPreviewWidth();

    private void ScheduleFitPreviewWidth()
    {
        if (_previewFitOperation is { Status: DispatcherOperationStatus.Pending })
        {
            return;
        }

        int generation = _previewGeneration;
        _previewFitOperation = Dispatcher.BeginInvoke(
            DispatcherPriority.Render,
            new Action(() => FitPreviewWidth(generation)));
    }

    private void FitPreviewWidth(int generation)
    {
        _previewFitOperation = null;
        if (generation != _previewGeneration
            || _fittingPreview
            || PreviewHost.Visibility != Visibility.Visible)
        {
            return;
        }

        var document = Preview.Document ?? FindFlowDocument(Preview);
        if (document is null)
        {
            return;
        }

        double width = MeasurePreviewContentWidth();
        if (width < 32)
        {
            return;
        }

        bool newDocument = !ReferenceEquals(_preparedPreviewDocument, document);
        if (!newDocument && Math.Abs(width - _lastPreviewWidth) < 0.5)
        {
            return;
        }

        _fittingPreview = true;
        try
        {
            // Only prepare a newly parsed document once. In particular, do not
            // replace Paragraph/Block objects during SizeChanged: doing so makes
            // WPF re-enter layout and was the source of crashes and flashing.
            if (newDocument)
            {
                PreparePreviewWrapping(document);
                _preparedPreviewDocument = document;
            }

            _lastPreviewWidth = width;
            Preview.MaxWidth = width;
            document.PagePadding = new Thickness(0);
            document.ColumnGap = 0;
            document.PageWidth = width;
            document.ColumnWidth = width;
            ApplyPreviewElementWidth(document.Blocks, width);
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or FormatException)
        {
            _lastPreviewWidth = 0;
        }
        finally
        {
            _fittingPreview = false;
        }
    }

    private static void PreparePreviewWrapping(FlowDocument document)
    {
        // ZWSP is invisible in the rendered document but gives WPF legal break
        // points inside long URLs/JSON keys and other unbroken code tokens.
        InsertWrapOpportunities(document.Blocks);
    }

    private static void ApplyPreviewElementWidth(BlockCollection blocks, double width)
    {
        foreach (Block block in blocks)
        {
            switch (block)
            {
                case Section section:
                    ApplyPreviewElementWidth(section.Blocks, width);
                    break;
                case System.Windows.Documents.List list:
                    foreach (ListItem item in list.ListItems)
                    {
                        ApplyPreviewElementWidth(item.Blocks, Math.Max(32, width - 28));
                    }
                    break;
                case Table table:
                    foreach (TableRowGroup group in table.RowGroups)
                    {
                        foreach (TableRow row in group.Rows)
                        {
                            foreach (TableCell cell in row.Cells)
                            {
                                ApplyPreviewElementWidth(cell.Blocks, Math.Max(32, width - 8));
                            }
                        }
                    }
                    break;
                case BlockUIContainer { Child: FrameworkElement element }:
                    element.MaxWidth = width;
                    element.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    if (element is TextBlock text)
                    {
                        text.TextWrapping = TextWrapping.Wrap;
                        text.Width = width;
                        text.MaxWidth = width;
                    }
                    break;
            }
        }
    }

    private double MeasurePreviewContentWidth()
    {
        double total = PreviewHost.ActualWidth > 1 ? PreviewHost.ActualWidth : Preview.ActualWidth;
        return Math.Max(32, total - SystemParameters.VerticalScrollBarWidth - 4);
    }

    private static void FlattenLists(BlockCollection blocks)
    {
        foreach (Section section in blocks.OfType<Section>().ToList())
        {
            FlattenLists(section.Blocks);
        }

        foreach (Table table in blocks.OfType<Table>().ToList())
        {
            foreach (TableRowGroup group in table.RowGroups)
            {
                foreach (TableRow row in group.Rows)
                {
                    foreach (TableCell cell in row.Cells)
                    {
                        FlattenLists(cell.Blocks);
                    }
                }
            }
        }

        foreach (System.Windows.Documents.List list in blocks.OfType<System.Windows.Documents.List>().ToList())
        {
            int index = 1;
            var replacement = new List<Block>();
            foreach (ListItem item in list.ListItems)
            {
                FlattenLists(item.Blocks);
                bool first = true;
                foreach (Block inner in item.Blocks.ToList())
                {
                    item.Blocks.Remove(inner);
                    if (first && inner is Paragraph paragraph)
                    {
                        string prefix = ListPrefix(list.MarkerStyle, index);
                        if (prefix.Length > 0)
                        {
                            Inline? firstInline = paragraph.Inlines.FirstInline;
                            if (firstInline is not null)
                            {
                                paragraph.Inlines.InsertBefore(firstInline, new Run(prefix));
                            }
                            else
                            {
                                paragraph.Inlines.Add(new Run(prefix));
                            }
                        }

                        paragraph.Margin = new Thickness(
                            Math.Max(paragraph.Margin.Left, 18),
                            paragraph.Margin.Top,
                            paragraph.Margin.Right,
                            paragraph.Margin.Bottom);
                        first = false;
                        index++;
                    }

                    replacement.Add(inner);
                }
            }

            Block? next = list.NextBlock;
            blocks.Remove(list);
            foreach (Block block in replacement)
            {
                if (next is not null)
                {
                    blocks.InsertBefore(next, block);
                }
                else
                {
                    blocks.Add(block);
                }
            }
        }
    }

    private static string ListPrefix(TextMarkerStyle style, int index)
        => style switch
        {
            TextMarkerStyle.Decimal => $"{index}. ",
            TextMarkerStyle.LowerLatin => $"{(char)('a' + Math.Clamp(index, 1, 26) - 1)}. ",
            TextMarkerStyle.UpperLatin => $"{(char)('A' + Math.Clamp(index, 1, 26) - 1)}. ",
            TextMarkerStyle.None => string.Empty,
            _ => "• ",
        };

    private static void InsertWrapOpportunities(BlockCollection blocks)
    {
        foreach (Block block in blocks)
        {
            switch (block)
            {
                case Paragraph paragraph:
                    InsertWrapOpportunities(paragraph.Inlines);
                    break;
                case Section section:
                    InsertWrapOpportunities(section.Blocks);
                    break;
                case System.Windows.Documents.List list:
                    foreach (ListItem item in list.ListItems)
                    {
                        InsertWrapOpportunities(item.Blocks);
                    }

                    break;
                case Table table:
                    foreach (TableRowGroup group in table.RowGroups)
                    {
                        foreach (TableRow row in group.Rows)
                        {
                            foreach (TableCell cell in row.Cells)
                            {
                                InsertWrapOpportunities(cell.Blocks);
                            }
                        }
                    }

                    break;
                case BlockUIContainer { Child: TextBlock text }:
                    text.Text = InsertZwsp(text.Text);
                    text.TextWrapping = TextWrapping.Wrap;
                    break;
            }
        }
    }

    private static void InsertWrapOpportunities(InlineCollection inlines)
    {
        foreach (Inline inline in inlines)
        {
            switch (inline)
            {
                case Run run:
                    run.Text = InsertZwsp(run.Text);
                    break;
                case Span span:
                    InsertWrapOpportunities(span.Inlines);
                    break;
            }
        }
    }

    private static string InsertZwsp(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        var builder = new StringBuilder(text.Length + 8);
        for (int i = 0; i < text.Length; i++)
        {
            char current = text[i];
            builder.Append(current);
            if (i >= text.Length - 1)
            {
                break;
            }

            char next = text[i + 1];
            if (current != '\u200B' && next != '\u200B' && ShouldInsertBreak(current, next))
            {
                builder.Append('\u200B');
            }
        }

        return builder.ToString();
    }

    private static bool ShouldInsertBreak(char current, char next)
    {
        if (IsWrapAfter(current))
        {
            return true;
        }

        return IsCjk(current) && IsLatinOrDigit(next)
            || IsLatinOrDigit(current) && IsCjk(next);
    }

    private static bool IsWrapAfter(char c)
        => c is '｜' or '|' or '/' or '\\' or '—' or '–' or '_' or '·' or '•' or '-' or '：' or ':';

    private static bool IsCjk(char c)
        => c is >= '\u2E80' and <= '\u9FFF'
            or >= '\uF900' and <= '\uFAFF'
            or >= '\uFE30' and <= '\uFE4F'
            or >= '\uFF00' and <= '\uFFEF';

    private static bool IsLatinOrDigit(char c)
        => char.IsLetterOrDigit(c) && !IsCjk(c);

    private static void WrapBlocks(BlockCollection blocks, double width)
    {
        foreach (Block block in blocks.ToList())
        {
            switch (block)
            {
                case Paragraph paragraph:
                    WrapParagraph(paragraph, width);
                    break;
                case Section section:
                    WrapBlocks(section.Blocks, width);
                    break;
                case System.Windows.Documents.List list:
                    foreach (ListItem item in list.ListItems)
                    {
                        WrapBlocks(item.Blocks, Math.Max(32, width - 28));
                    }

                    break;
                case Table table:
                    table.CellSpacing = Math.Min(table.CellSpacing, 4);
                    FitTableWidth(table, width);
                    int columns = Math.Max(1, table.Columns.Count);
                    double cellWidth = Math.Max(32, width / columns - 8);
                    foreach (TableRowGroup group in table.RowGroups)
                    {
                        foreach (TableRow row in group.Rows)
                        {
                            foreach (TableCell cell in row.Cells)
                            {
                                WrapBlocks(cell.Blocks, cellWidth);
                            }
                        }
                    }

                    break;
                case BlockUIContainer { Child: FrameworkElement element }:
                    element.MaxWidth = width;
                    if (element is TextBlock text)
                    {
                        text.TextWrapping = TextWrapping.Wrap;
                        text.Width = width;
                        text.MaxWidth = width;
                    }

                    break;
            }
        }
    }

    private static void WrapParagraph(Paragraph paragraph, double width)
    {
        InsertWrapOpportunities(paragraph.Inlines);

        var text = new TextBlock
        {
            TextWrapping = TextWrapping.Wrap,
            TextAlignment = paragraph.TextAlignment,
            Width = width,
            MaxWidth = width,
            FontFamily = paragraph.FontFamily,
            FontWeight = paragraph.FontWeight,
            FontStyle = paragraph.FontStyle,
            Foreground = paragraph.Foreground,
            Background = paragraph.Background,
        };
        if (!double.IsNaN(paragraph.FontSize) && paragraph.FontSize > 0)
        {
            text.FontSize = paragraph.FontSize;
        }

        foreach (Inline inline in paragraph.Inlines.ToList())
        {
            paragraph.Inlines.Remove(inline);
            text.Inlines.Add(inline);
        }

        BlockCollection? parentBlocks = paragraph.Parent switch
        {
            FlowDocument document => document.Blocks,
            Section section => section.Blocks,
            ListItem item => item.Blocks,
            TableCell cell => cell.Blocks,
            Figure figure => figure.Blocks,
            Floater floater => floater.Blocks,
            _ => null,
        };
        if (parentBlocks is null)
        {
            return;
        }

        var host = new BlockUIContainer(text)
        {
            Margin = paragraph.Margin,
            Padding = paragraph.Padding,
            BorderBrush = paragraph.BorderBrush,
            BorderThickness = paragraph.BorderThickness,
            Background = paragraph.Background,
            Tag = paragraph.Tag,
        };

        Block? next = paragraph.NextBlock;
        parentBlocks.Remove(paragraph);
        if (next is not null)
        {
            parentBlocks.InsertBefore(next, host);
        }
        else
        {
            parentBlocks.Add(host);
        }
    }

    private static void FitTableWidth(Table table, double width)
    {
        int columns = table.Columns.Count;
        if (columns == 0)
        {
            columns = table.RowGroups
                .SelectMany(group => group.Rows)
                .Select(row => row.Cells.Count)
                .DefaultIfEmpty(0)
                .Max();
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add(new TableColumn());
            }
        }

        if (columns <= 0)
        {
            return;
        }

        double columnWidth = Math.Max(24, (width - table.CellSpacing * (columns + 1)) / columns);
        foreach (TableColumn column in table.Columns)
        {
            column.Width = new GridLength(columnWidth);
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
        if (_diskReloadPending)
        {
            return;
        }

        _diskReloadPending = true;
        Dispatcher.BeginInvoke(() =>
        {
            try
            {
                if (CurrentPath is null || _pendingSaveText is not null)
                {
                    return;
                }

                string text = File.ReadAllText(CurrentPath);
                if (string.Equals(text, _lastSavedText, StringComparison.Ordinal)
                    || string.Equals(text, Editor.Text, StringComparison.Ordinal))
                {
                    return;
                }

                _loading = true;
                try
                {
                    Editor.Text = text;
                    _lastSavedText = text;
                    if (_isMarkdown && _previewMode)
                    {
                        Preview.Markdown = text;
                        _lastRenderedMarkdown = text;
                        _preparedPreviewDocument = null;
                        _lastPreviewWidth = 0;
                        _previewGeneration++;
                        ScheduleFitPreviewWidth();
                    }
                }
                finally
                {
                    _loading = false;
                }
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException
                or ArgumentException or InvalidOperationException or FormatException)
            {
            }
            finally
            {
                _diskReloadPending = false;
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
