using Android.App;
using Android.Views;
using Microsoft.Maui.Controls.Platform;
using Application = Android.App.Application;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Effects.PopoverEffect;

public partial class PopoverPlatformEffect
{
    
#nullable disable
    private FloatingContextMenuActivity m_floatingContextMenuActivity;
#nullable restore
    
    protected override partial void OnAttached()
    {
        m_floatingContextMenuActivity = new FloatingContextMenuActivity();
        
        m_floatingContextMenuActivity.RegisterForContextMenu(Control);
    }

    public class FloatingContextMenuActivity : Activity
    {
        public override void OnCreateContextMenu(IContextMenu? menu, View? v, IContextMenuContextMenuInfo? menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);

            menu!.Add(0, v!.Id, 0, "Test");
        }
    }

    protected override partial void OnDetached()
    {
        m_floatingContextMenuActivity.UnregisterForContextMenu(Control);
    }
}