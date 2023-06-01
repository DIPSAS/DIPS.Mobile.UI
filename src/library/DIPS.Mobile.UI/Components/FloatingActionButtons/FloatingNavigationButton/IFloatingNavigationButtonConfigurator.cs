using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingNavigationButton;

public interface IFloatingNavigationButtonConfigurator
{
    void OnBadgeCountChanged(int badgeCount);
    void AddNavigationButton(string title, ICommand command);
    void AddPageThatHidesButton(Type page);
}