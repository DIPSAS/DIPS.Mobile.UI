using CoreGraphics;
using UIKit;

namespace DIPS.Mobile.UI.Effects.PopoverEffect;

public partial class PopoverPlatformEffect
{
    private UIContextMenuInteraction m_interaction; 
    
    protected override partial void OnAttached()
    {
        var itemsSource = PopoverEffect.GetItemsSource(Element);
        
        m_interaction = new UIContextMenuInteraction(new PopoverContextMenuConfiguration(itemsSource));
        Control.AddInteraction(m_interaction);
    }

    protected override partial void OnDetached()
    {
        Control.RemoveInteraction(m_interaction);
    }
    
    public class PopoverContextMenuConfiguration : UIContextMenuInteractionDelegate
    {
        private readonly BindableObject[] m_itemsSource;

        public PopoverContextMenuConfiguration(BindableObject[] itemsSource)
        {
            m_itemsSource = itemsSource;
        }
        
        public override UIContextMenuConfiguration? GetConfigurationForMenu(UIContextMenuInteraction interaction, CGPoint location)
        {
            var test = UIAction.Create("Test", null, null, action => { });

            var menu = UIMenu.Create("Hoho", new UIMenuElement[] { test });
        
            return UIContextMenuConfiguration.Create(null, null, actions => menu);
        }

        protected override void Dispose(bool disposing)
        {
            
        }
    }
}