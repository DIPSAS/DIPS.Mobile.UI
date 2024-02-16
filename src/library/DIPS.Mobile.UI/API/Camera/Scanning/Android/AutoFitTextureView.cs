using System.Diagnostics.CodeAnalysis;
using Android.Content;
using Android.Views;
using Java.Lang;

namespace DIPS.Mobile.UI.API.Camera.Scanning.Android;

public class AutoFitTextureView : TextureView
{
    private int m_ratioWidth;
    private int m_ratioHeight;

    public AutoFitTextureView([NotNull] Context context) : base(context) { }
    
    /**
   * Sets the aspect ratio for this view. The size of the view will be measured based on the ratio
   * calculated from the parameters. Note that the actual sizes of parameters don't matter, that
   * is, calling setAspectRatio(2, 3) and setAspectRatio(4, 6) make the same result.
   *
   * @param width  Relative horizontal size
   * @param height Relative vertical size
   */
    public void SetAspectRatio(int width, int height) {
        if (width < 0 || height < 0) {
            throw new IllegalArgumentException("Size cannot be negative.");
        }
        m_ratioWidth = width;
        m_ratioHeight = height;
        RequestLayout();
    }

    protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
    {
        base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        var width = MeasureSpec.GetSize(widthMeasureSpec);
        var height = MeasureSpec.GetSize(heightMeasureSpec);
        if (0 == m_ratioWidth || 0 == m_ratioHeight) {
            SetMeasuredDimension(width, height);
        } else {
            if (width < height * m_ratioWidth / m_ratioHeight) {
                SetMeasuredDimension(width, width * m_ratioHeight / m_ratioWidth);
            } else {
                SetMeasuredDimension(height * m_ratioWidth / m_ratioHeight, height);
            }
        }
        base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
    }
}