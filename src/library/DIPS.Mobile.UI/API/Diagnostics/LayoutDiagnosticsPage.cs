using DIPS.Mobile.UI.Resources.Sizes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.API.Diagnostics;

/// <summary>
/// A ready-to-use page for viewing and controlling layout diagnostics.
/// Add this to a Tab in your app shell to enable runtime layout diagnostics.
/// </summary>
public class LayoutDiagnosticsPage : Components.Pages.ContentPage
{
    private readonly Label m_statusLabel;
    private readonly Button m_overlayButton;
    private readonly Button m_toggleButton;
    private readonly Label m_snapshotCountLabel;
    private readonly VerticalStackLayout m_snapshotList;

    public LayoutDiagnosticsPage()
    {
        Title = "Diagnostics";

        // Overlay toggle
        m_overlayButton = new Button
        {
            FontFamily = "UI",
            FontSize = 14,
            CornerRadius = 20,
            HeightRequest = 44,
            HorizontalOptions = LayoutOptions.Center,
            Padding = new Thickness(24, 0),
            Margin = new Thickness(0, 0, 0, 16)
        };
        m_overlayButton.Clicked += OnOverlayButtonClicked;

        m_statusLabel = new Label
        {
            FontSize = 16,
            FontFamily = "UI",
            FontAttributes = FontAttributes.Bold,
            HorizontalTextAlignment = TextAlignment.Center,
            Margin = new Thickness(0, 0, 0, 8)
        };

        m_toggleButton = new Button
        {
            FontFamily = "UI",
            FontSize = 14,
            CornerRadius = 20,
            HeightRequest = 44,
            HorizontalOptions = LayoutOptions.Center,
            Padding = new Thickness(24, 0)
        };
        m_toggleButton.Clicked += OnToggleClicked;

        m_snapshotCountLabel = new Label
        {
            FontSize = 13,
            FontFamily = "UI",
            TextColor = Colors.GetColor(ColorName.color_text_subtle),
            HorizontalTextAlignment = TextAlignment.Start,
            Margin = new Thickness(0, 16, 0, 4)
        };

        var clearButton = new Button
        {
            Text = "Clear snapshots",
            FontFamily = "UI",
            FontSize = 13,
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent,
            TextColor = Colors.GetColor(ColorName.color_text_link),
            HorizontalOptions = LayoutOptions.End,
            Padding = new Thickness(8, 0),
            HeightRequest = 32
        };
        clearButton.Clicked += OnClearClicked;

        var headerRow = new Grid
        {
            ColumnDefinitions =
            [
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto)
            ]
        };
        headerRow.Add(m_snapshotCountLabel);
        Grid.SetColumn((BindableObject)m_snapshotCountLabel, 0);
        headerRow.Add(clearButton);
        Grid.SetColumn((BindableObject)clearButton, 1);

        m_snapshotList = new VerticalStackLayout
        {
            Spacing = 8
        };

        var controlsLayout = new VerticalStackLayout
        {
            Spacing = 0,
            Padding = new Thickness(
                Sizes.GetSize(SizeName.content_margin_medium),
                Sizes.GetSize(SizeName.content_margin_medium),
                Sizes.GetSize(SizeName.content_margin_medium),
                0),
            Children =
            {
                m_overlayButton,
                m_statusLabel,
                m_toggleButton,
                headerRow
            }
        };

        var scrollView = new ScrollView
        {
            Padding = new Thickness(
                Sizes.GetSize(SizeName.content_margin_medium),
                0),
            Content = m_snapshotList
        };

        var layout = new Grid
        {
            RowDefinitions =
            [
                new RowDefinition(GridLength.Auto),
                new RowDefinition(GridLength.Star)
            ]
        };
        layout.Add(controlsLayout);
        Grid.SetRow(controlsLayout, 0);
        layout.Add(scrollView);
        Grid.SetRow(scrollView, 1);

        Content = layout;

