using Components.ComponentsSamples.ImageCapturing.ImageGallery;
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
        ImageGallerySamplesViewModel.StoredImages.Add(capturedImage.AsByteArray);
        new ImageGallerySamples().Open();
    }

    protected override async void OnAppearing()
    {
        _ = Start();
        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        ImageGallerySamplesViewModel.StoredImages.Clear();
        m_imageCapture.Stop();
        base.OnDisappearing();
    }

    public void OnSleep()
    {
        m_imageCapture.Stop();
    }

    public void OnResume()
    {
        _ = Start();
    } }