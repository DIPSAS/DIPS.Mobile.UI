namespace DIPS.Mobile.UI.Resources.Styles.Chip;

public static class InputStyle
{
    public static Style Current =>
        new(typeof(Components.Chips.Chip))
        {
            Setters =
            {
                new Setter()
                {
                    Property = Components.Chips.Chip.ColorProperty,
                    // TODO: Lisa
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_secondary_30)
                },
                new Setter()
                {
                    Property = Components.Chips.Chip.CloseButtonColorProperty,
                    // TODO: Lisa
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_neutral_70)
                },
                new Setter()
                {
                    Property = Components.Chips.Chip.CornerRadiusProperty,
                    Value = DIPS.Mobile.UI.Resources.Sizes.Sizes.GetSize(SizeName.radius_small)
                },
                new Setter
                {
                    Property = Components.Chips.Chip.TitleColorProperty,
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_text_default)
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
                    // TODO: Lisa
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(
                        ColorName.color_secondary_30)
                }
            },
        };
}