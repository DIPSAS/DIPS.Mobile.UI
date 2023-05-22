namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Android;

public class DatePickerService
{
    private static MaterialDatePickerFragment? m_materialDatePicker;
    internal const string DatePickerTag = "DUIMaterialDatePicker";

    public static void OpenDatePicker(DatePicker datePicker)
    {
        if (IsOpen())
            Close();
        
        m_materialDatePicker = new MaterialDatePickerFragment(datePicker);
    }

    internal static bool IsOpen() => m_materialDatePicker != null && m_materialDatePicker.IsOpen();

    public static void Close()
    {
        m_materialDatePicker?.Close();
    }
}