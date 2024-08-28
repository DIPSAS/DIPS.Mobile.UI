using System.Globalization;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker.iOS;
using DIPS.Mobile.UI.Converters.ValueConverters;

namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePicker
{
    public DateAndTimePicker()
    {
        DateChip = new Chip();
        TimeChip = new Chip();
        
        DateChip.Tapped += DateChipOnTapped;
        TimeChip.Tapped += DateChipOnTapped;
        
        Spacing = Sizes.GetSize(SizeName.size_1);
        
        Add(DateChip);
        Add(TimeChip);
    }
    
    public Chip DateChip { get; }
    public Chip TimeChip { get; }

    private void DateChipOnTapped(object? sender, EventArgs e)
    {
        if(sender is not View chip)
            return;
        
        DateAndTimePickerService.Open(this, chip, sender == DateChip);
    }

    protected partial void InternalSelectedDateTimeChanged(DateTime selectedDateTime)
    {
        var shouldConvert = string.IsNullOrEmpty(DateChip.Title);
        var date = selectedDateTime;
        if (shouldConvert)
        {
            date = date.ConvertDate(IgnoreLocalTime);
        }
        
        var timeSpan = new TimeSpan(date.Hour, date.Minute, date.Second);
        
        SetDateChipTitle(date);
        SetTimeChipTitle(timeSpan);
    }

    private void SetDateChipTitle(DateTime date)
    {
        var convertedDisplayValue =
            new DateConverter { Format = DateConverterFormat, ShouldConvertDateBeforeParsingToValue = false }.Convert(date,
                null, null,
                CultureInfo.CurrentCulture);
        if (convertedDisplayValue is string displayItemAsString)
        {
            DateChip.Title = displayItemAsString;
        }
    }

    private void SetTimeChipTitle(TimeSpan timeSpan)
    {
        var convertedDisplayValue =
            new TimeConverter { Format = TimeConverter.TimeConverterFormat.Default }.Convert(timeSpan,
                null, null,
                CultureInfo.CurrentCulture);
        if (convertedDisplayValue is string displayItemAsString)
        {
            TimeChip.Title = displayItemAsString;
        }
    }
}