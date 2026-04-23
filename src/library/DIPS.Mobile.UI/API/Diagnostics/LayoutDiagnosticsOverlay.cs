using DIPS.Mobile.UI.Resources.Colors;

namespace DIPS.Mobile.UI.API.Diagnostics;

/// <summary>
/// A floating overlay pill for layout diagnostics.
/// Collapsed: small draggable pill indicator. Expanded: panel with live status.
/// </summary>
internal class LayoutDiagnosticsOverlay : Grid
{
    private readonly Label m_statusLabel;
    private readonly Label m_detailLabel;
    private readonly Button m_toggleButton;
    private readonly Grid m_expandedContent;
    private readonly Border m_collapsedPill;
    private readonly Label m_collapsedCountLabel;
    private readonly BoxView m_recordingDot;
    private readonly BoxView m_expandedRecordingDot;
    private CancellationTokenSource? m_pulseCts;
    private bool m_isExpanded;
    
    internal const int OverlayIdentifier = 2910962;

    private static readonly Color BackgroundDark = Color.FromArgb("#DD1A1A2E");
    private static readonly Color AccentGreen = Color.FromArgb("#4ADE80");
    private static readonly Color AccentRed = Color.FromArgb("#F87171");
    private static readonly Color AccentOrange = Color.FromArgb("#FB923C");
    private static readonly Color TextPrimary = Microsoft.Maui.Graphics.Colors.White;
    private static readonly Color TextSecondary = Color.FromArgb("#A1A1AA");

    public LayoutDiagnosticsOverlay()
    {
        InputTransparent = false;
        CascadeInputTransparent = false;
        
        HorizontalOptions = LayoutOptions.End;
        VerticalOptions = LayoutOptions.Start;
        
        Margin = new Thickness(8, 54, 8, 0);

        // ── Collapsed pill ──
        m_recordingDot = new BoxView
        {
            Color = AccentGreen,
            WidthRequest = 8,
            HeightRequest = 8,
            CornerRadius = 4,
            VerticalOptions = LayoutOptions.Center,
            IsVisible = false
        };

        m_collapsedCountLabel = new Label
        {
            Text = "📊",
            FontSize = 13,
            FontFamily = "UI",
            TextColor = TextPrimary,
            VerticalOptions = LayoutOptions.Center,
            VerticalTextAlignment = TextAlignment.Center
        };

        var pillContent = new HorizontalStackLayout
        {
            Spacing = 6,
            VerticalOptions = LayoutOptions.Center,
            Children = { m_recordingDot, m_collapsedCountLabel }
        };

        m_collapsedPill = new Border
        {
            BackgroundColor = BackgroundDark,
            StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 20 },
            Stroke = Microsoft.Maui.Graphics.Colors.Transparent,
            StrokeThickness = 0,
            Padding = new Thickness(12, 8),
            HorizontalOptions = LayoutOptions.End,
            Content = pillContent,
            Shadow = new Shadow
            {
                Brush = new SolidColorBrush(Microsoft.Maui.Graphics.Colors.Black),
                Offset = new Point(0, 2),
                Radius = 8,
                Opacity = 0.4f
            }
        };
        
        var collapsedTap = new TapGestureRecognizer();
        collapsedTap.Tapped += async (_, _) => await AnimateExpand();
        m_collapsedPill.GestureRecognizers.Add(collapsedTap);

        // ── Expanded panel ──
        m_expandedRecordingDot = new BoxView
        {
            Color = AccentRed,
            WidthRequest = 8,
            HeightRequest = 8,
            CornerRadius = 4,
            VerticalOptions = LayoutOptions.Center,
            IsVisible = false
        };

        var statusRow = new HorizontalStackLayout
        {
            Spacing = 6,
            Children = { m_expandedRecordingDot }
        };

