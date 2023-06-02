using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingNavigationButton;

public interface IFloatingNavigationButtonConfigurator
{
    void OnBadgeCountChanged(int badgeCount);
    void AddNavigationButton(string identifier, string title, IconName iconName, ICommand command);
    void AddPageThatHidesButton(Type page);
}