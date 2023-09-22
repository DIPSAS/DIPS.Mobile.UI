using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Layout;

public partial class LayoutHandler : Microsoft.Maui.Handlers.LayoutHandler
{
    protected override void ConnectHandler(LayoutView platformView)
    {
        base.ConnectHandler(platformView);
        DoNotIgnoreSafeArea();
    }

    private void DoNotIgnoreSafeArea()
    {
        if (VirtualView is Microsoft.Maui.Controls.Layout layout)
        {
            layout.IgnoreSafeArea = true;
        }
    }
}