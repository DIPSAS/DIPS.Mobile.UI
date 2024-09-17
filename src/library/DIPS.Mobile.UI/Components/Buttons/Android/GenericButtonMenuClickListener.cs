using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Buttons.Android;

internal class GenericButtonMenuClickListener(Action callback) : Java.Lang.Object, View.IOnClickListener
{
    public void OnClick(View? v)
    {
        callback.Invoke();
    }
}