using CoreGraphics;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Helpers.ContextMenus.iOS;
using UIKit;

namespace DIPS.Mobile.UI.Effects.PopoverEffect;

public partial class PopoverPlatformEffect
{

#nullable disable
    private UIContextMenuInteraction m_interaction;
    private ContextMenu m_contextMenu;
#nullable restore
    
    protected override partial void OnAttached()
    {
        m_contextMenu = PopoverEffect.GetItemsSource(Element);
        
        if(m_contextMenu == null)
            return;

        m_contextMenu.BindingContext = Element.BindingContext;
        
        m_interaction = new UIContextMenuInteraction(new PopoverContextMenuConfiguration(m_contextMenu));
        Control.AddInteraction(m_interaction);
    }

    protected override partial void OnDetached()
    {
        Control.RemoveInteraction(m_interaction);
    }
    
    public class PopoverContextMenuConfiguration : UIContextMenuInteractionDelegate
    {
        private readonly ContextMenu m_contextMenu;

        public PopoverContextMenuConfiguration(ContextMenu contextMenu)
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
}