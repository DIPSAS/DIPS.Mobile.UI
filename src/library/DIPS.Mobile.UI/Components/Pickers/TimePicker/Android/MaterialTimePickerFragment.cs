using Android.Content;
using AndroidX.Fragment.App;
using DIPS.Mobile.UI.Components.Pickers.Platforms.Android;
using Google.Android.Material.DatePicker;
using Google.Android.Material.TimePicker;
using Microsoft.Maui.Platform;
using Object = Java.Lang.Object;

namespace DIPS.Mobile.UI.Components.Pickers.TimePicker.Android;

public class MaterialTimePickerFragment : IMaterialDateTimePickerFragment
{
    public MaterialTimePickerFragment(TimePicker timePicker)
    {
        var builder = new MaterialTimePicker.Builder()
            .SetHour(timePicker.SelectedTime.Hours)
            .SetMinute(timePicker.SelectedTime.Minutes);
        var materialTimePicker = builder.Build();

        var fragmentManager = Platform.CurrentActivity!.GetFragmentManager();
        materialTimePicker.Show(fragmentManager!, TimePickerService.TimePickerTag);
        materialTimePicker.AddOnDismissListener(new DismissListener(timePicker, materialTimePicker));
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

    private class DismissListener : Object, IDialogInterfaceOnDismissListener
    {
        private readonly TimePicker m_timePicker;
        private MaterialTimePicker m_materialTimePicker;

        public DismissListener(TimePicker timePicker, MaterialTimePicker materialTimePicker)
        {
            m_timePicker = timePicker;
            m_materialTimePicker = materialTimePicker;
        }
        
        public void OnDismiss(IDialogInterface? dialog)
        {
            m_timePicker.SelectedTime = new TimeSpan(m_materialTimePicker.Hour, m_materialTimePicker.Minute, 0);
            m_timePicker.SelectedTimeCommand?.Execute(null);
            
            m_materialTimePicker = null;
        }
    }
}