        UpdateState();
        LoadSnapshots();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        LayoutDiagnosticsService.SnapshotCompleted += OnSnapshotCompleted;
        UpdateState();
        LoadSnapshots();
    }

    protected override void OnDisappearing()
    {
        LayoutDiagnosticsService.SnapshotCompleted -= OnSnapshotCompleted;
        base.OnDisappearing();
    }

    private void OnSnapshotCompleted(LayoutDiagnosticsSnapshot snapshot)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            LoadSnapshots();
            UpdateState();
        });
    }

    private void OnToggleClicked(object? sender, EventArgs e)
    {
        LayoutDiagnosticsService.Toggle();
        UpdateState();
        LoadSnapshots();
    }

    private void OnOverlayButtonClicked(object? sender, EventArgs e)
    {
        if (LayoutDiagnosticsService.IsInitialized)
        {
            LayoutDiagnosticsService.Teardown();
        }
        else
        {
            LayoutDiagnosticsService.Initialize();
        }
        
        UpdateState();
    }

    private void OnClearClicked(object? sender, EventArgs e)
    {
        LayoutDiagnosticsService.ClearSnapshots();
        LoadSnapshots();
    }

    private void UpdateState()
    {
        // Overlay button
        if (LayoutDiagnosticsService.IsInitialized)
        {
            m_overlayButton.Text = "Disable Overlay";
            m_overlayButton.BackgroundColor = Colors.GetColor(ColorName.color_fill_default);
            m_overlayButton.TextColor = Microsoft.Maui.Graphics.Colors.White;
        }
        else
        {
            m_overlayButton.Text = "Enable Overlay";
            m_overlayButton.BackgroundColor = Colors.GetColor(ColorName.color_fill_default);
            m_overlayButton.TextColor = Microsoft.Maui.Graphics.Colors.White;
        }

        // Recording controls — only visible when overlay is active
        m_toggleButton.IsVisible = LayoutDiagnosticsService.IsInitialized;
        m_statusLabel.IsVisible = LayoutDiagnosticsService.IsInitialized;

        if (LayoutDiagnosticsService.IsEnabled)
        {
            m_statusLabel.Text = "⏺ Recording";
            m_statusLabel.TextColor = Color.FromArgb("#C62828");
            m_toggleButton.Text = "⏹ Stop Recording";
            m_toggleButton.BackgroundColor = Color.FromArgb("#C62828");
            m_toggleButton.TextColor = Microsoft.Maui.Graphics.Colors.White;
        }
        else
        {
            m_statusLabel.Text = "Stopped";
            m_statusLabel.TextColor = Colors.GetColor(ColorName.color_text_subtle);
            m_toggleButton.Text = "▶ Start Recording";
            m_toggleButton.BackgroundColor = Colors.GetColor(ColorName.color_fill_default);
            m_toggleButton.TextColor = Microsoft.Maui.Graphics.Colors.White;
        }
    }

    private void LoadSnapshots()
    {
        var snapshots = LayoutDiagnosticsService.CompletedSnapshots;
        m_snapshotCountLabel.Text = $"Snapshots ({snapshots.Count})";

        m_snapshotList.Children.Clear();

        if (snapshots.Count == 0)
        {
            m_snapshotList.Children.Add(new Label
            {
                Text = "No snapshots yet. Start recording and navigate between pages to capture layout data.",
                FontFamily = "UI",
                FontSize = 13,
                TextColor = Colors.GetColor(ColorName.color_text_subtle),
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 24)
            });
            return;
        }

        // Show newest first
        for (var i = snapshots.Count - 1; i >= 0; i--)
        {
            m_snapshotList.Children.Add(CreateSnapshotCard(snapshots[i]));
        }
    }

    private static View CreateSnapshotCard(LayoutDiagnosticsSnapshot snapshot)
    {
        var hasWarnings = snapshot.Warnings.Count > 0;

        var titleLabel = new Label
        {
            Text = snapshot.SourceName,
            FontFamily = "UI",
            FontSize = 14,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.GetColor(ColorName.color_text_default),
            MaxLines = 1,
            LineBreakMode = LineBreakMode.TailTruncation
        };

        var metricsLabel = new Label
        {
            Text = $"Measures: {snapshot.TotalMeasureCount}  |  Arranges: {snapshot.TotalArrangeCount}",
            FontFamily = "UI",
            FontSize = 12,
            TextColor = Colors.GetColor(ColorName.color_text_subtle)
        };

        var content = new VerticalStackLayout { Spacing = 4 };
        content.Add(titleLabel);
        content.Add(metricsLabel);

        // Single unified list: all types sorted by count, warnings highlighted
        const double avgPerInstanceThreshold = 3.0;
        var allTypes = snapshot.MeasureCountsByType
            .OrderByDescending(kvp => kvp.Value);

        foreach (var (type, count) in allTypes)
        {
            var shortName = type.Contains('.') ? type[(type.LastIndexOf('.') + 1)..] : type;
            var instanceCount = snapshot.MeasureInstancesByType.TryGetValue(type, out var inst) ? inst.Count : 0;
            var avgPerInstance = instanceCount > 0 ? (double)count / instanceCount : count;
            var isThrashing = avgPerInstance > avgPerInstanceThreshold;

            var text = instanceCount <= 1
                ? $"{shortName}: {count}×"
                : $"{shortName}: {count}× ({instanceCount} instances, ~{avgPerInstance:F0}×/each)";

            content.Add(new Label
            {
                Text = isThrashing ? $"⚠️ {text}" : $"  {text}",
                FontFamily = isThrashing ? "UI" : "Body",
                FontSize = 11,
                TextColor = isThrashing
                    ? Color.FromArgb("#E65100")
                    : Colors.GetColor(ColorName.color_text_subtle)
            });
        }

        return new Frame
        {
            Padding = new Thickness(12, 10),
            CornerRadius = 8,
            HasShadow = false,
            BackgroundColor = hasWarnings
                ? Colors.GetColor(ColorName.color_surface_warning)
                : Colors.GetColor(ColorName.color_surface_default),
            BorderColor = hasWarnings
                ? Color.FromArgb("#FFB74D")
                : Colors.GetColor(ColorName.color_border_subtle),
            Content = content
        };
    }
}
