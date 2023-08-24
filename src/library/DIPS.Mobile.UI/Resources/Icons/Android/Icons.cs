namespace DIPS.Mobile.UI.Resources.Icons;

public static partial class Icons
{
    public static bool TryGetDrawable(IconName iconName, out Android.Graphics.Drawables.Drawable? drawable) => (API.Library.DUI.TryGetDrawableFromFileName(GetIconName(IconName.important_fill), out drawable));
}