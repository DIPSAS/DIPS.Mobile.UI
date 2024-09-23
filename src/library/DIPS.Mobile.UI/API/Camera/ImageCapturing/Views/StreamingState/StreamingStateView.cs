using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Microsoft.Maui.Controls.Shapes;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.StreamingState;

public class StreamingStateView : Grid
{
    private bool m_isFlashOn;
    
    public StreamingStateView(Action onTappedShutterButton, Action onTappedFlashButton)
    {
        Margin = new Thickness(Sizes.GetSize(SizeName.size_5), 0);

        ShutterButton = new Border
        {
            BackgroundColor = Microsoft.Maui.Graphics.Colors.DimGray,
            StrokeShape = new Ellipse(),
            Stroke = Colors.GetColor(ColorName.color_system_white),
            StrokeThickness = Sizes.GetSize(SizeName.size_1),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 70,
            HeightRequest = 70
        };
        
        Touch.SetCommand(ShutterButton, new Command(onTappedShutterButton.Invoke));

        var blitzButton = new Button
        {
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
            ImageSource = Icons.GetIcon(IconName.flash_off_fill),
            ImageTintColor = Colors.GetColor(ColorName.color_system_white),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center,
            
        };

        blitzButton.Command = new Command(() =>
        {
            m_isFlashOn = !m_isFlashOn;
            onTappedFlashButton.Invoke();
            blitzButton.ImageSource = m_isFlashOn ? Icons.GetIcon(IconName.flash_fill) : Icons.GetIcon(IconName.flash_off_fill);
        });
        
        Add(ShutterButton);
        Add(blitzButton);
    }

    public Border ShutterButton { get; }
}