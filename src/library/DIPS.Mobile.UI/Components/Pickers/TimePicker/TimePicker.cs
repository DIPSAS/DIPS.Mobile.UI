using System.Globalization;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared;
using DIPS.Mobile.UI.Converters.ValueConverters;
using IDatePicker = DIPS.Mobile.UI.Components.Pickers.DatePickerShared.IDatePicker;

namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePicker : Chip, IDatePicker
{
    public TimePicker()
    {
        Tapped += OnTapped;
    }

    private void OnTapped(object? sender, EventArgs e)
    {
        TimePickerService.Open(this, this);
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);
        
        if(args.NewHandler is null)
            return;
        
        OnTimeChanged();
    }

    private void OnTimeChanged()
    {
        SetTitle(SelectedTime);
    }

    internal void SetTitle(TimeSpan timeSpan)
    {
        var convertedDisplayValue =
            new TimeConverter { Format = TimeConverter.TimeConverterFormat.Default }.Convert(timeSpan,
                null, null,
                CultureInfo.CurrentCulture);
        if (convertedDisplayValue is string displayItemAsString)
        {
            Title = displayItemAsString;
        }
    }

    public event Action<DateTime?>? SelectedDateTimeChanged;
    
    public DatePickerMode Mode => DatePickerMode.Time;
    
    public virtual void SetSelectedDateTime(DateTime? selectedDate)
    {
        if (selectedDate.HasValue)
        {
            if(selectedDate.Value.TimeOfDay.Ticks == SelectedTime.Ticks)
                return;
            
            SelectedTime = selectedDate.Value.TimeOfDay;
        }
        
        SelectedTimeCommand?.Execute(selectedDate?.TimeOfDay);
        SelectedDateTimeChanged?.Invoke(selectedDate);
    }

    public bool ShouldDisplayTodayButton => false;

    public bool IgnoreLocalTime => true;
    public DateTimeKind GetDateTimeKind()
    {
        return DateTimeKind.Unspecified;
    }

    public virtual TimeSpan GetTimeOnOpen()
    {
        return SelectedTime;
    }

}