namespace DIPS.Mobile.UI.Resources.Styles.Chip;

internal static class ToggleStyle
{
    internal static Style ToggledOn =>
        new(typeof(Components.Chips.Chip))
        {
            Setters =
            {
                new Setter
                {
                    Property = Components.Chips.Chip.ColorProperty,
                    // TODO: Wait for semantic color
                    Value = Colors.Colors.GetColor(ColorName.color_primary_80)
                },
                new Setter
                {
                    Property = Components.Chips.Chip.CornerRadiusProperty,
                    Value = Sizes.Sizes.GetSize(SizeName.radius_small)
                },
                new Setter
                {
                    Property = Components.Chips.Chip.TitleColorProperty,
                    Value = Colors.Colors.GetColor(ColorName.color_text_on_action)
                },
                new Setter
                {
                    Property = Components.Chips.Chip.BorderWidthProperty,
#if __ANDROID__
                    Value = 2.0,

#elif __IOS__
                    Value = 1.0,
#endif
                },
                new Setter
                {
                    Property = Components.Chips.Chip.BorderColorProperty,
                    Value = Microsoft.Maui.Graphics.Colors.Transparent
                }
            }
        };
}