using Android.Views;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Shell.Android;

public class ShellRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.ShellRenderer
{
    protected override IShellToolbarAppearanceTracker CreateToolbarAppearanceTracker() => ToolbarApperanceTrancer = new CustomToolbarAppearanceTracker(this);

    internal CustomToolbarAppearanceTracker ToolbarApperanceTrancer { get; set; }
}

internal class CustomToolbarAppearanceTracker : ShellToolbarAppearanceTracker
{
    private ShellAppearance? m_appearance;

    public Toolbar Toolbar { get; set; }
    public override void SetAppearance(Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
    {
        base.SetAppearance(toolbar, toolbarTracker, appearance);

        Toolbar = toolbar;
        m_appearance = appearance;
        
        toolbar.LayoutChange += ToolbarOnLayoutChange;  
        
        SetToolbarItemsTint();
    }

    private void SetToolbarItemsTint()
    {       
        for (var i = 0; i < Toolbar?.Menu?.Size(); i++)
        {
            var toolbarItem = Toolbar.Menu.GetItem(i);
            
            toolbarItem!.SetIconTintList(m_appearance?.ForegroundColor.ToDefaultColorStateList());
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
        
        if(Toolbar is null)
            return;
        
        Toolbar.LayoutChange -= ToolbarOnLayoutChange;  
    }
}