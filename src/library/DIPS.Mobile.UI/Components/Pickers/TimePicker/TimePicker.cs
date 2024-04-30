using DIPS.Mobile.UI.Components.Pickers.DatePickerShared;

namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePicker : View, INullableDatePicker
{
    public bool IsNullable { get; set; }
    public bool IsDateOrTimeNull => SelectedTime is null;

    public void SetDateOrTimeNull()
    {
        SelectedTime = null;
        SelectedTimeCommand?.Execute(null);
    }
}