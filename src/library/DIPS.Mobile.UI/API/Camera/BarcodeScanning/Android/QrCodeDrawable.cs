using Android.Graphics;
using Android.Graphics.Drawables;
using Rect = Android.Graphics.Rect;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning.Android;

//Taken from: https://github.com/android/camera-samples/blob/main/CameraX-MLKit/app/src/main/java/com/example/camerax_mlkit/QrCodeDrawable.kt
public class QrCodeDrawable : Drawable
{
    private readonly Xamarin.Google.MLKit.Vision.Barcode.Common.Barcode m_barcode;
    private readonly global::Android.Graphics.Paint m_boundingRectPaint;
    private readonly global::Android.Graphics.Paint m_contentRectPaint;
    private readonly global::Android.Graphics.Paint m_contentTextPaint;
    private readonly int m_contentPadding = 25;

    public QrCodeDrawable(Xamarin.Google.MLKit.Vision.Barcode.Common.Barcode barcode)
    {
        m_barcode = barcode;
        m_boundingRectPaint = new global::Android.Graphics.Paint()
        {
            Color = global::Android.Graphics.Color.Yellow,
            StrokeWidth = 1f,
            Alpha = 200
        };
        
        m_contentRectPaint = new global::Android.Graphics.Paint()
        {
            Color = global::Android.Graphics.Color.Yellow,
            Alpha = 255
        };
        
        m_contentTextPaint = new global::Android.Graphics.Paint()
        {
            Color = global::Android.Graphics.Color.DarkGray,
            Alpha = 255,
            TextSize = 36f
        };

    }

    public override void Draw(Canvas canvas)
    {
        canvas.DrawRect(m_barcode.BoundingBox, m_boundingRectPaint);
        // canvas.DrawRect(new Rect(
        //     100, 
        //     m_barcode.BoundingBox.Top,
        //     100,
        //     m_barcode.BoundingBox.Bottom 
        //     ), m_boundingRectPaint);
        // canvas.drawText(
        //     qrCodeViewModel.qrContent,
        //     (qrCodeViewModel.boundingRect.left + contentPadding).toFloat(),
        //     (qrCodeViewModel.boundingRect.bottom + contentPadding*2).toFloat(),
        //     contentTextPaint
    }

    public override void SetAlpha(int alpha)
    {
        m_boundingRectPaint.Alpha = alpha;
        m_contentRectPaint.Alpha = alpha;
        m_contentTextPaint.Alpha = alpha;
    }

    public override void SetColorFilter(ColorFilter? colorFilter)
    {
        m_boundingRectPaint.SetColorFilter(colorFilter);
        m_contentRectPaint.SetColorFilter(colorFilter);
        m_contentTextPaint.SetColorFilter(colorFilter);
    }

    public override int Opacity => (int)Format.Translucent;
}