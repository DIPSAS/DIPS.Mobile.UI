using Android.Views;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Shell.Android;

public class ShellRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.ShellRenderer
{
    protected override IShellToolbarAppearanceTracker CreateToolbarAppearanceTracker() {
        return new CustomToolbarAppearanceTracker(this);
    }
}

internal class CustomToolbarAppearanceTracker : ShellToolbarAppearanceTracker
{
    private Toolbar m_toolbar;
    private ShellAppearance m_appearance;

    public override void SetAppearance(Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
    {
        base.SetAppearance(toolbar, toolbarTracker, appearance);

        m_toolbar = toolbar;
        m_appearance = appearance;
        toolbar.LayoutChange += ToolbarOnLayoutChange;  
        
        SetToolbarItemsTint();
    }

    private void SetToolbarItemsTint()
    {
        for (var i = 0; i < m_toolbar.Menu?.Size(); i++)
        {
            var toolbarItem = m_toolbar.Menu.GetItem(i);
            
            toolbarItem!.SetIconTintList(m_appearance.ForegroundColor.ToDefaultColorStateList());
        }
    }

    private void ToolbarOnLayoutChange(object? sender, View.LayoutChangeEventArgs e)
    {
        SetToolbarItemsTint();
    }

    public CustomToolbarAppearanceTracker(IShellContext shellContext) : base(shellContext)
    {
        
    }
    
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        
        m_toolbar.LayoutChange -= ToolbarOnLayoutChange;  
    }
}