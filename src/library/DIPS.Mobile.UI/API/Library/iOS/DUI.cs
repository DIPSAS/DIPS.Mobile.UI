using Foundation;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.API.Library;

public static partial class DUI
{

    public static Action? OnRemoveViewsLocatedOnTopOfPage;
    
    private static partial void RemovePlatformSpecificViewsLocatedOnTopOfPage()
    {
        OnRemoveViewsLocatedOnTopOfPage?.Invoke();
    }
    
    private static async partial void PlatformInit()
    {
        await Task.Delay(10);
        var appDelegate = UIApplication.SharedApplication.Delegate as MauiUIApplicationDelegate;
        RootController = appDelegate.Window.RootViewController!.View!;

        NSNotificationCenter.DefaultCenter.AddObserver(UIDevice.OrientationDidChangeNotification, OrientationDidChange);
    }

    private static void OrientationDidChange(NSNotification notification)
    {
        OrientationChanged?.Invoke(UIDevice.CurrentDevice.Orientation switch
        {
            UIDeviceOrientation.Portrait => OrientationDegree.Orientation0,
            UIDeviceOrientation.LandscapeLeft => OrientationDegree.Orientation90,
            UIDeviceOrientation.PortraitUpsideDown => OrientationDegree.Orientation180,
            UIDeviceOrientation.LandscapeRight => OrientationDegree.Orientation270,
            _ => OrientationDegree.Orientation0
        });
    }

    public static bool TryGetUIImageFromImageSource(ImageSource? imageSource, out UIImage? uiImage)
    {
        uiImage = null;
        if (imageSource is FileImageSource fileImageSource)
        {
            return (TryGetUIImageFromBundle(fileImageSource.File, out uiImage));
        }

        return false;
    }
    
    public static bool TryGetUIImageFromBundle(string? name, out UIImage? uiImage)
    {
        uiImage = null;
        if (string.IsNullOrEmpty(name))
        {
            return false;
        }

        uiImage = UIImage.FromBundle(name);
        return uiImage != null;

    }
    
    public static UIView? RootController { get; private set; }

}