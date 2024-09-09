using Android.Content.Res;
using Android.Graphics;
using Android.Views;
using AndroidX.Camera.Core;
using AndroidX.Camera.Core.ResolutionSelector;
using AndroidX.Camera.View;
using AndroidX.Core.Content;
using DIPS.Mobile.UI.API.Camera.Gallery;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared.Android;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Internal.Logging;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Java.Lang;
using Java.Util.Concurrent;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.Shapes;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Color = Microsoft.Maui.Graphics.Color;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Matrix = Android.Graphics.Matrix;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture : CameraFragment
{
    private AndroidX.Camera.Core.ImageCapture? m_cameraCaptureUseCase;
    
    private partial Task PlatformStart()
    {
        var resolutionSelector = new ResolutionSelector.Builder()
            .SetResolutionStrategy(new ResolutionStrategy(new  Android.Util.Size(1280, 720),
                ResolutionStrategy.FallbackRuleClosestLower)).Build();
        m_cameraCaptureUseCase = new AndroidX.Camera.Core.ImageCapture.Builder()
            .SetResolutionSelector(resolutionSelector)
            .Build();

// Add listener to receive updates.
        return m_cameraPreview != null
            ? base.SetupCameraAndTryStartUseCase(m_cameraPreview, m_cameraCaptureUseCase)
            : Task.CompletedTask;
    }

    private partial Task PlatformStop()
    {
        return base.TryStop();
    }

    public override void OnStarted()
    {
        //Update target rotation
        if (m_cameraCaptureUseCase is null || PreviewView is null ||
            m_cameraPreview?.Handler is not CameraPreviewHandler previewHandler) return;

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
        
        Touch.SetCommand(shutterButton, new Command(() =>
        {
            if (Context == null) 
                return;
            
            m_cameraCaptureUseCase?.TakePicture(ContextCompat.GetMainExecutor(Context),
                new ImageCaptureCallback(OnImageCaptured, InvokeOnImageCaptureFailed));
        }));

        previewHandler.AddView(shutterButton);
    }

    private void InvokeOnImageCaptureFailed(ImageCaptureException obj)
    {
        if (obj.Message != null)
        {
            DUILogService.LogError<ImageCapture>(obj.Message);
        }
    }

    internal override void RotationChanged(SurfaceOrientation surfaceOrientation)
    {
    }

    private async void OnImageCaptured(IImageProxy image)
    {
        //REMEMBER ROTATION.
        
        var bitmapImage = image.ToBitmap();
        var matrix = new Matrix();
        //Rotate the bitmap depending on how the image was rotated when being captured.
        if (PreviewView is {Display: not null})
        {
            //Taken from: //https://developer.android.com/media/camera/camerax/orientation-rotation
            var rotationDegrees = image.ImageInfo.RotationDegrees;
            matrix.PostRotate(rotationDegrees);
        }

        var rotatedBitmap =
            Bitmap.CreateBitmap(bitmapImage, 0, 0, bitmapImage.Width, bitmapImage.Height, matrix, true);
        
        using var rotatedMemoryStream = new MemoryStream();
        await rotatedBitmap.CompressAsync(Bitmap.CompressFormat.Png!, 100, rotatedMemoryStream);
        var byteArray = rotatedMemoryStream.ToArray();
        var capturedImage = new CapturedImage(byteArray, image.ImageInfo);
        InvokeOnImageCaptured(capturedImage);
        rotatedBitmap.Recycle();
        bitmapImage.Recycle();
    }

    public void Execute(IRunnable? command)
    {
        throw new NotImplementedException();
    }

    public override void OnConfigurationChanged(Configuration newConfig)
    {
        base.OnConfigurationChanged(newConfig);
    }
}

internal class ImageCaptureCallback : AndroidX.Camera.Core.ImageCapture.OnImageCapturedCallback
{
    private Action<IImageProxy> m_invokeOnImageCaptured;
    private readonly Action<ImageCaptureException> m_invokeOnImageCaptureFailed;

    public ImageCaptureCallback(Action<IImageProxy> invokeOnImageCaptured, Action<ImageCaptureException> invokeOnImageCaptureFailed)
    {
        m_invokeOnImageCaptured = invokeOnImageCaptured;
        m_invokeOnImageCaptureFailed = invokeOnImageCaptureFailed;
    }


    public override void OnError(ImageCaptureException exception)
    {
        m_invokeOnImageCaptureFailed.Invoke(exception);
        base.OnError(exception);
    }
    

    public override void OnCaptureSuccess(IImageProxy image)
    {
        m_invokeOnImageCaptured.Invoke(image);
        base.OnCaptureSuccess(image);
        image?.Close();
    }

    protected override void Dispose(bool disposing)
    {
        m_invokeOnImageCaptured = null;
        base.Dispose(disposing);
    }
}