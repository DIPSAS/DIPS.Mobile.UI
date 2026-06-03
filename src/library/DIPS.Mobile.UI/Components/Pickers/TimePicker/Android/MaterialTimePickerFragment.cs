using Android.Text.Format;
using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.Android;
using Google.Android.Material.TimePicker;
using Microsoft.Maui.Platform;
using Object = Java.Lang.Object;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Pickers.TimePicker.Android;

public class MaterialTimePickerFragment : Object, IMaterialDateTimePickerFragment, View.IOnClickListener
{
    private readonly TimePicker m_timePicker;
    private readonly MaterialTimePicker m_materialTimePicker;

    public MaterialTimePickerFragment(TimePicker timePicker)
    {
        m_timePicker = timePicker;
        var format = (DateFormat.Is24HourFormat(Platform.AppContext)) ? TimeFormat.Clock24h : TimeFormat.Clock12h;

        var time = timePicker.GetTimeOnOpen();
        
        var builder = new MaterialTimePicker.Builder()
            .SetTimeFormat(format)
            .SetHour(time.Hours)
            .SetMinute(time.Minutes);
        
        m_materialTimePicker = builder.Build();

        var fragmentManager = Platform.CurrentActivity!.GetFragmentManager();
        m_materialTimePicker.Show(fragmentManager!, TimePickerService.TimePickerTag);
        m_materialTimePicker.AddOnPositiveButtonClickListener(this);
    }

    public bool IsOpen()
    {
        var fragment = Platform.CurrentActivity?.GetFragmentManager()?.FindFragmentByTag(TimePickerService.TimePickerTag);

        return fragment is MaterialTimePicker;
    }

    public void Close()
    {
        var fragment = Platform.CurrentActivity?.GetFragmentManager()?.FindFragmentByTag(TimePickerService.TimePickerTag);
        if (fragment is MaterialTimePicker datePickerFragment)
        {
            datePickerFragment.Dismiss();
        }
    }

    public void OnClick(View? v)
    {
        var selectedTime = new TimeSpan(m_materialTimePicker.Hour, m_materialTimePicker.Minute, 0);

        if (m_timePicker.MinimumTime is { } minimumTime && selectedTime < minimumTime ||
            m_timePicker.MaximumTime is { } maximumTime && selectedTime > maximumTime)
        {
            VibrationService.Error();
        }

        m_timePicker.SetSelectedDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, selectedTime.Hours, selectedTime.Minutes, 0));
    }
}