using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.API.Camera.ImageGallery.ImageGalleryThumbnails;

internal class CaptureButton : Button
{
    public CaptureButton()
    {
        ImageSource = Icons.GetIcon(IconName.camera);
        ImageTintColor = Colors.GetColor(ColorName.color_neutral_80);
        Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge);
        CornerRadius = Sizes.GetSize(SizeName.size_2);
        BackgroundColor = Colors.GetColor(ColorName.color_neutral_30);
        WidthRequest = Sizes.GetSize(SizeName.size_15);
        HeightRequest = Sizes.GetSize(SizeName.size_15);
    }
}