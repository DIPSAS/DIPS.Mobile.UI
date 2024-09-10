
using DIPS.Mobile.UI.API.Camera.ImageCapturing;

namespace Components.ComponentsSamples.ImageCapturing;

public partial class ImageCaptureSample
{
    private readonly ImageCapture m_imageCapture;

    public ImageCaptureSample()
    {
        InitializeComponent();
        m_imageCapture = new ImageCapture();
    }

    
    private async Task Start()
    {
        try
        {
            await m_imageCapture.Start(CameraPreview, OnImageCaptured);
        }
        catch (Exception exception)
        {
            await Application.Current?.MainPage?.DisplayAlert("Failed, check console!", exception.Message, "Ok")!;
            Console.WriteLine(exception);
        }
    }

    public void OnImageCaptured(CapturedImage capturedImage)
    {
        new ImagePreviewBottomSheet(capturedImage).Open();
    }

    protected override async void OnAppearing()
    {
        _ = Start();
        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        // m_imageCapture.Stop();
        base.OnDisappearing();
    }

    public void OnSleep()
    {
        // m_imageCapture.Stop();
    }

    public void OnResume()
    {
        // _ = Start();
    } }