namespace DIPS.Mobile.UI.Resources.Styles.Button;

public static class ButtonTypeStyle
{
    private static Style Primary => new(typeof(Components.Buttons.Button))
    {
        BasedOn = ButtonDefaultStyle.Current,
        Setters =
        {
            new Setter
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_fill_action)
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_text_default_inverted)
            },
            new Setter
            {
                Property = Components.Buttons.Button.ImageTintColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_icon_on_fill_default_active)
            }
        }
    };
    
    private static Style Secondary => new(typeof(Components.Buttons.Button))
    {
        BasedOn = ButtonDefaultStyle.Current,
        Setters =
        {
            new Setter
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_fill_action_secondary)
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.BorderColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_border_action_secondary)
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.BorderWidthProperty,
                Value = Sizes.Sizes.GetSize(SizeName.stroke_medium)
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_text_action_neutral)
            },
            new Setter
            {
                Property = Components.Buttons.Button.ImageTintColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_icon_default)
            }
        }
    };
    
    private static Style Ghost => new(typeof(Components.Buttons.Button))
    {
        BasedOn = ButtonDefaultStyle.Current,
        Setters =
        {
            new Setter
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_fill_action_ghost)
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_text_action)
            },
            new Setter
            {
                Property = Components.Buttons.Button.ImageTintColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_icon_action)
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.FontFamilyProperty,
                Value = "UI"
            },
        }
    };
    
    internal static Style Disabled => new(typeof(Components.Buttons.Button))
    {
        BasedOn = ButtonDefaultStyle.Current,
        Setters =
        {
            new Setter
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_fill_disabled)
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_text_on_fill_disabled)
            },
            new Setter
            {
                Property = Components.Buttons.Button.ImageTintColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_icon_on_fill_deactivated)
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.BorderWidthProperty,
                Value = 0
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.BorderColorProperty,
                Value = Microsoft.Maui.Graphics.Colors.Transparent
            }
        }
    };
    
    public static Style PrimaryLarge => new(typeof(Components.Buttons.Button))
    {
        BasedOn = Primary,
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = new Thickness(Sizes.Sizes.GetSize(SizeName.content_margin_xlarge), Sizes.Sizes.GetSize(SizeName.content_margin_medium))
            }
        }
    };
    
    public static Style PrimarySmall => new(typeof(Components.Buttons.Button))
    {
        BasedOn = Primary,
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = new Thickness(Sizes.Sizes.GetSize(SizeName.content_margin_large), Sizes.Sizes.GetSize(SizeName.content_margin_xsmall))
            }
        }
    };
    
    public static Style SecondaryLarge => new(typeof(Components.Buttons.Button))
    {
        BasedOn = Secondary,
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = new Thickness(Sizes.Sizes.GetSize(SizeName.content_margin_xlarge), Sizes.Sizes.GetSize(SizeName.content_margin_medium))
            }
        }
    };
    
    public static Style SecondarySmall => new(typeof(Components.Buttons.Button))
    {
        BasedOn = Secondary,
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = new Thickness(Sizes.Sizes.GetSize(SizeName.content_margin_large), Sizes.Sizes.GetSize(SizeName.content_margin_xsmall))
            }
        }
    };
    
    public static Style GhostLarge => new(typeof(Components.Buttons.Button))
    {
        BasedOn = Ghost,
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = new Thickness(Sizes.Sizes.GetSize(SizeName.content_margin_xlarge), Sizes.Sizes.GetSize(SizeName.content_margin_medium))
            }
        }
    };
    
    public static Style GhostSmall => new(typeof(Components.Buttons.Button))
    {
        BasedOn = Ghost,
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = new Thickness(Sizes.Sizes.GetSize(SizeName.content_margin_large), Sizes.Sizes.GetSize(SizeName.content_margin_xsmall))
            }
        }
    };
    
    public static Style PrimaryIconButtonSmall => new(typeof(Components.Buttons.Button))
    {
        BasedOn = Primary,
        Setters =
        {
            new Setter()
            {
                Property = VisualElement.HeightRequestProperty,
                Value = Sizes.Sizes.GetSize(SizeName.size_10)
            },
            new Setter()
            {
                Property = VisualElement.WidthRequestProperty,
                Value = Sizes.Sizes.GetSize(SizeName.size_10)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.CornerRadiusProperty,
                Value = (int)Sizes.Sizes.GetSize(SizeName.size_5)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = (DeviceInfo.Current.Platform == DevicePlatform.Android)
                    ? Sizes.Sizes.GetSize(SizeName.content_margin_xsmall)
                    : 0
            }
        }
    };
        
    public static Style PrimaryIconButtonLarge => new(typeof(Components.Buttons.Button))
    {
        BasedOn = Primary,
        Setters =
        {
            new Setter()
            {
                Property = VisualElement.HeightRequestProperty,
                Value = Sizes.Sizes.GetSize(SizeName.size_14)
            },
            new Setter()
            {
                Property = VisualElement.WidthRequestProperty,
                Value = Sizes.Sizes.GetSize(SizeName.size_14)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.CornerRadiusProperty,
                Value = (int)Sizes.Sizes.GetSize(SizeName.size_7)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = (DeviceInfo.Current.Platform == DevicePlatform.Android)
                    ? Sizes.Sizes.GetSize(SizeName.content_margin_xsmall)
                    : 0
            }
        }
    };
        
    public static Style GhostIconButtonSmall => new(typeof(Components.Buttons.Button))
    {
        BasedOn = Ghost,
        Setters =
        {
            new Setter()
            {
                Property = VisualElement.HeightRequestProperty,
                Value = Sizes.Sizes.GetSize(SizeName.size_10)
            },
            new Setter()
            {
                Property = VisualElement.WidthRequestProperty,
                Value = Sizes.Sizes.GetSize(SizeName.size_10)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.CornerRadiusProperty,
                Value = (int)Sizes.Sizes.GetSize(SizeName.size_5)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = (DeviceInfo.Current.Platform == DevicePlatform.Android)
                    ? Sizes.Sizes.GetSize(SizeName.content_margin_xsmall)
                    : 0
            }
        }
    };
        
    public static Style GhostIconButtonLarge => new(typeof(Components.Buttons.Button))
    {
        BasedOn = Ghost,
        Setters =
        {
            new Setter()
            {
                Property = VisualElement.HeightRequestProperty,
                Value = Sizes.Sizes.GetSize(SizeName.size_14)
            },
            new Setter()
            {
                Property = VisualElement.WidthRequestProperty,
                Value = Sizes.Sizes.GetSize(SizeName.size_14)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.CornerRadiusProperty,
                Value = (int)Sizes.Sizes.GetSize(SizeName.size_7)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = (DeviceInfo.Current.Platform == DevicePlatform.Android)
                    ? Sizes.Sizes.GetSize(SizeName.content_margin_xsmall)
                    : 0
            }
        }
    };
        
    public static Style SecondaryIconButtonSmall => new(typeof(Components.Buttons.Button))
    {
        BasedOn = Secondary,
        Setters =
        {
            new Setter()
            {
                Property = VisualElement.HeightRequestProperty,
                Value = Sizes.Sizes.GetSize(SizeName.size_10)
            },
            new Setter()
            {
                Property = VisualElement.WidthRequestProperty,
                Value = Sizes.Sizes.GetSize(SizeName.size_10)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.CornerRadiusProperty,
                Value = (int)Sizes.Sizes.GetSize(SizeName.size_5)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = (DeviceInfo.Current.Platform == DevicePlatform.Android)
                    ? Sizes.Sizes.GetSize(SizeName.content_margin_xsmall)
                    : 0
            }
        }
    };
        
    public static Style SecondaryIconButtonLarge => new(typeof(Components.Buttons.Button))
    {
        BasedOn = Secondary,
        Setters =
        {
            new Setter()
            {
                Property = VisualElement.HeightRequestProperty,
                Value = Sizes.Sizes.GetSize(SizeName.size_14)
            },
            new Setter()
            {
                Property = VisualElement.WidthRequestProperty,
                Value = Sizes.Sizes.GetSize(SizeName.size_14)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.CornerRadiusProperty,
                Value = (int)Sizes.Sizes.GetSize(SizeName.size_7)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = (DeviceInfo.Current.Platform == DevicePlatform.Android)
                    ? Sizes.Sizes.GetSize(SizeName.content_margin_xsmall)
                    : 0
            }
        }
    };
    
    public static Style CloseIconButtonSmall => new(typeof(Components.Buttons.Button))
    {
        BasedOn = Ghost,
        Setters =
        {
            new Setter()
            {
                Property = VisualElement.HeightRequestProperty,
                Value = 36
            },
            new Setter()
            {
                Property = VisualElement.WidthRequestProperty,
                Value = 36
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.CornerRadiusProperty,
                Value = 18
            },
            new Setter()
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_fill_subtle)
            },
            new Setter()
            {
                Property = Components.Buttons.Button.ImageTintColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_icon_on_fill_default)
            },
            new Setter()
            {
                Property = Components.Buttons.Button.ImageSourceProperty,
                Value = Icons.Icons.GetIcon(IconName.close_line)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = 8
            }
        }
    };
}