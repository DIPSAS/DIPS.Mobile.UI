using System.Timers;
using DIPS.Mobile.UI.Internal;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using Timer = System.Timers.Timer;

namespace DIPS.Mobile.UI.Components.Alerting.SystemMessage;

internal class SystemMessage : Grid, IDisposable
{
    private const double DismissVelocityThreshold = .12;
    private const double UpwardDismissHeightThreshold = .3;
    private const double VerticalIntentRatio = .85;
    private const double DragFollowMultiplier = 1.08;
    private const double OpenStartScale = .62;
    private const double OpenOvershootScale = 1.025;
    private const uint OpenScaleInAnimationLength = 280;
    private const uint OpenSettleAnimationLength = 220;
    private const uint CloseLiftAnimationLength = 140;
    private const uint CloseAwayAnimationLength = 230;
    private const uint SnapBackAnimationLength = 240;
    private const uint FlingAwayAnimationLength = 220;

    private readonly SystemMessageConfigurator m_configurator;
    private readonly Action<bool> m_onFinished;
    private readonly Timer m_timer;
    private readonly Grid m_contentGrid;
    private readonly Border m_backgroundBorder;
    private readonly PanGestureRecognizer m_panGestureRecognizer = new();
    private DateTimeOffset m_panStartedAt;
    private DateTimeOffset m_lastPanUpdateTime;
    private double m_panStartedY;
    private double m_lastPanY;
    private double m_panVelocityY;
    private bool m_hasDetectedPanDirection;
    private bool m_isVerticalPan;
    private bool m_isDismissing;
    private bool m_isDisposed;

    public SystemMessage(SystemMessageConfigurator configurator, Action<bool> onFinished)
    {
        m_configurator = configurator;
        m_onFinished = onFinished;

        m_timer = new Timer(configurator.Duration) { AutoReset = false };
        var hasIcon = configurator.Icon is not null;
        
        var label = new Label
        {
            AutomationId = "Label".ToDUIAutomationId<SystemMessage>(),
            Text = configurator.Text, 
            HorizontalTextAlignment = TextAlignment.Start, 
            VerticalTextAlignment = TextAlignment.Center,
            VerticalOptions = LayoutOptions.Center,
            InputTransparent = true,
            TextColor = configurator.TextColor
        };
        AutomationProperties.SetExcludedWithChildren(label, true);

        m_contentGrid = new Grid
        {
            AutomationId = "ContentGrid".ToDUIAutomationId<SystemMessage>(),
            ColumnSpacing = hasIcon ? Sizes.GetSize(SizeName.content_margin_xsmall) : 0,
            Padding = Sizes.GetSize(SizeName.content_margin_medium),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Start,
            InputTransparent = false,
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent,
            ColumnDefinitions = hasIcon
                ? [new ColumnDefinition(GridLength.Auto), new ColumnDefinition(GridLength.Star)]
                : [new ColumnDefinition(GridLength.Auto)]
        };

        AutomationProperties.SetExcludedWithChildren(m_contentGrid, true);
        m_contentGrid.GestureRecognizers.Add(m_panGestureRecognizer);
        
        
        // We have to fake that border contains the grid, because border does not resize its elements
        // correctly when animating its scale
        m_backgroundBorder = new Border
        {
            AutomationId = "FakeBorder".ToDUIAutomationId<SystemMessage>(),
            BackgroundColor = configurator.BackgroundColor,
            Margin = m_contentGrid.Padding.Top * -1,
            Stroke = configurator.Stroke,
            StrokeThickness = Sizes.GetSize(SizeName.stroke_medium),
            StrokeShape = new RoundRectangle { CornerRadius = Sizes.GetSize(SizeName.radius_small) },
            Shadow = new Shadow
            {
                Brush = new SolidColorBrush(Colors.GetColor(ColorName.color_surface_backdrop, .65f)),
                Offset = new Point(0, Sizes.GetSize(SizeName.size_1)),
                Radius = (float)Sizes.GetSize(SizeName.size_2),
                Opacity = .75f
            }
        };

        m_panGestureRecognizer.PanUpdated += OnMessagePanned;

        m_contentGrid.Add(m_backgroundBorder);
        Grid.SetColumnSpan(m_backgroundBorder, hasIcon ? 2 : 1);
        
        if(hasIcon)
        {
            var iconImage = new Image
            {
                AutomationId = "IconImage".ToDUIAutomationId<SystemMessage>(),
                Source = configurator.Icon,
                VerticalOptions = LayoutOptions.Center,
                InputTransparent = true,
                TintColor = configurator.IconColor
            };
            AutomationProperties.SetExcludedWithChildren(iconImage, true);
            m_contentGrid.Add(iconImage);
            
        }
        
        m_contentGrid.Add(label, hasIcon ? 1 : 0);
        
        Add(m_contentGrid);

        InputTransparent = true;
        CascadeInputTransparent = false;

        m_contentGrid.Opacity = 0;
        m_contentGrid.Scale = OpenStartScale;

        Padding = new Thickness(Sizes.GetSize(SizeName.size_10), GetTopPadding());
    }
    
