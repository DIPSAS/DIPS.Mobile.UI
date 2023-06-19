using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Alerting.SystemMessage;

internal class SystemMessageConfigurator : ISystemMessageConfigurator
{
    public Color BackgroundColor { get; set; } = Colors.GetColor(ColorName.color_neutral_70);
    public Color TextColor { get; set; } = Colors.GetColor(ColorName.color_system_white);
    public Color IconColor { get; set; } = Colors.GetColor(ColorName.color_system_white);
    public float Duration { get; set; } = 2500;
    public string Text { get; set; } = DUILocalizedStrings.YouHaveNotSetSomeText;
    public ImageSource? Icon { get; set; }
}