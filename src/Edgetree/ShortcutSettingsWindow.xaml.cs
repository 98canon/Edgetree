using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SidebarExplorer.App.Models;
using SidebarExplorer.App.Services;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MouseButtonEventArgs = System.Windows.Input.MouseButtonEventArgs;
using Button = System.Windows.Controls.Button;
using Brush = System.Windows.Media.Brush;
using Cursors = System.Windows.Input.Cursors;

namespace SidebarExplorer.App;

public partial class ShortcutSettingsWindow : Window
{
    private readonly AppSettings _settings;
    private string? _listeningId;
    private TextBlock? _listeningLabel;

    public ShortcutSettingsWindow(AppSettings settings)
    {
        _settings = settings;
        InitializeComponent();
        BuildRows();
    }

    private void BuildRows()
    {
        Rows.Children.Clear();
        foreach (var spec in ShortcutService.Catalog)
        {
            var row = new DockPanel { Margin = new Thickness(0, 0, 0, 8) };
            var clear = new Button
            {
                Content = Strings.ShortcutClear,
                Margin = new Thickness(6, 0, 0, 0),
                Padding = new Thickness(8, 2, 8, 2),
                Tag = spec.Id,
            };
            var reset = new Button
            {
                Content = Strings.ShortcutReset,
                Margin = new Thickness(6, 0, 0, 0),
                Padding = new Thickness(8, 2, 8, 2),
                Tag = spec.Id,
            };
            DockPanel.SetDock(clear, Dock.Right);
            DockPanel.SetDock(reset, Dock.Right);
            clear.Click += Clear_Click;
            reset.Click += Reset_Click;

            var keys = new TextBlock
            {
                Text = ShortcutService.Format(_settings, spec.Id),
                Width = 120,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Right,
                Opacity = 0.85,
                Tag = spec.Id,
                Cursor = Cursors.Hand,
            };
            DockPanel.SetDock(keys, Dock.Right);
            keys.MouseLeftButtonDown += Keys_Click;

            row.Children.Add(clear);
            row.Children.Add(reset);
            row.Children.Add(keys);
            row.Children.Add(new TextBlock
            {
                Text = ShortcutService.DisplayName(spec.Id),
                VerticalAlignment = VerticalAlignment.Center,
                TextTrimming = TextTrimming.CharacterEllipsis,
            });
            Rows.Children.Add(row);
        }
    }

    private void Keys_Click(object sender, MouseButtonEventArgs e)
    {
        if (sender is not TextBlock label || label.Tag is not string id)
        {
            return;
        }

        StopListening();
        _listeningId = id;
        _listeningLabel = label;
        label.Text = Strings.ShortcutPress;
        label.Foreground = (Brush)FindResource("DialogForeground");
        label.Opacity = 1;
    }

    private void Clear_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: string id })
        {
            return;
        }

        StopListening();
        ShortcutService.Clear(_settings, id);
        RefreshLabels();
    }

    private void Reset_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: string id })
        {
            return;
        }

        StopListening();
        ShortcutService.Reset(_settings, id);
        RefreshLabels();
    }

    private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (_listeningId is null)
        {
            if (e.Key == Key.Escape)
            {
                Close();
                e.Handled = true;
            }
            return;
        }

        if (e.Key is Key.LeftCtrl or Key.RightCtrl or Key.LeftShift or Key.RightShift
            or Key.LeftAlt or Key.RightAlt or Key.System)
        {
            return;
        }

        if (e.Key == Key.Escape)
        {
            StopListening();
            RefreshLabels();
            e.Handled = true;
            return;
        }

        Key capturedKey = e.Key == Key.System ? e.SystemKey : e.Key;
        ShortcutService.Set(_settings, _listeningId, capturedKey, Keyboard.Modifiers);
        StopListening();
        RefreshLabels();
        e.Handled = true;
    }

    private void StopListening()
    {
        _listeningId = null;
        _listeningLabel = null;
    }

    private void RefreshLabels()
    {
        foreach (DockPanel row in Rows.Children.OfType<DockPanel>())
        {
            foreach (var keys in row.Children.OfType<TextBlock>())
            {
                if (keys.Tag is string id)
                {
                    keys.Text = ShortcutService.Format(_settings, id);
                    keys.Opacity = 0.85;
                }
            }
        }
    }

    private void Close_Click(object sender, RoutedEventArgs e) => Close();
}
