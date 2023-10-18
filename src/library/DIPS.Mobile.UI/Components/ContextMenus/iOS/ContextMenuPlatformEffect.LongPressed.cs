using CoreGraphics;
using DIPS.Mobile.UI.Components.ContextMenus.iOS;
using Foundation;
using ObjCRuntime;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.ContextMenus;

public partial class ContextMenuPlatformEffect
{
    
#nullable disable
    private LongPressInteraction m_interaction;
#nullable restore
    
    private void OnLongPressed()
    {
        m_contextMenu = ContextMenuEffect.GetMenu(Element);
        
        if(m_contextMenu == null)
            return;

        m_contextMenu.BindingContext = Element.BindingContext;

        var delegater = new LongPressContextMenuDelegate();

        m_interaction = new LongPressInteraction(delegater);
        m_interaction.ContextMenu = m_contextMenu;
        
        Control.AddInteraction(m_interaction);
    }

    public class LongPressInteraction : UIContextMenuInteraction
    {
        
        protected LongPressInteraction(NSObjectFlag t) : base(t)
        {
        }

        protected internal LongPressInteraction(NativeHandle handle) : base(handle)
        {
        }

        public LongPressInteraction(IUIContextMenuInteractionDelegate @delegate) : base(@delegate)
        {
        }
        
        public ContextMenu? ContextMenu { get; set; }
    }
    
    public class LongPressContextMenuDelegate : UIContextMenuInteractionDelegate
    {
        public LongPressContextMenuDelegate()
        {
        }

        /// <summary>
        /// DO NOT REMOVE, WILL CRASH IF THIS IS NOT DECLARED
        /// </summary>
        public LongPressContextMenuDelegate(IntPtr intPtr) : base(intPtr)
        {
        }

        /// <summary>
        /// DO NOT REMOVE, WILL CRASH IF THIS IS NOT DECLARED
        /// </summary>
        public LongPressContextMenuDelegate(NativeHandle handle) : base(handle)
        {
        }

        public override UIContextMenuConfiguration? GetConfigurationForMenu(UIContextMenuInteraction interaction, CGPoint location)
        {
            if (interaction is not LongPressInteraction longPressInteraction)
                return new UIContextMenuConfiguration();

            var contextMenu = longPressInteraction.ContextMenu;
            
            var dict = ContextMenuHelper.CreateMenuItems(
                contextMenu!.ItemsSource!,
                contextMenu);
            var menu = UIMenu.Create(contextMenu.Title, dict.Select(k => k.Value).ToArray());
        
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