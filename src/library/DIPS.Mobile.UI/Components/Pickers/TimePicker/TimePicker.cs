using System.Globalization;
using DIPS.Mobile.UI.Components.Chips;
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

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        OnTimeChanged();
    }

    private void OnTimeChanged()
    {
        SetTitle(SelectedTime);
    }

    protected void SetTitle(TimeSpan timeSpan)
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
}