        m_statusLabel = new Label
        {
            TextColor = TextPrimary,
            FontSize = 13,
            FontFamily = "UI",
            FontAttributes = FontAttributes.Bold,
            HorizontalTextAlignment = TextAlignment.Start,
            VerticalOptions = LayoutOptions.Center
        };
        statusRow.Children.Add(m_statusLabel);

        m_detailLabel = new Label
        {
            TextColor = TextSecondary,
            FontSize = 11,
            FontFamily = "UI",
            HorizontalTextAlignment = TextAlignment.Start,
            MaxLines = 1,
            LineBreakMode = LineBreakMode.TailTruncation
        };

        m_toggleButton = new Button
        {
            FontSize = 12,
            FontFamily = "UI",
            HeightRequest = 32,
            MinimumWidthRequest = 56,
            CornerRadius = 16,
            Padding = new Thickness(14, 0),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center,
            Shadow = new Shadow
            {
                Brush = new SolidColorBrush(Microsoft.Maui.Graphics.Colors.Black),
                Offset = new Point(0, 1),
                Radius = 4,
                Opacity = 0.3f
            }
        };
        m_toggleButton.Clicked += OnToggleButtonClicked;

        var collapseButton = new Label
        {
            Text = "✕",
            FontSize = 14,
            TextColor = TextSecondary,
            WidthRequest = 28,
            HeightRequest = 28,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center
        };
        var collapseTap = new TapGestureRecognizer();
        collapseTap.Tapped += async (_, _) => await AnimateCollapse();
        collapseButton.GestureRecognizers.Add(collapseTap);

        var textStack = new VerticalStackLayout { Spacing = 2 };
        textStack.Add(statusRow);
        textStack.Add(m_detailLabel);

        var buttonRow = new HorizontalStackLayout
        {
            Spacing = 4,
            VerticalOptions = LayoutOptions.Center,
            Children = { m_toggleButton, collapseButton }
        };

