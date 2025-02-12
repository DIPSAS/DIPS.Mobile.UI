using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.TextFields.Editor;

public partial class EditorHandler
{
    protected override void ConnectHandler(MauiTextView platformView)
    {
        base.ConnectHandler(platformView);

        platformView.VerticalTextAlignment = TextAlignment.Start;
        platformView.Started += OnFocus;
    }

    private static partial void MapShouldUseDefaultPadding(EditorHandler handler, Editor editor)
    {
        if(editor.ShouldUseDefaultPadding)
            return;

        handler.PlatformView.TextContainer.LineFragmentPadding = 0;
        handler.PlatformView.TextContainerInset = new UIEdgeInsets(1f, 0f, 1f, 0f);
    }


    private async void OnFocus(object? sender, EventArgs e)
    {
        if (m_firstTimeFocus)
        {
            PlatformView.SelectedTextRange = PlatformView.GetTextRange(PlatformView.EndOfDocument, PlatformView.EndOfDocument);

            m_firstTimeFocus = false;
        }
        
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