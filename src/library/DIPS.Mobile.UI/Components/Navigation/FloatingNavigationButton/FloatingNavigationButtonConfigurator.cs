using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;

internal class FloatingNavigationButtonConfigurator : IFloatingNavigationButtonConfigurator
{
    public ObservableCollection<ExtendedNavigationMenuButton.ExtendedNavigationMenuButton> NavigationMenuButtons
    {
        get;
    } = new();

    public List<Type> PagesThatHidesButton { get; } = new();
    
    public void AddNavigationButton(string identifier, string title, IconName iconName, ICommand command)
    {
        NavigationMenuButtons.Add(new ExtendedNavigationMenuButton.ExtendedNavigationMenuButton
        {
            AutomationId = identifier,
            Title = title,
            Icon = Icons.GetIcon(iconName),
            Command = command
        });
    }

    public void AddPageThatHidesButton(Type page)
    {
        PagesThatHidesButton.Add(page);
    }
}