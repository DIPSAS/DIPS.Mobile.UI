
using Microsoft.Maui.Platform;
namespace DIPS.Mobile.UI.Components.Images.ImageButton;

[Obsolete("Use Button with styles if possible")]
public partial class ImageButton : Microsoft.Maui.Controls.ImageButton
{
    //TODO: Fix when MAUI fixes: https://github.com/dotnet/maui/issues/18001
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