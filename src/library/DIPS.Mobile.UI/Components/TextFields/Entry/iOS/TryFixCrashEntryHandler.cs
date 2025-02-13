using CoreGraphics;
using DIPS.Mobile.UI.Internal.Logging;
using Microsoft.Maui.Platform;
using ObjCRuntime;
using UIKit;

namespace DIPS.Mobile.UI.Components.TextFields.Entry.iOS;

/// <summary>
/// Can be removed when this issue is resolved: https://github.com/xamarin/xamarin-macios/issues/21648
/// </summary>
public class TryFixCrashEntryHandler : Microsoft.Maui.Handlers.EntryHandler
{
    public TryFixCrashEntryHandler(IPropertyMapper? mapper) : base(mapper)
    {
        
    }

    public TryFixCrashEntryHandler() : base(Mapper)
    {
    }
    
    protected override MauiTextField CreatePlatformView()
    {
        return new TryFixCrashMauiTextField { BorderStyle = UITextBorderStyle.RoundedRect, ClipsToBounds = true };
    }
}

internal class TryFixCrashMauiTextField : MauiTextField
{
    public TryFixCrashMauiTextField(CGRect frame): base(frame)
    {
    }
    
    protected TryFixCrashMauiTextField(NativeHandle nh)
    {
        DUILogService.LogDebug<TryFixCrashMauiTextField>("Prevented crash: https://github.com/xamarin/xamarin-macios/issues/21648");
    }

    public TryFixCrashMauiTextField()
    {
    }
}