
using DIPS.Mobile.UI.Components.ContextMenus.iOS;
using Foundation;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.ContextMenus;

public partial class ContextMenuPlatformEffect
{
    
#nullable disable
    private NSObject m_didEnterBackgroundNotification;
#nullable restore
    
    private async Task OnPressed()
    {
        if (Control is not UIButton uiButton)
        {
            uiButton = await CreateOverlayButton();
        }

        uiButton.Menu = CreateMenu();
        uiButton.ShowsMenuAsPrimaryAction = true;
        uiButton.TouchDown += OnTouchDown;

        m_uiButton = uiButton;
        
        //Recreate the menu to close it, and to make it possible to re-open it in one tap after it went to the background
        m_didEnterBackgroundNotification = NSNotificationCenter.DefaultCenter.AddObserver(
            UIApplication.DidEnterBackgroundNotification, delegate
            {
                uiButton.Menu = null;
                uiButton.Menu = CreateMenu(); 
            });
    }

    private async Task<UIButton> CreateOverlayButton()
    {
        var uiButton = new UIButton();
        // Wait for layout change
        await Task.Delay(300);
        Control.AddSubview(uiButton);
        uiButton.Frame = Control.Frame;

        m_uiButtonToRemove = uiButton;

        return uiButton;
    }
    
    private UIMenu? CreateMenu()
    {
        if (m_contextMenu == null)
        {
            return null;
        }

        var dict = ContextMenuHelper.CreateMenuItems(
            m_contextMenu.ItemsSource!,
            m_contextMenu);
        return UIMenu.Create(m_contextMenu.Title, dict.Select(k => k.Value).ToArray());
    }

    private void OnTouchDown(object? sender, EventArgs e)
    {
        m_uiButton.Menu =
            CreateMenu(); //Recreate the menu so the visuals of the items of the menu are able to change between each time the user opens the menu
        m_contextMenu!.SendContextMenuOpened();
    }

    private void DisposePressed()
    {
        if(m_uiButtonToRemove != null)
            Control.WillRemoveSubview(m_uiButtonToRemove);
        
        m_uiButton.TouchDown -= OnTouchDown;
        NSNotificationCenter.DefaultCenter.RemoveObserver(m_didEnterBackgroundNotification);
    }
}