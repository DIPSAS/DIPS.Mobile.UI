using DIPS.Mobile.UI.Components.TextFields.Entry.iOS;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.TextFields.Entry;

public partial class EntryHandler 
{
    protected override MauiTextField CreatePlatformView()
    {
        var platformEntry = new TryFixCrashMauiTextField();
        
        var accessoryView = new MauiDoneAccessoryView();
        accessoryView.SetDataContext(this);
        accessoryView.SetDoneClicked(OnDoneClicked);
        platformEntry.InputAccessoryView = accessoryView;

        return platformEntry;
    }

    static void OnDoneClicked(object sender)
    {
        if (sender is EntryHandler handler)
        {
            handler.PlatformView.ResignFirstResponder();
            handler.VirtualView.Completed();
        }
    }

    protected override void ConnectHandler(MauiTextField platformView)
    {
        base.ConnectHandler(platformView);

        platformView.EditingDidBegin += OnEditingDidBegin;
    }
    
    private static partial void MapShouldSelectTextOnTapped(EntryHandler handler, Entry entry)
    {
    }

    private static partial void MapShouldUseDefaultPadding(EntryHandler handler, Entry entry)
    {
        
    }

    private async void OnEditingDidBegin(object? sender, EventArgs e)
    {
        if(!((VirtualView as Entry)!).ShouldSelectAllTextOnFocused)
            return;
        
        await Task.Delay(1);
        PlatformView.SelectAll(null);    
    }

    private static partial void MapHasBorder(EntryHandler handler, Entry entry)
    {
        handler.PlatformView.BorderStyle = entry.HasBorder ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;
    }

    protected override void DisconnectHandler(MauiTextField platformView)
    {
        base.DisconnectHandler(platformView);

        platformView.EditingDidBegin -= OnEditingDidBegin;
    }
}