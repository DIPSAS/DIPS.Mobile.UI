using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.ContextMenus;

/// <summary>
/// A shared abstraction of the native context menu
/// </summary>
public interface IContextMenu
{
    /// <summary>
    /// The title of the context menu button
    /// </summary>
    public string Title { get; set; }
        
    /// <summary>
    /// The context menu items
    /// </summary>
    public IList<ContextMenuItem>? ItemsSource { get; }

    /// <summary>
    /// Command that gets invoked with a paramter when a <see cref="ContextMenuItem"/> was clicked by the user
    /// </summary>
    public ICommand? ItemClickedCommand { get; set; }
    
    /// <summary>
    /// Invoked when a item was clicked from the context menu
    /// </summary>
    public event EventHandler? DidClickItem;

    /// <summary>
    /// <see cref="ContextMenuHorizontalOptions"/>
    /// </summary>
    public ContextMenuHorizontalOptions ContextMenuHorizontalOptions { get; set; }
}
    
/// <summary>
/// The horizontal options for the context menu
/// </summary>
public enum ContextMenuHorizontalOptions
{
    /// <summary>
    /// Position the menu to the right of the content to attach a context menu to
    /// </summary>
    Right = 0,
    /// <summary>
    /// Position the menu to the left of the content to attach a context menu to
    /// </summary>
    Left
}