using Android.Gms.Tasks;
using Object = Java.Lang.Object;

namespace DIPS.Mobile.UI.Observable.Android;

public class OnSuccessListener(Action<Object> onSuccess) : Object, IOnSuccessListener
{
    private Action<Object>? m_onSuccess = onSuccess;

    public void OnSuccess(Object result)
    {
        m_onSuccess?.Invoke(result);       
    }

    protected override void Dispose(bool disposing)
    {
        m_onSuccess = null;
        base.Dispose(disposing);
    }
}