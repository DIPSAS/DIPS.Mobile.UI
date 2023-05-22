namespace DIPS.Mobile.UI.Components.Pickers.TimePicker.Android;

public class TimePickerService
{
    private static MaterialTimePickerFragment? m_materialDateTimePicker;
    internal const string TimePickerTag = "DUIMaterialTimePicker";

    public static void OpenTimePicker(TimePicker timePicker)
    {
        if (IsOpen())
            Close();
        
        m_materialDateTimePicker = new MaterialTimePickerFragment(timePicker);
    }

    internal static bool IsOpen() => m_materialDateTimePicker != null && m_materialDateTimePicker.IsOpen();

    public static void Close()
    {
        m_materialDateTimePicker?.Close();
    }
}