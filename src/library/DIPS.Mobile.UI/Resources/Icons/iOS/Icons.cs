using DIPS.Mobile.UI.API.Library;
using UIKit;

namespace DIPS.Mobile.UI.Resources.Icons;

public static partial class Icons
{
    public static bool TryGetUIImage(IconName iconName, out UIImage? uiImage) => DUI.TryGetUIImageFromBundle(GetIconName(iconName), out uiImage);
}