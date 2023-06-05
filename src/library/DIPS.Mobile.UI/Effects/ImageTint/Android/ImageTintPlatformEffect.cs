using Google.Android.Material.ImageView;
using Microsoft.Maui.Platform;
using Color = Android.Graphics.Color;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Effects.ImageTint;

public partial class ImageTintPlatformEffect
{
    private Color m_color;

    protected override partial void OnAttached()
    {
        m_color = ImageTint.GetColor(Element).ToPlatform();
        
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