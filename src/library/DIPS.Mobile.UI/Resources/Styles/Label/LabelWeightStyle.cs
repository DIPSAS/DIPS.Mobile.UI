namespace DIPS.Mobile.UI.Resources.Styles.Label;

public class LabelWeightStyle
{
    internal static Style OneThousand => new(typeof(Components.Labels.Label))
    {
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.FontSizeProperty,
                Value = 64
            }
        }
    };
    
    internal static Style NineHundred => new(typeof(Components.Labels.Label))
    {
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.FontSizeProperty,
                Value = 64
            }
        }
    };
    
    internal static Style EightHundred => new(typeof(Components.Labels.Label))
    {
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.FontSizeProperty,
                Value = 40
            }
        }
    };
    
    internal static Style SevenHundred => new(typeof(Components.Labels.Label))
    {
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.FontSizeProperty,
                Value = 32
            }
        }
    };
    
    internal static Style SixHundred => new(typeof(Components.Labels.Label))
    {
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.FontSizeProperty,
                Value = 24
            }
        }
    };
    
    internal static Style FiveHundred => new(typeof(Components.Labels.Label))
    {
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.FontSizeProperty,
                Value = 20
            }
        }
    };
    
    internal static Style FourHundred => new(typeof(Components.Labels.Label))
    {
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.FontSizeProperty,
                Value = 18
            }
        }
    };
    
    internal static Style ThreeHundred => new(typeof(Components.Labels.Label))
    {
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.FontSizeProperty,
                Value = 16
            }
        }
    };
    
    internal static Style TwoHundred => new(typeof(Components.Labels.Label))
    {
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.FontSizeProperty,
                Value = 14
            }
        }
    };
    
    internal static Style OneHundred => new(typeof(Components.Labels.Label))
    {
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.FontSizeProperty,
                Value = 12
            }
        }
    };
}