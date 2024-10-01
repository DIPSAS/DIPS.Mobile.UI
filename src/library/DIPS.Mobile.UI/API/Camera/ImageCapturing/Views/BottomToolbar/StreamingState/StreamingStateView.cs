using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.StreamingState;

internal class StreamingStateView : Grid
{
    private readonly ShutterButton m_shutterButton;

    public StreamingStateView(Action? onTappedShutterButton, Action? onTappedFlashButton, bool shouldBlitzBeActive)
    {
        var isBlitzOn = shouldBlitzBeActive;
        
        m_shutterButton = new ShutterButton(onTappedShutterButton);

        var blitzButton = new Button
        {
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
            ImageSource = isBlitzOn ? Icons.GetIcon(IconName.flash_fill) : Icons.GetIcon(IconName.flash_off_fill),
            ImageTintColor = Colors.GetColor(ColorName.color_system_white),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center
        };

        blitzButton.Command = new Command(() =>
        {
            isBlitzOn = !isBlitzOn;
            onTappedFlashButton?.Invoke();
            blitzButton.ImageSource = isBlitzOn ? Icons.GetIcon(IconName.flash_fill) : Icons.GetIcon(IconName.flash_off_fill);
        });
        
        Add(m_shutterButton);
        Add(blitzButton);
    }

    public void SetShutterButtonEnabled(bool isEnabled)
    {
        if (isEnabled)
        {
            m_shutterButton.Enable();
        }
        else
        {
            m_shutterButton.Disable();
        }
    }
}