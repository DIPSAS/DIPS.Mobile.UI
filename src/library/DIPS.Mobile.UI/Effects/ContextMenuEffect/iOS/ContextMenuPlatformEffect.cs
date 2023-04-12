using CoreGraphics;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Effects.ContextMenuEffect;

public partial class ContextMenuPlatformEffect
{
    
    protected override async partial void OnAttached()
    {
        if (Control is not UIButton uiButton)
        {
            uiButton = new UIButton();
            await Task.Delay(300);
            Control.AddSubview(uiButton);
            uiButton.Frame = Control.Frame;
        }

        var test = UIAction.Create("Test", null, null, action => { });

        var menu = UIMenu.Create("Hoho", new UIMenuElement[] { test });
        uiButton.Menu = menu;
        uiButton.ShowsMenuAsPrimaryAction = true;
    }

    protected override partial void OnDetached()
    {
        
    }
}