using DIPS.Mobile.UI.API.Camera;
using DIPS.Mobile.UI.API.Camera.ImageCapturing;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.API.Camera.Shared;
using DIPS.Mobile.UI.API.Camera.TIFF;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Resources.Icons;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;

namespace Components.ComponentsSamples.ImageCapturing;

public partial class ImageCaptureSample
{
    private readonly List<CapturedImage> m_images;
    private readonly ImageCapture m_imageCapture;

    public ImageCaptureSample()
    {
        InitializeComponent();
        m_images = [];

        m_imageCapture = new ImageCapture();
    }

    private async void GalleryThumbnails_OnCameraButtonTapped(object? sender, EventArgs e)
    {
        await StartImageCapture();
    }

    private async Task StartImageCapture()
    {
        ToggleCamera(true);
        await m_imageCapture.Start(CameraPreview, OnImageCaptured, OnCameraFailed,
            settings =>
            {
                settings.PostCaptureAction = PostCaptureAction.Close;
                settings.CanChangeMaxHeightOrWidth = true;
                settings.DoneButtonCommand = new Command(Close);
            });
    }

    private void Close()
    {
        if (m_images.Count == 0)
        {
            Application.Current?.MainPage?.Navigation.PopAsync();
        }
        else
        {
            ToggleCamera(false);
        }
    }
    
    protected override bool OnBackButtonPressed()
    {
        Close();

        return true;
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
        ToggleCamera(false);
        
        
        
        /*var raw = capturedimage.AsByte64String;
        var rotatedByteArray = await capturedimage.AsRotatedByteArray();
        if (rotatedByteArray != null)
        {
            var raw2 = Convert.ToBase64String(await capturedimage.AsRotatedByteArray() ?? []);
            var rotatedByteArraySize = ImageSize.InMegaBytes(rotatedByteArray);
            if (capturedimage.Size.SizeInMegaBytes < rotatedByteArraySize)
            {
                Console.WriteLine($"Size matters! The size of the rotated image is larger than the original image. Original image: {capturedimage.Size.SizeInMegaBytesWithTwoDecimals}, Rotated image: {rotatedByteArraySize}");
            }
        }*/
        
        m_images.Add(capturedimage);
        GalleryThumbnails.AddImage(capturedimage);
        ToggleCamera(false);
        /*var preTiff = await capturedimage.AsRotatedByteArray();
        var preTiffB64 = Convert.ToBase64String(preTiff);
        
        var rotated = await capturedimage.AsRotatedByteArray() ?? [];*/
        // await new BottomSheet()
        // {
        //     Content = new Image() {Source = ImageSource.FromStream(() => new MemoryStream(rotated)), VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.Center}
        // }.Open();
        
        var memoryStream = await new TiffFactory().ConvertToTiffAsync(capturedimage, CancellationToken.None);
        if (memoryStream != null)
        {
            var tiffbytearray = memoryStream.ToArray();
            var tiff = Convert.ToBase64String(tiffbytearray);
            var sizeOfTiff = ImageSize.InMegaBytes(tiffbytearray);
            if (capturedimage.Size.SizeInMegaBytes < sizeOfTiff)
            {
                Console.WriteLine($"Size matters! The size of the tiff image is larger than the original image. Original image: {capturedimage.Size.SizeInMegaBytesWithTwoDecimals}, Tiff image: {sizeOfTiff}");
            }
        }
      
    }

    private void ToggleCamera(bool shouldDisplay)
    {
        CameraPreview.IsVisible = shouldDisplay;
        GalleryThumbnails.IsVisible = !CameraPreview.IsVisible;
        NavigationPage.SetHasNavigationBar(this, GalleryThumbnails.IsVisible);
    }

    private void MenuItem_OnClicked(object? sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}