using DIPS.Mobile.UI.Resources.Colors;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Platforms.iOS;

public static class UIDatePickerExtensions
{
    public static void SetDefaultTintColor(this UIDatePicker datePicker)
    {
        datePicker.TintColor = Colors.GetColor(ColorName.color_primary_90).ToPlatform();
    }
}