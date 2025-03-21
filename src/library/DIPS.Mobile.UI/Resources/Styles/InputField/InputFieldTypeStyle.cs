using DIPS.Mobile.UI.Components.TextFields.InputFields;
using DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField;

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
               Value = Colors.Colors.GetColor(ColorName.color_stroke_default)
           },
           new Setter
           {
               Property = VisualElement.BackgroundColorProperty,
               Value = Colors.Colors.GetColor(ColorName.color_surface_deactivated)
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
                Value = Colors.Colors.GetColor(ColorName.color_stroke_active)
            },
            new Setter
            {
                Property = SingleLineInputField.BorderThicknessProperty,
                Value = Sizes.Sizes.GetSize(SizeName.stroke_large)
            }
        }
    };

    public static Style Error => new(typeof(MultiLineInputField))
    {
        BasedOn = InputFieldDefaultStyle.Current,
        Setters =
        {
            new Setter
            {
                Property = SingleLineInputField.BorderColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_error_dark)
            }
        }
    };
    
    public static Style Success => new(typeof(MultiLineInputField))
    {
        BasedOn = InputFieldDefaultStyle.Current,
        Setters =
        {
            new Setter
            {
                Property = SingleLineInputField.BorderColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_success_dark)
            }
        }
    };
}