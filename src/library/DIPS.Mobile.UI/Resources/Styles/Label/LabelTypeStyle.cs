namespace DIPS.Mobile.UI.Resources.Styles.Label;

public class LabelTypeStyle
{
    internal static Style SectionHeader => new(typeof(Components.Labels.Label))
    {
        BasedOn = LabelFontFamilyStyle.UI.ConcatenateWithStyle(LabelWeightStyle.ThreeHundred),
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_neutral_80)
            }
        }
    };
    
    internal static Style KeyOverValue => new(typeof(Components.Labels.Label))
    {
        BasedOn = LabelFontFamilyStyle.Body.ConcatenateWithStyle(LabelWeightStyle.ThreeHundred),
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_neutral_60)
            }
        }
    };
    
    internal static Style ValueBelowKey => new(typeof(Components.Labels.Label))
    {
        BasedOn = LabelFontFamilyStyle.Body.ConcatenateWithStyle(LabelWeightStyle.ThreeHundred),
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_neutral_90)
            }
        }
    };
    
    internal static Style KeyInlineWithValue => new(typeof(Components.Labels.Label))
    {
        BasedOn = LabelFontFamilyStyle.Body.ConcatenateWithStyle(LabelWeightStyle.ThreeHundred),
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_neutral_60)
            }
        }
    };
    
    internal static Style ValueInlineWithKey => new(typeof(Components.Labels.Label))
    {
        BasedOn = LabelFontFamilyStyle.Body.ConcatenateWithStyle(LabelWeightStyle.ThreeHundred),
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_neutral_90)
            }
        }
    };
}