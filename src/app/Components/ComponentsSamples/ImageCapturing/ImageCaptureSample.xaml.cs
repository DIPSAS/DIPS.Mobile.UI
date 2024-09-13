using DIPS.Mobile.UI.API.Camera;
using DIPS.Mobile.UI.API.Camera.ImageCapturing;

namespace Components.ComponentsSamples.ImageCapturing;

public partial class ImageCaptureSample
{
    private readonly Camera m_camera;
    private readonly List<CapturedImage> m_images;
    private bool m_firstTimeStartingCamera;

    public ImageCaptureSample()
    {
        InitializeComponent();
        m_camera = new Camera();
        GalleryThumbnails.CameraButtonTappedCommand = new Command(() => _ = Start());
        m_images = [];
        
    }

    
    private async Task Start()
    {
        try
        {
            _ = m_camera.StartImageCapture(OnImageCaptured);    
        }
        catch (Exception exception)
        {
            await Application.Current?.MainPage?.DisplayAlert("Failed, check console!", exception.Message, "Ok")!;
            Console.WriteLine(exception);
        }
    }

    public async void OnImageCaptured(CapturedImage capturedImage)
    {
        await m_camera.Stop();
        m_images.Add(capturedImage);
        GalleryThumbnails.Images = m_images.ToList();
    }

    protected override void OnAppearing()
    {
        if (!m_firstTimeStartingCamera)
        {
            _ = Start();
            m_firstTimeStartingCamera = true;
        }
        
        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
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