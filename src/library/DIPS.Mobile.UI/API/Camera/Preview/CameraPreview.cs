using DIPS.Mobile.UI.API.Camera.ImageCapturing;
using DIPS.Mobile.UI.API.Camera.Shared;

namespace DIPS.Mobile.UI.API.Camera.Preview;

public partial class CameraPreview : ContentView
{
    private readonly TaskCompletionSource m_hasLoadedTcs = new();
    private ICameraUseCase? m_cameraUseCase;

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

    internal void AddUseCase(ICameraUseCase cameraUseCase)
    {
        m_cameraUseCase = cameraUseCase;
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        if(args.NewHandler == null) //I am not needed anymore
        {
            m_cameraUseCase?.Stop();
            m_cameraUseCase = null;
        }
        
        base.OnHandlerChanging(args);
    }
}