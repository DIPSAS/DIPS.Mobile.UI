using System.Globalization;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.Android;
using DIPS.Mobile.UI.Converters.ValueConverters;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.DatePicker;

public partial class DatePickerHandler : BaseDatePickerHandler
{
    
    private static partial void MapSelectedDate(DatePickerHandler handler, DatePicker datePicker)
    {
        var convertedDisplayValue =
            new DateConverter {Format = DateConverter.DateConverterFormat.Default}.Convert(datePicker.SelectedDate,
                null, null,
                CultureInfo.CurrentCulture);
        if (convertedDisplayValue is string displayItemAsString)
        {
            handler.PlatformView.Text = displayItemAsString;
        }

    }
}