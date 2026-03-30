using DIPS.Mobile.UI.API.Camera;
using DIPS.Mobile.UI.API.Camera.ImageCapturing;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace Components.ComponentsSamples.ImageCapturing;

public partial class MultiImageCaptureSample
{
    private readonly List<CapturedImage> m_capturedImages = [];
    private readonly ImageCapture m_imageCapture = new();
    private bool m_firstTime = true;

    public MultiImageCaptureSample()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        if (m_firstTime)
        {
            _ = StartMultiCapture();
            m_firstTime = false;
        }

        base.OnAppearing();
    }

    private async Task StartMultiCapture()
    {
        CameraPreview.IsVisible = true;
        ResultView.IsVisible = false;

        await m_imageCapture.Start(CameraPreview, OnImageCaptured, OnCameraFailed,
            settings =>
            {
                settings.CaptureMode = CaptureMode.Multi;
                settings.RequiresConfirmation = true;
                settings.DoneButtonCommand = new Command(OnCancelled);
                settings.FinishedButtonCommand = new Command(OnFinished);
            });
    }

    private void OnImageCaptured(CapturedImage capturedImage)
    {
        m_capturedImages.Add(capturedImage);
    }

    private void OnFinished()
    {
        ShowResult($"Captured {m_capturedImages.Count} image(s)", m_capturedImages);
    }

    private void OnCancelled()
    {
        ShowResult("Cancelled — no images kept", []);
        m_capturedImages.Clear();
    }

    private void ShowResult(string message, List<CapturedImage> imagesToDisplay)
    {
        CameraPreview.IsVisible = false;
        ResultView.IsVisible = true;
        ResultLabel.Text = message;

        ImageList.Children.Clear();
        foreach (var capturedImage in imagesToDisplay)
        {
            var imageBytes = capturedImage.AsByteArray;
            var image = new Image
            {
                Source = ImageSource.FromStream(() => new MemoryStream(imageBytes)),
                HeightRequest = 200,
                Aspect = Aspect.AspectFit
            };
            ImageList.Children.Add(image);
        }
    }

    private void OnCameraFailed(CameraException e)
    {
        App.Current?.MainPage?.DisplayAlert("Something failed!", e.Message, "Ok");
    }

    private void OnCloseClicked(object? sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}
