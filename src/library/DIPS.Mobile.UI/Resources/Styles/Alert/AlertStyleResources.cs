using DIPS.Mobile.UI.Resources.Styles.InputField;

namespace DIPS.Mobile.UI.Resources.Styles.Alert;

public class AlertStyleResources
{
    public static Dictionary<AlertStyle, Style> Styles => new()
    {
        [AlertStyle.Information] = AlertTypeStyle.Information,
        [AlertStyle.Error] = AlertTypeStyle.Error,
        [AlertStyle.Warning] = AlertTypeStyle.Warning,
        [AlertStyle.Success] = AlertTypeStyle.Success,
    };
}