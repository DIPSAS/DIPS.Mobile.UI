using DIPS.Mobile.UI.API.Camera;
using DIPS.Mobile.UI.API.Camera.ImageCapturing;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.Resources.Colors;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace Playground.VetleSamples;

/// <summary>
/// Repro: Same as CameraNavBarColorRepro but with navigation bar hidden.
/// </summary>
public partial class CameraNavBarColorNoNavBarRepro
{
    private readonly ImageCapture m_imageCapture;

    public CameraNavBarColorNoNavBarRepro()
    {
        InitializeComponent();
        m_imageCapture = new ImageCapture();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = StartCamera();
    }

    private async Task StartCamera()
    {
        try
        {
            var cameraOptions = new CameraOptions
            {
                CancelButtonCommand = new Command(CloseModal)
            };

            await m_imageCapture.StartSingleImageCapture(
                CameraPreview,
                OnImageCaptured,
                OnCameraFailed,
                cameraOptions);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    private void OnImageCaptured(CapturedImage capturedImage)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            // Show captured image
            CapturedImageView.Source = ImageSource.FromStream(() => new MemoryStream(capturedImage.AsByteArray));
            CapturedImageView.IsVisible = true;
            CameraPreview.IsVisible = false;

            // Change navbar to default color
            SetValue(NavigationPage.BarBackgroundColorProperty,
                Colors.GetColor(ColorName.color_surface_default));

            Title = "Check Image";
        });
    }

    private void OnCameraFailed(CameraException e)
    {
        Console.WriteLine($"Camera failed: {e.Message}");
    }

    private void CloseModal()
    {
        m_imageCapture.StopAndDispose();
        Shell.Current.Navigation.PopModalAsync();
    }
}