    public void Show()
    {
        SemanticScreenReader.Default.Announce($"{DUILocalizedStrings.SystemMessage}: {m_configurator.Text}");

        m_timer.Enabled = true;
        m_timer.Elapsed += OnTimerEnded;

        _ = AnimateIn();
    }

    private void OnTimerEnded(object? sender, ElapsedEventArgs e)
    {
        m_timer.Stop();
        MainThread.BeginInvokeOnMainThread(() => m_onFinished.Invoke(true));
    }

    private async Task AnimateIn()
    {
        var fade = m_contentGrid.FadeToAsync(1, OpenScaleInAnimationLength, Easing.CubicOut);
        var scale = m_contentGrid.ScaleToAsync(OpenOvershootScale, OpenScaleInAnimationLength, Easing.CubicOut);
        await Task.WhenAll(fade, scale);
        await m_contentGrid.ScaleToAsync(1, OpenSettleAnimationLength, Easing.CubicOut);
    }

    private static double GetTopPadding()
    {
        return DeviceInfo.Platform == DevicePlatform.Android
            ? Sizes.GetSize(SizeName.size_10)
            : Sizes.GetSize(SizeName.content_margin_large);
    }

    private void OnMessagePanned(object? sender, PanUpdatedEventArgs e)
    {
        if (m_isDismissing)
            return;

        switch (e.StatusType)
        {
            case GestureStatus.Started:
                this.AbortAnimation(nameof(SnapBack));
                this.AbortAnimation(nameof(FlingAway));
                m_panStartedAt = DateTimeOffset.UtcNow;
                m_lastPanUpdateTime = m_panStartedAt;
                m_panStartedY = e.TotalY;
                m_lastPanY = e.TotalY;
                m_panVelocityY = 0;
                m_hasDetectedPanDirection = false;
                m_isVerticalPan = false;
                break;
            case GestureStatus.Running:
                if (!TryHandleVerticalPan(e.TotalX, e.TotalY))
                    return;

                UpdatePanVelocity(e);
                ApplyDrag(e.TotalY);
                break;
            case GestureStatus.Completed:
                if (!m_hasDetectedPanDirection && !TryHandleVerticalPan(e.TotalX, e.TotalY))
                {
                    return;
                }

                if (!m_isVerticalPan)
                {
                    return;
                }

                UpdateCompletedPanVelocity(e.TotalY);
                _ = ShouldDismiss(e.TotalY) ? FlingAway(e.TotalY) : SnapBack();
                break;
            case GestureStatus.Canceled:
                if (!m_isVerticalPan)
                {
                    return;
                }

                _ = SnapBack();
                break;
        }
    }

    private void UpdatePanVelocity(PanUpdatedEventArgs e)
    {
        var now = DateTimeOffset.UtcNow;
        var elapsedMilliseconds = Math.Max((now - m_lastPanUpdateTime).TotalMilliseconds, 1);

        m_panVelocityY = (e.TotalY - m_lastPanY) / elapsedMilliseconds;
        m_lastPanY = e.TotalY;
        m_lastPanUpdateTime = now;
    }

    private void UpdateCompletedPanVelocity(double translationY)
    {
        var elapsedMilliseconds = Math.Max((DateTimeOffset.UtcNow - m_panStartedAt).TotalMilliseconds, 1);
        var averageVelocityY = (translationY - m_panStartedY) / elapsedMilliseconds;

        if (Math.Abs(averageVelocityY) > Math.Abs(m_panVelocityY))
            m_panVelocityY = averageVelocityY;
    }

