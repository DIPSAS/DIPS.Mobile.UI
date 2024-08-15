using DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;

public class InlineDateAndTimePickerHandler : DateAndTimePickerHandler
{
    protected override UIDatePicker CreatePlatformView()
    {
        return new UIDatePicker
        {
            Mode = UIDatePickerMode.DateAndTime, PreferredDatePickerStyle = UIDatePickerStyle.Inline
        };
    }
}