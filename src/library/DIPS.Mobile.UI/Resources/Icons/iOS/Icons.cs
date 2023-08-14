using UIKit;

namespace DIPS.Mobile.UI.Resources.Icons;

public static partial class Icons
{
    public static bool TryGetUIImage(IconName iconName, out UIImage? uiImage)
    {
        uiImage = UIImage.FromBundle(GetIconName(iconName));
        return uiImage != null;
    }
}