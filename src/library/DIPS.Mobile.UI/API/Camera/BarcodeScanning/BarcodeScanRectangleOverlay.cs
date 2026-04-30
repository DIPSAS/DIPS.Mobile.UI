using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.Resources.Sizes;

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
    private View? m_tooltipView;
    private readonly float m_widthFraction;
    private readonly float m_heightFraction;

    public BarcodeScanRectangleOverlay(float widthFraction, float heightFraction)
    {
        m_widthFraction = widthFraction;
        m_heightFraction = heightFraction;

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

        InputTransparent = true;
        BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent;

        Children.Add(m_dimGraphicsView);
        Children.Add(m_cornersGraphicsView);

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

        animation.Commit(m_cornersGraphicsView, "CornerBreathing",
            rate: 32,
            length: 2400,
            repeat: () => true);
    }

    private void StopBreathingAnimation()
    {
        m_cornersGraphicsView.AbortAnimation("CornerBreathing");
    }

    internal void SetTooltipView(View tooltipView)
    {
        m_tooltipView = tooltipView;
        m_tooltipView.InputTransparent = true;
        m_tooltipView.HorizontalOptions = LayoutOptions.Center;
        m_tooltipView.VerticalOptions = LayoutOptions.Start;

        Children.Add(m_tooltipView);

        SizeChanged += OnSizeChanged;
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
        StopBreathingAnimation();
        SizeChanged -= OnSizeChanged;

        if (m_tooltipView is not null)
        {
            m_tooltipView.SizeChanged -= OnTooltipSizeChanged;
            m_tooltipView = null;
        }
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
        var totalLetterbox = height - actualPreviewHeight;
        var topToolbarHeight = totalLetterbox * CameraPreview.TopToolbarPercentHeightOfLetterBox;
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
            var cornerRadius = (float)Sizes.GetSize(SizeName.radius_medium);

            LastScanRect = new RectF(rectX, rectY, rectWidth, rectHeight);

            // Draw semi-transparent overlay covering everything
            canvas.FillColor = Microsoft.Maui.Graphics.Color.FromRgba(0, 0, 0, 0.5f);
            canvas.FillRectangle(dirtyRect);

            // Cut out the scan rectangle
            canvas.BlendMode = BlendMode.Clear;
            canvas.FillRoundedRectangle(rectX, rectY, rectWidth, rectHeight, cornerRadius);
            canvas.BlendMode = BlendMode.Normal;
        }
    }

    /// <summary>
    /// Draws corner bracket decorations at the four corners of the scan rectangle.
    /// </summary>
    private class CornerBracketsDrawable : IDrawable
    {
        private readonly float m_widthFraction;
        private readonly float m_heightFraction;

        private const float BracketLength = 30f;
        private const float StrokeWidth = 4f;

        /// <summary>
        /// Inset in points applied to the bracket corner positions. Animated to create a breathing effect.
        /// </summary>
        internal float Inset { get; set; }

        public CornerBracketsDrawable(float widthFraction, float heightFraction)
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

            // Apply inset to move brackets inward
            var insetRectX = rectX + Inset;
            var insetRectY = rectY + Inset;
            var insetRectWidth = rectWidth - Inset * 2;
            var insetRectHeight = rectHeight - Inset * 2;

            canvas.StrokeColor = Microsoft.Maui.Graphics.Colors.White;
            canvas.StrokeSize = StrokeWidth;
            canvas.StrokeLineCap = LineCap.Round;
            canvas.StrokeLineJoin = LineJoin.Round;

            // Top-left
            DrawBracket(canvas, insetRectX, insetRectY, 0, BracketLength, BracketLength, 0);
            // Top-right
            DrawBracket(canvas, insetRectX + insetRectWidth, insetRectY, 0, BracketLength, -BracketLength, 0);
            // Bottom-left
            DrawBracket(canvas, insetRectX, insetRectY + insetRectHeight, 0, -BracketLength, BracketLength, 0);
            // Bottom-right
            DrawBracket(canvas, insetRectX + insetRectWidth, insetRectY + insetRectHeight, 0, -BracketLength, -BracketLength, 0);
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
