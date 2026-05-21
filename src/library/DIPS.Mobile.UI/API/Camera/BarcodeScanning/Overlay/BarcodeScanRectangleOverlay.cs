using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.Icons;
using DIPS.Mobile.UI.Resources.Sizes;
using Microsoft.Maui.Controls.Shapes;
using DuiColors = DIPS.Mobile.UI.Resources.Colors.Colors;
using DuiImage = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

/// <summary>
/// A full-screen overlay that draws a semi-transparent dimmed area with a clear rounded-corner cutout
/// representing the barcode scan region, decorated with animated corner brackets.
/// Optionally displays a tooltip view above the scan rectangle.
/// </summary>
internal class BarcodeScanRectangleOverlay : Grid
{
    private readonly DimOverlayDrawable m_dimDrawable;
    private readonly CornerBracketsDrawable m_cornersDrawable;
    private readonly GraphicsView m_dimGraphicsView;
    private readonly GraphicsView m_cornersGraphicsView;
    private readonly Border m_collectionToken;
    private View? m_tooltipView;
    private readonly float m_widthFraction;
    private readonly float m_heightFraction;
    private readonly uint m_bracketsTravelLength;
    private readonly uint m_formingLength;
    private RectF? m_trackingTargetRect;
    private long m_lastTrackingTargetUpdateMilliseconds;
    private bool m_isAnimatingToBarcode;
    
    private const float HighlightPadding = 6f;
    private const long TrackingTargetUpdateThrottleMilliseconds = 32;
    private const float TrackingTargetSmallChangeThreshold = 1.5f;
    private const float TrackingTargetLargeChangeThreshold = 48f;
    private const float TrackingTargetSmoothing = .35f;
    private const float TrackingTargetFastSmoothing = .65f;

    private const string AnimationKeyCornerBreathing = "CornerBreathing";
    private const string AnimationKeyBracketsToBarcode = "BracketsToBarcode";
    private const string AnimationKeyBracketsReturn = "BracketsReturn";
    private const string AnimationKeyBracketsForming = "BracketsForming";
    private const string AnimationKeyBracketsSuccess = "BracketsSuccess";
    private const string AnimationKeyBracketsFailure = "BracketsFailure";
    private const string AnimationKeyCollectionToken = "CollectionToken";

    public BarcodeScanRectangleOverlay(float widthFraction, float heightFraction, TimeSpan bracketsTravelDuration, TimeSpan formingDuration)
    {
        m_widthFraction = widthFraction;
        m_heightFraction = heightFraction;
        m_bracketsTravelLength = (uint)bracketsTravelDuration.TotalMilliseconds;
        m_formingLength = (uint)formingDuration.TotalMilliseconds;

        m_dimDrawable = new DimOverlayDrawable(widthFraction, heightFraction);
        m_cornersDrawable = new CornerBracketsDrawable(widthFraction, heightFraction);

        m_dimGraphicsView = new GraphicsView
        {
            Drawable = m_dimDrawable,
            InputTransparent = true,
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent
        };

        m_cornersGraphicsView = new GraphicsView
        {
            Drawable = m_cornersDrawable,
            InputTransparent = true,
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent
        };

        m_collectionToken = new Border
        {
            InputTransparent = true,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Start,
            WidthRequest = Sizes.GetSize(SizeName.size_14),
            HeightRequest = Sizes.GetSize(SizeName.size_14),
            BackgroundColor = DuiColors.GetColor(ColorName.color_fill_success),
            Stroke = DuiColors.GetColor(ColorName.color_border_success),
            StrokeThickness = Sizes.GetSize(SizeName.stroke_small),
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(Sizes.GetSize(SizeName.radius_large))
            },
            Content = new DuiImage
            {
                Source = Icons.GetIcon(IconName.barcode_fill),
                TintColor = DuiColors.GetColor(ColorName.color_icon_on_fill_success),
                WidthRequest = Sizes.GetSize(SizeName.size_7),
                HeightRequest = Sizes.GetSize(SizeName.size_7),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            },
            Shadow = new Shadow
            {
                Brush = new SolidColorBrush(DuiColors.GetColor(ColorName.color_surface_backdrop, .65f)),
                Offset = new Point(0, Sizes.GetSize(SizeName.size_1)),
                Radius = (float)Sizes.GetSize(SizeName.size_2),
                Opacity = .75f
            },
            IsVisible = false,
            Opacity = 0,
            Scale = .65
        };

