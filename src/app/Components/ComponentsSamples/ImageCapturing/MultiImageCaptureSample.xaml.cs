using DIPS.Mobile.UI.API.Camera.ImageCapturing;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace Components.ComponentsSamples.ImageCapturing;

public partial class MultiImageCaptureSample
{
    public MultiImageCaptureSample()
    {
        InitializeComponent();
    }

    private void OnStartWithoutConfirmationClicked(object? sender, EventArgs e)
    {
        _ = StartMultiCapture(requiresConfirmation: false);
    }

    private void OnStartWithConfirmationClicked(object? sender, EventArgs e)
    {
        _ = StartMultiCapture(requiresConfirmation: true);
    }

    private async Task StartMultiCapture(bool requiresConfirmation)
    {
        var modalPage = new MultiImageCaptureModalPage(requiresConfirmation);
        await Shell.Current.Navigation.PushModalAsync(modalPage);
        var capturedImages = await modalPage.CompletionTask;

        var message = capturedImages.Count == 0
            ? "Cancelled, no images kept"
            : $"Captured {capturedImages.Count} image(s)";
        ShowResult(message, capturedImages);
    }

    private void ShowResult(string message, List<CapturedImage> imagesToDisplay)
    {
        SelectionView.IsVisible = false;
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

    private void OnCloseClicked(object? sender, EventArgs e)
    {
        ResultView.IsVisible = false;
        ImageList.Children.Clear();
        SelectionView.IsVisible = true;
    }
}
