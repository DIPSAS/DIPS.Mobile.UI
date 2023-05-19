using DIPS.Mobile.UI.Components.Pickers.DatePicker.Android;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.DatePicker;

public partial class DatePickerService
{
    private static MaterialDatePickerFragment? m_materialDatePicker;
    internal const string DatePickerTag = "DUIMaterialDatePicker";

    public static partial void OpenDatePicker(DatePicker datePicker)
    {
        if (IsOpen())
            _ = Close();
        
        m_materialDatePicker = new MaterialDatePickerFragment(datePicker);
    }

    internal static bool IsOpen() => m_materialDatePicker != null && m_materialDatePicker.IsOpen();

    public static partial Task Close()
    {
        m_materialDatePicker?.Close();
        return Task.CompletedTask;
    }
}