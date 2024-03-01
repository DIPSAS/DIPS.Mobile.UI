using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.API.Camera.Preview.iOS;

internal class UITapToFocusGestureRecognizer : UITapGestureRecognizer
{
    private Action<NSSet, UIEvent>? m_onTouchesBegan;

    public UITapToFocusGestureRecognizer(Action<NSSet, UIEvent> onTouchesBegan)
    {
        m_onTouchesBegan = onTouchesBegan;
    }

    public override void TouchesEnded(NSSet touches, UIEvent evt)
    {
        m_onTouchesBegan?.Invoke(touches, evt);
        base.TouchesEnded(touches, evt);
    }

    internal void RemoveReferences()
    {
        m_onTouchesBegan = null;
    }
}