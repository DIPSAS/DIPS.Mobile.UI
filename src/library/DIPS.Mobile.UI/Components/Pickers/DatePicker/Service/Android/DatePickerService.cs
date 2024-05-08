using DIPS.Mobile.UI.Components.Pickers.DatePicker.Android;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;

public partial class DatePickerService
{
    private static MaterialDatePickerFragment? m_materialDatePicker;
    internal const string DatePickerTag = "DUIMaterialDatePicker";

    public static partial void Open(DatePicker datePicker, View? sourceView = null)
    {
        if (IsOpen())
        {
            Close();
        }
        
        m_materialDatePicker = new MaterialDatePickerFragment(datePicker);
    }

    internal static partial bool IsOpen() => m_materialDatePicker != null && m_materialDatePicker.IsOpen();

    public static partial void Close()
    {
        m_materialDatePicker?.Close();
    }
}