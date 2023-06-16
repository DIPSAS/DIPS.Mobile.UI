using Android.Content;
using Android.OS;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;
using DIPS.Mobile.UI.Components.Pickers.Platforms.Android;
using Google.Android.Material.DatePicker;
using Java.Util;
using Microsoft.Maui.Platform;
using Object = Java.Lang.Object;

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
        m_materialDatePicker.Show(fragmentManager!, DatePickerService.DatePickerTag);
    }

    private void SetDatePickerSelection(MaterialDatePicker.Builder builder)
    {
        var date = m_datePicker.IgnoreLocalTime ? m_datePicker.SelectedDate : m_datePicker.SelectedDate.ToLocalTime();

        //Java uses the unix epoch, so we have find the total milliseconds from the date people have picked and the UnixEpoch start.
        builder.SetSelection(date.ToLong());
    }

    public bool IsOpen()
    {
        var fragment = Platform.CurrentActivity?.GetFragmentManager()?.FindFragmentByTag(DatePickerService.DatePickerTag);
        return fragment is MaterialDatePicker;
    }

    public void Close()
    {
        var fragment = Platform.CurrentActivity?.GetFragmentManager()?.FindFragmentByTag(DatePickerService.DatePickerTag);
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
            m_datePicker.SelectedDate = m_datePicker.IgnoreLocalTime ? dateFromJava : dateFromJava.ToLocalTime();
            m_datePicker.SelectedDateCommand?.Execute(null);
        }
    }
}

public class Something : Object, CalendarConstraints.IDateValidator
{
    private readonly DateTime m_minMinDateTime;
    private readonly DateTime m_maxMaxDateTime;

    public Something(DateTime minDateTime, DateTime maxDateTime)
    {
        m_minMinDateTime = minDateTime;
        m_maxMaxDateTime = maxDateTime;
    }

    public int DescribeContents()
    {
        return 0;
    }

    public void WriteToParcel(Parcel dest, ParcelableWriteFlags flags)
    {
        dest.WriteLongArray(new []{m_minMinDateTime.ToLong(), m_maxMaxDateTime.ToLong()});
    }

    public bool IsValid(long p0)
    {
        Date date = new Date(p0);
        var dateFromJava = DateTime.UnixEpoch.AddMilliseconds(p0);
        var minLong = m_minMinDateTime.ToLong();
        var maxLong = m_maxMaxDateTime.ToLong();
        if (dateFromJava > m_minMinDateTime && dateFromJava < m_maxMaxDateTime)
        {
            return true;
        }

        return false;
    }
}