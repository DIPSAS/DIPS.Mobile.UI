using Android.Content.Res;
using DIPS.Mobile.UI.Resources.Colors;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using TextAlignment = Android.Views.TextAlignment;

namespace DIPS.Mobile.UI.Components.Chips.Android;

public static class ChipExtensions
{
    public static void SetDefaultChipAttributes(this Google.Android.Material.Chip.Chip chip)
    {
        chip.SetPadding(8, 2, 8, 2);
        chip.TextAlignment = (TextAlignment)Microsoft.Maui.TextAlignment.Center;
        chip.SetTextColor(Colors.GetColor(ColorName.color_system_black).ToPlatform());
        chip.TextSize = Sizes.GetSize(SizeName.size_4);
        chip.ChipCornerRadius = 24;
        chip.SetEnsureMinTouchTargetSize(false); //Remove extra margins around the chip, this is added to get more space to hit the chip but its not necessary : https://stackoverflow.com/a/57188310 
    }
    
    public static void SetBackgroundColor(this Google.Android.Material.Chip.Chip chip,  Color color)
    {
        var states = new[]
        {
            new[] { global::Android.Resource.Attribute.StateEnabled}, // enabled
            new[] {-global::Android.Resource.Attribute.StateEnabled}, // disabled
            new[] {-global::Android.Resource.Attribute.StateChecked}, // unchecked
            new[] { global::Android.Resource.Attribute.StateChecked } // pressed
        };

        var colors = new int[] 
        {
            color.ToPlatform(),
            color.ToPlatform(),
            color.ToPlatform(),
            color.ToPlatform()
        };

        chip.ChipBackgroundColor = new ColorStateList(states, colors);
    }
}