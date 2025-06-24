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
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_fill_default_active_subtle)
                },
                new Setter()
                {
                    Property = Components.Chips.Chip.CloseButtonColorProperty,
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_text_action_on_fill)
                },
                new Setter()
                {
                    Property = Components.Chips.Chip.CornerRadiusProperty,
                    Value = DIPS.Mobile.UI.Resources.Sizes.Sizes.GetSize(SizeName.radius_small)
                },
                new Setter
                {
                    Property = Components.Chips.Chip.TitleColorProperty,
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_text_action_on_fill)
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
                    Value = Colors.Colors.GetColor(ColorName.color_border_action_secondary_active)
                },
                new Setter
                {
                    Property = Components.Chips.Chip.CustomIconTintColorProperty,
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_icon_action_on_fill)
                }
            },
        };
}