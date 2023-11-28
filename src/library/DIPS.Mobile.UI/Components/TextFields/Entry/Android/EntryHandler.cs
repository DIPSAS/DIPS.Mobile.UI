using Android.Content.Res;
using Android.Graphics.Drawables;
using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;
using Colors = Microsoft.Maui.Graphics.Colors;
using Object = Java.Lang.Object;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.TextFields.Entry;

public partial class EntryHandler 
{
    private Drawable? DefaultBackground { get; set; }
    
    protected override void ConnectHandler(AppCompatEditText platformView)
    {
        base.ConnectHandler(platformView);

        DefaultBackground = platformView.Background;
        
        platformView.FocusChange += OnFocusChanged;
    }

    private void OnFocusChanged(object? sender, View.FocusChangeEventArgs e)
    {
        PlatformView.SetBackground(((VirtualView as Entry)!).HasBorder ? DefaultBackground : null);
    }

    private static partial void MapShouldUseDefaultPadding(EntryHandler handler, Entry entry)
    {
        if(entry.ShouldUseDefaultPadding)
            return;
        
        handler.PlatformView.SetPadding(0, 0, 0, 0);
    }

    private static partial void MapShouldSelectTextOnTapped(EntryHandler handler, Entry entry)
    {
        handler.PlatformView.SetSelectAllOnFocus(entry.ShouldSelectAllTextOnFocused);
    }

    private static async partial void MapHasBorder(EntryHandler handler, Entry entry)
    {
        await Task.Delay(1);
        handler.PlatformView.SetBackground(entry.HasBorder ? handler.DefaultBackground : null);
    }

    protected override void DisconnectHandler(AppCompatEditText platformView)
    {
        base.DisconnectHandler(platformView);
        
        platformView.FocusChange -= OnFocusChanged;
    }
}