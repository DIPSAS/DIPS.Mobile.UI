using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.SystemMessage;

public static partial class SystemMessageService
{
    private static async partial void PlatformShow(SystemMessage systemMessage)
    {
        // Small delay to wait for iOS to initialize KeyWindow
        await Task.Delay(10);
        if (OperatingSystem.IsIOSVersionAtLeast(14, 1))
        {
            var appDelegate = UIApplication.SharedApplication.Delegate as MauiUIApplicationDelegate;
            var rootView = appDelegate.Window.RootViewController!.View!;
            systemMessage.HeightRequest = rootView.Frame.Height;
            systemMessage.WidthRequest = rootView.Frame.Width;
            rootView.AddSubview(systemMessage.ToPlatform(DUI.GetCurrentMauiContext!));
            
        }
    }
}