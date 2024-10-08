using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;

public static partial class FloatingNavigationButtonService
{
    private static FloatingNavigationButton? FloatingNavigationButton { get; set; }

    internal const int FloatingNavigationButtonIdentifier = 2910961;
    
    /// <summary>
    /// Adds a <see cref="FloatingNavigationButton"/> to the root window, placed in the bottom right of the screen
    /// </summary>
    /// <param name="config">To configure the <see cref="FloatingNavigationButton"/></param>
    public static void AddFloatingNavigationButton(Action<IFloatingNavigationButtonConfigurator> config)
    {
        var configurator = new FloatingNavigationButtonConfigurator();
        config.Invoke(configurator);

        var fab = new FloatingNavigationButton(configurator);
        try
        {
            DUILogService.LogDebug(nameof(FloatingNavigationButtonService), $"Will attach to root window.");
            AttachToRootWindow(fab);
        }
        catch (Exception e)
        {
            DUILogService.LogError(nameof(FloatingNavigationButtonService), e.Message);
            throw;
        }

        FloatingNavigationButton = fab;
    }

    private static partial void AttachToRootWindow(FloatingNavigationButton fab);

    /// <summary>
    /// Hides the <see cref="FloatingNavigationButton"/>
    /// </summary>
    public static Task Hide(bool shouldAnimate = true) => FloatingNavigationButton?.Hide(shouldAnimate) ?? Task.CompletedTask;
    
    /// <summary>
    /// Shows the <see cref="FloatingNavigationButton"/>
    /// </summary>
    /// <returns></returns>
    public static Task Show(bool shouldAnimate = true) => FloatingNavigationButton?.Show(shouldAnimate) ?? Task.CompletedTask;

    /// <summary>
    /// Sets the badge count of a <see cref="NavigationMenuButton"/>
    /// </summary>
    /// <param name="identifier">Which <see cref="NavigationMenuButton"/> badge count to be changed</param>
    /// <param name="value">The badge count</param>
    public static void SetNavigationMenuBadgeCount(string identifier, int value)
    {
        FloatingNavigationButton?.SetNavigationMenuBadgeCount(identifier, value);
    }

    /// <summary>
    /// Change the badge's color of a <see cref="NavigationMenuButton"/>
    /// </summary>
    /// <param name="identifier">Which <see cref="NavigationMenuButton"/> badge color to be changed</param>
    /// <param name="color">The color to change to</param>
    public static void ChangeNavigationMenuButtonBadgeColor(string identifier, Color color)
    {
        FloatingNavigationButton?.ChangeNavigationMenuButtonBadgeColor(identifier, color);
    }

    /// <summary>
    /// Change the badge color of <see cref="FloatingNavigationButton"/>
    /// </summary>
    /// <param name="color">The color to change to</param>
    public static void ChangeBadgeColor(Color color)
    {
        FloatingNavigationButton?.SetBadgeColor(color);
    }

    /// <summary>
    /// Checks whether the <see cref="FloatingNavigationButton"/> contains the <see cref="NavigationMenuButton"/> or not
    /// </summary>
    /// <param name="identifier">Which <see cref="NavigationMenuButton"/></param>
    /// <returns>Whether the button is contained or not</returns>
    public static bool ContainsNavigationMenuButton(string identifier)
    {
        return FloatingNavigationButton?.ContainsNavigationMenuButton(identifier) ?? false;
    }

    /// <summary>
    /// Removes a <see cref="NavigationMenuButton"/> from <see cref="FloatingNavigationButton"/>
    /// </summary>
    /// <param name="identifier">Which button to remove</param>
    public static void RemoveNavigationMenuButton(string identifier)
    {
        FloatingNavigationButton?.RemoveNavigationMenuButton(identifier);
    }

    /// <summary>
    /// Adds a new <see cref="NavigationMenuButton"/> to the <see cref="FloatingNavigationButton"/>
    /// </summary>
    /// <param name="identifier">Sets the identifier, so we can identify the button later</param>
    /// <param name="title">Sets the button's title</param>
    /// <param name="iconName">Sets the icon the button should have</param>
    /// <param name="command">Sets what should happen when the button is tapped</param>
    /// <param name="index">To what index it should be placed in</param>
    /// <param name="isLast">Indicates that the button should always be last, this will be respected by other buttons getting added</param>
    public static void AddNavigationMenuButton(string identifier = "", string title = "", IconName iconName = IconName.arrow_right_s_line, ICommand? command = null, int? index = null, bool isLast = false)
    {
        FloatingNavigationButton?.AddNavigationMenuButton(new ExtendedNavigationMenuButton.ExtendedNavigationMenuButton
        {
            AutomationId = identifier,
            Title = title,
            Icon = Icons.GetIcon(iconName),
            IsLast = isLast,
            Command = command ?? new Command(() => {})
        }, index);
    }

    /// <summary>
    /// Sets the <see cref="NavigationMenuButton"/> availability; whether it should be enabled or not
    /// </summary>
    /// <param name="identifier">Which <see cref="NavigationMenuButton"/></param>
    /// <param name="isEnabled">Sets the availability</param>
    public static void SetButtonAvailability(string identifier, bool isEnabled)
    {
        FloatingNavigationButton?.SetButtonAvailability(identifier, isEnabled);
    }

    /// <summary>
    /// Checks if a <see cref="NavigationMenuButton"/> is set to IsEnabled or not
    /// </summary>
    /// <param name="identifier">Which <see cref="NavigationMenuButton"/></param>
    /// <returns>The availability of the <see cref="NavigationMenuButton"/></returns>
    public static bool? CheckButtonAvailability(string identifier)
    {
        return FloatingNavigationButton?.CheckButtonAvailability(identifier);
    }

    /// <summary>
    /// Closes the <see cref="FloatingNavigationButton"/> if it is expanded
    /// </summary>
    public static void Close()
    {
        FloatingNavigationButton?.Close();
    }

    /// <summary>
    /// Removes the <see cref="FloatingNavigationButton"/> from the layout
    /// </summary>
    public static void Remove()
    {
        PlatformRemove();
    }

    private static partial void PlatformRemove();

    public static bool IsShowing()
    {
        return FloatingNavigationButton?.IsVisible ?? false;
    }
}