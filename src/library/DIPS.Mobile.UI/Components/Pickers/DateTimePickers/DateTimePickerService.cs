using DIPS.Mobile.UI.Components.Pickers.DateTimePickers;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers;

public static partial class DateTimePickerService
{
    public static partial void OpenDateTimePicker(IDateTimePicker dateTimePicker);

    public static partial Task Close();
}