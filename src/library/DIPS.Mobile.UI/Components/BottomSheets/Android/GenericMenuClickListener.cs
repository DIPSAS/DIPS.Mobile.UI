using Android.Views;

namespace DIPS.Mobile.UI.Components.BottomSheets;

internal class GenericMenuClickListener : Java.Lang.Object, IMenuItemOnMenuItemClickListener
{
    readonly Action m_callback;

    public GenericMenuClickListener(Action callback)
    {
        m_callback = callback;
    }

    public bool OnMenuItemClick(IMenuItem item)
    {
        m_callback.Invoke();
        return true;
    }
}