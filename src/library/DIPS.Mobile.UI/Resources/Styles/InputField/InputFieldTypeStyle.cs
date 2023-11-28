using DIPS.Mobile.UI.Components.TextFields.InputFields;

namespace DIPS.Mobile.UI.Resources.Styles.InputField;

public class InputFieldTypeStyle
{
    public static Style Disabled => new(typeof(SingleLineInputField))
    {
        BasedOn = InputFieldDefaultStyle.Current,
        Setters =
        {
           new Setter
           {
               Property = SingleLineInputField.BorderColorProperty,
               Value = Colors.Colors.GetColor(ColorName.color_neutral_30)
           },
           new Setter
           {
               Property = VisualElement.BackgroundColorProperty,
               Value = Colors.Colors.GetColor(ColorName.color_neutral_10)
           },
           new Setter
           {
               Property = SingleLineInputField.InputTextColorProperty,
               Value = Colors.Colors.GetColor(ColorName.color_neutral_60)
           }
        }
    };
    
    public static Style Focused => new(typeof(SingleLineInputField))
    {
        BasedOn = InputFieldDefaultStyle.Current,
        Setters =
        {
            new Setter
            {
                Property = SingleLineInputField.BorderColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_primary_80)
            },
            new Setter
            {
                Property = SingleLineInputField.BorderThicknessProperty,
                Value = 2
            }
        }
    };
}