using System.Globalization;
using DIPS.Mobile.UI.Converters.ValueConverters;
using Picker = DIPS.Mobile.UI.Components.Pickers.Base.Picker;

namespace DIPS.Mobile.UI.Components.Pickers.DateTimePickers;

public partial class TimePicker : Picker, IDateTimePicker
{
    public TimePicker()
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
    
    private static void OnSelectedTimeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (!(bindable is TimePicker timePicker))
            return;
        var formattedObject = new TimeConverter() { Format = timePicker.TimeFormat }.Convert(timePicker.SelectedTime, null, null, CultureInfo.CurrentCulture);
        if (!(formattedObject is string formattedDate))
            return;
        timePicker.PickedItemLabel.Text = formattedDate;
    }
}