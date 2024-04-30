using System.Globalization;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.Android;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerHandler : BaseDatePickerHandler
{
    private static partial void MapSelectedTime(TimePickerHandler handler, TimePicker timePicker)
    {
        if (timePicker is { IsDateOrTimeNull: true, IsNullable: true })
        {
            handler.Chip.Style = Styles.GetChipStyle(ChipStyle.EmptyInput);
            handler.Chip.Title = DUILocalizedStrings.Time;
        }
        else
        {
            handler.Chip.Style = Styles.GetChipStyle(ChipStyle.Input);
            
            var convertedDisplayValue =
                new TimeConverter { Format = TimeConverter.TimeConverterFormat.Default }.Convert(timePicker.SelectedTime, null, null,
                    CultureInfo.CurrentCulture);
            if (convertedDisplayValue is string displayItemAsString)
            {
                handler.Chip.Title = displayItemAsString; 
            }
        }
        
        handler.SetClearButtonVisibility();
    }
}