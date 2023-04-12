using Android.App;
using AndroidX.Core.SplashScreen;

namespace DIPS.Mobile.UI.API.Library.Android;

// ReSharper disable once InconsistentNaming
public static class DUI
{
    public static void Init(Activity activity)
    {
        SplashScreen.InstallSplashScreen(activity);
    }
    
    /// <summary>
    /// Return a resource identifier for the given resource name. A fully qualified resource name is of the form "package:type/entry". The first two components (package and type) are optional if defType and defPackage, respectively, are specified here.
    /// </summary>
    /// <param name="name">The name of the desired resource.</param>
    /// <param name="defType">Optional default resource type to find, if "type/" is not included in the name. Can be null to require an explicit type.</param>
    /// <param name="defPackage">Optional default package to find, if "package:" is not included in the name. Can be null to require an explicit package.</param>
    /// <returns></returns>
    /// <remarks>Taken from here https://developer.android.com/reference/android/content/res/Resources#getIdentifier(java.lang.String,%20java.lang.String,%20java.lang.String)</remarks>
    public static bool TryGetResourceId(string name, out int id, string? defType = null, string? defPackage = null)
    {
        id = 0;
        if (Platform.AppContext.Resources == null) return false;
        
        id = Platform.AppContext.Resources.GetIdentifier(name, defType, defPackage ?? Platform.AppContext.PackageName);
        return id != 0;
    }
}