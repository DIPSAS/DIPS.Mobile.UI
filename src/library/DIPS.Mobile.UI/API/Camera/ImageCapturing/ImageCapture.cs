using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views;
using DIPS.Mobile.UI.API.Camera.Permissions;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Internal.Logging;
using Microsoft.Maui.Controls.Shapes;
using ActivityIndicator = DIPS.Mobile.UI.Components.Loading.ActivityIndicator;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : ICameraUseCase
{
    private CameraPreview? m_cameraPreview;
    private DidCaptureImage? m_onImageCaptured;
    private Border? m_shutterButton;
    private ConfirmStateGrid? m_confirmStateGrid;
    private Image? m_confirmImage;
    private ActivityIndicator? m_activityIndicator;

    public async Task Start(CameraPreview cameraPreview, DidCaptureImage onImageCaptured, Action<ImageCaptureSettings>? configure = null)
    {
        var imageCaptureSettings = new ImageCaptureSettings();
        if (configure != null)
        {
            configure.Invoke(imageCaptureSettings);
        }
        
        m_cameraPreview = cameraPreview;
        m_cameraPreview.AddUseCase(this);
        m_onImageCaptured = onImageCaptured;
        if (await CameraPermissions.CanUseCamera())
        {
            Log("Permitted to use camera");
            await m_cameraPreview.HasLoaded();
            ConstructCrossPlatformViews();
            await PlatformStart(imageCaptureSettings);
        }
        else
        {
            Log("Not permitted to use camera");
        }
    }

    private async Task OnCapture()
    {
        if (m_shutterButton != null)
        {
            Touch.SetIsEnabled(m_shutterButton, false);    
        }

#if __ANDROID__ //iOS already has a blinking effect when we capture photo
        var blackBox = new BoxView { BackgroundColor = Microsoft.Maui.Graphics.Colors.Black, Opacity = 0 };
        m_cameraPreview?.AddViewToRoot(blackBox, 1);
        
        await blackBox.FadeTo(1, 50);
        await blackBox.FadeTo(0, 50);
        
        m_cameraPreview?.RemoveViewFromRoot(blackBox);
#endif

        m_activityIndicator = new ActivityIndicator
        {
            VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, IsRunning = true
        };
        m_cameraPreview?.AddViewToRoot(m_activityIndicator, 1);
    }
    
    private void SwitchToConfirmState(CapturedImage capturedImage, ImageCaptureSettings imageCaptureSettings)
    {
        PlatformStop();

        if (m_cameraPreview == null)
        {
            return;
        }

        m_confirmImage = new Image
        {
            Source = ImageSource.FromStream(() => new MemoryStream(capturedImage.AsByteArray))
        };

        m_confirmStateGrid = new ConfirmStateGrid(() =>
            {
                m_onImageCaptured?.Invoke(capturedImage);
                switch (imageCaptureSettings.PostCaptureAction)
                {
                    case PostCaptureAction.Close:
                        Stop();
                        break;
                    case PostCaptureAction.Continue:
                        PlatformStart(imageCaptureSettings);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                ResetToCaptureImageState();
            },
            () =>
            {
                PlatformStart(imageCaptureSettings);
                ResetToCaptureImageState();
            });

        m_cameraPreview.AddViewToRoot(m_confirmImage, 1);
        m_cameraPreview.AddToolbarView(m_confirmStateGrid);
        m_cameraPreview.RemoveToolbarView(m_shutterButton);
        m_cameraPreview.RemoveViewFromRoot(m_activityIndicator);
        m_cameraPreview.GoToConfirmingState();
    }

    private void ResetToCaptureImageState()
    {
        m_cameraPreview?.GoToStreamingState();

        if (m_shutterButton != null)
        {
            Touch.SetIsEnabled(m_shutterButton, true);    
        }
        
        m_cameraPreview?.RemoveViewFromRoot(m_confirmImage);
        m_cameraPreview?.RemoveToolbarView(m_confirmStateGrid);
        m_cameraPreview?.AddToolbarView(m_shutterButton);
    }

    private void ConstructCrossPlatformViews()
    {
        m_shutterButton = new Border
        {
            BackgroundColor = Microsoft.Maui.Graphics.Colors.DimGray,
            StrokeShape = new Ellipse(),
            Stroke = Colors.GetColor(ColorName.color_system_white),
            StrokeThickness = Sizes.GetSize(SizeName.size_1),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 70,
            HeightRequest = 70
        };
        
        Touch.SetCommand(m_shutterButton, new Command(() =>
        {
            _ = OnCapture();
            PlatformCapturePhoto();
        }));

        m_cameraPreview?.AddToolbarView(m_shutterButton);
    }

    private partial void PlatformCapturePhoto();
    private partial Task PlatformStart(ImageCaptureSettings imageCaptureSettings);
    private partial Task PlatformStop();

    private void Log(string message)
    {
        DUILogService.LogDebug<ImageCapture>(message);
    }

    /// <summary>
    /// Will stop the capture session.
    /// </summary>
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