using Android.Graphics.Drawables;
using Google.Android.Material.Shape;
using Microsoft.Maui.Platform;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Effects.Layout;

public partial class  LayoutPlatformEffect
{
    private Drawable? m_originalBackground;

    private partial void PlatformOnAttached(CornerRadius? cornerRadius, Color? stroke)
    {
        m_originalBackground = Control.Background;
        
        var materialShapeDrawable = MaterialShapeDrawableHelper.GetMaterialShapeDrawableFromCornerRadius(cornerRadius, stroke, Layout.GetStrokeThickness(Element));
      
        Control.ClipToOutline = true;
        
        if (Element is VisualElement visualElement)
        {
            materialShapeDrawable.FillColor = visualElement.BackgroundColor is not null ? 
                visualElement.BackgroundColor.ToDefaultColorStateList() 
                : Colors.Transparent.ToDefaultColorStateList();
        }
        
        Control.Background = materialShapeDrawable;
    }

    protected override partial void OnDetached()
    {
        if(Control is null)
            return;
        
        Control.Background = m_originalBackground;
    }
}