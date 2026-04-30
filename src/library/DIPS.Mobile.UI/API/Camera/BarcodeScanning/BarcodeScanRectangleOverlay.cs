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
        var animation = new Animation(v =>
        {
            m_cornersGraphicsView.Scale = v;
        }, 1.0, 1.03);

        animation.Commit(m_cornersGraphicsView, "CornerBreathing",
            length: 2000,
            easing: Easing.SinInOut,
            repeat: () => true,
            finished: (_, _) => { });
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
        if (m_tooltipView is null || Height <= 0 || m_tooltipView.Height <= 0)
            return;

        var rectHeight = Height * m_heightFraction;
        var rectY = (Height - rectHeight) / 2f;
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
            var rectY = (dirtyRect.Height - rectHeight) / 2f;
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
            var rectY = (dirtyRect.Height - rectHeight) / 2f;

            canvas.StrokeColor = Microsoft.Maui.Graphics.Colors.White;
            canvas.StrokeSize = StrokeWidth;
            canvas.StrokeLineCap = LineCap.Round;
            canvas.StrokeLineJoin = LineJoin.Round;

            // Top-left: vertical arm going down, horizontal arm going right
            DrawBracket(canvas, rectX, rectY, 0, BracketLength, BracketLength, 0);
            // Top-right: vertical arm going down, horizontal arm going left
            DrawBracket(canvas, rectX + rectWidth, rectY, 0, BracketLength, -BracketLength, 0);
            // Bottom-left: vertical arm going up, horizontal arm going right
            DrawBracket(canvas, rectX, rectY + rectHeight, 0, -BracketLength, BracketLength, 0);
            // Bottom-right: vertical arm going up, horizontal arm going left
            DrawBracket(canvas, rectX + rectWidth, rectY + rectHeight, 0, -BracketLength, -BracketLength, 0);
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
