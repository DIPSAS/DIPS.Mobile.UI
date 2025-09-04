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
                Property = AlertView.IconProperty, 
                Value = Icons.Icons.GetIcon(IconName.information_line)
            },
            new Setter
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_surface_information)
            },
            new Setter
            {
                Property = AlertView.IconColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_icon_information)
            }
        }
    };

    public static Style Error => new(typeof(AlertView))
    {
        Setters =
        {
            new Setter
            {
                Property = AlertView.IconProperty, 
                Value = Icons.Icons.GetIcon(IconName.important_line)
            },
            new Setter
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_surface_danger)
            },
            new Setter
            {
                Property = AlertView.IconColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_icon_danger)
            }
        }
    };

    public static Style Warning => new(typeof(AlertView))
    {
        Setters =
        {
            new Setter
            {
                Property = AlertView.IconProperty, 
                Value = Icons.Icons.GetIcon(IconName.alert_line)
            },
            new Setter
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_surface_warning)
            },
            new Setter
            {
                Property = AlertView.IconColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_icon_warning)
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
                Value = Icons.Icons.GetIcon(IconName.check_circle_line)
            },
            new Setter
            {
                Property = VisualElement.BackgroundColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_surface_success)
            },
            new Setter
            {
                Property = AlertView.IconColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_icon_success)
            }
        }
    };
}