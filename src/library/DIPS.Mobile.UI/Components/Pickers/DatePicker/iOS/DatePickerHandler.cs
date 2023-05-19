using DIPS.Mobile.UI.Components.Pickers.Platforms.iOS;
using DIPS.Mobile.UI.Platforms.iOS;
using Foundation;
using Microsoft.Maui.Handlers;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.DatePicker;

public partial class DatePickerHandler : ViewHandler<DatePicker, UIDatePicker>
{
    protected override UIDatePicker CreatePlatformView()
    {
        return new UIDatePicker { PreferredDatePickerStyle = UIDatePickerStyle.Compact, Mode = UIDatePickerMode.Date };
    }

    protected override void ConnectHandler(UIDatePicker platformView)
    {
        base.ConnectHandler(platformView);

        platformView.ValueChanged += OnDateSelected;
        platformView.SetDefaultTintColor();
    }

    private partial void AppendPropertyMapper()
    {
        DatePickerPropertyMapper.Add(nameof(DatePicker.HorizontalOptions), MapHorizontalOptions);
        DatePickerPropertyMapper.Add(nameof(DatePicker.Background), MapOverrideBackground);
        DatePickerPropertyMapper.Add(nameof(DatePicker.IgnoreLocalTime), MapIgnoreLocalTime);
        DatePickerPropertyMapper.Add(nameof(DatePicker.MinimumDate), MapMinimumDate);
        DatePickerPropertyMapper.Add(nameof(DatePicker.MaximumDate), MapMaximumDate);
    }

    private static void MapMaximumDate(DatePickerHandler handler, DatePicker datePicker)
    {
        if(datePicker.MaximumDate is null or null)
            return;

        handler.PlatformView.MaximumDate = ((DateTime)datePicker.MaximumDate).ConvertDate();
    }

    private static void MapMinimumDate(DatePickerHandler handler, DatePicker datePicker)
    {
        if(datePicker.MinimumDate is null or null)
            return;
        
        handler.PlatformView.MinimumDate = ((DateTime)datePicker.MinimumDate).ConvertDate();
    }

    private void MapOverrideBackground(DatePickerHandler handler, DatePicker datePicker)
    {
    }

    private static void MapHorizontalOptions(DatePickerHandler handler, DatePicker datePicker)
    {
        handler.PlatformView.SetHorizontalAlignment(datePicker);
    }

    private void OnDateSelected(object? sender, EventArgs e)
    {
        VirtualView.SelectedDate = (DateTime)PlatformView.Date;
        VirtualView.SelectedDateCommand?.Execute(null);
    }

    private static void MapIgnoreLocalTime(DatePickerHandler handler, DatePicker datePicker)
    {
        handler.PlatformView.TimeZone = datePicker.IgnoreLocalTime ? new NSTimeZone("UTC") : NSTimeZone.LocalTimeZone;
    }

    private static partial void MapSelectedDate(DatePickerHandler handler, DatePicker datePicker)
    {
        handler.PlatformView.SetDate(datePicker.SelectedDate.ConvertDate(), true);
    }

    protected override void DisconnectHandler(UIDatePicker platformView)
    {
        base.DisconnectHandler(platformView);

        platformView.ValueChanged -= OnDateSelected;
    }
}