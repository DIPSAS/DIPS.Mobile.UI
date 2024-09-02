using Components.ComponentsSamples.BarcodeScanning;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning;
using DIPS.Mobile.UI.API.Camera.PhotoCapturing;

namespace Components.ComponentsSamples.PhotoCapturing;

public partial class PhotoCaptureSample
{
    private readonly PhotoCapture m_photoCapture;

    public PhotoCaptureSample()
    {
        InitializeComponent();
        m_photoCapture = new PhotoCapture();
    }

    
    private async Task Start()
    {
        try
        {
            await m_photoCapture.Start(CameraPreview);
        }
        catch (Exception exception)
        {
            await Application.Current?.MainPage?.DisplayAlert("Failed, check console!", exception.Message, "Ok")!;
            Console.WriteLine(exception);
        }
    }

    protected override async void OnAppearing()
    {
        _ = Start();
        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        m_photoCapture.Stop();
        base.OnDisappearing();
    }

    public void OnSleep()
    {
        m_photoCapture.Stop();
    }

    public void OnResume()
    {
        _ = Start();
    }

    private void ShowTip(object? sender, EventArgs e)
    {
        CameraPreview.ShowZoomSliderTip("Om strekkoden er liten, er det bedre å bruke zoom funksjonen isteden for å ha mobilen for nært strekkoden.");
    }
}