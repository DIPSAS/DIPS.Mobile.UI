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
                Value = 4
            },
            new Setter
            {
                Property = SingleLineInputField.BorderThicknessProperty, 
                Value = 1
            },
            new Setter
            {
                Property = SingleLineInputField.BorderColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_neutral_40)
            },
            new Setter
            {
                Property = VisualElement.MinimumHeightRequestProperty,
                Value = 52
            }
        }
    };
}