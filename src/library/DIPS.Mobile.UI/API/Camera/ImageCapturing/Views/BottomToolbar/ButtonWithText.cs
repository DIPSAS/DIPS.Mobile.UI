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
            ImageTintColor = Microsoft.Maui.Graphics.Colors.White,
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Black,
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
            Command = new Command(onTapped)
        };
        
        var label = new Label
        {
            Text = text,
            Style = Styles.GetLabelStyle(LabelStyle.UI100),
            TextColor = Microsoft.Maui.Graphics.Colors.White,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, Sizes.GetSize(SizeName.content_margin_medium), 0, 0)
        };
        
        Add(button);
        Add(label);
    }
}