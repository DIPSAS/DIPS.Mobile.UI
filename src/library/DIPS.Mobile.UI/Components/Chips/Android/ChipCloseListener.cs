namespace DIPS.Mobile.UI.Components.Chips.Android;

public class ChipCloseListener : Java.Lang.Object, global::Android.Views.View.IOnClickListener
{
    private readonly ChipHandler m_chipHandler;

    public ChipCloseListener(ChipHandler chipHandler)
    {
        m_chipHandler = chipHandler;
    }

    public void OnClick(global::Android.Views.View? v)
    {
        m_chipHandler.OnCloseTapped();
    }
}