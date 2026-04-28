using DIPS.Mobile.UI.Resources.Sizes;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

/// <summary>
/// A full-screen overlay that draws a semi-transparent dimmed area with a clear rounded-corner cutout
/// representing the barcode scan region.
/// </summary>
internal class BarcodeScanRectangleOverlay : GraphicsView
{
    private readonly BarcodeScanRectangleDrawable m_drawable;

    public BarcodeScanRectangleOverlay(float widthFraction, float heightFraction)
    {
        m_drawable = new BarcodeScanRectangleDrawable(widthFraction, heightFraction);
        Drawable = m_drawable;
        InputTransparent = true;
        BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent;
    }

    /// <summary>
    /// Returns the scan rectangle bounds in view-relative coordinates, useful for filtering barcode results.
    /// Only valid after the overlay has been laid out.
    /// </summary>
    internal RectF GetScanRectangle()
    {
        return m_drawable.LastScanRect;
    }

    private class BarcodeScanRectangleDrawable : IDrawable
    {
        private readonly float m_widthFraction;
        private readonly float m_heightFraction;

        internal RectF LastScanRect { get; private set; }

        public BarcodeScanRectangleDrawable(float widthFraction, float heightFraction)
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
            var cornerRadius = Sizes.GetSize(SizeName.radius_medium);
            
            LastScanRect = new RectF(rectX, rectY, rectWidth, rectHeight);

            // Draw semi-transparent overlay covering everything
            canvas.FillColor = Microsoft.Maui.Graphics.Color.FromRgba(0, 0, 0, 0.5f);
            canvas.FillRectangle(dirtyRect);

            // Cut out the scan rectangle by drawing it with BlendMode
            canvas.BlendMode = BlendMode.Clear;
            canvas.FillRoundedRectangle(rectX, rectY, rectWidth, rectHeight, (float)cornerRadius);

            // Reset blend mode and draw the border
            canvas.BlendMode = BlendMode.Normal;
            canvas.StrokeColor = Microsoft.Maui.Graphics.Colors.White;
            canvas.StrokeSize = 3;
            canvas.DrawRoundedRectangle(rectX, rectY, rectWidth, rectHeight, (float)cornerRadius);
        }
    }
}
