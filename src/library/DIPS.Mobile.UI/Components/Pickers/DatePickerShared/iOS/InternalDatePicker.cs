using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

internal class InternalDatePicker : View
{
    public UIDatePickerMode Mode { get; set; }
    public INullableDatePicker DatePicker { get; set; }
}