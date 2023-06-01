using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingNavigationButton;

internal class FloatingNavigationButtonConfigurator : IFloatingNavigationButtonConfigurator
{
    public ObservableCollection<ExtendedFloatingActionButton.ExtendedFloatingActionButton> NavigationMenuButtons
    {
        get;
    } = new();

    public List<Type> PagesThatHidesButton { get; } = new();
    
    public void OnBadgeCountChanged(int badgeCount)
    {
    }

    public void AddNavigationButton(string title, ICommand command)
    {
        NavigationMenuButtons.Add(new ExtendedFloatingActionButton.ExtendedFloatingActionButton{ Title = title, Command = command });        
    }

    public void AddPageThatHidesButton(Type page)
    {
        PagesThatHidesButton.Add(page);
    }
}