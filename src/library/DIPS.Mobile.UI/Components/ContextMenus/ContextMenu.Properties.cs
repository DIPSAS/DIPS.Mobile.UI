using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.ContextMenus;

public partial class ContextMenu : IContextMenu
{
    public event Action? ItemsSourceUpdated;
#if __IOS__
    internal event Action? ItemPropertiesUpdated;
#endif

    public static readonly BindableProperty ItemsShouldSendGlobalClicksProperty = BindableProperty.Create(
        nameof(ItemsShouldSendGlobalClicks),
        typeof(bool),
        typeof(ContextMenu), defaultValue:false);
    
    /// <see cref="Title"/>
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(ContextMenuItem), defaultValue: string.Empty);

    /// <summary>
    /// The title of the context menu button
    /// </summary>
    /// <remarks>Only works on iOS</remarks>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// <see cref="ItemClickedCommand"/>
    /// </summary>
    public static readonly BindableProperty ItemClickedCommandProperty = BindableProperty.Create(
        nameof(ItemClickedCommand),
        typeof(ICommand),
        typeof(ContextMenu));

    /// <summary>
    /// <see cref="ItemsSource"/>
    /// </summary>
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource),
        typeof(IList<IContextMenuItem>),
        typeof(ContextMenu),
        defaultValueCreator:(bindable => new List<IContextMenuItem>()));

    /// <summary>
    /// The context menu items to display in the context menu when its opened
    /// </summary>
    public IList<IContextMenuItem>? ItemsSource
    {
        get => (IList<IContextMenuItem>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    /// <summary>
    /// Command that gets invoked with a parameter when a <see cref="ContextMenuItem"/> was clicked by the user
    /// </summary>
    public ICommand? ItemClickedCommand
    {
        get => (ICommand)GetValue(ItemClickedCommandProperty);
        set => SetValue(ItemClickedCommandProperty, value);
    }
    
    /// <summary>
    /// Determines if all items in the menu should log when they are clicked.
    /// </summary>
    /// <remarks>Default value : false</remarks>
    public bool ItemsShouldSendGlobalClicks
    {
        get => (bool)GetValue(ItemsShouldSendGlobalClicksProperty);
        set => SetValue(ItemsShouldSendGlobalClicksProperty, value);
    }
    

    /// <summary>
    /// <see cref="ContextMenuHorizontalOptions"/>
    /// </summary>
    public static readonly BindableProperty ContextMenuHorizontalOptionsProperty = BindableProperty.Create(
        nameof(ContextMenuHorizontalOptions),
        typeof(ContextMenuHorizontalOptions),
        typeof(ContextMenu));

    /// <summary>
    /// <see cref="ContextMenuHorizontalOptions"/>
    /// </summary>
    public ContextMenuHorizontalOptions ContextMenuHorizontalOptions
    {
        get => (ContextMenuHorizontalOptions)GetValue(ContextMenuHorizontalOptionsProperty);
        set => SetValue(ContextMenuHorizontalOptionsProperty, value);
    }
    
    /// <summary>
    /// Get the mode of the context menu.
    /// </summary>
    /// <remarks>This is non-settable on this class. <a href="https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/Context-Menus#modes"> See documentation on how to set it.</a></remarks>
    public ContextMenuEffect.ContextMenuMode Mode { get; internal set; }

    /// <summary>
    /// Event that gets raised when a <see cref="ContextMenuItem"/> was clicked by the user.
    /// </summary>
    public event EventHandler? DidClickItem;

    internal void SendClicked(ContextMenuItem item)
    {
        ItemClickedCommand?.Execute(item);
        DidClickItem?.Invoke(item, EventArgs.Empty);
    }
}