using System.Windows.Input;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.ListItems;

public partial class NavigationListItem
{
    

    public ICommand? Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public static readonly BindableProperty CustomContentItemProperty = BindableProperty.Create(
        nameof(CustomContentItem),
        typeof(IView),
        typeof(NavigationListItem), propertyChanged: ((bindable, value, newValue) =>
        {
            if (bindable is not NavigationListItem navigationListItem) return;
            navigationListItem.OnCustomContentItemPropertyChanged();
        }));

    /// <summary>
    /// A custom content that is placed on the left side of the navigation icon
    /// </summary>
    public IView? CustomContentItem
    {
        get => (IView)GetValue(CustomContentItemProperty);
        set => SetValue(CustomContentItemProperty, value);
    }

    public ImageSource? Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public Color IconColor
    {
        get => (Color)GetValue(IconColorProperty);
        set => SetValue(IconColorProperty, value);
    }
    
    public static readonly BindableProperty IconColorProperty = BindableProperty.Create(
        nameof(IconColor),
        typeof(Color),
        typeof(NavigationListItem),
        defaultValue: Colors.GetColor(ColorName.color_system_black));

    
    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon),
        typeof(ImageSource),
        typeof(NavigationListItem));
    
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        nameof(CommandParameter),
        typeof(object),
        typeof(NavigationListItem));
    
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(NavigationListItem));
    
    public event EventHandler? Tapped;
}