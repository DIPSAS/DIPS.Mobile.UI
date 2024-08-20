using Android.Content;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.Android;
using Google.Android.Material.DatePicker;
using Microsoft.Maui.Platform;
using Object = Java.Lang.Object;
using TimePickerService = DIPS.Mobile.UI.Components.Pickers.TimePicker.TimePickerService;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Android;

public class MaterialDatePickerFragment : Object, IMaterialDateTimePickerFragment, IMaterialPickerOnPositiveButtonClickListener
{
    private readonly DatePicker m_datePicker;
    private readonly MaterialDatePicker m_materialDatePicker;

    public MaterialDatePickerFragment(DatePicker datePicker)
    {
        m_datePicker = datePicker;
        
        var builder = MaterialDatePicker.Builder.DatePicker();
        SetDatePickerSelection(builder);

        //Set min and max time
        var calendarConstraints = new CalendarConstraints.Builder();
        var listValidators = new List<CalendarConstraints.IDateValidator>();
        if (datePicker.MinimumDate != null) //TODO: Support only setting one of these as well
        {
            listValidators.Add(DateValidatorPointForward.From(datePicker.MinimumDate.Value.ToLong()));
        }
        if (datePicker.MaximumDate != null)
        {
            listValidators.Add(DateValidatorPointBackward.Before(datePicker.MaximumDate.Value.ToLong()));
        }

        if (listValidators.Any())
        {
            calendarConstraints.SetValidator(CompositeDateValidator.AllOf(listValidators));    
        }
        
        m_materialDatePicker = builder.SetCalendarConstraints(calendarConstraints.Build()).Build();
        
        m_materialDatePicker.AddOnPositiveButtonClickListener(this);
        
        

        var fragmentManager = Platform.CurrentActivity!.GetFragmentManager();
        m_materialDatePicker.Show(fragmentManager!, TimePickerService.TimePickerTag);
    }

    private void SetDatePickerSelection(MaterialDatePicker.Builder builder)
    {
        var date = m_datePicker.IgnoreLocalTime ? m_datePicker.SelectedDate : m_datePicker.SelectedDate.ToLocalTime();

        //Java uses the unix epoch, so we have find the total milliseconds from the date people have picked and the UnixEpoch start.
        builder.SetSelection(date.ToLong());
    }

    public bool IsOpen()
    {
        var fragment = Platform.CurrentActivity?.GetFragmentManager()?.FindFragmentByTag(TimePickerService.TimePickerTag);
        return fragment is MaterialDatePicker;
    }

    public void Close()
    {
        var fragment = Platform.CurrentActivity?.GetFragmentManager()?.FindFragmentByTag(TimePickerService.TimePickerTag);
        if (fragment is MaterialDatePicker datePickerFragment)
        {
            datePickerFragment.Dismiss();
        }
    }

    public void OnPositiveButtonClick(Object? p0)
    {
        if (long.TryParse(m_materialDatePicker.Selection?.ToString(), out var milliseconds))
        {
            //Java uses the unix epoch, so we have to create a csharp date time based on the UnixEpoch start with the milliseconds picked by people using the date picker.
            var dateFromJava = DateTime.UnixEpoch.AddMilliseconds(milliseconds);

            var dateTime = m_datePicker.IgnoreLocalTime ? dateFromJava.ToUniversalTime() : dateFromJava.ToLocalTime();
            m_datePicker.SetSelectedDateTime(dateTime);
        }
    }
}