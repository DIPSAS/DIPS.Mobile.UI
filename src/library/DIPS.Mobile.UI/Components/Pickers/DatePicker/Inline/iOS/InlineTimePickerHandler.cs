using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using DIPS.Mobile.UI.Components.Pickers.TimePicker;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;

internal class InlineTimePickerHandler : TimePickerHandler
{
    protected override UIDatePicker CreatePlatformView()
    {
        return new UIDatePicker
        {
            PreferredDatePickerStyle = UIDatePickerStyle.Wheels, Mode = UIDatePickerMode.Time
        };
    }
}