using DIPS.Mobile.UI.Components.Pickers.TimePicker.Android;

namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerService
{
    private static MaterialTimePickerFragment? m_materialDateTimePicker;
    internal const string TimePickerTag = "DUIMaterialTimePicker";

    public static partial void Open(TimePicker timePicker, View? sourceView = null)
    {
        if (IsOpen())
            Close();
        
        m_materialDateTimePicker = new MaterialTimePickerFragment(timePicker);
    }

    internal static partial bool IsOpen() => m_materialDateTimePicker != null && m_materialDateTimePicker.IsOpen();

    public static partial void Close()
    {
        m_materialDateTimePicker?.Close();
    }
}