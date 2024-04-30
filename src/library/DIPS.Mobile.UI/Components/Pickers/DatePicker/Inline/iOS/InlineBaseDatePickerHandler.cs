using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;

internal class InlineBaseDatePickerHandler : BaseDatePickerHandler
{
    /*protected override DUIDatePicker CreatePlatformView()
    {
        return new DUIDatePicker {PreferredDatePickerStyle = UIDatePickerStyle.Inline, Mode = UIDatePickerMode.Date};
    }*/

    public InlineBaseDatePickerHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
    {
    }

    protected override UIDatePickerMode GetMode()
    {
        throw new NotImplementedException();
    }

    protected override void OnDateSelected()
    {
        throw new NotImplementedException();
    }
}