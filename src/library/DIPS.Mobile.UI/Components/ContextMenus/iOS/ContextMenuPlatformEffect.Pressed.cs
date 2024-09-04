using CoreGraphics;
using DIPS.Mobile.UI.Components.ContextMenus.iOS;
using Foundation;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.ContextMenus;

public partial class ContextMenuPlatformEffect
{
    
    private void SetupPressedMode(ContextMenu contextMenu)
    {
        contextMenu.ItemsSourceUpdated += RebuildMenu;
        
        if (Control is not UIButton uiButton)
        {
            uiButton = CreateOverlayButton();
        }

        m_uiButton = uiButton;
        m_uiButton.ShowsMenuAsPrimaryAction = true;
        m_uiButton.SetTitleColor(Colors.GetColor(ColorName.color_primary_90).ToPlatform(), UIControlState.Highlighted);
        
        if (contextMenu.ItemsSource is not null && contextMenu.ItemsSource.Any())
        {
            RebuildMenu();
        }
    }

    private UIButton CreateOverlayButton()
    {
        var uiButton = new UIButton();
        
        Control.AddSubview(uiButton);
        m_uiButtonToRemove = uiButton;

        return uiButton;
    }

    // We need to update the frame of the overlay button when the Control is resized (For example when an Item in ItemPicker is selected, thus the title is changed)
    private async Task UpdateOverlayButtonFrame()
    {
        if (Control is not UIButton)
        {
            // Wait for layout change
            await Task.Delay(300);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                //X and Y is not relevant as it is added to the Control subview
                m_uiButton.Frame = new CGRect(0,0, Control.Frame.Width, Control.Frame.Height); 
            });
        }
    }

    private void RebuildMenu()
    {
        if (m_contextMenu == null)
        {
            return;
        }
        
        _ = UpdateOverlayButtonFrame();

        var dict = ContextMenuHelper.CreateMenuItems(
            m_contextMenu.ItemsSource!,
            m_contextMenu, RebuildMenu);
        m_uiButton.Menu = UIMenu.Create(m_contextMenu.Title, dict.Select(k => k.Value).ToArray());
    }

    private void DisposePressed()
    {
        m_uiButton?.Menu?.Dispose();
        if(m_uiButton?.Menu is not null)
            m_uiButton.Menu = null;
        m_uiButtonToRemove?.RemoveFromSuperview();
        
        m_contextMenu.ItemsSourceUpdated -= RebuildMenu;
    }
}