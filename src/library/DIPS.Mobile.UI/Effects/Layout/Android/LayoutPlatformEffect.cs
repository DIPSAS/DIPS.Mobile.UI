using Android.Graphics.Drawables;
using Google.Android.Material.Shape;
using Microsoft.Maui.Platform;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Effects.Layout;

public partial class  LayoutPlatformEffect
{
    private Drawable? m_originalBackground;
    
    protected override partial void OnAttached()
    {
        m_originalBackground = Control.Background;
        
        var shapeAppearanceModel = new ShapeAppearanceModel.Builder().SetAllCorners(CornerFamily.Rounded, (float)(Layout.GetCornerRadius(Element).TopLeft * Control.Context.GetDisplayDensity())).Build();
        var materialShapeDrawable = new MaterialShapeDrawable(shapeAppearanceModel);
      
        Control.ClipToOutline = true;

        if (Element is VisualElement visualElement)
        {
            materialShapeDrawable.FillColor = visualElement.BackgroundColor is not null ? 
                visualElement.BackgroundColor.ToDefaultColorStateList() 
                : Colors.Transparent.ToDefaultColorStateList();
        }
        
        materialShapeDrawable.StrokeWidth = 0;
        
        Control.Background = materialShapeDrawable;
    }

    protected override partial void OnDetached()
    {
        Control.Background = m_originalBackground;
    }
}