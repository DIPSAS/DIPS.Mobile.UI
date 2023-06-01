namespace DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingNavigationButton;

public partial class FloatingNavigationButtonService
{
    public static void AddFloatingNavigationButton(Action<IFloatingNavigationButtonConfigurator> config)
    {
        var configurator = new FloatingNavigationButtonConfigurator();
        config.Invoke(configurator);

        var fab = new FloatingNavigationButton(configurator);
        AttachToRootWindow(fab);
    }

    internal static partial void AttachToRootWindow(FloatingNavigationButton fab);

    public static partial void Hide();
    
    //public static ExtendedFloatingActionButton.ExtendedFloatingActionButton
}