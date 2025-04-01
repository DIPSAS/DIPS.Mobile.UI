namespace DIPS.Mobile.UI.Components.ContextMenus;

/// <summary>
/// The Android idea: https://developer.android.com/develop/ui/views/components/menus#PopupMenu
/// The iOS idea: https://developer.apple.com/design/human-interface-guidelines/components/menus-and-actions/context-menus/
/// </summary>

[ContentProperty(nameof(ItemsSource))]
public partial class ContextMenu : Button
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (ItemsSource != null)
        {
            foreach (var c in ItemsSource)
            {
                (c as ContextMenuItem)!.ContextMenu = this;
                (c as Element)!.BindingContext = BindingContext;
            }
        }
    }

    internal void ResetIsCheckedForTheRest(ContextMenuItem contextMenuItem)
    {
        if (ItemsSource == null) return;
        
        if (ItemsSource.Contains(contextMenuItem)) //its on the root, and others on the root should not be resetted
        {
            return;
        }
        
        foreach (var child in ItemsSource)
        {
            if (child == contextMenuItem) //this is the item that should not be reseted
            {
                continue;
            }

            if (child is ContextMenuGroup contextMenuGroup)
            {
                if (contextMenuGroup.ItemsSource == null) return;
                if (!contextMenuGroup.IsCheckable) return; //This means multiple choice mode
                
                if (contextMenuGroup.ItemsSource
                    .Contains(
                        contextMenuItem)) //Its added to a group so we need to uncheck the rest in the group before checking the item
                {
                    foreach (var c in contextMenuGroup.ItemsSource)
                    {
                        if (c != contextMenuItem)
                        {
                            c.IsChecked = false;
                        }
                    }
                    continue;
                }
            }

            if(child is ContextMenuItem menuItem)
                menuItem.IsChecked = false;
        }
    }

    public void SendItemsSourceUpdated()
    {
        ItemsSourceUpdated?.Invoke();
    }

    internal void SendItemPropertiesUpdated()
    {
        ItemPropertiesUpdated?.Invoke();
    }
}