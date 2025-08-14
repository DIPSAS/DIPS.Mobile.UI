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

        var bindingContext = ContextMenuEffect.GetMenuBindingContext(Element);
        m_contextMenu.BindingContext = bindingContext ?? Element.BindingContext;
        
        m_mode = ContextMenuEffect.GetMode(Element);
        m_contextMenu.Mode = m_mode;
        
        if (m_mode == ContextMenuEffect.ContextMenuMode.Pressed)
        {
            SetupPressedMode(m_contextMenu);
        }
        else
        {
            OnLongPressed();
        }
    }

    protected override partial void OnDetached()
    {
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