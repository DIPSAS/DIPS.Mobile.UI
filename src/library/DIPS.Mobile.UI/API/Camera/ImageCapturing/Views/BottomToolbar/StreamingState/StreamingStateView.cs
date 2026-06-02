using DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.StreamingState;

internal class StreamingStateView : Grid
{
    private readonly ShutterButton m_shutterButton;

    public StreamingStateView(IStreamingStateObserver streamingStateObserver, StreamingTrailingControl trailingControl, View? leadingControl = null)
    {
        m_shutterButton = new ShutterButton(streamingStateObserver.OnTappedShutterButton);
        Add(m_shutterButton);
        
        if (leadingControl is not null)
        {
            Add(leadingControl);
        }

        View trailingView = trailingControl switch
        {
            StreamingTrailingControl.FinishCapture finishCapture => CreateFinishCaptureButton(finishCapture),
            StreamingTrailingControl.Flash => new FlashButton(streamingStateObserver) { HorizontalOptions = LayoutOptions.End },
            _ => throw new ArgumentOutOfRangeException(nameof(trailingControl), trailingControl, "Unknown streaming trailing control.")
        };

        Add(trailingView);
    }

    private static ButtonWithText CreateFinishCaptureButton(StreamingTrailingControl.FinishCapture finishCapture)
    {
        var finishButton = new ButtonWithText(finishCapture.Text, Icons.GetIcon(IconName.check_line), finishCapture.OnTapped)
        {
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center
        };

        finishButton.RotateWithDeviceOrientation();

        return finishButton;
    }

    public void SetShutterButtonEnabled(bool isEnabled)
    {
        if (isEnabled)
        {
            m_shutterButton.Enable();
            m_shutterButton.SetSemanticFocus();
        }
        else
        {
            m_shutterButton.Disable();
        }
    }
}
