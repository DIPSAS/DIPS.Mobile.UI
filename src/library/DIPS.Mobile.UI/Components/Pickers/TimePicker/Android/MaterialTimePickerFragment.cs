using Android.Content;
using AndroidX.Fragment.App;
using DIPS.Mobile.UI.Components.Pickers.Platforms.Android;
using Google.Android.Material.DatePicker;
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
        var builder = new MaterialTimePicker.Builder()
            .SetHour(timePicker.SelectedTime.Hours)
            .SetMinute(timePicker.SelectedTime.Minutes);
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
        m_timePicker.SelectedTime = new TimeSpan(m_materialTimePicker.Hour, m_materialTimePicker.Minute, 0);
        m_timePicker.SelectedTimeCommand?.Execute(null);
    }
}