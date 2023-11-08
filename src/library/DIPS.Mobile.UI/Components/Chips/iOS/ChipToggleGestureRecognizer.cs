using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.Components.Chips;

public class ChipToggleGestureRecognizer : UIGestureRecognizer
{

    private readonly ChipHandler m_chipHandler;
    public ChipToggleGestureRecognizer(ChipHandler chipHandler)
    {
        m_chipHandler = chipHandler;
    }

    public override void TouchesBegan(NSSet touches, UIEvent evt)
    {
        base.TouchesBegan(touches, evt);
        if (m_chipHandler.VirtualView.IsToggleable)
        {
            m_chipHandler.VirtualView.IsToggled = !m_chipHandler.VirtualView.IsToggled;
        }

    }
}