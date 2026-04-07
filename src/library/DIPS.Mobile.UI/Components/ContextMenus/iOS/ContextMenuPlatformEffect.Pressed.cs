using System.ComponentModel;
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
        contextMenu.SetPlatformRebuildAction(RebuildMenu);
        
        if (Control is not UIButton uiButton)
        {
            uiButton = CreateOverlayButton();
        }

        m_uiButton = uiButton;
        m_uiButton.ShowsMenuAsPrimaryAction = true;
        m_uiButton.SetTitleColor(Colors.GetColor(ColorName.color_text_link_hover).ToPlatform(), UIControlState.Highlighted);
        
        if (contextMenu.ItemsSource is not null && contextMenu.ItemsSource.Any())
        {
            RebuildMenu();
        }
    }

    private UIButton CreateOverlayButton()
    {
        var uiButton = new UIButton();
        uiButton.TranslatesAutoresizingMaskIntoConstraints = false;
        
        Control.AddSubview(uiButton);
        m_uiButtonToRemove = uiButton;
        
        NSLayoutConstraint.ActivateConstraints([
            uiButton.LeadingAnchor.ConstraintEqualTo(Control.LeadingAnchor),
            uiButton.BottomAnchor.ConstraintEqualTo(Control.BottomAnchor),
            uiButton.TrailingAnchor.ConstraintEqualTo(Control.TrailingAnchor),
            uiButton.HeightAnchor.ConstraintEqualTo(Control.HeightAnchor)
        ]);

        if (Element is VisualElement visualElement)
        {
            uiButton.Enabled = visualElement.IsEnabled;
        }
        
        Element.PropertyChanged += ElementOnPropertyChanged;

        return uiButton;
    }

    private void ElementOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if(Element is not VisualElement visualElement)
            return;

        if(e.PropertyName == nameof(visualElement.IsEnabled))
            m_uiButton.Enabled = visualElement.IsEnabled;
    }

    private void RebuildMenu()
    {
        if (m_contextMenu == null)
        {
            return;
        }

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
        
        m_contextMenu.SetPlatformRebuildAction(null);
        
        if(Element is not null)
            Element.PropertyChanged -= ElementOnPropertyChanged;
    }
}