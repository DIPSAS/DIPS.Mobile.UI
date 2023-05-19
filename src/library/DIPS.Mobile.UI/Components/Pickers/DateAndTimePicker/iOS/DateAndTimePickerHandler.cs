using CoreGraphics;
using DIPS.Mobile.UI.Components.Pickers.Platforms.iOS;
using DIPS.Mobile.UI.Platforms.iOS;
using Foundation;
using Microsoft.Maui.Handlers;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePickerHandler : ViewHandler<DateAndTimePicker, UIDatePicker>
{
    protected override UIDatePicker CreatePlatformView()
    {
        return new UIDatePicker
        {
            Mode = UIDatePickerMode.DateAndTime, PreferredDatePickerStyle = UIDatePickerStyle.Compact
        };
    }
    
    protected override void ConnectHandler(UIDatePicker platformView)
    {
        base.ConnectHandler(platformView);

        platformView.ValueChanged += OnDateSelected;
        platformView.SetDefaultTintColor();
    }

    private partial void AppendPropertyMapper()
    {
        DateAndTimePickerPropertyMapper.Add(nameof(DateAndTimePicker.HorizontalOptions), MapHorizontalOptions);
        DateAndTimePickerPropertyMapper.Add(nameof(DateAndTimePicker.Background), MapOverrideBackground);
        DateAndTimePickerPropertyMapper.Add(nameof(DateAndTimePicker.IgnoreLocalTime), MapIgnoreLocalTime);
    }

    private static partial void MapMaximumDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        if(dateAndTimePicker.MaximumDate is null or null)
            return;

        handler.PlatformView.MaximumDate = ((DateTime)dateAndTimePicker.MaximumDate).ConvertDate();
    }

    private static partial void MapMinimumDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        if(dateAndTimePicker.MinimumDate is null or null)
            return;
        
        handler.PlatformView.MinimumDate = ((DateTime)dateAndTimePicker.MinimumDate).ConvertDate();
    }

    private static void MapOverrideBackground(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
    }

    private static void MapHorizontalOptions(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        handler.PlatformView.SetHorizontalAlignment(dateAndTimePicker);
    }

    private void OnDateSelected(object? sender, EventArgs e)
    {
        VirtualView.SelectedDateTime = (DateTime)PlatformView.Date;
        VirtualView.SelectedDateTimeCommand?.Execute(null);
    }

    private static partial void MapIgnoreLocalTime(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        handler.PlatformView.TimeZone = dateAndTimePicker.IgnoreLocalTime ? new NSTimeZone("UTC") : NSTimeZone.LocalTimeZone;
    }

    private static partial void MapSelectedDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        handler.PlatformView.SetDate(dateAndTimePicker.SelectedDateTime.ConvertDate(), true);
    }

    protected override void DisconnectHandler(UIDatePicker platformView)
    {
        base.DisconnectHandler(platformView);

        platformView.ValueChanged -= OnDateSelected;
    }
}