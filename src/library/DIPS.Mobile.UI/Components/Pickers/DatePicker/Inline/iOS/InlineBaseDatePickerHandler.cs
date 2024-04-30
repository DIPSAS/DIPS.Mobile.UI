using DIPS.Mobile.UI.Components.Pickers.DatePicker.iOS;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;

internal class InlineBaseDatePickerHandler : DatePickerHandler
{
    public InlineBaseDatePickerHandler() : base(PropertyMapper)
    {
    }

    protected override UIView CreatePlatformView()
    {
        m_nativeDatePicker = new DUIDatePicker
        {
            PreferredDatePickerStyle = UIDatePickerStyle.Inline, Mode = UIDatePickerMode.Date
        };

        return m_nativeDatePicker;
    }
}