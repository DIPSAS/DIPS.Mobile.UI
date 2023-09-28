using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Shell.Android;

public class ShellRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.ShellRenderer
{
    protected override IShellToolbarAppearanceTracker CreateToolbarAppearanceTracker() {
        return new CustomToolbarAppearanceTracker();
    }
}

internal class CustomToolbarAppearanceTracker : IShellToolbarAppearanceTracker
{
    public void Dispose() {
    }

    public void SetAppearance(AndroidX.AppCompat.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
    {
        for (var i = 0; i < toolbar.Menu?.Size(); i++)
        {
            var toolbarItem = toolbar.Menu.GetItem(i);
            
            toolbarItem!.SetIconTintList(appearance.ForegroundColor.ToDefaultColorStateList());
        }
    }

    public void ResetAppearance(AndroidX.AppCompat.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker)
    {
    }
}