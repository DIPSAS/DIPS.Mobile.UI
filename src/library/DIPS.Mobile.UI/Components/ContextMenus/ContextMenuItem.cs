namespace DIPS.Mobile.UI.Components.ContextMenus;

/// <summary>
/// A context menu item to use in a context menu
/// </summary>
public partial class ContextMenuItem : Element, IContextMenuItem
{
    internal void SendClicked(ContextMenu contextMenu)
    {
        contextMenu.SendClicked(this);
        Command?.Execute(CommandParameter);
        DidClick?.Invoke(this, EventArgs.Empty);
        
        var contextMenuLoggingMetadata = new GlobalContextMenuClickMetadata(this, contextMenu);
        var didSendGlobalClick = false;
        if (contextMenu.ItemsShouldSendGlobalClicks) //If consumer wants to send clicks globally for all items
        {
            ContextMenuEffect.ContextMenuItemGlobalClicksCallBack?.Invoke(contextMenuLoggingMetadata);
            didSendGlobalClick = true;
        }
        
        if (ShouldSendGlobalClick && !didSendGlobalClick) //If consumer wants a single item to log, no matter what parent says
        {
            ContextMenuEffect.ContextMenuItemGlobalClicksCallBack?.Invoke(contextMenuLoggingMetadata);    
        }
    }

    public void Dispose()
    {
        DidClick = null;
    }
}