using Android.Graphics.Drawables;
using Google.Android.Material.Shape;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Effects.Layout;

public partial class  LayoutPlatformEffect
{
    private Drawable? m_originalBackground;
    
    protected override partial void OnAttached()
    {
        m_originalBackground = Control.Background;
        
        var shapeAppearanceModel = new ShapeAppearanceModel.Builder().SetAllCorners(CornerFamily.Rounded, Layout.GetUniformCornerRadius(Element) * Control.Context.GetDisplayDensity()).Build();
        var materialShapeDrawable = new MaterialShapeDrawable(shapeAppearanceModel);
      
        Control.ClipToOutline = true;

        materialShapeDrawable.FillColor = ((Element as VisualElement)!).BackgroundColor.ToDefaultColorStateList();
        materialShapeDrawable.StrokeWidth = 0;
        
        Control.Background = materialShapeDrawable;
    }

    protected override partial void OnDetached()
    {
        Control.Background = m_originalBackground;
    }
}