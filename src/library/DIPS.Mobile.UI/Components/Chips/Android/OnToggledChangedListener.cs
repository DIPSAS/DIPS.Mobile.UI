using Android.Widget;

namespace DIPS.Mobile.UI.Components.Chips;

internal class OnToggledChangedListener : Java.Lang.Object, CompoundButton.IOnCheckedChangeListener
{
    private readonly ChipHandler m_handler;
    public OnToggledChangedListener(ChipHandler handler)
    {
        m_handler = handler;
    }
    
    public void OnCheckedChanged(CompoundButton? buttonView, bool isChecked)
    {
        m_handler.VirtualView.IsToggled = isChecked;
    }
}