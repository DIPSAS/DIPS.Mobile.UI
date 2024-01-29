using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.ContextMenus;

public partial class ContextMenuPlatformEffect
{
    private UIButton? m_uiButtonToRemove;
    
#nullable disable
    private ContextMenu m_contextMenu;
    private UIButton m_uiButton;
    private ContextMenuEffect.ContextMenuMode m_mode;
#nullable restore
    
    protected override partial void OnAttached()
    {
        m_contextMenu = ContextMenuEffect.GetMenu(Element);
        
        if (m_contextMenu == null)
            return;
        
        m_contextMenu.BindingContext = Element.BindingContext;
        
        m_contextMenu.DidClickItem += ContextMenuOnDidClickItem;

        m_mode = ContextMenuEffect.GetMode(Element);

        if (m_mode == ContextMenuEffect.ContextMenuMode.Pressed)
        {
            _ = SetupPressedMode(m_contextMenu);
        }
        else
        {
            OnLongPressed();
        }
    }

    private void ContextMenuOnDidClickItem(object? sender, EventArgs e)
    {
        if (sender is not ContextMenuItem contextMenuItem) return;
        
        ContextMenuEffect.ContextMenuItemClickedCallback?.Invoke(contextMenuItem);
    }

    protected override partial void OnDetached()
    {
        if (m_contextMenu is not null)
        {
            m_contextMenu.DidClickItem -= ContextMenuOnDidClickItem;
        }
        
        if (m_mode == ContextMenuEffect.ContextMenuMode.Pressed)
        {
            DisposePressed();
        }
        else
        {
            DisposeLongPressed();
        }
    }
}