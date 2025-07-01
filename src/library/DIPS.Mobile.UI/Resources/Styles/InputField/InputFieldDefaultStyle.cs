using DIPS.Mobile.UI.Components.TextFields.InputFields;

namespace DIPS.Mobile.UI.Resources.Styles.InputField;

public class InputFieldDefaultStyle
{
    public static Style Current => new(typeof(SingleLineInputField))
    {
        Setters =
        {
            new Setter
            {
                Property = SingleLineInputField.BorderCornerRadiusProperty, 
                Value = Sizes.Sizes.GetSize(SizeName.radius_xsmall)
            },
            new Setter
            {
                Property = SingleLineInputField.BorderThicknessProperty, 
                Value = Sizes.Sizes.GetSize(SizeName.stroke_medium)
            },
            new Setter
            {
                Property = SingleLineInputField.BorderColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_border_default)
            },
            new Setter
            {
                Property = VisualElement.MinimumHeightRequestProperty,
                Value = 52
            },
            new Setter
            {
                Property = SingleLineInputField.HelpTextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_text_subtle)
            }
        }
    };
}