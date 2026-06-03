using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Android;
using DIPS.Mobile.UI.Components.Pickers.TimePicker.Android;

namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePickerService
{
    private static MaterialDatePickerFragment? s_materialDatePicker;
    private static MaterialTimePickerFragment? s_materialTimePicker;
    
    public static partial void Open(DateAndTimePicker dateAndTimePicker, View chipTapped, bool datePickerTapped)
    {
        if (IsOpen())
        {
            Close();
        }

        var dateOnOpen = dateAndTimePicker.GetDateOnOpen();
        
        if (datePickerTapped)
        {
            var datePicker = new DatePicker.DatePicker
            {
                SelectedDate = dateOnOpen,
                MaximumDate = dateAndTimePicker.MaximumDate,
                MinimumDate = dateAndTimePicker.MinimumDate
            };
            
            datePicker.SelectedDateCommand = new Command<DateTime?>(selectedDate =>
            {
                var selectedDateTime = new DateTime(datePicker.SelectedDate.Year, datePicker.SelectedDate.Month, datePicker.SelectedDate.Day, dateOnOpen.TimeOfDay.Hours, dateOnOpen.TimeOfDay.Minutes, 0, dateAndTimePicker.IgnoreLocalTime ? DateTimeKind.Utc : DateTimeKind.Local)
                    .ConvertDate(dateAndTimePicker.GetDateTimeKind());

                if (!IsOutsideDateBounds(dateAndTimePicker, selectedDate) &&
                    IsOutsideBounds(dateAndTimePicker, selectedDateTime))
                {
                    VibrationService.Error();
                }

                dateAndTimePicker.SetSelectedDateTime(selectedDateTime);
            });
            
            s_materialDatePicker = new MaterialDatePickerFragment(datePicker);
        }
        else
        {
            var timePicker = CreateTimePicker(dateAndTimePicker, dateOnOpen);
            
            timePicker.SelectedTimeCommand = new Command(() =>
            {
                var selectedTime = timePicker.SelectedTime;
                var selectedDateTime = new DateTime(dateOnOpen.Year, dateOnOpen.Month, dateOnOpen.Day, selectedTime.Hours, selectedTime.Minutes, 0, dateAndTimePicker.IgnoreLocalTime ? DateTimeKind.Utc : DateTimeKind.Local)
                    .ConvertDate(dateAndTimePicker.GetDateTimeKind());
                
                dateAndTimePicker.SetSelectedDateTime(selectedDateTime);
            });
            
            s_materialTimePicker = new MaterialTimePickerFragment(timePicker);
        }
    }

    private static TimePicker.TimePicker CreateTimePicker(DateAndTimePicker dateAndTimePicker, DateTime dateOnOpen)
    {
        var selectedDate = dateOnOpen.ConvertDate(dateAndTimePicker.IgnoreLocalTime);
        var timePicker = new TimePicker.TimePicker { SelectedTime = selectedDate.TimeOfDay };

        if (dateAndTimePicker.MaximumDate is { } maximumDate)
        {
            var convertedMaximumDate = maximumDate.ConvertDate(dateAndTimePicker.IgnoreLocalTime);
            if (selectedDate.Date == convertedMaximumDate.Date)
            {
                timePicker.MaximumTime = convertedMaximumDate.TimeOfDay;
            }
        }

        if (dateAndTimePicker.MinimumDate is { } minimumDate)
        {
            var convertedMinimumDate = minimumDate.ConvertDate(dateAndTimePicker.IgnoreLocalTime);
            if (selectedDate.Date == convertedMinimumDate.Date)
            {
                timePicker.MinimumTime = convertedMinimumDate.TimeOfDay;
            }
        }

        return timePicker;
    }

    private static bool IsOutsideBounds(DateAndTimePicker dateAndTimePicker, DateTime? selectedDateTime) =>
        selectedDateTime is { } dateTime &&
        (dateAndTimePicker.MinimumDate is { } minimumDate && dateTime < minimumDate ||
         dateAndTimePicker.MaximumDate is { } maximumDate && dateTime > maximumDate);

    private static bool IsOutsideDateBounds(DateAndTimePicker dateAndTimePicker, DateTime? selectedDateTime) =>
        selectedDateTime is { } dateTime &&
        (dateAndTimePicker.MinimumDate is { } minimumDate && dateTime.Date < minimumDate.Date ||
         dateAndTimePicker.MaximumDate is { } maximumDate && dateTime.Date > maximumDate.Date);

    internal static partial bool IsOpen()
    {
        if (s_materialDatePicker is not null && s_materialDatePicker.IsOpen())
            return true;

        return s_materialTimePicker is not null && s_materialTimePicker.IsOpen();
    }

    public static partial void Close()
    {
        s_materialDatePicker?.Close();
        s_materialTimePicker?.Close();
    }
}