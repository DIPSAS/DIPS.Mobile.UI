using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.Icons;
using DIPS.Mobile.UI.Resources.Sizes;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.API.Diagnostics;

/// <summary>
/// A floating overlay pill for layout diagnostics.
/// Collapsed: small pill indicator. Tap to expand and see live status, start/stop controls, and snapshot info.
/// </summary>
internal class LayoutDiagnosticsOverlay : Grid
{
    private readonly Label m_statusLabel;
    private readonly Label m_detailLabel;
    private readonly Button m_toggleButton;
    private readonly Grid m_expandedContent;
    private readonly Border m_collapsedPill;
    private readonly Border m_expandedPanel;
    private readonly Label m_collapsedCountLabel;
    private readonly BoxView m_recordingDot;
    private readonly BoxView m_expandedRecordingDot;
    private CancellationTokenSource? m_pulseCts;
    private bool m_isExpanded;
    
    internal const int OverlayIdentifier = 2910962;

    public LayoutDiagnosticsOverlay()
    {
        InputTransparent = false;
        CascadeInputTransparent = false;

        // ── Collapsed pill ──
        m_recordingDot = new BoxView
        {
            Color = Colors.GetColor(ColorName.color_icon_success),
            WidthRequest = Sizes.GetSize(SizeName.size_2),
            HeightRequest = Sizes.GetSize(SizeName.size_2),
            CornerRadius = Sizes.GetSize(SizeName.radius_xsmall),
            VerticalOptions = LayoutOptions.Center,
            IsVisible = false
        };

        m_collapsedCountLabel = new Label
        {
            Text = "📊",
            Style = Styles.GetLabelStyle(LabelStyle.UI100),
            TextColor = Colors.GetColor(ColorName.color_text_on_button),
            VerticalOptions = LayoutOptions.Center,
            VerticalTextAlignment = TextAlignment.Center
        };

        var pillContent = new HorizontalStackLayout
        {
            Spacing = Sizes.GetSize(SizeName.content_margin_xsmall),
            VerticalOptions = LayoutOptions.Center,
            Children = { m_recordingDot, m_collapsedCountLabel }
        };

        m_collapsedPill = new Border
        {
            BackgroundColor = Colors.GetColor(ColorName.color_surface_backdrop),
            Padding = new Thickness(
                Sizes.GetSize(SizeName.content_margin_small),
                Sizes.GetSize(SizeName.size_1)),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
            Margin = new Thickness(0, 54, Sizes.GetSize(SizeName.content_margin_small), 0),
            Stroke = Brush.Transparent,
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(Sizes.GetSize(SizeName.radius_xlarge)) }
        };
        m_collapsedPill.Content = pillContent;
        
        var collapsedTap = new TapGestureRecognizer();
        collapsedTap.Tapped += async (_, _) => await AnimateExpand();
        m_collapsedPill.GestureRecognizers.Add(collapsedTap);

        // ── Expanded panel ──
        m_expandedRecordingDot = new BoxView
        {
            Color = Colors.GetColor(ColorName.color_icon_danger),
            WidthRequest = Sizes.GetSize(SizeName.size_2),
            HeightRequest = Sizes.GetSize(SizeName.size_2),
            CornerRadius = Sizes.GetSize(SizeName.radius_xsmall),
            VerticalOptions = LayoutOptions.Center,
            IsVisible = false
        };

        var statusRow = new HorizontalStackLayout
        {
            Spacing = Sizes.GetSize(SizeName.content_margin_xsmall),
            Children = { m_expandedRecordingDot }
        };

        m_statusLabel = new Label
        {
            TextColor = Colors.GetColor(ColorName.color_text_on_button),
            Style = Styles.GetLabelStyle(LabelStyle.UI200),
            FontAttributes = FontAttributes.Bold,
            HorizontalTextAlignment = TextAlignment.Start,
            VerticalOptions = LayoutOptions.Center
        };
        statusRow.Children.Add(m_statusLabel);

        m_detailLabel = new Label
        {
            TextColor = Colors.GetColor(ColorName.color_text_on_button).WithAlpha(0.7f),
            Style = Styles.GetLabelStyle(LabelStyle.UI100),
            HorizontalTextAlignment = TextAlignment.Start,
            MaxLines = 1,
            LineBreakMode = LineBreakMode.TailTruncation
        };

