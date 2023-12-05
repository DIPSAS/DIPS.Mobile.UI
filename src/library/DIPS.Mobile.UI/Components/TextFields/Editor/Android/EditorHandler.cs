using Android.Graphics.Drawables;
using AndroidX.AppCompat.Widget;
using DIPS.Mobile.UI.Components.BottomSheets.Android;
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
        
        var activity = Platform.CurrentActivity;
        var fragment = activity?.GetFragmentManager()?.FindFragmentByTag(nameof(BottomSheetFragment));
        if (fragment is BottomSheetFragment bottomSheetFragment)
        {
            bottomSheetFragment.AttachInputView((VirtualView as InputView)!);
        }

        platformView.FocusChange += OnFocusChanged;
    }

    private void OnFocusChanged(object? sender, View.FocusChangeEventArgs e)
    {
        if (m_firstTimeFocus)
        {
            if (PlatformView.Text != null)
            {
                PlatformView.SetSelection(PlatformView.Text.Length);
            }

            m_firstTimeFocus = false;
        }
        
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