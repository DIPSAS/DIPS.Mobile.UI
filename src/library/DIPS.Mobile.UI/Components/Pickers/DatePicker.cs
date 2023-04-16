using System.Globalization;
using DIPS.Mobile.UI.Converters.ValueConverters;
using Picker = DIPS.Mobile.UI.Components.Pickers.Base.Picker;

namespace DIPS.Mobile.UI.Components.Pickers
{
    public partial class DatePicker : Picker
    {

        public DatePicker()
        {
            base.GestureRecognizers.Add(new TapGestureRecognizer{ Command = new Command(Open)});
        }
        
        public void Open()
        {
            IsOpen = true;
            DatePickerService.OpenDatePicker(this);
        }

        public void Close()
        {
            IsOpen = false;
            DatePickerService.Close();
        }

        private static void OnSelectedDateChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is not DatePicker datePicker) return;
            datePicker.FormatAndSetDateText(datePicker.SelectedDate);
            datePicker.SelectedDateCommand?.Execute(datePicker.SelectedDate);
            datePicker.DidSelectDate?.Invoke(datePicker, datePicker.SelectedDate);
            
        }

        private void FormatAndSetDateText(DateTime dateTime)
        {
            var convertedDisplayValue =
                new DateConverter() {Format = DateFormat}.Convert(dateTime, null, null,
                    CultureInfo.CurrentCulture);
            if (convertedDisplayValue is string displayItemAsString)
            {
                SetPickedItemText(displayItemAsString);
            }
        }
    }
}