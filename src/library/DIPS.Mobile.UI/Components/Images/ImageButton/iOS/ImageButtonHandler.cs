using DIPS.Mobile.UI.Components.Images.iOS;
using DIPS.Mobile.UI.Platforms.iOS;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Images.ImageButton;

public partial class ImageButtonHandler
{
    private IconTintColorHandler m_iconTintColorHandler;

    protected override UIButton CreatePlatformView()
    {
        return new UIButtonWithExtraTappableArea
        {
            ClipsToBounds = true
        };
    }

    protected override void ConnectHandler(UIButton platformView)
    {
        base.ConnectHandler(platformView);

        m_iconTintColorHandler = new IconTintColorHandler(VirtualView);
    }

    private static partial void MapAdditionalHitBoxSize(ImageButtonHandler handler, ImageButton imageButton)
    {
        if (handler.PlatformView is UIButtonWithExtraTappableArea uiButton)
            uiButton.AdditionalHitBoxSize = imageButton.AdditionalHitBoxSize;
    }

    protected override void DisconnectHandler(UIButton platformView)
    {
        base.DisconnectHandler(platformView);
        
        m_iconTintColorHandler.Dispose();
    }
}