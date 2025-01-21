using Android.Widget;
using DIPS.Mobile.UI.Components.Images.iOS;
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

        try
        {
            m_handler.PlatformView.CheckedIconTint = m_handler.VirtualView.TitleColor?.ToDefaultColorStateList();
        }
        catch
        {
            DUILogService.LogError<IconTintColorHandler>("@@@" +
                                                         "PlatformView is null, this should not happen." +
                                                         "Likely the issue is that the Content is rendered and then the handler is instantly disconnected." +
                                                         "Make sure to address this issue!");
        }
    }
}