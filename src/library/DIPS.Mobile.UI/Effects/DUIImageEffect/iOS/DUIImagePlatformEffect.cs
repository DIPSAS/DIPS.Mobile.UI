using Microsoft.Maui.Platform;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Effects.DUIImageEffect;

public partial class DUIImagePlatformEffect
{
    private UIColor m_color;
    
    protected override partial void OnAttached()
    {
        m_color = DUIImageEffect.GetColor(Element).ToPlatform();
        
        if (Control is UIButton uiButton)
        {
            ApplyImageButtonColor(uiButton);
        }
    }

    private void ApplyImageButtonColor(UIButton uiButton)
    {
        if(uiButton.ImageView.Image is null)
            return;
        
        uiButton.SetImage(uiButton.CurrentImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), UIControlState.Normal);
        uiButton.TintColor = m_color;
    }

    protected override partial void OnDetached()
    {
        
    }
   
}