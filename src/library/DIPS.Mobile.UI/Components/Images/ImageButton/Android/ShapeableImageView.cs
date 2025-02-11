using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using DIPS.Mobile.UI.Extensions.Android;

namespace DIPS.Mobile.UI.Components.Images.ImageButton;

public class ShapeableImageView : Google.Android.Material.ImageView.ShapeableImageView
{
    protected ShapeableImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
    {
    }

    public ShapeableImageView(Context? context) : base(context)
    {
    }

    public ShapeableImageView(Context? context, IAttributeSet? attrs) : base(context, attrs)
    {
    }

    public ShapeableImageView(Context? context, IAttributeSet? attrs, int defStyle) : base(context, attrs, defStyle)
    {
    }
    
    public ImageButton? ImageButton { get; set; }

    protected override void OnDraw(Canvas canvas)
    {
        base.OnDraw(canvas);
        
        if(ImageButton is null || ImageButton.AdditionalHitBoxSize == Thickness.Zero)
            return;
        
        this.SetAdditionalHitBoxSize(ImageButton.AdditionalHitBoxSize);
    }
}