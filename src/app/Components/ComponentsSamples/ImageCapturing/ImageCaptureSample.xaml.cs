using DIPS.Mobile.UI.API.Camera;
using DIPS.Mobile.UI.API.Camera.ImageCapturing;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Output;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.API.Camera.TIFF;
using DIPS.Mobile.UI.Components.BottomSheets;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace Components.ComponentsSamples.ImageCapturing;

public partial class ImageCaptureSample
{
    private readonly List<CapturedImage> m_images; 

    public ImageCaptureSample()
    {
        InitializeComponent();
        m_images = [];
    }

    private async void GalleryThumbnails_OnCameraButtonTapped(object? sender, EventArgs e)
    {
        await StartImageCapture();
    }

    private async Task StartImageCapture()
    {
        ToggleCamera(true);
        await new ImageCapture().Start(CameraPreview, OnImageCaptured, OnCameraFailed,
            settings => settings.PostCaptureAction = PostCaptureAction.Close);
    }

    private void OnCameraFailed(CameraException e)
    {
        App.Current.MainPage.DisplayAlert("Something failed!", e.Message, "Ok");
    }

    protected override void OnAppearing()
    { 
        _ = StartImageCapture();
        base.OnAppearing();
    }

    private async void OnImageCaptured(CapturedImage capturedimage)
    {
        var raw = capturedimage.AsByte64String;
        var rotatedByteArray = await capturedimage.AsRotatedByteArray();
        if (rotatedByteArray != null)
        {
            var raw2 = Convert.ToBase64String(await capturedimage.AsRotatedByteArray() ?? []);
            var rotatedByteArraySize = ImageSize.InMegaBytes(rotatedByteArray);
            if (capturedimage.Size.SizeInMegaBytes < rotatedByteArraySize)
            {
                App.Current.MainPage.DisplayAlert("Size matters!", $"The size of the rotated image is larger than the original image. Original image: {capturedimage.Size.SizeInMegaBytesWithTwoDecimals}, Rotated image: {rotatedByteArraySize}", "Ok");
            }
        }
        
        m_images.Add(capturedimage);
        GalleryThumbnails.AddImage(capturedimage);
        ToggleCamera(false);
        var memoryStream = await new TiffFactory().ConvertToTiffAsync(capturedimage, CancellationToken.None);
        if (memoryStream != null)
        {
            var tiff = Convert.ToBase64String(memoryStream.ToArray());
        }
    }

    private void ToggleCamera(bool shouldDisplay)
    {
        CameraPreview.IsVisible = shouldDisplay;
        GalleryThumbnails.IsVisible = !CameraPreview.IsVisible ;
    }
}