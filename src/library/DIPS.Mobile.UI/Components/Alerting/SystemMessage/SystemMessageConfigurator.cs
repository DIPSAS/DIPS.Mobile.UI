using System.ComponentModel;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Alerting.SystemMessage;

internal class SystemMessageConfigurator : ISystemMessageConfigurator
{
    public Color BackgroundColor { get; set; } = Colors.GetColor(ColorName.color_surface_subtle);
    public Color TextColor { get; set; } = Colors.GetColor(ColorName.color_text_action);
    public Color IconColor { get; set; } = Colors.GetColor(ColorName.color_icon_action);
    public float Duration { get; set; } = 2500;
    public string Text { get; set; } = DUILocalizedStrings.YouHaveNotSetSomeText;
    [TypeConverter(nameof(ImageSourceConverter))]
    public ImageSource? Icon { get; set; }
}