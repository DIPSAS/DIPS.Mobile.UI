using Android.Content.Res;
using DIPS.Mobile.UI.Resources.Colors;
using Microsoft.Maui.Platform;
using Chip = Google.Android.Material.Chip.Chip;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using TextAlignment = Android.Views.TextAlignment;

namespace DIPS.Mobile.UI.Platforms.Android;

public static class ChipExtensions
{
    public static void SetDefaultChipAttributes(this Chip chip)
    {
        chip.SetPadding(8, 2, 8, 2);
        chip.TextAlignment = (TextAlignment)Microsoft.Maui.TextAlignment.Center;
        chip.SetTextColor(Colors.GetColor(ColorName.color_system_black).ToPlatform());
        chip.TextSize = 17;
        chip.ChipCornerRadius = 24;
        
        SetBackgroundColor(new Color(118, 118, 128, 30), chip);
    }
    
    private static void SetBackgroundColor(Color color, Chip chip)
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