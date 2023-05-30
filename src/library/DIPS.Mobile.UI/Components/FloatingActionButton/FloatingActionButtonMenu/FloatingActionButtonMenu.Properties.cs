using System.Collections.ObjectModel;

namespace DIPS.Mobile.UI.Components.FloatingActionButton.FloatingActionButtonMenu;

public partial class FloatingActionButtonMenu
{

    // Pages inside this list will hide the NavigationButton
    public List<Type> PagesNotContaining
    {
        get => (List<Type>)GetValue(PagesNotContainingProperty);
        set => SetValue(PagesNotContainingProperty, value);
    }

    public ObservableCollection<NavigationMenuButton.NavigationMenuButton> NavigationMenuButtons
    {
        get => (ObservableCollection<NavigationMenuButton.NavigationMenuButton>)GetValue(NavigationMenuButtonsProperty);
        set => SetValue(NavigationMenuButtonsProperty, value);
    }
    
    public static readonly BindableProperty NavigationMenuButtonsProperty = BindableProperty.Create(
        nameof(NavigationMenuButtons),
        typeof(ObservableCollection<NavigationMenuButton.NavigationMenuButton>),
        typeof(FloatingActionButtonMenu),
        new ObservableCollection<NavigationMenuButton.NavigationMenuButton>(),
        propertyChanged: OnNavigationMenuButtonsChanged);
    
    public static readonly BindableProperty PagesNotContainingProperty = BindableProperty.Create(
        nameof(PagesNotContaining),
        typeof(List<Type>),
        typeof(FloatingActionButtonMenu),
        defaultValue:new List<Type>());
}