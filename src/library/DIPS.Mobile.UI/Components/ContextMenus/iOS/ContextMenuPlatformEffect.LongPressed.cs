using CoreGraphics;
using DIPS.Mobile.UI.Components.ContextMenus.iOS;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.ContextMenus;

public partial class ContextMenuPlatformEffect
{
    
#nullable disable
    private UIContextMenuInteraction m_interaction;
#nullable restore
    
    private void OnLongPressed()
    {
        m_contextMenu = ContextMenuEffect.GetMenu(Element);
        
        if(m_contextMenu == null)
            return;

        m_contextMenu.BindingContext = Element.BindingContext;
        
        m_interaction = new UIContextMenuInteraction(new LongPressContextMenuConfigurator(m_contextMenu));
        Control.AddInteraction(m_interaction);
    }
    
    public class LongPressContextMenuConfigurator : UIContextMenuInteractionDelegate
    {
        private readonly ContextMenu m_contextMenu;

        public LongPressContextMenuConfigurator(ContextMenu contextMenu)
        {
            m_contextMenu = contextMenu;
        }
        
        public override UIContextMenuConfiguration? GetConfigurationForMenu(UIContextMenuInteraction interaction, CGPoint location)
        {
            var dict = ContextMenuHelper.CreateMenuItems(
                m_contextMenu.ItemsSource!,
                m_contextMenu);
            var menu = UIMenu.Create(m_contextMenu.Title, dict.Select(k => k.Value).ToArray());
        
            return UIContextMenuConfiguration.Create(null, null, actions => menu);
        }

        protected override void Dispose(bool disposing)
        {
            
        }
    }

    private void DisposeLongPressed()
    {
        Control.RemoveInteraction(m_interaction);
    }
}