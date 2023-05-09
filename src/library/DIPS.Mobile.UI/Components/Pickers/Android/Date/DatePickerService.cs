using Android.App;
using Android.Content;
using Google.Android.Material.DatePicker;
using Microsoft.Maui.Platform;
using Object = Java.Lang.Object;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers;

public static partial class DatePickerService
{
    private static MaterialDatePicker m_materialDatePicker;
    private static DatePicker m_duiDatePicker;
    private const string DatePickerTag = "DUIMaterialDatePicker";

    public static partial void OpenDatePicker(DatePicker datePicker)
    {
        m_duiDatePicker = datePicker;
        if (datePicker == null) return;

        if (datePicker.IsOpen &&
            m_materialDatePicker == null) //This will only run if the date picker was not previously opened 
        {
            var builder = MaterialDatePicker.Builder.DatePicker();
            SetDatePickerSelection(builder, datePicker);
            SetDatePickerTitle(builder, datePicker);
            m_materialDatePicker = builder.Build();

            var fragmentManager = Platform.CurrentActivity.GetFragmentManager();
            m_materialDatePicker.Show(fragmentManager, DatePickerTag);
            m_materialDatePicker.AddOnDismissListener(new DismissListener());
        }
        else
        {
            if (m_materialDatePicker is not {IsVisible: true}) return;

            m_materialDatePicker.Dismiss();
        }
    }

    private static void SetDatePickerTitle(MaterialDatePicker.Builder builder, DatePicker datePicker)
    {
        if (datePicker != null && !string.IsNullOrEmpty(datePicker.Description))
        {
            builder.SetTitleText(datePicker.Description);
        }
    }

    private static void SetDatePickerSelection(MaterialDatePicker.Builder builder, DatePicker datePicker)
    {
        if (datePicker == null || datePicker.SelectedDate == default) return;

        //Java uses the unix epoch, so we have find the total milliseconds from the date people have picked and the UnixEpoch start.
        builder.SetSelection((long)(datePicker.SelectedDate - DateTime.UnixEpoch).TotalMilliseconds);
    }

    internal static bool IsOpen()
    {
        var fragment = Platform.AppContext.GetFragmentManager()!.FindFragmentByTag(DatePickerTag);
        return fragment is MaterialDatePicker;
    }

    public static partial Task Close()
    {
        var fragment = Platform.AppContext.GetFragmentManager()!.FindFragmentByTag(DatePickerTag);
        if (fragment is MaterialDatePicker datePickerFragment)
        {
            datePickerFragment.Dismiss();
        }

        return Task.CompletedTask;
    }

    public class DismissListener : Object, IDialogInterfaceOnDismissListener
    {
        public void OnDismiss(IDialogInterface? dialog)
        {
            if (m_duiDatePicker == null || m_materialDatePicker == null) return;

            if (long.TryParse(m_materialDatePicker.Selection?.ToString(), out var milliseconds))
            {
                //Java uses the unix epoch, so we have to create a csharp date time based on the UnixEpoch start with the milliseconds picked by people using the date picker.
                var dateFromJava = DateTime.UnixEpoch.AddMilliseconds(milliseconds);
                m_duiDatePicker.SelectedDate = dateFromJava;
            }

            m_materialDatePicker = null;
            m_duiDatePicker.IsOpen = false;
        }
    }
}