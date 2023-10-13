
using Microsoft.Maui.Platform;
namespace DIPS.Mobile.UI.Components.Images.ImageButton;

public partial class ImageButton : Microsoft.Maui.Controls.ImageButton
{
#if __ANDROID__
    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (propertyName.Equals(IsVisibleProperty.PropertyName))
        {
            if (IsVisible)
            {
                if (Handler?.PlatformView is Google.Android.Material.ImageView.ShapeableImageView shapeableImageView && Handler.VirtualView is IImageButton imageButton)
                {
                   shapeableImageView.UpdatePadding(imageButton);
                }
            }
        }
    }
#endif
    
}