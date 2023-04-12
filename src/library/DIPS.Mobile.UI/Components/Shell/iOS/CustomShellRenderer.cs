using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using UIKit;

namespace DIPS.Mobile.UI.Components.Shell.iOS;

public class CustomShellRenderer : ShellRenderer
{
    protected override IShellNavBarAppearanceTracker CreateNavBarAppearanceTracker()
    {
        return new MyCustomAppearanceTracker(base.CreateNavBarAppearanceTracker());
    }
}

public class MyCustomAppearanceTracker : IShellNavBarAppearanceTracker
{
    private readonly IShellNavBarAppearanceTracker m_createNavBarAppearanceTracker;

    public MyCustomAppearanceTracker(IShellNavBarAppearanceTracker createNavBarAppearanceTracker)
    {
        m_createNavBarAppearanceTracker = createNavBarAppearanceTracker;
    }
    public void Dispose()
    {
        m_createNavBarAppearanceTracker.Dispose();
    }

    public void ResetAppearance(UINavigationController controller)
    {
        m_createNavBarAppearanceTracker.ResetAppearance(controller);
    }

    public void SetAppearance(UINavigationController controller, ShellAppearance appearance)
    {
        m_createNavBarAppearanceTracker.SetAppearance(controller, appearance);
    }

    public void UpdateLayout(UINavigationController controller)
    {
        m_createNavBarAppearanceTracker.UpdateLayout(controller);
        var asd = controller.NavigationBar.TopItem.BackButtonTitle;
    }

    public void SetHasShadow(UINavigationController controller, bool hasShadow)
    {
        m_createNavBarAppearanceTracker.SetHasShadow(controller, hasShadow);
    }
}