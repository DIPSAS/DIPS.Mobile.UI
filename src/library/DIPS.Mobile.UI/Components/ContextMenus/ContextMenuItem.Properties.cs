using System.ComponentModel;
using System.Windows.Input;
using DIPS.Mobile.UI.API.Builder;

namespace DIPS.Mobile.UI.Components.ContextMenus;

public partial class ContextMenuItem
{
    public static readonly BindableProperty ShouldSendGlobalClickProperty = BindableProperty.Create(
        nameof(ShouldSendGlobalClick),
        typeof(bool),
        typeof(ContextMenuItem), defaultValue: false);

    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(
        nameof(IsChecked),
        typeof(bool),
        typeof(ContextMenuItem));

    /// <summary>
    /// <see cref="Command"/>
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(ContextMenuItem));

    /// <summary>
    /// <see cref="CommandParameter"/>
    /// </summary>
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        nameof(CommandParameter),
        typeof(object),
        typeof(ContextMenuItem));

    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon),
        typeof(ImageSource),
        typeof(ContextMenuItem));

    public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(
        nameof(IsVisible),
        typeof(bool),
        typeof(ContextMenuItem),
        defaultValue: true);

    public static readonly BindableProperty IsDestructiveProperty = BindableProperty.Create(
        nameof(IsDestructive),
        typeof(bool),
        typeof(ContextMenuItem));

    /// <summary>
    /// Whether the menu item should be displayed as destructive, which means red text and icon
    /// </summary>
    public bool IsDestructive
    {
        get => (bool)GetValue(IsDestructiveProperty);
        set => SetValue(IsDestructiveProperty, value);
    }

    /// <summary>
    /// The command to run when the item was clicked
    /// </summary>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// The command parameter to send to the command when the item was clicked
    /// </summary>
    public object CommandParameter
    {
        get => (object)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    /// <summary>
    /// Determines if the <see cref="ContextMenuItem"/> is visible in the context menu
    /// </summary>
    public bool IsVisible
    {
        get => (bool)GetValue(IsVisibleProperty);
        set => SetValue(IsVisibleProperty, value);
    }

    /// <summary>
    /// The clicked event when the item was clicked
    /// </summary>
    public event EventHandler? DidClick;

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(ContextMenuItem));

    /// <summary>
    /// The title of the context menu item
    /// </summary>
    public string? Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty IsCheckableProperty = BindableProperty.Create(
        nameof(IsCheckable),
        typeof(bool),
        typeof(ContextMenuItem));

    /// <summary>
    /// Determines if the native check mark should be added to the item when its tapped
    /// </summary>
    public bool IsCheckable
    {
        get => (bool)GetValue(IsCheckableProperty);
        set => SetValue(IsCheckableProperty, value);
    }

    /// <summary>
    /// Determines if the item should be checked when its opened
    /// </summary>
    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    /// <summary>
    /// The parent of the context menu item
    /// </summary>
    public object? Parent { get; internal set; }

    /// <summary>
    /// <see cref="iOSContextMenuItemOptions"/>
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public iOSContextMenuItemOptions iOSOptions { get; set; } = new();

    /// <summary>
    /// <see cref="AndroidContextMenuItemOptions"/>
    /// </summary>
    public AndroidContextMenuItemOptions AndroidOptions { get; set; } = new();

    /// <summary>
    /// The icon to be used as a image with the context menu item
    /// </summary>
    [TypeConverter(nameof(ImageSourceConverter))]
    public ImageSource? Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Determines if the context menu item should invoke <see cref="DIPSUIOptions.HandleContextMenuGlobalClicks"/>
    /// </summary>
    /// <remarks>Default value : false. If this is left false, but the parent <see cref="ContextMenu.ItemsShouldSendGlobalClicks"/> is true, it will log. If this is set to true but parent <see cref="ContextMenu.ItemsShouldSendGlobalClicks"/> is set to false, it will log.</remarks>
    public bool ShouldSendGlobalClick
    {
        get => (bool)GetValue(ShouldSendGlobalClickProperty);
        set => SetValue(ShouldSendGlobalClickProperty, value);
    }
    
    /// <summary>
    /// The ContextMenu that the item is in
    /// </summary>
    public ContextMenu? ContextMenu { get; set; }
}

/// <summary>
/// The Android specific context menu item options
/// </summary>
public class AndroidContextMenuItemOptions
{
    /// <summary>
    /// Set this to override the Context menu item icon with a Android Resource  
    /// </summary>
    /// <remarks>This can be any resource in your Resources drawable, but you can also check out Android.Resource.Drawable.icon-name which is built in</remarks>
    public string IconResourceName { get; set; } = string.Empty;
}

/// <summary>
/// The iOS specific context menu item options
/// </summary>
public class iOSContextMenuItemOptions
{
    /// <summary>
    /// Set this to override the Context menu item icon with a SF Symbol 
    /// </summary>
    /// <remarks>To see all SF Symbols go to https://developer.apple.com/sf-symbols/</remarks>
    public string SystemIconName { get; set; } = string.Empty;
}