using DIPS.Mobile.UI.API.Library;
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
                var rootView = DUI.RootController;
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
        MainThread.BeginInvokeOnMainThread(delegate
        {
            foreach (var uiView in DUI.RootController!.Subviews)
            {
                if(uiView.Tag == SystemMessageTagId)
                    uiView.RemoveFromSuperview();
            }
        });
    }
  
}