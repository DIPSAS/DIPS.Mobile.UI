using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Shell.Android;

public class ShellRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.ShellRenderer
{
    protected override IShellToolbarAppearanceTracker CreateToolbarAppearanceTracker() {
        return new CustomToolbarAppearanceTracker(this);
    }
}

internal class CustomToolbarAppearanceTracker : ShellToolbarAppearanceTracker
{
    public override void SetAppearance(AndroidX.AppCompat.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
    {
        base.SetAppearance(toolbar, toolbarTracker, appearance);
        
        for (var i = 0; i < toolbar.Menu?.Size(); i++)
        {
            var toolbarItem = toolbar.Menu.GetItem(i);
            
            toolbarItem!.SetIconTintList(appearance.ForegroundColor.ToDefaultColorStateList());
        }
    }

    public CustomToolbarAppearanceTracker(IShellContext shellContext) : base(shellContext)
    {
    }
}