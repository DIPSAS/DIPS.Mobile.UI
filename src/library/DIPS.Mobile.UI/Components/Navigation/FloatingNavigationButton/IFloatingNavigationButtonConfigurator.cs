using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;

public interface IFloatingNavigationButtonConfigurator
{
    void AddNavigationButton(string identifier, string title, IconName iconName, ICommand command, bool isLast = false);
}