using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;

internal class InlineBaseDatePickerHandler : DatePickerHandler
{
    public InlineBaseDatePickerHandler() : base(PropertyMapper)
    {
    }

    protected override DUIDatePicker CreatePlatformView()
    {
        return new DUIDatePicker
        {
            PreferredDatePickerStyle = UIDatePickerStyle.Inline, Mode = UIDatePickerMode.Date
        };;
    }
}