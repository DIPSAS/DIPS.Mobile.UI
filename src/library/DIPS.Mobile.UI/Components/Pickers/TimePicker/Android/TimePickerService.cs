using DIPS.Mobile.UI.Components.Pickers.TimePicker.Android;

namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerService
{
    private static MaterialTimePickerFragment? m_materialDateTimePicker;
    internal const string TimePickerTag = "DUIMaterialTimePicker";

    public static void Open(TimePicker timePicker, View? sourceView = null)
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