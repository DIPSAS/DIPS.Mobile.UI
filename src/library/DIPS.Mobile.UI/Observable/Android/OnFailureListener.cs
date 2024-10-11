using Android.Gms.Tasks;
using Exception = Java.Lang.Exception;
using Object = Java.Lang.Object;

namespace DIPS.Mobile.UI.Observable.Android;

public class OnFailureListener(Action<Exception> onFailure) : Object, IOnFailureListener
{
    private Action<Exception>? m_onFailure = onFailure;

    protected override void Dispose(bool disposing)
    {
        m_onFailure = null;
        base.Dispose(disposing);
    }

    public void OnFailure(Exception e)
    {  
        m_onFailure?.Invoke(e);
    }
}