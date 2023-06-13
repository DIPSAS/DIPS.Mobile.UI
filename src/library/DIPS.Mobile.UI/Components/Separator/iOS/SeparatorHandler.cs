using CoreGraphics;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Separator;

public partial class SeparatorHandler : ViewHandler<Separator, UIView>
{
    protected override UIView CreatePlatformView()
    {
        return new UIView(){ TranslatesAutoresizingMaskIntoConstraints = false};
    }

    protected override void ConnectHandler(UIView platformView)
    {
        base.ConnectHandler(platformView);
      //  platformView.Frame = new CGRect(platformView.Frame.X, platformView.Frame.Y, 300, 1);
      platformView.HeightAnchor.ConstraintEqualTo(1);
      platformView.WidthAnchor.ConstraintEqualTo(1000);
      
          
    }

    public static partial void MapOverrideBackgroundColor(SeparatorHandler handler, Separator separator)
    {
        handler.PlatformView.BackgroundColor = separator.BackgroundColor.ToPlatform();
    }
}