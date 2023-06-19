using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Alerts.SystemMessage;

internal class SystemMessageConfigurator : ISystemMessageConfigurator
{
    public Color BackgroundColor { get; private set; } = Colors.GetColor(ColorName.color_neutral_70);
    public Color TextColor { get; private set; } = Colors.GetColor(ColorName.color_system_white);
    public Color IconColor { get; private set; } = Colors.GetColor(ColorName.color_system_white);
    public float Duration { get; private set; } = 2500;
    public string Text { get; private set; } = DUILocalizedStrings.YouHaveNotSetSomeText;
    public ImageSource? Icon { get; private set; }
    
    public void SetBackgroundColor(Color color)
    {
        BackgroundColor = color;
    }

    public void SetTextColor(Color color)
    {
        TextColor = color;
    }

    public void SetIconColor(Color color)
    {
        IconColor = color;
    }

    public void SetDuration(float duration)
    {
        Duration = duration;
    }

    public void SetText(string text)
    {
        Text = text;
    }

    public void SetIcon(ImageSource imageSource)
    {
        Icon = imageSource;
    }
}