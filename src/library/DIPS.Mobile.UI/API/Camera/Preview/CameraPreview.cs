using DIPS.Mobile.UI.API.Tip;

namespace DIPS.Mobile.UI.API.Camera.Preview;

public class CameraPreview : ContentView
{
    private readonly TaskCompletionSource m_hasLoadedTcs = new();

    public CameraPreview()
    {
        Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, EventArgs e)
    {
        m_hasLoadedTcs.TrySetResult();
        Loaded -= OnLoaded;
    }
    
    public Task HasLoaded()
    {
        return m_hasLoadedTcs.Task;
    }

    public void ShowZoomSliderTip(string message, int durationInMilliseconds = 4000)
    {
        if (Handler is not CameraPreviewHandler cameraPreviewHandler) return;
        cameraPreviewHandler.ShowZoomSliderTip(message, durationInMilliseconds);
    }
}