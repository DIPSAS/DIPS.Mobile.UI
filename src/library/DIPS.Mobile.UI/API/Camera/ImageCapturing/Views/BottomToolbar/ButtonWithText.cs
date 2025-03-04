using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar;

public class ButtonWithText : VerticalStackLayout
{
    public ButtonWithText(string text, ImageSource imageSource, Action? onTapped)
    {
        var button = new Button
        {
            ImageSource = imageSource,
            ImageTintColor = Colors.GetColor(ColorName.color_icon_on_action),
            BackgroundColor = Colors.GetColor(ColorName.color_surface_action_on_black),
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
            Command = new Command(onTapped)
        };
        
        var label = new Label
        {
            Text = text,
            Style = Styles.GetLabelStyle(LabelStyle.UI100),
            TextColor = Colors.GetColor(ColorName.color_text_on_action),
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, Sizes.GetSize(SizeName.content_margin_medium), 0, 0)
        };
        
        Add(button);
        Add(label);
    }
}