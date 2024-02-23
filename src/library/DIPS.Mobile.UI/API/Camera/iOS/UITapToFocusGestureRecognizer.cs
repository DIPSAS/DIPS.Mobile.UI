using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.API.Camera.iOS;

internal class UITapToFocusGestureRecognizer : UITapGestureRecognizer
{
    private Action<NSSet, UIEvent>? m_onTouchesBegan;

    public UITapToFocusGestureRecognizer(Action<NSSet, UIEvent> onTouchesBegan)
    {
        m_onTouchesBegan = onTouchesBegan;
    }

    public override void TouchesBegan(NSSet touches, UIEvent evt)
    {
        m_onTouchesBegan?.Invoke(touches, evt);
        base.TouchesBegan(touches, evt);
    }

    internal void RemoveReferences()
    {
        m_onTouchesBegan = null;
    }
}