        m_toggleButton = new Button
        {
            FontSize = 12,
            FontFamily = "UI",
            HeightRequest = Sizes.GetSize(SizeName.size_8),
            MinimumWidthRequest = Sizes.GetSize(SizeName.size_14),
            CornerRadius = (int)Sizes.GetSize(SizeName.radius_large),
            Padding = new Thickness(Sizes.GetSize(SizeName.content_margin_medium), 0),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center
        };
        m_toggleButton.Clicked += OnToggleButtonClicked;

        var collapseButton = new Button
        {
            Style = Styles.GetButtonStyle(ButtonStyle.CloseIconSmall),
            VerticalOptions = LayoutOptions.Center
        };
        collapseButton.Clicked += async (_, _) => await AnimateCollapse();

        var textStack = new VerticalStackLayout { Spacing = Sizes.GetSize(SizeName.size_half) };
        textStack.Add(statusRow);
        textStack.Add(m_detailLabel);

        var buttonRow = new HorizontalStackLayout
        {
            Spacing = Sizes.GetSize(SizeName.size_1),
            VerticalOptions = LayoutOptions.Center,
            Children = { m_toggleButton, collapseButton }
        };

        m_expandedContent = new Grid
        {
            Padding = new Thickness(
                Sizes.GetSize(SizeName.content_margin_medium),
                Sizes.GetSize(SizeName.content_margin_small)),
            ColumnSpacing = Sizes.GetSize(SizeName.content_margin_small),
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

        m_expandedPanel = new Border
        {
            BackgroundColor = Colors.GetColor(ColorName.color_surface_backdrop),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
            Margin = new Thickness(0, 54, Sizes.GetSize(SizeName.content_margin_small), 0),
            Stroke = Brush.Transparent,
            StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(Sizes.GetSize(SizeName.radius_large)) }
        };
        m_expandedPanel.Content = m_expandedContent;

        this.Add(m_collapsedPill);
        this.Add(m_expandedPanel);

        // Start collapsed
        m_expandedPanel.Opacity = 0;
        m_expandedPanel.Scale = 0.8;
        m_expandedPanel.IsVisible = false;
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

        await m_collapsedPill.FadeTo(0, 150, Easing.CubicIn);
        m_collapsedPill.IsVisible = false;

        m_expandedPanel.IsVisible = true;
        m_expandedPanel.Opacity = 0;
        m_expandedPanel.Scale = 0.85;
        m_expandedPanel.TranslationX = Sizes.GetSize(SizeName.size_5);

        await Task.WhenAll(
            m_expandedPanel.FadeTo(1, 250, Easing.CubicOut),
            m_expandedPanel.ScaleTo(1, 250, Easing.SpringOut),
            m_expandedPanel.TranslateTo(0, 0, 250, Easing.CubicOut)
        );
    }

    private async Task AnimateCollapse()
    {
        if (!m_isExpanded) return;
        m_isExpanded = false;

        await Task.WhenAll(
            m_expandedPanel.FadeTo(0, 200, Easing.CubicIn),
            m_expandedPanel.ScaleTo(0.85, 200, Easing.CubicIn),
            m_expandedPanel.TranslateTo(Sizes.GetSize(SizeName.size_5), 0, 200, Easing.CubicIn)
        );
        m_expandedPanel.IsVisible = false;

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
        m_toggleButton.BackgroundColor = Colors.GetColor(ColorName.color_fill_success);
        m_toggleButton.TextColor = Colors.GetColor(ColorName.color_text_on_button);
        m_statusLabel.Text = "Layout Diagnostics";
        m_detailLabel.Text = "Tap Start to begin";
        m_recordingDot.IsVisible = false;
        m_expandedRecordingDot.IsVisible = false;
        m_collapsedCountLabel.Text = "📊";
    }

    internal void UpdateRecording()
    {
        m_toggleButton.Text = "⏹ Stop";
        m_toggleButton.BackgroundColor = Colors.GetColor(ColorName.color_fill_button_danger);
        m_toggleButton.TextColor = Colors.GetColor(ColorName.color_text_on_button);
        m_statusLabel.Text = "Recording";
        m_detailLabel.Text = "Navigate to capture layout data";
        m_recordingDot.IsVisible = true;
        m_recordingDot.Color = Colors.GetColor(ColorName.color_icon_danger);
        m_expandedRecordingDot.IsVisible = true;
        m_collapsedCountLabel.Text = "REC";
        StartPulse();
    }

    internal void UpdateWithSnapshot(LayoutDiagnosticsSnapshot snapshot)
    {
        m_detailLabel.Text = $"{snapshot.SourceName}: {snapshot.ToCompactString()}";

        if (snapshot.Warnings.Count > 0)
        {
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
