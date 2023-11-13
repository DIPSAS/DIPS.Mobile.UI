using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.TextFields.Editor;

public partial class EditorHandler
{
    protected override void ConnectHandler(MauiTextView platformView)
    {
        base.ConnectHandler(platformView);

        platformView.VerticalTextAlignment = TextAlignment.Start;
        platformView.Started += OnFocus;
    }

    private async void OnFocus(object? sender, EventArgs e)
    {
        if(!((VirtualView as Editor)!).ShouldSelectAllTextOnFocused)
            return;
        
        await Task.Delay(1);
        PlatformView.SelectAll(null);  
    }

    private static partial void MapShouldSelectTextOnTapped(EditorHandler handler, Editor entry)
    {
    }

    private static partial void MapHasBorder(EditorHandler handler, Editor entry)
    {
    }

    protected override void DisconnectHandler(MauiTextView platformView)
    {
        base.DisconnectHandler(platformView);
        
        platformView.Started -= OnFocus;
    }

}