using DIPS.Mobile.UI.Components.Pickers.Platforms;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

internal class InternalDatePicker : View
{
    public UIDatePickerMode Mode { get; set; }
    public IDateTimePicker DateTimePicker { get; set; }
}