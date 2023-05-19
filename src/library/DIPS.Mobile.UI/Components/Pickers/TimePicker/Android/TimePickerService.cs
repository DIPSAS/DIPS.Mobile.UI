using DIPS.Mobile.UI.Components.Pickers.TimePicker.Android;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerService
{
    private static MaterialTimePickerFragment? m_materialDateTimePicker;
    internal const string TimePickerTag = "DUIMaterialTimePicker";

    public static partial void OpenTimePicker(TimePicker timePicker)
    {
        if (IsOpen())
            _ = Close();
        
        m_materialDateTimePicker = new MaterialTimePickerFragment(timePicker);
    }

    internal static bool IsOpen() => m_materialDateTimePicker != null && m_materialDateTimePicker.IsOpen();

    public static partial Task Close()
    {
        m_materialDateTimePicker?.Close();
        return Task.CompletedTask;
    }
}