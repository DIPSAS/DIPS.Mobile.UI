using CoreGraphics;
using DIPS.Mobile.UI.Components.ContextMenus.iOS;
using DIPS.Mobile.UI.Effects.Touch;
using Microsoft.Maui.Platform;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.ContextMenus;

public partial class ContextMenuPlatformEffect
{
    private readonly LongPressContextMenuDelegate m_delegate = new();

#nullable disable
    private UIContextMenuInteraction m_interaction;
#nullable restore
    
    private void OnLongPressed()
    {
        m_delegate.Element = Element;
        m_interaction = new UIContextMenuInteraction(m_delegate);
        
        if (ContextMenuEffect.GetIsEnabled(Element))
        {
            Control.AddInteraction(m_interaction);
        }
        
        Element.PropertyChanged += OnLongPressedElementPropertyChanged;
    }

    private void OnLongPressedElementPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName != ContextMenuEffect.IsEnabledProperty.PropertyName)
            return;
        
        var isEnabled = ContextMenuEffect.GetIsEnabled(Element);
        if (isEnabled)
        {
            Control.AddInteraction(m_interaction);
        }
        else
        {
            Control.RemoveInteraction(m_interaction);
        }
    }

    public class LongPressContextMenuDelegate : UIContextMenuInteractionDelegate
    {
        public override void WillEnd(UIContextMenuInteraction interaction, UIContextMenuConfiguration configuration,
            IUIContextMenuInteractionAnimating? animator)
        {
            if (Element is null)
                return;

            Touch.SetIsEnabled(Element, true);
        }
        
        public override UIContextMenuConfiguration? GetConfigurationForMenu(UIContextMenuInteraction interaction, CGPoint location)
        {
            if (Element is null) return null;
            
            Touch.SetIsEnabled(Element, false);

            var contextMenu = ContextMenuEffect.GetMenu(Element);
            
            var dict = ContextMenuHelper.CreateMenuItems(
                contextMenu!.ItemsSource!,
                contextMenu);
            var menu = UIMenu.Create(contextMenu.Title, dict.Select(k => k.Value).ToArray());

            return UIContextMenuConfiguration.Create(null, null, actions => menu);
        }

        public Element? Element { get; set; }

        protected override void Dispose(bool disposing)
        {
            Element = null;
            base.Dispose(disposing);
        }
    }

    private void DisposeLongPressed()
    {
        Element.PropertyChanged -= OnLongPressedElementPropertyChanged;
        Control.RemoveInteraction(m_interaction);
    }
}