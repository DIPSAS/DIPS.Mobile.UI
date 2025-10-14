namespace DIPS.Mobile.UI.Resources.Styles.Button;

public static class ButtonTypeStyle
{
    private static Style CallToAction => new(typeof(Components.Buttons.Button))
    {
        BasedOn = ButtonDefaultStyle.Current,
        Setters =
        {
            new Setter
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_fill_button_cta)
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_text_on_button_inverted)
            },
            new Setter
            {
                Property = Components.Buttons.Button.ImageTintColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_icon_on_button_inverted)
            }
        }
    };
    
    private static Style Default => new(typeof(Components.Buttons.Button))
    {
        BasedOn = ButtonDefaultStyle.Current,
        Setters =
        {
            new Setter
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_fill_button)
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.BorderColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_border_button)
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.BorderWidthProperty,
                Value = Sizes.Sizes.GetSize(SizeName.stroke_medium)
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_text_on_button)
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
                Value = Colors.Colors.GetColor(ColorName.color_fill_button_ghost)
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
                Value = Colors.Colors.GetColor(ColorName.color_fill_button_disabled)
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_text_on_fill_disabled)
            },
            new Setter
            {
                Property = Components.Buttons.Button.ImageTintColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_icon_on_fill_disabled)
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
    
    public static Style CallToActionLarge => new(typeof(Components.Buttons.Button))
    {
        BasedOn = CallToAction,
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = new Thickness(Sizes.Sizes.GetSize(SizeName.content_margin_xlarge), Sizes.Sizes.GetSize(SizeName.content_margin_medium))
            }
        }
    };
    
    public static Style CallToActionSmall => new(typeof(Components.Buttons.Button))
    {
        BasedOn = CallToAction,
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = new Thickness(Sizes.Sizes.GetSize(SizeName.content_margin_large), Sizes.Sizes.GetSize(SizeName.content_margin_xsmall))
            }
        }
    };
    
    public static Style DefaultLarge => new(typeof(Components.Buttons.Button))
    {
        BasedOn = Default,
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = new Thickness(Sizes.Sizes.GetSize(SizeName.content_margin_xlarge), Sizes.Sizes.GetSize(SizeName.content_margin_medium))
            }
        }
    };
    
    public static Style DefaultSmall => new(typeof(Components.Buttons.Button))
    {
        BasedOn = Default,
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
    
    public static Style CallToActionIconSmall => new(typeof(Components.Buttons.Button))
    {
        BasedOn = CallToAction,
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
        
    public static Style CallToActionIconLarge => new(typeof(Components.Buttons.Button))
    {
        BasedOn = CallToAction,
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
        
    public static Style GhostIconSmall => new(typeof(Components.Buttons.Button))
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
        
    public static Style GhostIconLarge => new(typeof(Components.Buttons.Button))
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
        
    public static Style DefaultIconSmall => new(typeof(Components.Buttons.Button))
    {
        BasedOn = Default,
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
        
    public static Style DefaultIconLarge => new(typeof(Components.Buttons.Button))
    {
        BasedOn = Default,
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
    
    public static Style CloseIconSmall => new(typeof(Components.Buttons.Button))
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
                Value = Colors.Colors.GetColor(ColorName.color_fill_neutral)
            },
            new Setter()
            {
                Property = Components.Buttons.Button.ImageTintColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_icon_subtle)
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
    
    public static Style DefaultFloatingButton => new(typeof(Components.Buttons.Button))
    {
        BasedOn = DefaultIconLarge,
        Setters =
        {
            new Setter
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_fill_button_hover)
            }
        }
    };
    
    public static Style DefaultFloating => new(typeof(Components.Buttons.Button))
    {
        BasedOn = Default,
        Setters =
        {
            new Setter
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_fill_button_hover)
            }
        }
    };
}