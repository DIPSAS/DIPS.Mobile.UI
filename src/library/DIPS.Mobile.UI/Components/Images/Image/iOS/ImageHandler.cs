using UIKit;

namespace DIPS.Mobile.UI.Components.Images.Image;

public partial class ImageHandler
{
    private IconTintColorHandler m_iconTintColorHandler;

    protected override void ConnectHandler(UIImageView platformView)
    {
        base.ConnectHandler(platformView);

        m_iconTintColorHandler = new IconTintColorHandler(VirtualView);
    }

    protected override void DisconnectHandler(UIImageView platformView)
    {
        base.DisconnectHandler(platformView);
        
        m_iconTintColorHandler.Dispose();
    }
}