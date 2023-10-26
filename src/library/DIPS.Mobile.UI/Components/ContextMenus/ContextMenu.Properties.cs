using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.ContextMenus;

public partial class ContextMenu : IContextMenu
{
    public event Action? ItemsSourceUpdated;
    
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
    /// 
    /// </summary>
    public event EventHandler? DidClickItem;

    internal void SendClicked(ContextMenuItem item)
    {
        ItemClickedCommand?.Execute(item);
        DidClickItem?.Invoke(item, EventArgs.Empty);
    }
}