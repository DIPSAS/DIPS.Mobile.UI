namespace DIPS.Mobile.UI.Components.ContextMenus;

/// <summary>
/// A context menu item to use in a context menu
/// </summary>
public partial class ContextMenuItem : Element, IContextMenuItem
{
    internal async void SendClicked(ContextMenu contextMenu)
    {

        
        contextMenu.SendClicked(this);
        Command?.Execute(CommandParameter);
#if __IOS__
        // We need a small delay to make sure that something is not presented on top of the context menu's UIViewController, which breaks the app.
        // This can happen in release, where everything runs fast.
        await Task.Delay(10);
#endif
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