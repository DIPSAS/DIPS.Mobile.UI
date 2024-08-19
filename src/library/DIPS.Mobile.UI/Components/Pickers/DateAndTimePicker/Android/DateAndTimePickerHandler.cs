using Android.Widget;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using ActionMenuView = AndroidX.AppCompat.Widget.ActionMenuView;
using View = Android.Views.View;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePickerHandler : ViewHandler<DateAndTimePicker, View>
{
    private DateTime m_dateSetFromPickers;

    private Grid m_grid;
    
    private Pickers.DatePicker.DatePicker DatePicker { get; } = new();
    private Pickers.TimePicker.TimePicker TimePicker { get; } = new();
    
    protected override View CreatePlatformView()
    {
        m_grid = new Grid
        {
            ColumnDefinitions = [new ColumnDefinition(GridLength.Star), new ColumnDefinition(GridLength.Auto)],
            ColumnSpacing = Sizes.GetSize(SizeName.size_1)
        };

        return m_grid.ToPlatform(DUI.GetCurrentMauiContext!);
    }

    protected override void ConnectHandler(View platformView)
    {
        base.ConnectHandler(platformView);

        m_grid.Add(DatePicker);
        m_grid.Add(TimePicker, 1, 0);

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

    private void OnDatePickerValueUpdated() => SetSelectedDateTime();

    private void SetSelectedDateTime()
    {
        var dateTime = new DateTime(DatePicker.SelectedDate.Year, 
            DatePicker.SelectedDate.Month,
            DatePicker.SelectedDate.Day, 
            TimePicker.SelectedTime.Hours, 
            TimePicker.SelectedTime.Minutes, 
            TimePicker.SelectedTime.Seconds, 
            VirtualView.SelectedDateTime.Kind);
        
        m_dateSetFromPickers = ConvertDate(dateTime, VirtualView.IgnoreLocalTime);

        VirtualView.SelectedDateTime = m_dateSetFromPickers;
    }

    private void OnTimePickerValueUpdated() => SetSelectedDateTime();
    
    private static partial void MapSelectedDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        // If the date is already set from the pickers, we don't want to show the converted date to the consumer.
        if(handler.m_dateSetFromPickers == dateAndTimePicker.SelectedDateTime)
            return;

        var dateTime = ConvertDate(dateAndTimePicker.SelectedDateTime, dateAndTimePicker.IgnoreLocalTime);
        
        var timeSpan = new TimeSpan(dateTime.Hour,
                    dateTime.Minute,
                    dateTime.Second);
        
        handler.DatePicker.SelectedDate = dateTime;
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
    
    public static DateTime ConvertDate(DateTime date, bool ignoreLocalTime)
    {
        if (date.Kind == DateTimeKind.Unspecified)
        {
            date = DateTime.SpecifyKind(date, ignoreLocalTime ? DateTimeKind.Utc : DateTimeKind.Local);
        }
        var dateTime = ignoreLocalTime ? date.ToUniversalTime() : date.ToLocalTime();

        return dateTime;
    }
}
