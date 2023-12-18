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
        m_contextMenu = ContextMenuEffect.GetMenu(Element);
        
        if(m_contextMenu == null)
            return;

        m_contextMenu.BindingContext = Element.BindingContext;

        m_interaction = new UIContextMenuInteraction(m_delegate);
        Control.AddInteraction(m_interaction);
    }

    public class LongPressContextMenuDelegate : UIContextMenuInteractionDelegate
    {
        public override void WillEnd(UIContextMenuInteraction interaction, UIContextMenuConfiguration configuration,
            IUIContextMenuInteractionAnimating? animator)
        {
            if (interaction.View is not MauiView view)
                return; 
            
            Touch.SetIsEnabled((VisualElement)view.View, true);
        }

        public override UIContextMenuConfiguration? GetConfigurationForMenu(UIContextMenuInteraction interaction, CGPoint location)
        {
            if (interaction.View is not MauiView view)
                return null; 
            
            Touch.SetIsEnabled((VisualElement)view.View, false);

            var contextMenu = ContextMenuEffect.GetMenu(((VisualElement)view.View!));
            
            var dict = ContextMenuHelper.CreateMenuItems(
                contextMenu!.ItemsSource!,
                contextMenu);
            var menu = UIMenu.Create(contextMenu.Title, dict.Select(k => k.Value).ToArray());

            return UIContextMenuConfiguration.Create(null, null, actions => menu);
        }
    }

    private void DisposeLongPressed()
    {
        Control.RemoveInteraction(m_interaction);
    }
}