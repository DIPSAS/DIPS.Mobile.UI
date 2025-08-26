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
            Task.Run(() =>
            {
                ContextMenuEffect.ContextMenuItemGlobalClicksCallBack?.Invoke(contextMenuLoggingMetadata);
            });
            didSendGlobalClick = true;
        }
        
        if (ShouldSendGlobalClick && !didSendGlobalClick) //If consumer wants a single item to log, no matter what parent says
        {
            Task.Run(() =>
            {
                ContextMenuEffect.ContextMenuItemGlobalClicksCallBack?.Invoke(contextMenuLoggingMetadata);
            });
        }
    }

#if __IOS__
    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        
        if(propertyName == IsVisibleProperty.PropertyName || 
           propertyName == IconProperty.PropertyName ||
           propertyName == TitleProperty.PropertyName ||
           propertyName == IsCheckedProperty.PropertyName || 
           propertyName == IsDestructiveProperty.PropertyName)
        {
            ContextMenu?.SendItemPropertiesUpdated();
        }
    }
#endif

    public void Dispose()
    {
        DidClick = null;
    }
}