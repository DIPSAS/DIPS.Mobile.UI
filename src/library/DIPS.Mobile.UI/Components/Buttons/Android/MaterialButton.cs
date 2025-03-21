using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using DIPS.Mobile.UI.Extensions.Android;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Buttons.Android;

public class MaterialButton : MauiMaterialButton
{
    protected MaterialButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
    {
    }

    public MaterialButton(Context context) : base(context)
    {
    }

    public MaterialButton(Context context, IAttributeSet? attrs) : base(context, attrs)
    {
    }

    public MaterialButton(Context context, IAttributeSet? attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
    {
    }
    
    public Button? Button { get; set; }

    protected override void OnDraw(Canvas canvas)
    {
        base.OnDraw(canvas);
        
        if(Button is null || Button.AdditionalHitBoxSize == Thickness.Zero)
            return;
        
        this.SetAdditionalHitBoxSize(Button.AdditionalHitBoxSize);
    }
}