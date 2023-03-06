using System;
using System.Globalization;
using DIPS.Mobile.UI.Components.Pickers.Base;
using DIPS.Mobile.UI.Converters.ValueConverters;
using Xamarin.Forms;
using Picker = DIPS.Mobile.UI.Components.Pickers.Base.Picker;

namespace DIPS.Mobile.UI.Components.Pickers
{
    public partial class DatePicker : Picker
    {

        public override void Open()
        {
            IsOpen = true;
        }

        public override void Close()
        {
            IsOpen = false;
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