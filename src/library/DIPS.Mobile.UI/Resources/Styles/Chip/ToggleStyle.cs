namespace DIPS.Mobile.UI.Resources.Styles.Chip;

public static class ToggleStyle
{
    public static Style ToggledOff =>
        new(typeof(Components.Chips.Chip))
        {
            Setters =
            {
                new Setter()
                {
                    Property = Components.Chips.Chip.CornerRadiusProperty,
                    Value = DIPS.Mobile.UI.Resources.Sizes.Sizes.GetSize(SizeName.size_2)
                },
                new Setter
                {
                    Property = Components.Chips.Chip.ColorProperty,
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(
                        ColorName.color_system_white)
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
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(
                        ColorName.color_neutral_30)
                }
            }
        };
    
    public static Style ToggledOn =>
        new(typeof(Components.Chips.Chip))
        {
            Setters =
            {
                new Setter()
                {
                    Property = Components.Chips.Chip.CornerRadiusProperty,
                    Value = DIPS.Mobile.UI.Resources.Sizes.Sizes.GetSize(SizeName.size_2)
                },
                new Setter
                {
                    Property = Components.Chips.Chip.ColorProperty,
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(
                        ColorName.color_primary_80)
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
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(
                        ColorName.color_primary_80)
                },
                new Setter
                {
                    Property = Components.Chips.Chip.TitleColorProperty,
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(
                        ColorName.color_system_white)
                }
            }
        };
}