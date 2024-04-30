using Android.Widget;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using ActionMenuView = AndroidX.AppCompat.Widget.ActionMenuView;
using DatePickerHandler = DIPS.Mobile.UI.Components.Pickers.DatePicker.DatePickerHandler;

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

        DatePicker.IsNullable = VirtualView.IsNullable;
        TimePicker.IsNullable = VirtualView.IsNullable;
        
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
        
        if(DatePicker.Handler is DatePickerHandler datePickerHandler)
        {
            datePickerHandler.RemoveClearButton();
        }
    }

    private void OnDatePickerValueUpdated()
    {
        if (TimePicker is { IsNullable: true, IsDateOrTimeNull: true })
        {
            VirtualView.SelectedDateTime = new DateTime(DatePicker.SelectedDate.Value.Year, 
                DatePicker.SelectedDate.Value.Month,
                DatePicker.SelectedDate.Value.Day, 
                DateTime.Now.TimeOfDay.Hours, 
                DateTime.Now.TimeOfDay.Hours, 
                DateTime.Now.TimeOfDay.Hours);
            
            return;
        }
        
        VirtualView.SelectedDateTime = new DateTime(DatePicker.SelectedDate.Value.Year, 
            DatePicker.SelectedDate.Value.Month,
            DatePicker.SelectedDate.Value.Day, 
            VirtualView.SelectedDateTime.Value.Hour, 
            VirtualView.SelectedDateTime.Value.Minute, 
            VirtualView.SelectedDateTime.Value.Second);
    }

    private void OnTimePickerValueUpdated()
    {
        if (DatePicker is { IsNullable: true, IsDateOrTimeNull: true })
        {
            VirtualView.SelectedDateTime = new DateTime(DateTime.Now.Year, 
                DateTime.Now.Month,
                DateTime.Now.Day,
                TimePicker.SelectedTime.Value.Hours, 
                TimePicker.SelectedTime.Value.Minutes, 
                TimePicker.SelectedTime.Value.Seconds);
            
            return;
        }

        if (TimePicker is { IsNullable: true, IsDateOrTimeNull: true })
        {
            VirtualView.SelectedDateTime = null;
            return;
        }

        VirtualView.SelectedDateTime = new DateTime(VirtualView.SelectedDateTime.Value.Year,
            VirtualView.SelectedDateTime.Value.Month,
            VirtualView.SelectedDateTime.Value.Day, 
            TimePicker.SelectedTime.Value.Hours, 
            TimePicker.SelectedTime.Value.Minutes, 
            TimePicker.SelectedTime.Value.Seconds);
    }
    
    private static partial void MapSelectedDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        TimeSpan? timeSpan = dateAndTimePicker.SelectedDateTime is null ? null : new TimeSpan(dateAndTimePicker.SelectedDateTime.Value.Hour,
            dateAndTimePicker.SelectedDateTime.Value.Minute,
            dateAndTimePicker.SelectedDateTime.Value.Second);

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
