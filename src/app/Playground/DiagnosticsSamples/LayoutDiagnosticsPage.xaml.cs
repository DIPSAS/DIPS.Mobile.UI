using DIPS.Mobile.UI.API.Diagnostics;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.Sizes;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using LayoutEffect = DIPS.Mobile.UI.Effects.Layout.Layout;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

namespace Playground.DiagnosticsSamples;

public partial class LayoutDiagnosticsPage
{
    public LayoutDiagnosticsPage()
    {
        InitializeComponent();
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
            LayoutDiagnosticsService.Teardown();
        else
            LayoutDiagnosticsService.Initialize();

        UpdateState();
    }

    private void OnClearClicked(object? sender, EventArgs e)
    {
        LayoutDiagnosticsService.ClearSnapshots();
        LoadSnapshots();
    }

    private void UpdateState()
    {
        OverlayButton.Text = LayoutDiagnosticsService.IsInitialized ? "Disable Overlay" : "Enable Overlay";

        ToggleButton.IsVisible = LayoutDiagnosticsService.IsInitialized;
        StatusLabel.IsVisible = LayoutDiagnosticsService.IsInitialized;

        if (LayoutDiagnosticsService.IsEnabled)
        {
            StatusLabel.Text = "⏺ Recording";
            StatusLabel.TextColor = Colors.GetColor(ColorName.color_text_danger);
            ToggleButton.Text = "⏹ Stop Recording";
            ToggleButton.BackgroundColor = Colors.GetColor(ColorName.color_fill_button_danger);
            ToggleButton.TextColor = Colors.GetColor(ColorName.color_text_on_button);
        }
        else
        {
            StatusLabel.Text = "Stopped";
            StatusLabel.TextColor = Colors.GetColor(ColorName.color_text_subtle);
            ToggleButton.Text = "▶ Start Recording";
            ToggleButton.BackgroundColor = Colors.GetColor(ColorName.color_fill_button);
            ToggleButton.TextColor = Colors.GetColor(ColorName.color_text_on_button);
        }
    }

    private void LoadSnapshots()
    {
        var snapshots = LayoutDiagnosticsService.CompletedSnapshots;
        SnapshotCountLabel.Text = $"Snapshots ({snapshots.Count})";

        SnapshotList.Children.Clear();

        if (snapshots.Count == 0)
        {
            SnapshotList.Children.Add(new Label
            {
                Text = "No snapshots yet. Start recording and navigate between pages to capture layout data.",
                Style = Styles.GetLabelStyle(LabelStyle.UI100),
                TextColor = Colors.GetColor(ColorName.color_text_subtle),
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, Sizes.GetSize(SizeName.size_6))
            });
            return;
        }

        for (var i = snapshots.Count - 1; i >= 0; i--)
        {
            SnapshotList.Children.Add(CreateSnapshotCard(snapshots[i]));
        }
    }

    private static View CreateSnapshotCard(LayoutDiagnosticsSnapshot snapshot)
    {
        var hasWarnings = snapshot.Warnings.Count > 0;

        var titleLabel = new Label
        {
            Text = snapshot.SourceName,
            Style = Styles.GetLabelStyle(LabelStyle.UI200),
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.GetColor(ColorName.color_text_default),
            MaxLines = 1,
            LineBreakMode = LineBreakMode.TailTruncation
        };

        var metricsLabel = new Label
        {
            Text = $"Measures: {snapshot.TotalMeasureCount}  |  Arranges: {snapshot.TotalArrangeCount}",
            Style = Styles.GetLabelStyle(LabelStyle.UI100),
            TextColor = Colors.GetColor(ColorName.color_text_subtle)
        };

        var content = new VerticalStackLayout { Spacing = Sizes.GetSize(SizeName.size_1) };
        content.Add(titleLabel);
        content.Add(metricsLabel);

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
                Style = Styles.GetLabelStyle(isThrashing ? LabelStyle.UI100 : LabelStyle.Body100),
                TextColor = isThrashing
                    ? Colors.GetColor(ColorName.color_text_danger)
                    : Colors.GetColor(ColorName.color_text_subtle)
            });
        }

        var card = new Grid
        {
            Padding = new Thickness(
                Sizes.GetSize(SizeName.content_margin_medium),
                Sizes.GetSize(SizeName.content_margin_small)),
            BackgroundColor = hasWarnings
                ? Colors.GetColor(ColorName.color_surface_warning)
                : Colors.GetColor(ColorName.color_surface_default)
        };
        LayoutEffect.SetCornerRadius(card, new CornerRadius(Sizes.GetSize(SizeName.radius_small)));
        LayoutEffect.SetStroke(card, hasWarnings
            ? Colors.GetColor(ColorName.color_border_warning)
            : Colors.GetColor(ColorName.color_border_subtle));
        card.Add(content);

        return card;
    }
}