        InputTransparent = true;
        BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent;

        Children.Add(m_dimGraphicsView);
        Children.Add(m_cornersGraphicsView);
        Children.Add(m_collectionToken);

        SizeChanged += OnSizeChanged;

        StartBreathingAnimation();
    }

    private void StartBreathingAnimation()
    {
        var animation = new Animation
        {
            { 0.0, 0.5, new Animation(v =>
            {
                m_cornersDrawable.Inset = (float)v;
                m_cornersGraphicsView.Invalidate();
            }, 0, -4, Easing.SinInOut) },
            { 0.5, 1.0, new Animation(v =>
            {
                m_cornersDrawable.Inset = (float)v;
                m_cornersGraphicsView.Invalidate();
            }, -4, 0, Easing.SinInOut) }
        };

        animation.Commit(m_cornersGraphicsView, AnimationKeyCornerBreathing,
            rate: 32,
            length: 2400,
            repeat: () => true);
    }

    private void StopBreathingAnimation()
    {
        m_cornersGraphicsView.AbortAnimation(AnimationKeyCornerBreathing);
    }

    internal void SetTooltipView(View tooltipView)
    {
        m_tooltipView = tooltipView;
        m_tooltipView.InputTransparent = true;
        m_tooltipView.HorizontalOptions = LayoutOptions.Center;
        m_tooltipView.VerticalOptions = LayoutOptions.Start;

        Children.Add(m_tooltipView);

        m_tooltipView.SizeChanged += OnTooltipSizeChanged;
    }

    private void OnTooltipSizeChanged(object? sender, EventArgs e)
    {
        PositionTooltip();
    }

    private void OnSizeChanged(object? sender, EventArgs e)
    {
        PositionTooltip();
    }

    private void PositionTooltip()
    {
        if (m_tooltipView is null || Height <= 0 || Width <= 0 || m_tooltipView.Height <= 0)
            return;

        var rectHeight = Height * m_heightFraction;
        var rectY = GetCameraFeedCenterY((float)Width, (float)Height) - rectHeight / 2f;
        var spacing = Sizes.GetSize(SizeName.content_margin_small);

        m_tooltipView.TranslationY = rectY - m_tooltipView.Height - spacing;
    }

    internal void Cleanup()
    {
        StopAllAnimations();
        SizeChanged -= OnSizeChanged;

        if (m_tooltipView is not null)
        {
            m_tooltipView.SizeChanged -= OnTooltipSizeChanged;
            m_tooltipView = null;
        }
    }

    private void StopAllAnimations()
    {
        m_cornersGraphicsView.AbortAnimation(AnimationKeyCornerBreathing);
        m_cornersGraphicsView.AbortAnimation(AnimationKeyBracketsToBarcode);
        m_cornersGraphicsView.AbortAnimation(AnimationKeyBracketsReturn);
        m_cornersGraphicsView.AbortAnimation(AnimationKeyBracketsForming);
        m_cornersGraphicsView.AbortAnimation(AnimationKeyBracketsSuccess);
        m_cornersGraphicsView.AbortAnimation(AnimationKeyBracketsFailure);
        m_collectionToken.AbortAnimation(AnimationKeyCollectionToken);
    }

    internal void SetBarcodeDetected()
    {
        StopBreathingAnimation();
        m_cornersDrawable.Inset = 0;
        m_cornersDrawable.BracketColor = CornerBracketsDrawable.DetectedBracketColor;
        m_cornersDrawable.MaxStrokeWidth = CornerBracketsDrawable.DetectedStrokeWidth;
        m_cornersGraphicsView.Invalidate();
    }

    /// <summary>
    /// Aborts the in-progress bracket-to-barcode animation so the brackets can snap
    /// to an updated position without being overwritten by the animation callback.
    /// </summary>
    internal void AbortBracketAnimation()
    {
        m_cornersGraphicsView.AbortAnimation(AnimationKeyBracketsToBarcode);
    }

    /// <summary>
    /// Animates the corner brackets from their current position to shrink around the detected barcode.
    /// Calls <paramref name="onArrived"/> when the animation completes (brackets have formed around the barcode).
    /// </summary>
    internal void AnimateBracketsToBarcode(RectF barcodeRect, Action? onArrived = null)
    {
        // Start from current override position (mid-animation) or scan rect (idle).
        // Read before aborting BracketsReturn so we capture the brackets' current visual position.
        var startRect = m_cornersDrawable.OverrideRect ?? GetScanRectangleForDrawable();

        var startX = startRect.X;
        var startY = startRect.Y;
        var startW = startRect.Width;
        var startH = startRect.Height;

        m_trackingTargetRect = GetHighlightedBarcodeRect(barcodeRect);
        m_lastTrackingTargetUpdateMilliseconds = Environment.TickCount64;
        m_isAnimatingToBarcode = true;

        // Also abort BracketsReturn — if brackets are mid-return and a new barcode is detected,
        // we must cancel the return so its finished callback does not call StartBreathingAnimation.
        m_cornersGraphicsView.AbortAnimation(AnimationKeyBracketsReturn);
        m_cornersGraphicsView.AbortAnimation(AnimationKeyBracketsToBarcode);
        
        var animation = new Animation(v =>
        {
            var t = (float)v;
            var targetRect = m_trackingTargetRect ?? GetHighlightedBarcodeRect(barcodeRect);
            m_cornersDrawable.OverrideRect = new RectF(
                startX + (targetRect.X - startX) * t,
                startY + (targetRect.Y - startY) * t,
                startW + (targetRect.Width - startW) * t,
                startH + (targetRect.Height - startH) * t);
            m_cornersGraphicsView.Invalidate();
        }, 0, 1, Easing.CubicOut);

        animation.Commit(m_cornersGraphicsView, AnimationKeyBracketsToBarcode,
            rate: 16,
            length: m_bracketsTravelLength,
            finished: (_, cancelled) =>
            {
                m_isAnimatingToBarcode = false;
                if (!cancelled)
                    onArrived?.Invoke();
            });
    }

    /// <summary>
    /// Returns true if the barcode rect is small enough that the bracket arms
    /// already cover the full sides (no forming animation needed).
    /// </summary>
    internal bool IsAlreadyFormedAtCurrentSize()
    {
        if (m_cornersDrawable.OverrideRect is not { } rect)
            return false;
        return rect.Width <= BracketArmLength * 2 && rect.Height <= BracketArmLength * 2;
    }

    private const float BracketArmLength = 30f;

    /// <summary>
    /// After brackets have arrived at the barcode, animates the corner arms extending
    /// until they meet and form a full rectangle border (~2 seconds).
    /// Calls <paramref name="onFormed"/> when the rectangle is fully formed.
    /// </summary>
    internal void StartFormingAnimation(Action? onFormed = null)
    {
        m_cornersGraphicsView.AbortAnimation(AnimationKeyBracketsForming);
        m_cornersDrawable.FormingProgress = 0;

        var animation = new Animation(v =>
        {
            m_cornersDrawable.FormingProgress = (float)v;
            m_cornersGraphicsView.Invalidate();
        }, 0, 1, Easing.CubicInOut);

        animation.Commit(m_cornersGraphicsView, AnimationKeyBracketsForming,
            rate: 16,
            length: m_formingLength,
            finished: (_, cancelled) =>
            {
                if (!cancelled)
                    onFormed?.Invoke();
            });
    }

    /// <summary>
    /// Updates the bracket target position while already tracking a barcode.
    /// Directly sets the override rect without restarting the animation, so the brackets
    /// smoothly follow the barcode as the camera moves.
    /// </summary>
    internal void UpdateBracketTarget(RectF barcodeRect)
    {
        var targetRect = GetHighlightedBarcodeRect(barcodeRect);
        var currentTargetRect = m_trackingTargetRect;
        if (currentTargetRect is not null)
        {
            var targetDelta = GetMaximumRectDelta(currentTargetRect.Value, targetRect);
            if (targetDelta < TrackingTargetSmallChangeThreshold)
                return;

            var now = Environment.TickCount64;
            if (now - m_lastTrackingTargetUpdateMilliseconds < TrackingTargetUpdateThrottleMilliseconds &&
                targetDelta < TrackingTargetLargeChangeThreshold)
            {
                return;
            }

            m_lastTrackingTargetUpdateMilliseconds = now;
            targetRect = Lerp(currentTargetRect.Value, targetRect,
                targetDelta > TrackingTargetLargeChangeThreshold ? TrackingTargetFastSmoothing : TrackingTargetSmoothing);
        }
        else
        {
            m_lastTrackingTargetUpdateMilliseconds = Environment.TickCount64;
        }

        m_trackingTargetRect = targetRect;

        if (m_isAnimatingToBarcode)
            return;

        var currentRect = m_cornersDrawable.OverrideRect ?? targetRect;
        m_cornersDrawable.OverrideRect = Lerp(currentRect, targetRect, TrackingTargetFastSmoothing);
        m_cornersGraphicsView.Invalidate();
    }

    private static RectF GetHighlightedBarcodeRect(RectF barcodeRect) => new(
        barcodeRect.X - HighlightPadding,
        barcodeRect.Y - HighlightPadding,
        barcodeRect.Width + HighlightPadding * 2,
        barcodeRect.Height + HighlightPadding * 2);

    private static RectF Lerp(RectF startRect, RectF endRect, float progress) => new(
        startRect.X + (endRect.X - startRect.X) * progress,
        startRect.Y + (endRect.Y - startRect.Y) * progress,
        startRect.Width + (endRect.Width - startRect.Width) * progress,
        startRect.Height + (endRect.Height - startRect.Height) * progress);

    private static float GetMaximumRectDelta(RectF startRect, RectF endRect) => Math.Max(
        Math.Max(Math.Abs(startRect.X - endRect.X), Math.Abs(startRect.Y - endRect.Y)),
        Math.Max(Math.Abs(startRect.Width - endRect.Width), Math.Abs(startRect.Height - endRect.Height)));

    /// <summary>
    /// Plays a polished success animation: brackets pulse outward with a spring-like
    /// overshoot, color transitions to green, holds briefly, then resets to idle.
    /// All animation happens inside the drawable for crisp rendering.
    /// </summary>
    internal Task PlaySuccessAndResetAsync()
    {
        m_cornersGraphicsView.AbortAnimation(AnimationKeyBracketsForming);
        
        m_cornersDrawable.BracketColor = CornerBracketsDrawable.SuccessBracketColor;
        m_cornersDrawable.MaxStrokeWidth = CornerBracketsDrawable.SuccessStrokeWidth;
        m_cornersGraphicsView.Invalidate();

        var taskCompletionSource = new TaskCompletionSource<bool>();

        var animation = new Animation
        {
            // Phase 1 (0–30%): Spring-like outward pulse — brackets expand beyond their rect
            { 0.0, 0.3, new Animation(v =>
            {
                m_cornersDrawable.Inset = (float)v;
                m_cornersGraphicsView.Invalidate();
            }, 0, -6, Easing.CubicOut) },
            
            // Phase 2 (30–50%): Spring settle — overshoot back inward slightly past zero
            { 0.3, 0.5, new Animation(v =>
            {
                m_cornersDrawable.Inset = (float)v;
                m_cornersGraphicsView.Invalidate();
            }, -6, 1, Easing.CubicInOut) },
            
            // Phase 3 (50–65%): Settle to rest
            { 0.5, 0.65, new Animation(v =>
            {
                m_cornersDrawable.Inset = (float)v;
                m_cornersGraphicsView.Invalidate();
            }, 1, 0, Easing.CubicOut) },
            
            // Phase 4 (65–100%): Hold at green — let the user see the confirmation
            { 0.65, 1.0, new Animation(_ => { }, 0, 1) }
        };

        animation.Commit(m_cornersGraphicsView, AnimationKeyBracketsSuccess,
            rate: 16,
            length: 700,
            finished: (_, cancelled) =>
            {
                m_cornersDrawable.Inset = 0;
                if (!cancelled)
                    ResetBarcodeDetection();
                taskCompletionSource.TrySetResult(true);
            });

        return taskCompletionSource.Task;
    }

    internal Task PlayCollectionTokenAsync(Point? targetCenter = null)
    {
        if (Height <= 0 || Width <= 0)
            return Task.CompletedTask;

        var tokenSourceRect = m_cornersDrawable.OverrideRect ?? m_trackingTargetRect ?? GetScanRectangleForDrawable();
        var startX = tokenSourceRect.X + tokenSourceRect.Width / 2 - Width / 2;
        var startY = tokenSourceRect.Y + tokenSourceRect.Height / 2 - m_collectionToken.HeightRequest / 2;
        var endX = 0d;
        var endY = Math.Max(startY, Height - Sizes.GetSize(SizeName.size_18));
        if (targetCenter is { } landingPoint)
        {
            endX = landingPoint.X - Width / 2;
            endY = landingPoint.Y - m_collectionToken.HeightRequest / 2;
        }
        var arcDirection = startX >= 0 ? -1 : 1;
        var arcHeight = Sizes.GetSize(SizeName.size_8);
        var arcWidth = Sizes.GetSize(SizeName.size_5) * arcDirection;

        m_collectionToken.AbortAnimation(AnimationKeyCollectionToken);
        m_collectionToken.IsVisible = true;
        m_collectionToken.Opacity = 0;
        m_collectionToken.Scale = .65;
        m_collectionToken.Rotation = 0;
        m_collectionToken.TranslationX = startX;
        m_collectionToken.TranslationY = startY;

        var taskCompletionSource = new TaskCompletionSource<bool>();
        var animation = new Animation(progress =>
        {
            var easedProgress = Easing.CubicInOut.Ease(progress);
            var arcProgress = Math.Sin(progress * Math.PI);
            var popProgress = Math.Sin(Math.Min(progress, .36) / .36 * Math.PI);

            m_collectionToken.TranslationX = startX + (endX - startX) * easedProgress + arcWidth * arcProgress;
            m_collectionToken.TranslationY = startY + (endY - startY) * easedProgress - arcHeight * arcProgress;
            m_collectionToken.Scale = .65 + .46 * popProgress - .28 * easedProgress;
            m_collectionToken.Opacity = progress < .18 ? progress / .18 : Math.Max(.18, 1 - Math.Max(0, progress - .68) / .32);
            m_collectionToken.Rotation = -10 * arcDirection * arcProgress;
        }, 0, 1, Easing.Linear);

        animation.Commit(m_collectionToken, AnimationKeyCollectionToken,
            rate: 16,
            length: 680,
            finished: (_, _) =>
            {
                m_collectionToken.IsVisible = false;
                m_collectionToken.Opacity = 0;
                m_collectionToken.Scale = .65;
                m_collectionToken.Rotation = 0;
                m_collectionToken.TranslationX = 0;
                m_collectionToken.TranslationY = startY;
                taskCompletionSource.TrySetResult(true);
            });

        return taskCompletionSource.Task;
    }

    internal async Task PlayFailureAndResetAsync(string? errorMessage)
    {
        m_cornersGraphicsView.AbortAnimation(AnimationKeyBracketsForming);
        m_cornersDrawable.BracketColor = CornerBracketsDrawable.FailureBracketColor;
        m_cornersDrawable.MaxStrokeWidth = CornerBracketsDrawable.FailureStrokeWidth;
        m_cornersGraphicsView.Invalidate();

        var shakeDistance = Sizes.GetSize(SizeName.size_2);
        await m_cornersGraphicsView.TranslateTo(-shakeDistance, 0, 55, Easing.CubicOut);
        await m_cornersGraphicsView.TranslateTo(shakeDistance, 0, 80, Easing.CubicInOut);
        await m_cornersGraphicsView.TranslateTo(-shakeDistance / 2, 0, 65, Easing.CubicInOut);
        await m_cornersGraphicsView.TranslateTo(0, 0, 80, Easing.CubicOut);

        ResetBarcodeDetection();
    }

    internal void ResetBarcodeDetection()
    {
        m_cornersGraphicsView.AbortAnimation(AnimationKeyBracketsToBarcode);
        m_cornersGraphicsView.AbortAnimation(AnimationKeyBracketsReturn);
        m_cornersGraphicsView.AbortAnimation(AnimationKeyBracketsForming);
        m_isAnimatingToBarcode = false;
        m_trackingTargetRect = null;
        m_lastTrackingTargetUpdateMilliseconds = 0;
        m_cornersGraphicsView.TranslationX = 0;
        m_cornersDrawable.FormingProgress = 0;
        m_cornersDrawable.BracketColor = CornerBracketsDrawable.DefaultBracketColor;
        m_cornersDrawable.MaxStrokeWidth = CornerBracketsDrawable.DefaultStrokeWidth;

        var currentRect = m_cornersDrawable.OverrideRect;
        var scanRect = GetScanRectangleForDrawable();

        if (currentRect is not null)
        {
            // Animate back to scan rectangle
            var startX = currentRect.Value.X;
            var startY = currentRect.Value.Y;
            var startW = currentRect.Value.Width;
            var startH = currentRect.Value.Height;

            var animation = new Animation(v =>
            {
                var t = (float)v;
                m_cornersDrawable.OverrideRect = new RectF(
                    startX + (scanRect.X - startX) * t,
                    startY + (scanRect.Y - startY) * t,
                    startW + (scanRect.Width - startW) * t,
                    startH + (scanRect.Height - startH) * t);
                m_cornersGraphicsView.Invalidate();
            }, 0, 1, Easing.CubicInOut);

            animation.Commit(m_cornersGraphicsView, AnimationKeyBracketsReturn,
                rate: 16,
                length: 280,
                finished: (_, cancelled) =>
                {
                    m_cornersDrawable.OverrideRect = null;
                    if (!cancelled)
                        StartBreathingAnimation();
                });
        }
        else
        {
            m_cornersDrawable.OverrideRect = null;
            StartBreathingAnimation();
        }

    }
    private RectF GetScanRectangleForDrawable()
    {
        var w = (float)Width;
        var h = (float)Height;
        var rectWidth = w * m_widthFraction;
        var rectHeight = h * m_heightFraction;
        var rectX = (w - rectWidth) / 2f;
        var centerY = GetCameraFeedCenterY(w, h);
        var rectY = centerY - rectHeight / 2f;
        return new RectF(rectX, rectY, rectWidth, rectHeight);
    }

    /// <summary>
    /// Converts normalized (0-1) barcode bounds relative to the camera feed into overlay coordinates,
    /// accounting for the camera feed position within the overlay.
    /// </summary>
    internal RectF NormalizedBoundsToOverlay(float normX, float normY, float normWidth, float normHeight)
    {
        var width = (float)Width;
        var height = (float)Height;
        if (width <= 0 || height <= 0)
            return RectF.Zero;

        var actualPreviewHeight = Math.Min(width / CameraPreview.ThreeFourRatio, height);
        var centerY = GetCameraFeedCenterY(width, height);
        var cameraFeedTop = centerY - actualPreviewHeight / 2f;

        return new RectF(
            normX * width,
            cameraFeedTop + normY * actualPreviewHeight,
            normWidth * width,
            normHeight * actualPreviewHeight);
    }

    /// <summary>
    /// Returns the scan rectangle bounds in view-relative coordinates, useful for filtering barcode results.
    /// Only valid after the overlay has been laid out.
    /// </summary>
    internal RectF GetScanRectangle()
    {
        return m_dimDrawable.LastScanRect;
    }

    /// <summary>
    /// Computes the Y center of the camera feed in overlay coordinates.
    /// The overlay is translated up by the top toolbar height, so naively centering
    /// in dirtyRect lands off-center relative to the actual camera feed.
    /// </summary>
    internal static float GetCameraFeedCenterY(float width, float height)
    {
        var actualPreviewHeight = Math.Min(width / CameraPreview.ThreeFourRatio, height);
        var topToolbarHeight = CameraPreview.ComputeTopToolbarHeight(width, height);
        return topToolbarHeight + actualPreviewHeight / 2f;
    }

    /// <summary>
    /// Draws the semi-transparent dim overlay with a clear rounded-corner cutout.
    /// </summary>
    private class DimOverlayDrawable : IDrawable
    {
        private readonly float m_widthFraction;
        private readonly float m_heightFraction;

        internal RectF LastScanRect { get; private set; }

        public DimOverlayDrawable(float widthFraction, float heightFraction)
        {
            m_widthFraction = widthFraction;
            m_heightFraction = heightFraction;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            var rectWidth = dirtyRect.Width * m_widthFraction;
            var rectHeight = dirtyRect.Height * m_heightFraction;
            var rectX = (dirtyRect.Width - rectWidth) / 2f;
            var centerY = GetCameraFeedCenterY(dirtyRect.Width, dirtyRect.Height);
            var rectY = centerY - rectHeight / 2f;

            LastScanRect = new RectF(rectX, rectY, rectWidth, rectHeight);

            // Only dim the camera feed area, leaving top and bottom toolbars unaffected
            var actualPreviewHeight = Math.Min(dirtyRect.Width / CameraPreview.ThreeFourRatio, dirtyRect.Height);
            var cameraFeedTop = centerY - actualPreviewHeight / 2f;
            var cameraFeedBottom = cameraFeedTop + actualPreviewHeight;
            var scanTop = Math.Max(rectY, cameraFeedTop);
            var scanBottom = Math.Min(rectY + rectHeight, cameraFeedBottom);
            var scanHeight = Math.Max(scanBottom - scanTop, 0);
            var scanRight = rectX + rectWidth;

            canvas.FillColor = Microsoft.Maui.Graphics.Color.FromRgba(0, 0, 0, 0.5f);
            FillRectangleIfVisible(canvas, 0, cameraFeedTop, dirtyRect.Width, scanTop - cameraFeedTop);
            FillRectangleIfVisible(canvas, 0, scanBottom, dirtyRect.Width, cameraFeedBottom - scanBottom);
            FillRectangleIfVisible(canvas, 0, scanTop, rectX, scanHeight);
            FillRectangleIfVisible(canvas, scanRight, scanTop, dirtyRect.Width - scanRight, scanHeight);
        }

        private static void FillRectangleIfVisible(ICanvas canvas, float x, float y, float width, float height)
        {
            if (width <= 0 || height <= 0)
                return;

            canvas.FillRectangle(x, y, width, height);
        }
    }

    /// <summary>
    /// Draws corner bracket decorations at the four corners of the scan rectangle.
    /// When <see cref="FormingProgress"/> is 0, draws short corner brackets.
    /// As it approaches 1, the arms extend until they meet and form a full rounded rectangle.
    /// </summary>
    private class CornerBracketsDrawable : IDrawable
    {
        private readonly float m_widthFraction;
        private readonly float m_heightFraction;

        private const float BracketLength = 30f;
        internal const float DefaultStrokeWidth = 4f;
        
        internal static readonly Color DefaultBracketColor = Microsoft.Maui.Graphics.Colors.White;
        internal static readonly Color DetectedBracketColor = DuiColors.GetColor(ColorName.color_border_warning);
        internal static readonly Color SuccessBracketColor = DuiColors.GetColor(ColorName.color_border_success);
        internal static readonly Color FailureBracketColor = DuiColors.GetColor(ColorName.color_border_danger);
        internal const float DetectedStrokeWidth = 2f;
        internal const float SuccessStrokeWidth = 3f;
        internal const float FailureStrokeWidth = 3f;

        /// <summary>
        /// Inset in points applied to the bracket corner positions. Animated to create a breathing effect.
        /// </summary>
        internal float Inset { get; set; }

        /// <summary>
        /// When set, overrides the fraction-based rectangle with an explicit rect.
        /// Used to animate the brackets to the barcode position.
        /// </summary>
        internal RectF? OverrideRect { get; set; }

        /// <summary>
        /// Progress of arm extension: 0 = short corner brackets, 1 = full rectangle border.
        /// Animated after brackets arrive at the barcode to "lock on".
        /// </summary>
        internal float FormingProgress { get; set; }

        /// <summary>
        /// The maximum stroke width for the corner bracket strokes.
        /// </summary>
        internal float MaxStrokeWidth { get; set; } = DefaultStrokeWidth;

        /// <summary>
        /// The color used for the corner bracket strokes.
        /// </summary>
        internal Color BracketColor { get; set; } = DefaultBracketColor;

        public CornerBracketsDrawable(float widthFraction, float heightFraction)
        {
            m_widthFraction = widthFraction;
            m_heightFraction = heightFraction;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            float rectX, rectY, rectWidth, rectHeight;

            if (OverrideRect is { } over)
            {
                rectX = over.X;
                rectY = over.Y;
                rectWidth = over.Width;
                rectHeight = over.Height;
            }
            else
            {
                rectWidth = dirtyRect.Width * m_widthFraction;
                rectHeight = dirtyRect.Height * m_heightFraction;
                rectX = (dirtyRect.Width - rectWidth) / 2f;
                var centerY = GetCameraFeedCenterY(dirtyRect.Width, dirtyRect.Height);
                rectY = centerY - rectHeight / 2f;
            }

            // Apply inset to move brackets inward
            var insetRectX = rectX + Inset;
            var insetRectY = rectY + Inset;
            var insetRectWidth = rectWidth - Inset * 2;
            var insetRectHeight = rectHeight - Inset * 2;

            // Interpolate arm lengths: from starting length to half the side (arms from two corners meet in the middle)
            // For large rects, start at BracketLength. For small rects, start at 30% of max so there's always visible growth.
            var maxHArm = insetRectWidth / 2f;
            var maxVArm = insetRectHeight / 2f;
            var minHArm = Math.Min(BracketLength, maxHArm * 0.3f);
            var minVArm = Math.Min(BracketLength, maxVArm * 0.3f);
            var hArm = minHArm + (maxHArm - minHArm) * FormingProgress;
            var vArm = minVArm + (maxVArm - minVArm) * FormingProgress;

            // Scale stroke width proportionally to bracket size so small barcodes get thinner lines
            var shortSide = Math.Min(insetRectWidth, insetRectHeight);
            var scaledStroke = Math.Clamp(shortSide / 30f, 1.5f, MaxStrokeWidth);

            canvas.StrokeColor = BracketColor;
            canvas.StrokeSize = scaledStroke;
            canvas.StrokeLineCap = LineCap.Round;
            canvas.StrokeLineJoin = LineJoin.Round;

            // Top-left
            DrawBracket(canvas, insetRectX, insetRectY, 0, vArm, hArm, 0);
            // Top-right
            DrawBracket(canvas, insetRectX + insetRectWidth, insetRectY, 0, vArm, -hArm, 0);
            // Bottom-left
            DrawBracket(canvas, insetRectX, insetRectY + insetRectHeight, 0, -vArm, hArm, 0);
            // Bottom-right
            DrawBracket(canvas, insetRectX + insetRectWidth, insetRectY + insetRectHeight, 0, -vArm, -hArm, 0);
        }

        private static void DrawBracket(ICanvas canvas, float cornerX, float cornerY, float vDx, float vDy, float hDx, float hDy)
        {
            var cornerRadius = (float)Sizes.GetSize(SizeName.radius_medium);
            
            // Normalize directions
            var vNormX = Math.Sign(vDx);
            var vNormY = Math.Sign(vDy);
            var hNormX = Math.Sign(hDx);
            var hNormY = Math.Sign(hDy);

            // Path: end of vertical arm → approach corner → curve → depart corner → end of horizontal arm
            var path = new PathF();

            // Start of vertical arm (far end)
            path.MoveTo(cornerX + vDx, cornerY + vDy);

            // Line to where the curve begins (cornerRadius away from the actual corner)
            path.LineTo(cornerX + vNormX * cornerRadius, cornerY + vNormY * cornerRadius);

            // Quadratic bezier: control point is the sharp corner, end point is cornerRadius along the horizontal arm
            path.QuadTo(cornerX, cornerY, cornerX + hNormX * cornerRadius, cornerY + hNormY * cornerRadius);

            // Line to end of horizontal arm
            path.LineTo(cornerX + hDx, cornerY + hDy);

            canvas.DrawPath(path);
        }
    }

}
