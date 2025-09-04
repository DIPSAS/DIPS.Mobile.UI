namespace DIPS.Mobile.UI.Resources.Styles.Shared;

public class SharedWeightStyle
{
    internal static Style GetOneThousandStyle(Type targetType) => new(targetType)
    {
        Setters =
        {
            new Setter
            {
                Property = GetFontSizeProperty(targetType),
                Value = 64
            }
        }
    };
    
    internal static Style GetNineHundredStyle(Type targetType) => new(targetType)
    {
        Setters =
        {
            new Setter
            {
                Property = GetFontSizeProperty(targetType),
                Value = 64
            }
        }
    };
    
    internal static Style GetEightHundredStyle(Type targetType) => new(targetType)
    {
        Setters =
        {
            new Setter
            {
                Property = GetFontSizeProperty(targetType),
                Value = 40
            }
        }
    };
    
    internal static Style GetSevenHundredStyle(Type targetType) => new(targetType)
    {
        Setters =
        {
            new Setter
            {
                Property = GetFontSizeProperty(targetType),
                Value = 32
            }
        }
    };
    
    internal static Style GetSixHundredStyle(Type targetType) => new(targetType)
    {
        Setters =
        {
            new Setter
            {
                Property = GetFontSizeProperty(targetType),
                Value = 24
            }
        }
    };
    
    internal static Style GetFiveHundredStyle(Type targetType) => new(targetType)
    {
        Setters =
        {
            new Setter
            {
                Property = GetFontSizeProperty(targetType),
                Value = 20
            }
        }
    };
    
    internal static Style GetFourHundredStyle(Type targetType) => new(targetType)
    {
        Setters =
        {
            new Setter
            {
                Property = GetFontSizeProperty(targetType),
                Value = 18
            }
        }
    };
    
    internal static Style GetThreeHundredStyle(Type targetType) => new(targetType)
    {
        Setters =
        {
            new Setter
            {
                Property = GetFontSizeProperty(targetType),
                Value = 16
            }
        }
    };
    
    internal static Style GetTwoHundredStyle(Type targetType) => new(targetType)
    {
        Setters =
        {
            new Setter
            {
                Property = GetFontSizeProperty(targetType),
                Value = 14
            }
        }
    };
    
    internal static Style GetOneHundredStyle(Type targetType) => new(targetType)
    {
        Setters =
        {
            new Setter
            {
                Property = GetFontSizeProperty(targetType),
                Value = 12
            }
        }
    };

    private static BindableProperty GetFontSizeProperty(Type targetType)
    {
        if (targetType == typeof(Microsoft.Maui.Controls.Span))
            return Microsoft.Maui.Controls.Span.FontSizeProperty;
        
        return Microsoft.Maui.Controls.Label.FontSizeProperty;
    }
}
