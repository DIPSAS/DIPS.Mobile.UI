// ReSharper disable once CheckNamespace

using Android.Views;
using Google.Android.Material.ImageView;
using Microsoft.Maui.Platform;
using Color = Android.Graphics.Color;

namespace DIPS.Mobile.UI.Effects.DUIImageEffect;

public partial class DUIImagePlatformEffect
{
    private Color m_color;

    protected override partial void OnAttached()
    {
        m_color = DUIImageEffect.GetColor(Element).ToPlatform();
        
        if (Control is ShapeableImageView uiButton)
        {
            ApplyImageButtonColor(uiButton);
        }
    }

    private void ApplyImageButtonColor(ShapeableImageView uiButton)
    {
        uiButton.SetColorFilter(m_color);
    }

    protected override partial void OnDetached()
    {
        
    }
   
}