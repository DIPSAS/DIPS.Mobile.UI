using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.Sizes;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = Microsoft.Maui.Graphics.Colors;
using DuiColors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

internal class BarcodeScanProgressView : Grid
{
    private readonly Label m_counterLabel;

    public BarcodeScanProgressView()
    {
        InputTransparent = true;
        HorizontalOptions = LayoutOptions.Center;
        VerticalOptions = LayoutOptions.Start;
        Margin = new Thickness(0, Sizes.GetSize(SizeName.size_2), 0, 0);
        Padding = new Thickness(Sizes.GetSize(SizeName.content_margin_small));

        m_counterLabel = new Label
        {
            Style = Styles.GetLabelStyle(LabelStyle.UI300),
            TextColor = Colors.White,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Start,
            VerticalOptions = LayoutOptions.Start
        };

        Add(m_counterLabel);
    }

    public bool IsReducedMotionEnabled { get; set; }

    public void UpdateCounter(int currentScanCount, int requiredScanCount)
    {
        m_counterLabel.Text = string.Format(DUILocalizedStrings.BarcodeScanProgress, currentScanCount, requiredScanCount);
    }

    public Point? GetCounterCenterRelativeTo(VisualElement relativeTo)
    {
        if (m_counterLabel.Width <= 0 || m_counterLabel.Height <= 0 || relativeTo.Width <= 0 || relativeTo.Height <= 0)
            return null;

        var counterOrigin = GetElementOrigin(m_counterLabel);
        var relativeOrigin = GetElementOrigin(relativeTo);

        return new Point(
            counterOrigin.X - relativeOrigin.X + m_counterLabel.Width / 2,
            counterOrigin.Y - relativeOrigin.Y + m_counterLabel.Height / 2);
    }

    private static Point GetElementOrigin(VisualElement element)
    {
        var x = 0d;
        var y = 0d;
        Element? currentElement = element;

        while (currentElement is VisualElement visualElement)
        {
            x += visualElement.X + visualElement.TranslationX;
            y += visualElement.Y + visualElement.TranslationY;
            currentElement = visualElement.Parent;
        }

        return new Point(x, y);
    }

    public async Task AnimateCounterChangedAsync()
    {
        if (IsReducedMotionEnabled)
        {
            await m_counterLabel.FadeTo(1, 80, Easing.CubicOut);
            return;
        }

        m_counterLabel.AbortAnimation(nameof(AnimateCounterChangedAsync));
        var landingDistance = Sizes.GetSize(SizeName.size_2);
        var reboundDistance = Sizes.GetSize(SizeName.size_1);

        await Task.WhenAll(
            m_counterLabel.ScaleTo(.94, 85, Easing.CubicOut),
            m_counterLabel.TranslateTo(0, landingDistance, 85, Easing.CubicOut));

        await Task.WhenAll(
            m_counterLabel.ScaleTo(1.16, 145, Easing.SpringOut),
            m_counterLabel.TranslateTo(0, -reboundDistance, 145, Easing.SpringOut));

        await Task.WhenAll(
            m_counterLabel.ScaleTo(1, 220, Easing.SpringOut),
            m_counterLabel.TranslateTo(0, 0, 220, Easing.SpringOut));
    }

    public async Task AnimateCompletedAsync()
    {
        if (IsReducedMotionEnabled)
            return;

        await Task.WhenAll(
            m_counterLabel.ScaleTo(1.28, 150, Easing.CubicOut),
            m_counterLabel.FadeTo(.82, 150, Easing.CubicOut));
        await Task.WhenAll(
            m_counterLabel.ScaleTo(1, 320, Easing.SpringOut),
            m_counterLabel.FadeTo(1, 220, Easing.CubicOut));
    }
}