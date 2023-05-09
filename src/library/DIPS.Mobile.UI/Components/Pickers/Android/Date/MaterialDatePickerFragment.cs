using Android.Content;
using DIPS.Mobile.UI.Components.Pickers.DateTimePickers;
using Google.Android.Material.DatePicker;
using Microsoft.Maui.Platform;
using DatePicker = DIPS.Mobile.UI.Components.Pickers.DateTimePickers.DatePicker;
using Object = Java.Lang.Object;

namespace DIPS.Mobile.UI.Components.Pickers.Android.Date;

public class MaterialDatePickerFragment : IMaterialDateTimePickerFragment
{
    private readonly DatePicker? m_datePicker;
    private readonly MaterialDatePicker m_materialDatePicker;

    private const string DatePickerTag = "DUIMaterialDateTimePicker";

    public MaterialDatePickerFragment(IDateTimePicker? datePicker)
    {
        if (datePicker == null) return;
        
        m_datePicker = (datePicker as DatePicker)!;

        //This will only run if the date picker was not previously opened 
        if (datePicker.IsOpen && m_materialDatePicker == null)
        {
            var builder = MaterialDatePicker.Builder.DatePicker();
            SetDatePickerSelection(builder);
            SetDatePickerTitle(builder);
            m_materialDatePicker = builder.Build();

            var fragmentManager = Platform.CurrentActivity!.GetFragmentManager();
            m_materialDatePicker.Show(fragmentManager!, DatePickerTag);
            m_materialDatePicker.AddOnDismissListener(new DismissListener(m_datePicker, m_materialDatePicker));
        }
        else
        {
            if (m_materialDatePicker is not {IsVisible: true}) return;

            m_materialDatePicker.Dismiss();
        }
    }
    
    private void SetDatePickerTitle(MaterialDatePicker.Builder builder)
    {
        if (m_datePicker != null && !string.IsNullOrEmpty(m_datePicker.Description))
        {
            builder.SetTitleText(m_datePicker.Description);
        }
    }

    private void SetDatePickerSelection(MaterialDatePicker.Builder builder)
    {
        if (m_datePicker == null || m_datePicker.SelectedDate == default) return;

        //Java uses the unix epoch, so we have find the total milliseconds from the date people have picked and the UnixEpoch start.
        builder.SetSelection((long)(m_datePicker.SelectedDate - DateTime.UnixEpoch).TotalMilliseconds);
    }

    public bool IsOpen()
    {
        var fragment = Platform.AppContext.GetFragmentManager()!.FindFragmentByTag(DatePickerTag);
        return fragment is MaterialDatePicker;
    }

    public void Close()
    {
        var fragment = Platform.AppContext.GetFragmentManager()!.FindFragmentByTag(DatePickerTag);
        if (fragment is MaterialDatePicker datePickerFragment)
        {
            datePickerFragment.Dismiss();
        }
    }

    private class DismissListener : Object, IDialogInterfaceOnDismissListener
    {
        private readonly DatePicker? m_datePicker;
        private MaterialDatePicker? m_materialDatePicker;

        public DismissListener(DatePicker datePicker, MaterialDatePicker materialDatePicker)
        {
            m_datePicker = datePicker;
            m_materialDatePicker = materialDatePicker;
        }
        
        public void OnDismiss(IDialogInterface? dialog)
        {
            if (m_datePicker == null || m_materialDatePicker == null) return;

            if (long.TryParse(m_materialDatePicker.Selection?.ToString(), out var milliseconds))
            {
                //Java uses the unix epoch, so we have to create a csharp date time based on the UnixEpoch start with the milliseconds picked by people using the date picker.
                var dateFromJava = DateTime.UnixEpoch.AddMilliseconds(milliseconds);
                m_datePicker.SelectedDate = dateFromJava;
            }

            m_materialDatePicker = null;
            m_datePicker.IsOpen = false;
        }
    }

    
}