using DIPS.Mobile.UI.Components.Pickers.DatePicker.iOS;
using DIPS.Mobile.UI.Platforms.iOS;
using Microsoft.Maui.Handlers;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

internal class InternalDatePickerHandler : ViewHandler<InternalDatePicker, DUIDatePicker>
{
    public InternalDatePickerHandler() : base(PropertyMapper)
    {
    }
    
    public static readonly IPropertyMapper<InternalDatePicker, InternalDatePickerHandler> PropertyMapper = new PropertyMapper<InternalDatePicker, InternalDatePickerHandler>(ViewMapper)
    {
        [nameof(InternalDatePicker.HorizontalOptions)] = MapOverrideHorizontalOptions
    };

    private static void MapOverrideHorizontalOptions(InternalDatePickerHandler handler, InternalDatePicker internalDatePicker)
    {
        handler.PlatformView.SetHorizontalAlignment(internalDatePicker);
    }

    protected override DUIDatePicker CreatePlatformView()
    {
        return new DUIDatePicker {PreferredDatePickerStyle = UIDatePickerStyle.Compact, Mode = VirtualView.Mode, InternalDatePicker = VirtualView};
    }
}