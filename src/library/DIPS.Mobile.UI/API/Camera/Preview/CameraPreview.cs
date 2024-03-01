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
}