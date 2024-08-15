using System.Globalization;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker.iOS;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using DIPS.Mobile.UI.Converters.ValueConverters;

namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePicker
{
    private readonly Chip m_dateChip;
    private readonly Chip m_timeChip;

    public DateAndTimePicker()
    {
        m_dateChip = new Chip();
        m_timeChip = new Chip();
        
        m_dateChip.Tapped += DateChipOnTapped;
        m_timeChip.Tapped += DateChipOnTapped;
        
        Spacing = Sizes.GetSize(SizeName.size_3);
        
        Add(m_dateChip);
        Add(m_timeChip);
    }

    private void DateChipOnTapped(object? sender, EventArgs e)
    {
        DateAndTimePickerService.Open(this, this);
    }

    private partial void InternalSelectedDateTimeChanged()
    {
        var shouldConvert = string.IsNullOrEmpty(m_dateChip.Title);
        var date = SelectedDateTime;
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
            new DateConverter { Format = DateConverter.DateConverterFormat.Default, ShouldConvertDateBeforeParsingToValue = false }.Convert(date,
                null, null,
                CultureInfo.CurrentCulture);
        if (convertedDisplayValue is string displayItemAsString)
        {
            m_dateChip.Title = displayItemAsString;
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
            m_timeChip.Title = displayItemAsString;
        }
    }
}