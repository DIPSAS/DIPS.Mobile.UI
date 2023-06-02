using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingNavigationButton;

public static partial class FloatingNavigationButtonService
{
    private static FloatingNavigationButton? FloatingNavigationButton { get; set; }
    
    public static void AddFloatingNavigationButton(Action<IFloatingNavigationButtonConfigurator> config)
    {
        var configurator = new FloatingNavigationButtonConfigurator();
        config.Invoke(configurator);

        var fab = new FloatingNavigationButton(configurator);
        AttachToRootWindow(fab);

        FloatingNavigationButton = fab;
    }

    private static partial void AttachToRootWindow(FloatingNavigationButton fab);

    public static Task Hide() => FloatingNavigationButton?.Hide() ?? Task.CompletedTask;
    
    public static Task Show() => FloatingNavigationButton?.Show() ?? Task.CompletedTask;

    public static void TryHideOrShowFloatingNavigationButton(ContentPage page)
    {
        FloatingNavigationButton?.TryHideOrShowFloatingNavigationButton(page);
    }

    public static void SetNavigationMenuBadgeCount(string identifier, int value)
    {
        FloatingNavigationButton?.SetNavigationMenuBadgeCount(identifier, value);
    }

    public static void ChangeNavigationMenuButtonBadgeColor(string identifier, Color color)
    {
        FloatingNavigationButton?.ChangeNavigationMenuButtonBadgeColor(identifier, color);
    }

    public static void ChangeBadgeColor(Color color)
    {
        FloatingNavigationButton?.SetBadgeColor(color);
    }

    public static bool ContainsNavigationMenuButton(string identifier)
    {
        return FloatingNavigationButton?.ContainsNavigationMenuButton(identifier) ?? false;
    }

    public static void RemoveNavigationMenuButton(string identifier)
    {
        FloatingNavigationButton?.RemoveNavigationMenuButton(identifier);
    }

    public static void AddNavigationMenuButton(string identifier = "", string title = "", IconName iconName = IconName.arrow_right_s_line, ICommand? command = null, int? index = null)
    {
        FloatingNavigationButton?.AddNavigationMenuButton(new ExtendedNavigationMenuButton.ExtendedNavigationMenuButton
        {
            AutomationId = identifier,
            Title = title,
            Icon = Icons.GetIcon(iconName),
            Command = command ?? new Command(() => {})
        }, index);
    }

    public static void ToggleNavigationButton(string identifier)
    {
        FloatingNavigationButton?.ToggleNavigationButton(identifier);
    }
}