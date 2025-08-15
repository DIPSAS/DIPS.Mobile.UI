namespace DIPS.Mobile.UI.Resources.Styles.Chip;

public static class ReadOnlyStyle
{
    public static Style Current =>
        new(typeof(Components.Chips.Chip))
        {
            BasedOn = InputStyle.Current,
            Setters =
            {
                new Setter()
                {
                    Property = Components.Chips.Chip.ColorProperty,
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_fill_input_readonly)
                },
                new Setter()
                {
                    Property = Components.Chips.Chip.CornerRadiusProperty,
                    Value = DIPS.Mobile.UI.Resources.Sizes.Sizes.GetSize(SizeName.radius_small)
                },
                new Setter
                {
                    Property = Components.Chips.Chip.TitleColorProperty,
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_text_readonly)
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
                    Value = Colors.Colors.GetColor(ColorName.color_border_input_readonly)
                }
            },
        };
}