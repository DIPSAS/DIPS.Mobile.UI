using Android.App;
using Android.Graphics.Drawables;
using Android.Views;
using AndroidX.Core.SplashScreen;
using DIPS.Mobile.UI.API.Camera.Shared.Android;
using DIPS.Mobile.UI.API.Library.Android;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using TimePickerService = DIPS.Mobile.UI.Components.Pickers.TimePicker.TimePickerService;

// ReSharper disable once InconsistentNaming
namespace DIPS.Mobile.UI.API.Library;

public static partial class DUI
{
    public static void Init(Activity activity)
    {
        SplashScreen.InstallSplashScreen(activity);
        
        activity.GetFragmentManager()?.RegisterFragmentLifecycleCallbacks(new FragmentLifeCycleCallback(), false);
        
        TryEnableCustomHideSoftInputOnTappedImplementation(activity);
    }

    private static void TryEnableCustomHideSoftInputOnTappedImplementation(Activity activity)
    {
        if (!ShouldUseCustomHideSoftInputOnTappedImplementation)
            return;

        if (activity.Window is not null)
        {
            var originalWindow = activity.Window;
            var originalCallback = originalWindow.Callback;
            if(originalCallback is null)
                return;
                
            activity.Window.Callback = new UnfocusWindowCallback(originalWindow, originalCallback);
        }

        PageHandler.Mapper.ReplaceMapping<ContentPage, IPageHandler>(nameof(ContentPage.HideSoftInputOnTapped), HideSoftInputOnTapHandlerMappings.MapHideSoftInputOnTapped);
        EntryHandler.Mapper.ReplaceMapping<Entry, IEntryHandler>(nameof(VisualElement.IsFocused), HideSoftInputOnTapHandlerMappings.MapInputIsFocused);
        EditorHandler.Mapper.ReplaceMapping<Editor, IEditorHandler>(nameof(VisualElement.IsFocused), HideSoftInputOnTapHandlerMappings.MapInputIsFocused);
        SearchBarHandler.Mapper.ReplaceMapping<SearchBar, ISearchBarHandler>(nameof(VisualElement.IsFocused), HideSoftInputOnTapHandlerMappings.MapInputIsFocused);
    }

    private static partial void PlatformInit()
    {
        try
        {
            var deviceRotationListener = new DeviceRotationListener(OnOrientationUpdated, Platform.AppContext);
            deviceRotationListener.Enable();
        }
        catch
        {
            // ignored
        }
    }

    private static void OnOrientationUpdated(SurfaceOrientation surfaceOrientation)
    {
        OrientationChanged?.Invoke(surfaceOrientation switch
        {
            SurfaceOrientation.Rotation0 => OrientationDegree.Orientation0,
            SurfaceOrientation.Rotation90 => OrientationDegree.Orientation90,
            SurfaceOrientation.Rotation180 => OrientationDegree.Orientation180,
            SurfaceOrientation.Rotation270 => OrientationDegree.Orientation270,
            _ => OrientationDegree.Orientation0
        });
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