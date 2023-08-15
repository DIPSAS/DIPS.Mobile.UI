using DIPS.Mobile.UI.Components.Pickers.DatePicker.iOS;
using Foundation;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline;

internal class InlineDatePickerHandler : DatePickerHandler
{
    protected override DUIDatePicker CreatePlatformView()
    {
        return new DUIDatePicker {PreferredDatePickerStyle = UIDatePickerStyle.Inline, Mode = UIDatePickerMode.Date};
    }
}