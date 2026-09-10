using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SidebarExplorer.App.Services;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using Point = System.Windows.Point;
using Button = System.Windows.Controls.Button;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Cursors = System.Windows.Input.Cursors;

namespace SidebarExplorer.App;

public partial class DocumentViewerWindow : Window
{
    private readonly List<DocumentTab> _tabs = new();
    private DocumentTab? _active;
    private bool _awaitingNewTab;
    private bool _suppressActivateEvent;
    private bool _panning;
    private Point _panStart;
    private double _imageScale = 1;

    public DocumentViewerWindow()
    {
        InitializeComponent();
        DocPane.ShowPopOut = false;
        Loaded += (_, _) => PullThemeFromOwner();
        Closed += (_, _) => DocPane.Unload();
    }

    private void PullThemeFromOwner()
    {
        if (Owner is not FrameworkElement host)
        {
            return;
        }

        foreach (string key in new[]
        {
            "ViewerBackground",
            "ForegroundText",
            "HoverBackground",
            "PanelDividerBrush",
            "ViewerChipBorderBrush",
            "FooterChipFontSize",
        })
        {
            var value = host.TryFindResource(key);
            if (value is not null)
            {
                Resources[key] = value;
            }
        }

        if (TryFindResource("ViewerBackground") is Brush background)
        {
            Background = background;
        }

        DocPane.ApplyEditorColors();
    }

    public event EventHandler<string>? TabActivated;

    public void FollowPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
        {
            return;
        }

        var existing = _tabs.FirstOrDefault(t =>
            t.Path is not null && string.Equals(t.Path, path, StringComparison.OrdinalIgnoreCase));
        if (existing is not null)
        {
            FollowActivate(existing);
            return;
        }

        var empty = _tabs.FirstOrDefault(t => t.Path is null);
        if (empty is not null)
        {
            empty.Path = path;
            empty.Title = Path.GetFileName(path);
            RebuildTabs();
            FollowActivate(empty);
            _awaitingNewTab = false;
            return;
        }

        if (_awaitingNewTab || _tabs.Count == 0)
        {
            var tab = new DocumentTab { Path = path, Title = Path.GetFileName(path) };
            _tabs.Add(tab);
            _awaitingNewTab = false;
            RebuildTabs();
            FollowActivate(tab);
            return;
        }