        m_expandedContent = new Grid
        {
            Padding = new Thickness(14, 10),
            ColumnSpacing = 10,
            MinimumWidthRequest = 220,
            ColumnDefinitions =
            [
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto)
            ]
        };

        m_expandedContent.Add(textStack);
        Grid.SetColumn(textStack, 0);
        m_expandedContent.Add(buttonRow);
        Grid.SetColumn(buttonRow, 1);

        var expandedBorder = new Border
        {
            BackgroundColor = BackgroundDark,
            StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 16 },
            Stroke = Microsoft.Maui.Graphics.Colors.Transparent,
            StrokeThickness = 0,
            Padding = 0,
            HorizontalOptions = LayoutOptions.End,
            Content = m_expandedContent,
            Shadow = new Shadow
            {
                Brush = new SolidColorBrush(Microsoft.Maui.Graphics.Colors.Black),
                Offset = new Point(0, 4),
                Radius = 12,
                Opacity = 0.5f
            }
        };

        this.Add(m_collapsedPill);
        this.Add(expandedBorder);

        // Start collapsed
        expandedBorder.Opacity = 0;
        expandedBorder.Scale = 0.8;
        expandedBorder.IsVisible = false;
        m_isExpanded = false;

        UpdateStopped();
    }

    private void OnToggleButtonClicked(object? sender, EventArgs e)
    {
        LayoutDiagnosticsService.Toggle();
    }

    private async Task AnimateExpand()
    {
        if (m_isExpanded) return;
        m_isExpanded = true;

        var expandedBorder = (Border)Children[1];

        // Fade out pill
        await m_collapsedPill.FadeTo(0, 150, Easing.CubicIn);
        m_collapsedPill.IsVisible = false;

        // Show and animate in expanded panel
        expandedBorder.IsVisible = true;
        expandedBorder.Opacity = 0;
        expandedBorder.Scale = 0.85;
        expandedBorder.TranslationX = 20;

        await Task.WhenAll(
            expandedBorder.FadeTo(1, 250, Easing.CubicOut),
            expandedBorder.ScaleTo(1, 250, Easing.SpringOut),
            expandedBorder.TranslateTo(0, 0, 250, Easing.CubicOut)
        );
    }

    private async Task AnimateCollapse()
    {
        if (!m_isExpanded) return;
        m_isExpanded = false;

        var expandedBorder = (Border)Children[1];

        // Animate out expanded panel
        await Task.WhenAll(
            expandedBorder.FadeTo(0, 200, Easing.CubicIn),
            expandedBorder.ScaleTo(0.85, 200, Easing.CubicIn),
            expandedBorder.TranslateTo(20, 0, 200, Easing.CubicIn)
        );
        expandedBorder.IsVisible = false;

        // Show pill with pop animation
        m_collapsedPill.IsVisible = true;
        m_collapsedPill.Opacity = 0;
        m_collapsedPill.Scale = 0.5;

        await Task.WhenAll(
            m_collapsedPill.FadeTo(1, 200, Easing.CubicOut),
            m_collapsedPill.ScaleTo(1, 300, Easing.SpringOut)
        );
    }

    internal void UpdateStopped()
    {
        StopPulse();
        m_toggleButton.Text = "▶ Start";
        m_toggleButton.BackgroundColor = AccentGreen;
        m_toggleButton.TextColor = Color.FromArgb("#1A1A2E");
        m_statusLabel.Text = "Layout Diagnostics";
        m_detailLabel.Text = "Tap Start to begin";
        m_recordingDot.IsVisible = false;
        m_expandedRecordingDot.IsVisible = false;
        m_collapsedCountLabel.Text = "📊";
    }

    internal void UpdateRecording()
    {
        m_toggleButton.Text = "⏹ Stop";
        m_toggleButton.BackgroundColor = AccentRed;
        m_toggleButton.TextColor = TextPrimary;
        m_statusLabel.Text = "Recording";
        m_detailLabel.Text = "Navigate to capture layout data";
        m_recordingDot.IsVisible = true;
        m_recordingDot.Color = AccentRed;
        m_expandedRecordingDot.IsVisible = true;
        m_collapsedCountLabel.Text = "REC";
        StartPulse();
    }

    internal void UpdateWithSnapshot(LayoutDiagnosticsSnapshot snapshot)
    {
        m_detailLabel.Text = $"{snapshot.SourceName}: {snapshot.ToCompactString()}";

        if (snapshot.Warnings.Count > 0)
        {
            // Only update pill text if not expanded (don't override REC while recording)
            if (!m_isExpanded)
            {
                m_collapsedCountLabel.Text = $"\u26a0 {snapshot.Warnings.Count}";
            }
            AnimateWarningFlash();
        }
    }

    private async void AnimateWarningFlash()
    {
        await m_collapsedPill.ScaleTo(1.15, 100, Easing.CubicOut);
        await m_collapsedPill.ScaleTo(1.0, 200, Easing.SpringOut);
    }

    private void StartPulse()
    {
        StopPulse();
        m_pulseCts = new CancellationTokenSource();
        var token = m_pulseCts.Token;
        
        _ = PulseLoop(token);
    }

    private async Task PulseLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                await Task.WhenAll(
                    m_recordingDot.FadeTo(0.2, 500, Easing.SinInOut),
                    m_expandedRecordingDot.FadeTo(0.2, 500, Easing.SinInOut));
                if (token.IsCancellationRequested) break;
                await Task.WhenAll(
                    m_recordingDot.FadeTo(1.0, 500, Easing.SinInOut),
                    m_expandedRecordingDot.FadeTo(1.0, 500, Easing.SinInOut));
            }
            catch (TaskCanceledException)
            {
                break;
            }
        }
    }

    private void StopPulse()
    {
        m_pulseCts?.Cancel();
        m_pulseCts?.Dispose();
        m_pulseCts = null;
        m_recordingDot.Opacity = 1.0;
    }
}
