using System.Diagnostics;
using Android.App;
using Android.Graphics.Drawables;
using AndroidX.Core.SplashScreen;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Android;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;
using DIPS.Mobile.UI.Components.Pickers.TimePicker.Android;

// ReSharper disable once InconsistentNaming
namespace DIPS.Mobile.UI.API.Library;

public static partial class DUI
{
    public static void Init(Android.App.Activity activity)
    {
        SplashScreen.InstallSplashScreen(activity);
    }
    
    private static partial void PlatformInit()
    {
    }
    
    private static partial void RemovePlatformSpecificViewsLocatedOnTopOfPage()
    {
        if (DatePickerService.IsOpen())
        {
            DatePickerService.Close();
        }

        if (TimePickerService.IsOpen())
        {
            TimePickerService.Close();
        }
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

    public static bool TryGetDrawableFromFileName(string fileName, out Drawable? drawable)
    {
        TryGetResourceId(fileName, out var id, defType:"drawable");
        drawable = null;
        if (id == 0)
        {
            return false;
        }

        drawable = OperatingSystem.IsAndroidVersionAtLeast(31,1) ? Platform.AppContext.Resources?.GetDrawable(id) : Platform.AppContext.Resources?.GetDrawable(id, Platform.CurrentActivity?.Theme);
        return true;

    }
    
    public static bool TryGetDrawableFromFileImageSource(ImageSource imageSource, out Drawable? drawable)
    {
        drawable = null;
        if (imageSource is FileImageSource fileImageSource)
        {
            return (TryGetDrawableFromFileName(fileImageSource.File.Replace(".png", ""), out drawable));
        }

        return false;
    }
    
    /// <summary>
    /// Return a resource identifier for the given resource name. A fully qualified resource name is of the form "package:type/entry". The first two components (package and type) are optional if defType and defPackage, respectively, are specified here.
    /// </summary>
    /// <param name="name">The name of the desired resource.</param>
    /// <param name="defType">Optional default resource type to find, if "type/" is not included in the name. Can be null to require an explicit type.</param>
    /// <param name="defPackage">Optional default package to find, if "package:" is not included in the name. Can be null to require an explicit package.</param>
    /// <returns></returns>
    /// <remarks>Taken from here https://developer.android.com/reference/android/content/res/Resources#getIdentifier(java.lang.String,%20java.lang.String,%20java.lang.String)</remarks>
    internal static int? GetResourceId(string name, string? defType = null, string? defPackage = null)
    {
        var id = Platform.AppContext.Resources?.GetIdentifier(name, defType, Platform.AppContext.PackageName);
        return id > 0 ? id : null;
    }

}