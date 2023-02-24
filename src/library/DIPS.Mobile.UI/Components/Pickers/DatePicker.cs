using System;
using System.Globalization;
using DIPS.Mobile.UI.Converters.ValueConverters;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Pickers
{
    public partial class DatePicker : PickerBase
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
            if (newvalue is not DateTime dateTime) return;

            var convertedDisplayValue =
                new DateConverter() {Format = datePicker.DateFormat}.Convert(dateTime, null, null,
                    CultureInfo.CurrentCulture);
            if (convertedDisplayValue is string displayItemAsString)
            {
                datePicker.SetPickedItemText(displayItemAsString);    
            }
            
        }
    }
}