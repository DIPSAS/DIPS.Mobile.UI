using System.ComponentModel;
using CoreGraphics;
using DIPS.Mobile.UI.Components.ContextMenus.iOS;
using DIPS.Mobile.UI.Effects.Touch.iOS;
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
        var isEnabled = ContextMenuEffect.GetIsEnabled(Element);
        m_uiButton.ShowsMenuAsPrimaryAction = isEnabled;
        m_uiButton.SetTitleColor(Colors.GetColor(ColorName.color_text_link_hover).ToPlatform(), UIControlState.Highlighted);
        
        if (contextMenu.ItemsSource is not null && contextMenu.ItemsSource.Any())
        {
            RebuildMenu();
        }
    }

    private UIButton CreateOverlayButton()
    {
        var uiButton = new ContextMenuButton(this)
        {
            TranslatesAutoresizingMaskIntoConstraints = false
        };

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
        
        if (e.PropertyName == ContextMenuEffect.IsEnabledProperty.PropertyName)
        {
            var isEnabled = ContextMenuEffect.GetIsEnabled(Element);
            m_uiButton.ShowsMenuAsPrimaryAction = isEnabled;
            m_uiButton.Menu = isEnabled ? m_cachedMenu : null;
        }
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
        m_cachedMenu = UIMenu.Create(m_contextMenu.Title, dict.Select(k => k.Value).ToArray());
        if (ContextMenuEffect.GetIsEnabled(Element))
        {
            m_uiButton.Menu = m_cachedMenu;
        }
    }

    private void DisposePressed()
    {
        m_uiButton?.Menu?.Dispose();
        if(m_uiButton?.Menu is not null)
            m_uiButton.Menu = null;
        m_cachedMenu?.Dispose();
        m_cachedMenu = null;
        m_pressedMenuTouchBlocker?.Dispose();
        m_pressedMenuTouchBlocker = null;
        m_uiButtonToRemove?.RemoveFromSuperview();
        
        m_contextMenu.SetPlatformRebuildAction(null);
        
        if(Element is not null)
            Element.PropertyChanged -= ElementOnPropertyChanged;
    }

    private sealed class ContextMenuButton(ContextMenuPlatformEffect effect) : UIButton
    {
        private readonly WeakReference<ContextMenuPlatformEffect> m_effectReference = new(effect);

        public override void WillDisplayMenu(UIContextMenuInteraction interaction,
            UIContextMenuConfiguration configuration, IUIContextMenuInteractionAnimating? animator)
        {
            if (m_effectReference.TryGetTarget(out var contextMenuPlatformEffect))
            {
                contextMenuPlatformEffect.m_pressedMenuTouchBlocker ??= TouchOverlayTouchBlocker.Block();
            }

            base.WillDisplayMenu(interaction, configuration, animator);
        }

        public override void WillEnd(UIContextMenuInteraction interaction, UIContextMenuConfiguration configuration,
            IUIContextMenuInteractionAnimating? animator)
        {
            if (m_effectReference.TryGetTarget(out var contextMenuPlatformEffect))
            {
                contextMenuPlatformEffect.DisposePressedMenuTouchBlocker();
            }

            base.WillEnd(interaction, configuration, animator);
        }
    }

    private void DisposePressedMenuTouchBlocker()
    {
        m_pressedMenuTouchBlocker?.Dispose();
        m_pressedMenuTouchBlocker = null;
    }
}