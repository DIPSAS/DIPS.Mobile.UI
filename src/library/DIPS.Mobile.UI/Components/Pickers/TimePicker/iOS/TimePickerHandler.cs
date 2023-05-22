using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Platforms.iOS;
using Foundation;
using Microsoft.Maui.Handlers;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerHandler : ViewHandler<TimePicker, UIDatePicker>
{
    protected override UIDatePicker CreatePlatformView() => new() { Mode = UIDatePickerMode.Time, PreferredDatePickerStyle = UIDatePickerStyle.Compact };

    private bool m_isOpen;
    
    protected override void ConnectHandler(UIDatePicker platformView)
    {
        base.ConnectHandler(platformView);
        
        platformView.SetDefaultTintColor();
        platformView.ValueChanged += OnTimeSelected;
        platformView.EditingDidBegin += OnOpen;
        platformView.EditingDidEnd += OnClose;

        DUI.OnRemoveViewsLocatedOnTopOfPage += TryClose;
    }

    private void OnOpen(object? sender, EventArgs e)
    {
        m_isOpen = true;
    }
    
    private void OnClose(object? sender, EventArgs e)
    {
        m_isOpen = false;
    }

    private void TryClose()
    {
        if (!m_isOpen)
            return;
        
        var currentPresentedUiViewController = Platform.GetCurrentUIViewController();
        currentPresentedUiViewController?.DismissViewController(false, null);
    }

    private partial void AppendPropertyMapper()
    {
        TimePickerPropertyMapper.Add(nameof(TimePicker.HorizontalOptions), MapHorizontalOptions);
        TimePickerPropertyMapper.Add(nameof(TimePicker.Background), MapOverrideBackground);
    }

    private static void MapOverrideBackground(TimePickerHandler handler, TimePicker timePicker)
    {
    }

    private static void MapHorizontalOptions(TimePickerHandler handler, TimePicker timePicker)
    {
        handler.PlatformView.SetHorizontalAlignment(timePicker);
    }
    
    private void OnTimeSelected(object? sender, EventArgs e)
    {
        var components = NSCalendar.CurrentCalendar.Components(NSCalendarUnit.Hour | NSCalendarUnit.Minute, PlatformView.Date);
        var timeSpan = new TimeSpan((int)components.Hour, (int)components.Minute, 0);
        VirtualView.SelectedTime = timeSpan;
        VirtualView.SelectedTimeCommand?.Execute(null);
    }

    private static partial void MapSelectedTime(TimePickerHandler handler, TimePicker datePicker)
    {
        var calendar = NSCalendar.CurrentCalendar;
        var components = NSCalendar.CurrentCalendar.Components(NSCalendarUnit.Hour | NSCalendarUnit.Minute, new NSDate());
        components.Hour = datePicker.SelectedTime.Hours;
        components.Minute = datePicker.SelectedTime.Minutes;
        
        handler.PlatformView.SetDate(calendar.DateFromComponents(components), true);
    }

    protected override void DisconnectHandler(UIDatePicker platformView)
    {
        base.DisconnectHandler(platformView);
        
        platformView.ValueChanged -= OnTimeSelected;
        platformView.EditingDidBegin -= OnOpen;
        platformView.EditingDidEnd -= OnClose;

        DUI.OnRemoveViewsLocatedOnTopOfPage -= TryClose;
    }
}