    private bool TryHandleVerticalPan(double translationX, double translationY)
    {
        if (m_hasDetectedPanDirection)
            return m_isVerticalPan;

        var absoluteX = Math.Abs(translationX);
        var absoluteY = Math.Abs(translationY);
        if (Math.Max(absoluteX, absoluteY) < Sizes.GetSize(SizeName.size_2))
            return false;

        m_hasDetectedPanDirection = true;
        m_isVerticalPan = absoluteY >= absoluteX * VerticalIntentRatio;
        if (!m_isVerticalPan)
            return false;

        m_timer.Stop();
        return true;
    }

    private static double GetDragOpacity(double translationY)
    {
        var distance = Math.Abs(translationY);
        return Math.Max(.55, 1 - distance / (Sizes.GetSize(SizeName.size_25) * 2));
    }

    private void ApplyDrag(double translationY)
    {
        var visualTranslationY = translationY * DragFollowMultiplier;

        m_contentGrid.TranslationX = 0;
        m_contentGrid.TranslationY = visualTranslationY;
        m_contentGrid.Opacity = GetDragOpacity(translationY);
        m_contentGrid.Scale = Math.Max(.97, 1 - Math.Abs(translationY) / (Sizes.GetSize(SizeName.size_25) * 18));
        m_contentGrid.Rotation = 0;
    }

    private bool ShouldDismiss(double translationY)
    {
        var distance = Math.Abs(translationY);
        var velocity = Math.Abs(m_panVelocityY);
        var projectedY = translationY + m_panVelocityY * FlingAwayAnimationLength;
        var projectedDistance = Math.Abs(projectedY);
        var upwardDismissDistance = Math.Max(Sizes.GetSize(SizeName.size_5), m_contentGrid.Height * UpwardDismissHeightThreshold);

        return (translationY < 0 && distance >= upwardDismissDistance) ||
               distance >= Sizes.GetSize(SizeName.size_10) ||
               projectedDistance >= Sizes.GetSize(SizeName.size_14) ||
               velocity >= DismissVelocityThreshold;
    }

    private async Task SnapBack()
    {
        await Task.WhenAll(
            m_contentGrid.TranslateToAsync(0, 0, SnapBackAnimationLength, Easing.CubicOut),
            m_contentGrid.FadeToAsync(1, SnapBackAnimationLength, Easing.CubicOut),
            m_contentGrid.ScaleToAsync(1, SnapBackAnimationLength, Easing.SpringOut),
            m_contentGrid.RotateToAsync(0, SnapBackAnimationLength, Easing.SpringOut));

        if (!m_isDisposed)
            m_timer.Start();
    }

    private async Task FlingAway(double translationY)
    {
        m_isDismissing = true;
        m_timer.Stop();

        var targetY = GetFlingTargetY(translationY);

        await Task.WhenAll(
            m_contentGrid.TranslateToAsync(0, targetY, FlingAwayAnimationLength, Easing.CubicOut),
            m_contentGrid.FadeToAsync(0, FlingAwayAnimationLength, Easing.CubicOut),
            m_contentGrid.ScaleToAsync(.96, FlingAwayAnimationLength, Easing.CubicOut),
            m_contentGrid.RotateToAsync(0, FlingAwayAnimationLength, Easing.CubicOut));

        m_onFinished.Invoke(false);
    }

    private double GetFlingTargetY(double translationY)
    {
        var directionY = translationY + m_panVelocityY * FlingAwayAnimationLength;

        if (Math.Abs(directionY) < .01)
            directionY = -1;
        
        var targetDistance = Math.Max(Width, Height);
        if (targetDistance <= 0)
            targetDistance = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

        targetDistance += Sizes.GetSize(SizeName.size_25);

        return Math.Sign(directionY) * targetDistance;
    }

    public async Task Hide()
    {
        m_isDismissing = true;
        m_timer.Stop();

        await m_contentGrid.ScaleToAsync(OpenOvershootScale, CloseLiftAnimationLength, Easing.CubicOut);

        var fade = m_contentGrid.FadeToAsync(0, CloseAwayAnimationLength, Easing.CubicIn);
        var scale = m_contentGrid.ScaleToAsync(OpenStartScale, CloseAwayAnimationLength, Easing.CubicIn);
        await Task.WhenAll(fade, scale);
    }

    public void Dispose()
    {
        m_isDisposed = true;
        m_timer.Stop();
        m_timer.Elapsed -= OnTimerEnded;
        m_timer.Dispose();
        m_panGestureRecognizer.PanUpdated -= OnMessagePanned;
        m_contentGrid.GestureRecognizers.Remove(m_panGestureRecognizer);
    }
}