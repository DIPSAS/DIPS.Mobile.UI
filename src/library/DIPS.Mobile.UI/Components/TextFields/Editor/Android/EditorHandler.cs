using Android.Graphics.Drawables;
using Android.Graphics.Text;
using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.TextFields.Editor;

public partial class EditorHandler
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
        PlatformView.SetBackground(((VirtualView as Editor)!).HasBorder ? DefaultBackground : null);
    }

    private static partial void MapShouldSelectTextOnTapped(EditorHandler handler, Editor entry)
    {
        handler.PlatformView.SetSelectAllOnFocus(entry.ShouldSelectAllTextOnFocused);
    }

    private static async partial void MapHasBorder(EditorHandler handler, Editor entry)
    {
        await Task.Delay(1);
        handler.PlatformView.SetBackground(entry.HasBorder ? handler.DefaultBackground : null);
    }
    
    private static partial void MapShouldUseDefaultPadding(EditorHandler handler, Editor editor)
    {
        if(editor.ShouldUseDefaultPadding)
            return;
        
        handler.PlatformView.SetPadding(0, 0, 0, 0);
    }

    protected override void DisconnectHandler(AppCompatEditText platformView)
    {
        base.DisconnectHandler(platformView);
        
        platformView.FocusChange -= OnFocusChanged;
    }
}