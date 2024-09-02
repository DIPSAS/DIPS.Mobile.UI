using System.Globalization;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared;
using DIPS.Mobile.UI.Converters.ValueConverters;
using HorizontalStackLayout = DIPS.Mobile.UI.Components.Lists.HorizontalStackLayout;
using IDatePicker = DIPS.Mobile.UI.Components.Pickers.DatePickerShared.IDatePicker;

namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePicker : HorizontalStackLayout, IDatePicker
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
                null!, null!,
                CultureInfo.CurrentCulture);
        if (convertedDisplayValue is string displayItemAsString)
        {
            TimeChip.Title = displayItemAsString;
        }
    }
    
    protected void OnSelectedDateTimeChanged(DateTime dateTime)
    {    
        var date = dateTime.ConvertDate(IgnoreLocalTime);
        
        var timeSpan = new TimeSpan(date.Hour, date.Minute, date.Second);
        
        SetDateChipTitle(date);
        SetTimeChipTitle(timeSpan);
    }

    public event Action<DateTime?>? SelectedDateTimeChanged;
    public DatePickerMode Mode => DatePickerMode.DateAndTime;
    public virtual void SetSelectedDateTime(DateTime? selectedDate)
    {
        if (selectedDate.HasValue)
        {
            if(selectedDate.Value.Ticks == SelectedDateTime.Ticks)
                return;
            
            SelectedDateTime = ValidateDateTime(selectedDate.Value.ConvertDate(SelectedDateTime.Kind));
        }

        SelectedDateTimeCommand?.Execute(selectedDate);
        SelectedDateTimeChanged?.Invoke(selectedDate);
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        SelectedDateTime = ValidateDateTime(SelectedDateTime);
        OnSelectedDateTimeChanged(SelectedDateTime);
    }

    protected DateTime ValidateDateTime(DateTime selectedDate)
    {
        // SelectedDate should not be above maximum date
        if (MaximumDate != null && SelectedDateTime > MaximumDate)
        {
            return new DateTime(MaximumDate.Value.Year,
                MaximumDate.Value.Month,
                MaximumDate.Value.Day,
                SelectedDateTime.Hour, SelectedDateTime.Minute, SelectedDateTime.Second, SelectedDateTime.Kind);
        }

        // SelectedDate should not be below minimum date
        if (MinimumDate != null && SelectedDateTime < MinimumDate)
        {
            return new DateTime(MinimumDate.Value.Year,
                MinimumDate.Value.Month,
                MinimumDate.Value.Day,
                SelectedDateTime.Hour, SelectedDateTime.Minute, SelectedDateTime.Second, SelectedDateTime.Kind);
        }

        return selectedDate;
    }

    public virtual DateTime GetDateOnOpen()
    {
        return SelectedDateTime;
    }

}