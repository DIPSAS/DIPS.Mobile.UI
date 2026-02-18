using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Extensions.iOS;
using DIPS.Mobile.UI.Platforms.iOS;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Alerting.SystemMessage;

public static partial class SystemMessageService
{
    private static partial void PlatformShow(SystemMessage systemMessage)
    {
        if (OperatingSystem.IsIOSVersionAtLeast(14, 1))
        {
            MainThread.BeginInvokeOnMainThread(delegate
            {
                var rootView = Platform.GetCurrentUIViewController()?.View; 
                systemMessage.HeightRequest = rootView.Frame.Height;
                systemMessage.WidthRequest = rootView.Frame.Width;
                var systemMessageUIView = systemMessage.ToPlatform(DUI.GetCurrentMauiContext!);
                systemMessageUIView.Tag = SystemMessageTagId;
                rootView.AddSubview(systemMessageUIView);
            });
        }
    }

    private static partial void PlatformRemove()
    {
        DUI.RootController!.RemoveUIViewChildWithTag(SystemMessageTagId);
    }
  
}