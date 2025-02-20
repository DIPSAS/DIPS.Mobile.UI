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
        
        // var mode = (ContextMenuEffect.ContextMenuMode)contextMenu.GetValue(ContextMenuEffect.ModeProperty);
        var contextMenuLoggingMetadata = new ContextMenuLoggingMetadata(this, contextMenu);
        var didLog = false;
        if (contextMenu.ItemsShouldLogWhenClicked) //If consumer wants to log for all items
        {
            ContextMenuEffect.ContextMenuItemLoggingCallback?.Invoke(contextMenuLoggingMetadata);
            didLog = true;
        }
        
        if (ShouldLogWhenClicked && !didLog) //If consumer wants a single item to log, no matter what parent says
        {
            ContextMenuEffect.ContextMenuItemLoggingCallback?.Invoke(contextMenuLoggingMetadata);    
        }
    }

    public void Dispose()
    {
        DidClick = null;
    }
}