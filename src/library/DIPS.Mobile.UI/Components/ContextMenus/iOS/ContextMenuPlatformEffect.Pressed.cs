using CoreGraphics;
using DIPS.Mobile.UI.Components.ContextMenus.iOS;
using DIPS.Mobile.UI.Resources.Colors;
using Foundation;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.ContextMenus;

public partial class ContextMenuPlatformEffect
{
    
#nullable disable
    private NSObject m_didEnterBackgroundNotification;
#nullable restore
    
    private async Task SetupPressedMode()
    {
        if (Control is not UIButton uiButton)
        {
            uiButton = await CreateOverlayButton();
        }
        m_uiButton = uiButton;
        UpdateMenuForNextTimeItOpens();
        m_uiButton.ShowsMenuAsPrimaryAction = true;
        m_uiButton.SetTitleColor(Colors.GetColor(ColorName.color_primary_90).ToPlatform(), UIControlState.Highlighted);
        
        //Recreate the menu to close it, and to make it possible to re-open it in one tap after it went to the background
        m_didEnterBackgroundNotification = NSNotificationCenter.DefaultCenter.AddObserver(
            UIApplication.DidEnterBackgroundNotification, delegate
            {
                m_uiButton.Menu = null;
                UpdateMenuForNextTimeItOpens();
            });
    }

    private async Task<UIButton> CreateOverlayButton()
    {
        var uiButton = new UIButton();
        // Wait for layout change
        await Task.Delay(300);
        Control.AddSubview(uiButton);
        uiButton.Frame = new CGRect(0,0, Control.Frame.Width, Control.Frame.Height); //X and Y is not relevant as it is added to the Control subview
        m_uiButtonToRemove = uiButton;

        return uiButton;
    }

    private void UpdateMenuForNextTimeItOpens()
    {
        if (m_contextMenu == null)
        {
            return;
        }

        var dict = ContextMenuHelper.CreateMenuItems(
            m_contextMenu.ItemsSource!,
            m_contextMenu, UpdateMenuForNextTimeItOpens);
        m_uiButton.Menu =  UIMenu.Create(m_contextMenu.Title, dict.Select(k => k.Value).ToArray());
    }


    private void DisposePressed()
    {
        if(m_uiButtonToRemove != null)
            Control.WillRemoveSubview(m_uiButtonToRemove);
        
        NSNotificationCenter.DefaultCenter.RemoveObserver(m_didEnterBackgroundNotification);
    }
}