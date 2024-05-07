using Android.Widget;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using ActionMenuView = AndroidX.AppCompat.Widget.ActionMenuView;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePickerHandler : ViewHandler<DateAndTimePicker, LinearLayout>
{
    private Pickers.DatePicker.DatePicker DatePicker { get; } = new();
    private Pickers.TimePicker.TimePicker TimePicker { get; } = new();
    
    protected override LinearLayout CreatePlatformView()
    {
        return new LinearLayout(Context) { Orientation = Orientation.Horizontal };
    }

    protected override void ConnectHandler(LinearLayout platformView)
    {
        base.ConnectHandler(platformView);

        var space = new Space(Context);
        space.LayoutParameters = new ActionMenuView.LayoutParams(Sizes.GetSize(SizeName.size_3), 0);

        platformView.AddView(DatePicker.ToPlatform(MauiContext!));
        platformView.AddView(space);
        platformView.AddView(TimePicker.ToPlatform(MauiContext!));

        TimePicker.SelectedTimeCommand = new Command(() =>
        {
            OnTimePickerValueUpdated();
            VirtualView.SelectedDateTimeCommand?.Execute(null);
        });
        DatePicker.SelectedDateCommand = new Command(() =>
        {
            OnDatePickerValueUpdated();
            VirtualView.SelectedDateTimeCommand?.Execute(null);
        });
        
        DatePicker.SetBinding(VisualElement.IsEnabledProperty, new Binding(nameof(VirtualView.IsEnabled), source: VirtualView));
        TimePicker.SetBinding(VisualElement.IsEnabledProperty, new Binding(nameof(VirtualView.IsEnabled), source: VirtualView));
    }

    private void OnDatePickerValueUpdated()
    {
        VirtualView.SelectedDateTime = new DateTime(DatePicker.SelectedDate.Year, 
            DatePicker.SelectedDate.Month,
            DatePicker.SelectedDate.Day, 
            VirtualView.SelectedDateTime.Hour, 
            VirtualView.SelectedDateTime.Minute, 
            VirtualView.SelectedDateTime.Second);
    }

    private void OnTimePickerValueUpdated()
    {
        VirtualView.SelectedDateTime = new DateTime(VirtualView.SelectedDateTime.Year,
            VirtualView.SelectedDateTime.Month,
            VirtualView.SelectedDateTime.Day, 
            TimePicker.SelectedTime.Hours, 
            TimePicker.SelectedTime.Minutes, 
            TimePicker.SelectedTime.Seconds);
    }
    
    private static partial void MapSelectedDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        var timeSpan = new TimeSpan(dateAndTimePicker.SelectedDateTime.Hour,
            dateAndTimePicker.SelectedDateTime.Minute,
            dateAndTimePicker.SelectedDateTime.Second);

        handler.DatePicker.SelectedDate = dateAndTimePicker.SelectedDateTime;
        handler.TimePicker.SelectedTime = timeSpan;
    }
    
    private static partial void MapIgnoreLocalTime(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        handler.DatePicker.IgnoreLocalTime = dateAndTimePicker.IgnoreLocalTime;
    }
    
    private static partial void MapMaximumDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        handler.DatePicker.MaximumDate = dateAndTimePicker.MaximumDate;
    }

    private static partial void MapMinimumDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        handler.DatePicker.MinimumDate = dateAndTimePicker.MinimumDate;
    }
}
