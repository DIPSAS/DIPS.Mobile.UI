using System.Globalization;
using DIPS.Mobile.UI.Converters.ValueConverters;
using Picker = DIPS.Mobile.UI.Components.Pickers.Base.Picker;

namespace DIPS.Mobile.UI.Components.Pickers.DateTimePickers;

public partial class DateAndTimePicker : Picker, IDateTimePicker
{
    public DateAndTimePicker()
    {
        GestureRecognizers.Add(new TapGestureRecognizer {Command = new Command(Open)});
    }
        
    public void Open()
    {
        IsOpen = true;
        DateTimePickerService.OpenDateTimePicker(this);
    }

    public void Close()
    {
        IsOpen = false;
        //DateTimePickerService.OpenDateTimePicker.Close();
    }
    
    private void FormatAndSetDateText()
    {
        var convertedDisplayValue = new DateAndTimeConverter { Format = DateFormat, IgnoreLocalTime = IgnoreLocalTime}
            .Convert(SelectedDateTime,
                null!,
                null!, 
                CultureInfo.CurrentCulture);
        if (convertedDisplayValue is string displayItemAsString)
        {
            SetPickedItemText(displayItemAsString);
        }
    }

    private static void OnSelectedDateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not DateAndTimePicker datePicker)
            return;
            
        datePicker.FormatAndSetDateText();
        datePicker.SelectedDateTimeCommand?.Execute(datePicker.SelectedDateTime);
        datePicker.DidSelectDateTime?.Invoke(datePicker, datePicker.SelectedDateTime);
    }
}