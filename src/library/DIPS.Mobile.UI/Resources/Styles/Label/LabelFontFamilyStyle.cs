namespace DIPS.Mobile.UI.Resources.Styles.Label;

public class LabelFontFamilyStyle
{
    internal static Style Body => new(typeof(Components.Labels.Label))
    {
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.FontFamilyProperty,
                Value = "Body"
            }
        }
    };
    
    internal static Style UI => new(typeof(Components.Labels.Label))
    {
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.FontFamilyProperty,
                Value = "UI"
            }
        }
    };
    
    internal static Style Header => new(typeof(Components.Labels.Label))
    {
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.FontFamilyProperty,
                Value = "Header"
            }
        }
    };
}