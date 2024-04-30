using System.Globalization;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.Android;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.DatePicker;

public partial class DatePickerHandler : BaseDatePickerHandler
{
    private static partial void MapSelectedDate(DatePickerHandler handler, DatePicker datePicker)
    {
        if(datePicker is { IsDateOrTimeNull: true, IsNullable: true })
        {
            handler.Chip.Style = Styles.GetChipStyle(ChipStyle.EmptyInput);
            handler.Chip.Title = DUILocalizedStrings.Date;
        }
        else
        {
            handler.Chip.Style = Styles.GetChipStyle(ChipStyle.Input);
            
            var convertedDisplayValue =
                new DateConverter {Format = DateConverter.DateConverterFormat.Default}.Convert(datePicker.SelectedDate,
                    null, null,
                    CultureInfo.CurrentCulture);
            if (convertedDisplayValue is string displayItemAsString)
            {
                handler.Chip.Title = displayItemAsString;
            }
        }

        handler.SetClearButtonVisibility();
    }
}