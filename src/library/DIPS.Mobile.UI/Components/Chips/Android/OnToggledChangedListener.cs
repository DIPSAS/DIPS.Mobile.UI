using Android.Widget;
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
        m_handler.PlatformView.CheckedIconTint = m_handler.VirtualView.TitleColor?.ToDefaultColorStateList();
    }
}