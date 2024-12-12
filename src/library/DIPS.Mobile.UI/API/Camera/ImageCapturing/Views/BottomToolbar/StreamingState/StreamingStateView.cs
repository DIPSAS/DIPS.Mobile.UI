using System.Xml;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.StreamingState;

internal class StreamingStateView : Grid
{
    private readonly ShutterButton m_shutterButton;
    private readonly Button m_blitzButton;
    private readonly Button m_macroButton;

    public StreamingStateView(IStreamingStateObserver streamingStateObserver, bool canUseMacroMode, bool isUsingMacroMode)
    {
        var isBlitzOn = streamingStateObserver.FlashActive;

        m_shutterButton = new ShutterButton(streamingStateObserver.OnTappedShutterButton);
        m_macroButton = new Button()
        {
            HorizontalOptions = LayoutOptions.Start,
            ImageSource = Icons.GetIcon(IconName.bacteria_line),
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonSmall),
            Command = new Command(streamingStateObserver.OnTappedMacroButton)
        };
        m_macroButton.ImageTintColor = Colors.GetColor(isUsingMacroMode ? ColorName.color_system_white : ColorName.color_neutral_40);

        m_blitzButton = new Button
        {
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
            ImageSource = isBlitzOn ? Icons.GetIcon(IconName.flash_fill) : Icons.GetIcon(IconName.flash_off_fill),
            ImageTintColor = Colors.GetColor(ColorName.color_system_white),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center
        };

        m_blitzButton.Command = new Command(() =>
        {
            isBlitzOn = !isBlitzOn;
            streamingStateObserver.OnTappedFlashButton();
            m_blitzButton.ImageSource = isBlitzOn ? Icons.GetIcon(IconName.flash_fill) : Icons.GetIcon(IconName.flash_off_fill);
        });
        
        if (canUseMacroMode)
        {
            Add(m_macroButton);
        }
        
        Add(m_shutterButton);
        Add(m_blitzButton);
        
        DUI.OrientationChanged += DUIOnOrientationChanged;
    }

    private void DUIOnOrientationChanged(OrientationDegree orientationDegree)
    {
        m_blitzButton.RotateTo(orientationDegree.OrientationDegreeToMauiRotation());
        m_macroButton.RotateTo(orientationDegree.OrientationDegreeToMauiRotation());
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

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is null)
        {
            DUI.OrientationChanged -= DUIOnOrientationChanged;
        }
    }
}