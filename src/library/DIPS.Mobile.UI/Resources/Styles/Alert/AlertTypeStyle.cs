using DIPS.Mobile.UI.Components.Alerting.Alert;

namespace DIPS.Mobile.UI.Resources.Styles.Alert;

public class AlertTypeStyle
{
    public static Style Information => new(typeof(AlertView))
    {
        Setters =
        {
            new Setter
            {
                Property = AlertView.IconProperty, Value = Icons.Icons.GetIcon(IconName.information_line)
            },
            new Setter
            {
                Property = VisualElement.BackgroundProperty,
                Value = Colors.Colors.GetColor(ColorName.color_information_light)
            },
            new Setter()
            {
                Property = AlertView.IconColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_neutral_90)
            }
        }
    };

    public static Style Error => new(typeof(AlertView))
    {
        Setters =
        {
            new Setter
            {
                Property = AlertView.IconProperty, Value = Icons.Icons.GetIcon(IconName.information_line)
            },
            new Setter
            {
                Property = VisualElement.BackgroundProperty,
                Value = Colors.Colors.GetColor(ColorName.color_error_light)
            }
            ,
            new Setter()
            {
                Property = AlertView.IconColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_error_dark)
            }
        }
    };

    public static Style Warning => new(typeof(AlertView))
    {
        Setters =
        {
            new Setter {Property = AlertView.IconProperty, Value = Icons.Icons.GetIcon(IconName.alert_line)},
            new Setter
            {
                Property = VisualElement.BackgroundProperty,
                Value = Colors.Colors.GetColor(ColorName.color_attention_extralight)
            }
            ,
            new Setter()
            {
                Property = AlertView.IconColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_neutral_90)
            }
        }
    };

    public static Style Success => new(typeof(AlertView))
    {
        Setters =
        {
            new Setter
            {
                Property = AlertView.IconProperty,
                Value = Icons.Icons.GetIcon(IconName.check_circle_fill)
            },
            new Setter
            {
                Property = VisualElement.BackgroundProperty,
                Value = Colors.Colors.GetColor(ColorName.color_success_light)
            }
            ,
            new Setter()
            {
                Property = AlertView.IconColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_success_dark)
            }
        }
    };
}