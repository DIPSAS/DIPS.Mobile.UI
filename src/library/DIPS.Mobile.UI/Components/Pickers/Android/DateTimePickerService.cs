using DIPS.Mobile.UI.Components.Pickers.Android;
using DIPS.Mobile.UI.Components.Pickers.Android.Date;
using DIPS.Mobile.UI.Components.Pickers.DateTimePickers;
using DatePicker = DIPS.Mobile.UI.Components.Pickers.DateTimePickers.DatePicker;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers;

public static partial class DateTimePickerService
{
    private static IMaterialDateTimePickerFragment? m_materialDateTimePicker;
    private const string DatePickerTag = "DUIMaterialDateTimePicker";

    public static partial void OpenDateTimePicker(IDateTimePicker dateTimePicker)
    {
        switch (dateTimePicker)
        {
            case DatePicker:
                m_materialDateTimePicker = new MaterialDatePickerFragment(dateTimePicker);
                break;
        }
       
    }

    internal static bool IsOpen() => m_materialDateTimePicker != null && m_materialDateTimePicker.IsOpen();

    public static partial Task Close()
    {
        m_materialDateTimePicker?.Close();
        return Task.CompletedTask;
    }

}