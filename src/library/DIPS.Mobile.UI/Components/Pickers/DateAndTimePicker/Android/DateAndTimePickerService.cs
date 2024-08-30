using DIPS.Mobile.UI.Components.Pickers.DatePicker.Android;
using DIPS.Mobile.UI.Components.Pickers.TimePicker.Android;

namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePickerService
{
    private static MaterialDatePickerFragment? m_materialDatePicker;
    private static MaterialTimePickerFragment? m_materialTimePicker;
    
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
            
            datePicker.SelectedDateCommand = new Command(() =>
            {
                var selectedDateTime = new DateTime(datePicker.SelectedDate.Year, datePicker.SelectedDate.Month, datePicker.SelectedDate.Day, dateOnOpen.TimeOfDay.Hours, dateOnOpen.TimeOfDay.Minutes, 0);
                dateAndTimePicker.SetSelectedDateTime(selectedDateTime);
            });
            
            m_materialDatePicker = new MaterialDatePickerFragment(datePicker);
        }
        else
        {
            var timePicker = new TimePicker.TimePicker { SelectedTime = dateOnOpen.ConvertDate(dateAndTimePicker.IgnoreLocalTime).TimeOfDay };
            
            timePicker.SelectedTimeCommand = new Command(() =>
            {
                var selectedTime = timePicker.SelectedTime;
                var selectedDateTime = new DateTime(dateOnOpen.Year, dateOnOpen.Month, dateOnOpen.Day, selectedTime.Hours, selectedTime.Minutes, 0, dateAndTimePicker.IgnoreLocalTime ? DateTimeKind.Utc : DateTimeKind.Local);
                dateAndTimePicker.SetSelectedDateTime(selectedDateTime);
            });
            
            m_materialTimePicker = new MaterialTimePickerFragment(timePicker);
        }
    }

    private static void OnDateSelected(DateTime datePickerSelectedDate, DateAndTimePicker dateAndTimePicker)
    {
        
    }

    internal static partial bool IsOpen()
    {
        if (m_materialDatePicker is not null && m_materialDatePicker.IsOpen())
            return true;

        return m_materialTimePicker is not null && m_materialTimePicker.IsOpen();
    }

    public static partial void Close()
    {
        m_materialDatePicker?.Close();
        m_materialTimePicker?.Close();
    }
}