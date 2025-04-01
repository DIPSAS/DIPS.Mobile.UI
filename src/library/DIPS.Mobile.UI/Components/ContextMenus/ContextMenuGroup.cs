namespace DIPS.Mobile.UI.Components.ContextMenus;

/// <summary>
/// A context menu group with multiple items
/// </summary>
[ContentProperty(nameof(ItemsSource))]
    
public partial class ContextMenuGroup : ContextMenuItem
{
        
    /// <summary>
    /// <inheritdoc cref="BindableObject"/>
    /// </summary>
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        
        if (ItemsSource != null)
        {
            foreach (var c in ItemsSource)
            {
                c.ContextMenu = ContextMenu;
                c.BindingContext = BindingContext;
            }
        }
    }
}