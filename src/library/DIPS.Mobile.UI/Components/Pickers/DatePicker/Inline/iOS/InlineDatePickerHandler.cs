using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline;

internal partial class InlineDatePickerHandler : DatePickerHandler
{
    protected override UIDatePicker CreatePlatformView()
    {
        return new UIDatePicker {PreferredDatePickerStyle = UIDatePickerStyle.Inline, Mode = UIDatePickerMode.Date};
    }
}