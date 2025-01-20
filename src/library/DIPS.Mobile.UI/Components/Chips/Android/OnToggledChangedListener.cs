using Android.Widget;
using DIPS.Mobile.UI.Internal.Logging;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Chips;

internal class OnToggledChangedListener : Java.Lang.Object, CompoundButton.IOnCheckedChangeListener
{
    private readonly ChipHandler m_handler;
    public OnToggledChangedListener(ChipHandler handler)
    {
        m_handler = handler;
    }
    
    public async void OnCheckedChanged(CompoundButton? buttonView, bool isChecked)
    {
        m_handler.VirtualView.IsToggled = isChecked;

        await Task.Delay(1);
        
        if (m_handler.PlatformView is null)
        {
            DUILogService.LogError<OnToggledChangedListener>("PlatformView is null, this should not happen, most likely the issue is that the Content is rendered and then the handler is instantly disconnected");
            return;
        }
        m_handler.PlatformView.CheckedIconTint = m_handler.VirtualView.TitleColor?.ToDefaultColorStateList();
    }
}