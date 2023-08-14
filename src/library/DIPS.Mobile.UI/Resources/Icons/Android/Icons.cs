namespace DIPS.Mobile.UI.Resources.Icons;

public static partial class Icons
{
    public static bool TryGetDrawable(IconName iconName, out Android.Graphics.Drawables.Drawable? drawable)
    {
        API.Library.DUI.TryGetResourceId(Icons.GetIconName(IconName.important_fill), out var id, defType:"drawable");
        if (id != 0)
        {
            drawable = OperatingSystem.IsAndroidVersionAtLeast(31,1) ? Platform.AppContext.Resources?.GetDrawable(id) : Platform.AppContext.Resources?.GetDrawable(id, Platform.CurrentActivity?.Theme);
            return true;
        }

        drawable = null;
        return false;
    }
}