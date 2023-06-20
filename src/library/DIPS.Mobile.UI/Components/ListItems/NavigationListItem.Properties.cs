using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.ListItems;

public partial class NavigationListItem
{
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(NavigationListItem));

    public ICommand? Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        nameof(CommandParameter),
        typeof(object),
        typeof(NavigationListItem));

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
    
    public event EventHandler? Tapped;
}