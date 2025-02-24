using System.ComponentModel;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Alerting.SystemMessage;

internal class SystemMessageConfigurator : ISystemMessageConfigurator
{
    // TODO: Lisa
    public Color BackgroundColor { get; set; } = Colors.GetColor(ColorName.color_neutral_70);
    public Color TextColor { get; set; } = Colors.GetColor(ColorName.color_text_on_action);
    public Color IconColor { get; set; } = Colors.GetColor(ColorName.color_icon_on_action);
    public float Duration { get; set; } = 2500;
    public string Text { get; set; } = DUILocalizedStrings.YouHaveNotSetSomeText;
    [TypeConverter(nameof(ImageSourceConverter))]
    public ImageSource? Icon { get; set; }
}