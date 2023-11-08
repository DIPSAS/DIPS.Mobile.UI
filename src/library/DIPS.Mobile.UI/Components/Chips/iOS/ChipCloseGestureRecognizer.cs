using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.Components.Chips;

public class ChipCloseGestureRecognizer : UITapGestureRecognizer
{
    private readonly ChipHandler m_chipHandler;

    public ChipCloseGestureRecognizer(ChipHandler chipHandler)
    {
        m_chipHandler = chipHandler;
    }


    public override void TouchesBegan(NSSet touches, UIEvent evt)
    {
        base.TouchesBegan(touches, evt);
        if (!m_chipHandler.VirtualView.IsCloseable)
        {
            m_chipHandler.OnChipTapped();
            return;
        }

        var firstObject = touches.First();
        if (firstObject is not UITouch uiTouch) return;
        var uiButton = m_chipHandler.PlatformView;
        var touchLocationInView = uiTouch.LocationInView(uiButton);
        var didTouchInsideImage =
            touchLocationInView.X > uiButton.TitleLabel.Frame.X + uiButton.TitleLabel.Frame.Width;

        if (didTouchInsideImage)
        {
            m_chipHandler.OnCloseTapped();
        }
        else
        {
            m_chipHandler.OnChipTapped();
        }
    }
        
}