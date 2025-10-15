using Android.Graphics.Drawables;
using Google.Android.Material.Shape;
using Microsoft.Maui.Platform;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Effects.Layout;

public partial class  LayoutPlatformEffect
{
    private Drawable? m_originalBackground;
    private Thickness? m_previousPadding;

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
        
        if (stroke is not null && Element is Microsoft.Maui.Controls.Layout layout)
        {
            m_previousPadding = layout.Padding;
            
            // We have to add padding to make the stroke visible, since the stroke is drawn inside the view. And views inside the layout will otherwise overlap the stroke.
            layout.Padding = Layout.GetStrokeThickness(Element);
        }

        Control.Background = materialShapeDrawable;
    }

    protected override partial void OnDetached()
    {
        if(Control is null)
            return;
        
        Control.Background = m_originalBackground;
        
        if(m_previousPadding is not null && Element is Microsoft.Maui.Controls.Layout layout)
        {
            layout.Padding = m_previousPadding.Value;
        }
    }
}