        if (_active is not null)
        {
            _active.Path = path;
            _active.Title = Path.GetFileName(path);
            RebuildTabs();
            FollowActivate(_active);
        }
    }

    private void FollowActivate(DocumentTab tab)
    {
        _suppressActivateEvent = true;
        try
        {
            ActivateTab(tab);
        }
        finally
        {
            _suppressActivateEvent = false;
        }
    }

    private void AddTabButton_Click(object sender, RoutedEventArgs e)
    {
        if (_tabs.Any(t => t.Path is null))
        {
            ActivateTab(_tabs.First(t => t.Path is null));
            return;
        }

        var tab = new DocumentTab { Title = Strings.DocumentNewTab };
        _tabs.Add(tab);
        _awaitingNewTab = true;
        RebuildTabs();
        ActivateTab(tab);
    }

    private void RebuildTabs()
    {
        TabStrip.Children.Clear();
        foreach (var tab in _tabs)
        {
            var chip = new Border
            {
                Tag = tab,
                Padding = new Thickness(10, 4, 6, 4),
                Margin = new Thickness(0, 0, 4, 0),
                CornerRadius = new CornerRadius(3),
                Background = ReferenceEquals(tab, _active)
                    ? (Brush)FindResource("HoverBackground")
                    : Brushes.Transparent,
                BorderBrush = (Brush)FindResource("ViewerChipBorderBrush"),
                BorderThickness = new Thickness(1),
                Opacity = ReferenceEquals(tab, _active) ? 1 : 0.7,
                Cursor = Cursors.Hand,
            };
            var row = new DockPanel();
            var close = new TextBlock
            {
                Text = "×",
                Width = 16,
                Margin = new Thickness(8, 0, 0, 0),
                Foreground = (Brush)FindResource("ForegroundText"),
                Cursor = Cursors.Hand,
                Tag = tab,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            DockPanel.SetDock(close, Dock.Right);
            close.MouseLeftButtonDown += CloseTab_Click;
            row.Children.Add(close);
            row.Children.Add(new TextBlock
            {
                Text = tab.Title,
                VerticalAlignment = VerticalAlignment.Center,
                TextTrimming = TextTrimming.CharacterEllipsis,
                MaxWidth = 180,
                Foreground = (Brush)FindResource("ForegroundText"),
            });
            chip.Child = row;
            chip.MouseLeftButtonDown += TabButton_Click;
            TabStrip.Children.Add(chip);
        }
    }

    private void TabButton_Click(object sender, MouseButtonEventArgs e)
    {
        if (sender is FrameworkElement { Tag: DocumentTab tab })
        {
            ActivateTab(tab);
        }
    }

    private void CloseTab_Click(object sender, MouseButtonEventArgs e)
    {
        e.Handled = true;
        if (sender is not FrameworkElement { Tag: DocumentTab tab })
        {
            return;
        }

        int index = _tabs.IndexOf(tab);
        _tabs.Remove(tab);
        if (_tabs.Count == 0)
        {
            Close();
            return;
        }

        var next = _tabs[Math.Clamp(index, 0, _tabs.Count - 1)];
        RebuildTabs();
        ActivateTab(next);
    }

    private void ActivateTab(DocumentTab tab)
    {
        DocPane.FlushSave();
        _active = tab;
        RebuildTabs();
        Title = string.IsNullOrEmpty(tab.Path) ? "Edgetree" : $"{tab.Title} — Edgetree";

        if (tab.Path is null)
        {
            DocPane.Visibility = Visibility.Collapsed;
            ImageHost.Visibility = Visibility.Collapsed;
            EmptyHint.Visibility = Visibility.Visible;
            DocPane.Unload();
            return;
        }

        EmptyHint.Visibility = Visibility.Collapsed;
        if (FileTypeFilter.IsTextPreview(tab.Path))
        {
            ImageHost.Visibility = Visibility.Collapsed;
            PreviewImage.Source = null;
            DocPane.Visibility = Visibility.Visible;
            DocPane.Load(tab.Path);
        }
        else
        {
            DocPane.Unload();
            DocPane.Visibility = Visibility.Collapsed;
            ShowImage(tab.Path);
        }

        if (!_suppressActivateEvent && tab.Path is not null)
        {
            TabActivated?.Invoke(this, tab.Path);
        }
    }

    private void ShowImage(string path)
    {
        ImageHost.Visibility = Visibility.Visible;
        _imageScale = 1;
        ImageZoom.ScaleX = 1;
        ImageZoom.ScaleY = 1;
        ImagePan.X = 0;
        ImagePan.Y = 0;
        try
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();
            bitmap.Freeze();
            PreviewImage.Source = bitmap;
        }
        catch
        {
            PreviewImage.Source = null;
        }
    }

    private void ImageHost_MouseWheel(object sender, MouseWheelEventArgs e)
    {
        _imageScale = Math.Clamp(_imageScale * (e.Delta > 0 ? 1.1 : 0.9), 0.2, 8);
        ImageZoom.ScaleX = _imageScale;
        ImageZoom.ScaleY = _imageScale;
        e.Handled = true;
    }

    private void ImageHost_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        _panning = true;
        _panStart = e.GetPosition(ImageHost);
        ImageHost.CaptureMouse();
    }

    private void ImageHost_MouseMove(object sender, MouseEventArgs e)
    {
        if (!_panning)
        {
            return;
        }

        var now = e.GetPosition(ImageHost);
        var offset = now - _panStart;
        _panStart = now;
        ImagePan.X += offset.X;
        ImagePan.Y += offset.Y;
    }

    private void ImageHost_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        _panning = false;
        ImageHost.ReleaseMouseCapture();
    }

    private void ImageHost_LostMouseCapture(object sender, MouseEventArgs e)
        => _panning = false;

    private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            Close();
            e.Handled = true;
        }
    }

    private sealed class DocumentTab
    {
        public string? Path { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
