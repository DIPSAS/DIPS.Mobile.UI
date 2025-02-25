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
                Value = Colors.Colors.GetColor(ColorName.color_text_subtle_large)
            }
        }
    };
}