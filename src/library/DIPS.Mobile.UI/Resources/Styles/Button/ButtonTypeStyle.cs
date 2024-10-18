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
                Value = Colors.Colors.GetColor(ColorName.color_primary_90)
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_system_white)
            },
            new Setter
            {
                Property = Components.Buttons.Button.ImageTintColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_system_white)
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
                Value = Colors.Colors.GetColor(ColorName.color_system_white)
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.BorderColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_secondary_90)
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.BorderWidthProperty,
                Value = 1
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_neutral_90)
            },
            new Setter
            {
                Property = Components.Buttons.Button.ImageTintColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_neutral_80)
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
                Value = Microsoft.Maui.Graphics.Colors.Transparent
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_primary_90)
            },
            new Setter
            {
                Property = Components.Buttons.Button.ImageTintColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_primary_90)
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
                Value = Colors.Colors.GetColor(ColorName.color_neutral_30)
            },
            new Setter
            {
                Property = Microsoft.Maui.Controls.Button.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_neutral_80)
            },
            new Setter
            {
                Property = DIPS.Mobile.UI.Components.Buttons.Button.ImageTintColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_neutral_80)
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
                Value = new Thickness(Sizes.Sizes.GetSize(SizeName.size_7), Sizes.Sizes.GetSize(SizeName.size_3))
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
                Value = new Thickness(Sizes.Sizes.GetSize(SizeName.size_4), Sizes.Sizes.GetSize(SizeName.size_1))
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
                Value = new Thickness(Sizes.Sizes.GetSize(SizeName.size_7), Sizes.Sizes.GetSize(SizeName.size_3))
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
                Value = new Thickness(Sizes.Sizes.GetSize(SizeName.size_4), Sizes.Sizes.GetSize(SizeName.size_1))
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
                Value = new Thickness(Sizes.Sizes.GetSize(SizeName.size_7), Sizes.Sizes.GetSize(SizeName.size_3))
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
                Value = new Thickness(Sizes.Sizes.GetSize(SizeName.size_4), Sizes.Sizes.GetSize(SizeName.size_1))
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
                Value = Sizes.Sizes.GetSize(SizeName.size_5)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = (DeviceInfo.Current.Platform == DevicePlatform.Android)
                    ? Sizes.Sizes.GetSize(SizeName.size_1)
                    : Sizes.Sizes.GetSize(SizeName.size_2)
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
                Value = Sizes.Sizes.GetSize(SizeName.size_7)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = (DeviceInfo.Current.Platform == DevicePlatform.Android)
                    ? Sizes.Sizes.GetSize(SizeName.size_1)
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
                Value = Sizes.Sizes.GetSize(SizeName.size_5)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = (DeviceInfo.Current.Platform == DevicePlatform.Android)
                    ? Sizes.Sizes.GetSize(SizeName.size_1)
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
                Value = Sizes.Sizes.GetSize(SizeName.size_7)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = (DeviceInfo.Current.Platform == DevicePlatform.Android)
                    ? Sizes.Sizes.GetSize(SizeName.size_1)
                    : Sizes.Sizes.GetSize(SizeName.size_2)
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
                Value = Sizes.Sizes.GetSize(SizeName.size_5)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = (DeviceInfo.Current.Platform == DevicePlatform.Android)
                    ? Sizes.Sizes.GetSize(SizeName.size_1)
                    : Sizes.Sizes.GetSize(SizeName.size_2)
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
                Value = Sizes.Sizes.GetSize(SizeName.size_7)
            },
            new Setter()
            {
                Property = Microsoft.Maui.Controls.Button.PaddingProperty,
                Value = (DeviceInfo.Current.Platform == DevicePlatform.Android)
                    ? Sizes.Sizes.GetSize(SizeName.size_1)
                    : Sizes.Sizes.GetSize(SizeName.size_2)
            }
        }
    };
}