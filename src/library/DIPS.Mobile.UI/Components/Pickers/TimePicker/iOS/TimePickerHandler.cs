using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.iOS;
using DIPS.Mobile.UI.Platforms.iOS;
using Foundation;
using Microsoft.Maui.Handlers;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerHandler : ViewHandler<TimePicker, DUIDatePicker>
{
    protected override DUIDatePicker CreatePlatformView() => new() { Mode = UIDatePickerMode.Time, PreferredDatePickerStyle = UIDatePickerStyle.Compact, DateTimePicker = VirtualView };

    private bool m_isOpen;
    
    protected override void ConnectHandler(DUIDatePicker platformView)
    {
        base.ConnectHandler(platformView);
        
        platformView.SetInLineLabelColors();
        platformView.ValueChanged += OnValueChanged;
        platformView.EditingDidBegin += OnOpen;
        platformView.EditingDidEnd += OnClose;

        DUI.OnRemoveViewsLocatedOnTopOfPage += TryClose;
    }

    private void OnValueChanged(object? sender, EventArgs e)
    {
        OnTimeSelected();
    }

    private void OnOpen(object? sender, EventArgs e)
    {
        m_isOpen = true;
        
        OnTimeSelected();
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
    
    private void OnTimeSelected()
    {
        if (VirtualView.IsNullable && VirtualView.SelectedTime == TimeSpan.Zero)
        {
            VirtualView.SelectedTime = DateTime.Now.TimeOfDay;
            VirtualView.SelectedTimeCommand?.Execute(null);
            return;
        }
        
        var components = NSCalendar.CurrentCalendar.Components(NSCalendarUnit.Hour | NSCalendarUnit.Minute, PlatformView.Date);
        var timeSpan = new TimeSpan((int)components.Hour, (int)components.Minute, 0);
        VirtualView.SelectedTime = timeSpan;
        VirtualView.SelectedTimeCommand?.Execute(null);
    }

    private static partial void MapSelectedTime(TimePickerHandler handler, TimePicker timePicker)
    {
        var calendar = NSCalendar.CurrentCalendar;
        var components = NSCalendar.CurrentCalendar.Components(NSCalendarUnit.Hour | NSCalendarUnit.Minute, new NSDate());
        components.Hour = timePicker.SelectedTime.Hours;
        components.Minute = timePicker.SelectedTime.Minutes;
        
        handler.PlatformView.SetDate(calendar.DateFromComponents(components), true);
        handler.PlatformView.UpdatePlaceholders();
    }

    protected override void DisconnectHandler(DUIDatePicker platformView)
    {
        base.DisconnectHandler(platformView);
        
        platformView.DisposeLayer();
        platformView.ValueChanged -= OnValueChanged;
        platformView.EditingDidBegin -= OnOpen;
        platformView.EditingDidEnd -= OnClose;

        DUI.OnRemoveViewsLocatedOnTopOfPage -= TryClose;
    }
}