using System.ComponentModel;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles.SystemMessage;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Alerting.SystemMessage;

internal class SystemMessageConfigurator : ISystemMessageConfigurator
{
    private SystemMessageStyle m_style;
    private Color? m_iconColor;
    private Color m_textColor = Colors.GetColor(ColorName.color_text_action);

    public Color BackgroundColor { get; set; } = Colors.GetColor(ColorName.color_surface_subtle);
    public Color TextColor
    {
        get => m_textColor;
        set => m_textColor = value;
    }

    internal Color Stroke { get; private set; } = Colors.GetColor(ColorName.color_border_subtle);
    public Color IconColor
    {
        get => m_iconColor ?? TextColor;
        set => m_iconColor = value;
    }

    public float Duration { get; set; } = 2500;
    public string Text { get; set; } = DUILocalizedStrings.YouHaveNotSetSomeText;
    public SystemMessageStyle Style
    {
        get => m_style;
        set
        {
            m_style = value;
            if (value is SystemMessageStyle.None)
                return;

            if (!SystemMessageStyleResources.Styles.TryGetValue(value, out var colors))
                return;

            BackgroundColor = colors.BackgroundColor;
            TextColor = colors.TextColor;
            Stroke = colors.Stroke;
        }
    }

    [TypeConverter(nameof(ImageSourceConverter))]
    public ImageSource? Icon { get; set; }
}