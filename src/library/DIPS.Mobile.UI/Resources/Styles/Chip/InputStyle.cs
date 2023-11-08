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
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_secondary_30)
                },
                new Setter()
                {
                    Property = Components.Chips.Chip.CloseButtonColorProperty,
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_neutral_70)
                },
                new Setter()
                {
                    Property = Components.Chips.Chip.CornerRadiusProperty,
                    Value = DIPS.Mobile.UI.Resources.Sizes.Sizes.GetSize(SizeName.size_2)
                },
                new Setter
                {
                    Property = Components.Chips.Chip.TitleColorProperty,
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_system_black)
                },
            },
        };
}