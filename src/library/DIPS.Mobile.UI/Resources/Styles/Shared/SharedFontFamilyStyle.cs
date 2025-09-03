namespace DIPS.Mobile.UI.Resources.Styles.Shared;

public class SharedFontFamilyStyle
{
    internal static Style GetBodyStyle(Type targetType) => new(targetType)
    {
        Setters =
        {
            new Setter
            {
                Property = GetFontFamilyProperty(targetType),
                Value = "Body"
            }
        }
    };
    
    internal static Style GetUIStyle(Type targetType) => new(targetType)
    {
        Setters =
        {
            new Setter
            {
                Property = GetFontFamilyProperty(targetType),
                Value = "UI"
            }
        }
    };
    
    internal static Style GetHeaderStyle(Type targetType) => new(targetType)
    {
        Setters =
        {
            new Setter
            {
                Property = GetFontFamilyProperty(targetType),
                Value = "Header"
            }
        }
    };

    private static BindableProperty GetFontFamilyProperty(Type targetType)
    {
        if (targetType == typeof(Microsoft.Maui.Controls.Span))
            return Microsoft.Maui.Controls.Span.FontFamilyProperty;
        
        return Microsoft.Maui.Controls.Label.FontFamilyProperty;
    }
}
