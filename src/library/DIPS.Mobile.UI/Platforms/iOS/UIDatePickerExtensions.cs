using CoreAnimation;
using CoreGraphics;
using CoreImage;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Extensions.iOS;
using DIPS.Mobile.UI.Resources.Colors;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Platforms.iOS;

public static class UIDatePickerExtensions
{
    public static void SetInLineLabelColors(this UIDatePicker datePicker)
    {
        datePicker.TintColor = Colors.GetColor(ColorName.color_primary_90).ToPlatform();
    }
}