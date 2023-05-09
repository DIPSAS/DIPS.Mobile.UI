using System.Globalization;
using DIPS.Mobile.UI.Converters.ValueConverters;
using Picker = DIPS.Mobile.UI.Components.Pickers.Base.Picker;

namespace DIPS.Mobile.UI.Components.Pickers.DateTimePickers
{
    public partial class DatePicker : Picker, IDateTimePicker
    {
        
        public DatePicker()
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
            var convertedDisplayValue = new DateConverter { Format = DateFormat, IgnoreLocalTime = IgnoreLocalTime }.
                Convert(SelectedDate,
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
            if(bindable is not DatePicker datePicker)
                return;
            
            datePicker.FormatAndSetDateText();
            datePicker.SelectedDateCommand?.Execute(datePicker.SelectedDate);
            datePicker.DidSelectDate?.Invoke(datePicker, datePicker.SelectedDate);
        }
    }
}