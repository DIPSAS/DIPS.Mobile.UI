using DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.StreamingState;

internal sealed class FlashButton : Button
{
    private readonly IStreamingStateObserver m_streamingStateObserver;

    public FlashButton(IStreamingStateObserver streamingStateObserver)
    {
        m_streamingStateObserver = streamingStateObserver;

        Style = Styles.GetButtonStyle(ButtonStyle.GhostIconLarge);
        ImageTintColor = Microsoft.Maui.Graphics.Colors.White;
        VerticalOptions = LayoutOptions.Center;
        Command = new Command(OnTapped);

        UpdateFlashIcon();
        SemanticProperties.SetDescription(this, DUILocalizedStrings.Accessability_TapToActivateFlash);

        this.RotateWithDeviceOrientation();
    }

    private void OnTapped()
    {
        m_streamingStateObserver.OnTappedFlashButton();
        UpdateFlashIcon();
    }

    private void UpdateFlashIcon()
    {
        ImageSource = m_streamingStateObserver.FlashActive
            ? Icons.GetIcon(IconName.flash_line)
            : Icons.GetIcon(IconName.flash_off_fill);
    }
}
