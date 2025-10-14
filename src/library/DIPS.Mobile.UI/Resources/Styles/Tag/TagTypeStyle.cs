namespace DIPS.Mobile.UI.Resources.Styles.Tag;

public class TagTypeStyle
{
    public static Style Default => new(typeof(Components.Tag.Tag))
    {
        Setters =
        {
            new Setter
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_fill_default)
            },
            new Setter
            {
                Property = Components.Tag.Tag.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_text_default)
            },
            new Setter
            {
                Property = Components.Tag.Tag.IconColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_icon_default)
            }
        }
    };
    
    public static Style Danger => new(typeof(Components.Tag.Tag))
    {
        Setters =
        {
            new Setter
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_fill_danger)
            },
            new Setter
            {
                Property = Components.Tag.Tag.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_text_default)
            },
            new Setter
            {
                Property = Components.Tag.Tag.IconColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_icon_readonly)
            }
        }
    };
    
    public static Style Subtle => new(typeof(Components.Tag.Tag))
    {
        Setters =
        {
            new Setter
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_fill_neutral)
            },
            new Setter
            {
                Property = Components.Tag.Tag.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_text_readonly)
            },
            new Setter
            {
                Property = Components.Tag.Tag.IconColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_icon_readonly)
            }
        }
    };
    
    public static Style Success => new(typeof(Components.Tag.Tag))
    {
        Setters =
        {
            new Setter
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_fill_success)
            },
            new Setter
            {
                Property = Components.Tag.Tag.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_text_default)
            },
            new Setter
            {
                Property = Components.Tag.Tag.IconColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_icon_default)
            }
        }
    };
    
    public static Style Warning => new(typeof(Components.Tag.Tag))
    {
        Setters =
        {
            new Setter
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_fill_warning)
            },
            new Setter
            {
                Property = Components.Tag.Tag.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_text_default)
            },
            new Setter
            {
                Property = Components.Tag.Tag.IconColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_icon_default)
            }
        }
    };
    
    public static Style Information => new(typeof(Components.Tag.Tag))
    {
        Setters =
        {
            new Setter
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_fill_information)
            },
            new Setter
            {
                Property = Components.Tag.Tag.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_text_default)
            },
            new Setter
            {
                Property = Components.Tag.Tag.IconColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_icon_default)
            }
        }
    };
}