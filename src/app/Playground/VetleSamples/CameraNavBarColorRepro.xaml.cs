using DIPS.Mobile.UI.API.Camera;
using DIPS.Mobile.UI.API.Camera.ImageCapturing;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.Resources.Colors;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace Playground.VetleSamples;

/// <summary>
/// Repro: Open modal camera with black navbar, take photo, switch content
/// to show captured image and change navbar color to default.
/// </summary>
public partial class CameraNavBarColorRepro
{
    private readonly ImageCapture m_imageCapture;

    public CameraNavBarColorRepro()
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
            SetValue(NavigationPage.BarTextColorProperty,
                Colors.GetColor(ColorName.color_text_default));

            Title = "Check Image";
            RetakeToolbarItem.IsEnabled = true;
        });
    }

    private void Retake(object? sender, EventArgs e)
    {
        CapturedImageView.IsVisible = false;
        CapturedImageView.Source = null;
        CameraPreview.IsVisible = true;
        RetakeToolbarItem.IsEnabled = false;

        // Change navbar back to black
        SetValue(NavigationPage.BarBackgroundColorProperty, Microsoft.Maui.Graphics.Colors.Black);
        SetValue(NavigationPage.BarTextColorProperty, Microsoft.Maui.Graphics.Colors.White);
        Title = "Camera NavBar Color";

        _ = StartCamera();
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

    private void Close(object? sender, EventArgs e) => CloseModal();
}
