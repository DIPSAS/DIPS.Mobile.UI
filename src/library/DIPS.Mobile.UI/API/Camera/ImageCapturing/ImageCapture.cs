using DIPS.Mobile.UI.API.Camera.Permissions;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Internal.Logging;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : ICameraUseCase
{
    
    private CameraPreview? m_cameraPreview;
    private Action<CapturedImage>? m_onImageCaptured;

    public async Task Start(CameraPreview cameraPreview, Action<CapturedImage> onImageCaptured)
    {
        m_cameraPreview = cameraPreview;
        ConstructCrossPlatformViews();
        m_cameraPreview.AddUseCase(this);
        m_onImageCaptured = onImageCaptured;
        if (await CameraPermissions.CanUseCamera())
        {
            Log("Permitted to use camera");
            await m_cameraPreview.HasLoaded();
            await PlatformStart();
        }
        else
        {
            Log("Not permitted to use camera");
        }
    }

    private void ConstructCrossPlatformViews()
    {
        var shutterButton = new Border
        {
            BackgroundColor = Microsoft.Maui.Graphics.Colors.DimGray,
            StrokeShape = new Ellipse(),
            Stroke = Colors.GetColor(ColorName.color_system_white),
            StrokeThickness = Sizes.GetSize(SizeName.size_1),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 70,
            HeightRequest = 70
        };
        
        Touch.SetCommand(shutterButton, new Command(PlatformCapturePhoto));

        m_cameraPreview?.AddView(shutterButton);
    }

    private partial void PlatformCapturePhoto();
    private partial Task PlatformStart();
    private partial Task PlatformStop();

    private void Log(string message)
    {
        DUILogService.LogDebug<ImageCapture>(message);
    }

    public void Stop()
    {
        PlatformStop();
        m_cameraPreview = null;
        m_onImageCaptured = null;
    }

    internal void InvokeOnImageCaptured(CapturedImage capturedImage)
    {
        m_onImageCaptured?.Invoke(capturedImage);
    }
   
}