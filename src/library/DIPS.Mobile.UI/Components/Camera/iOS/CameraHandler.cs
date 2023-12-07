using AVFoundation;
using Microsoft.Maui.Handlers;
using UIKit;

namespace DIPS.Mobile.UI.Components.Camera;

public partial class CameraHandler : ViewHandler<Camera, UIView>
{
    private AVCaptureSession m_captureSession;
    private AVCaptureDeviceInput? m_captureDeviceInput;
    private AVCaptureStillImageOutput m_stillImageOutput;
    private bool m_isAuthorized;


    protected override UIView CreatePlatformView()
    {
        return new UIView();
    }
    
    protected override void ConnectHandler(UIView nativeView)
    {
        base.ConnectHandler(nativeView);
        if(!m_isAuthorized)
            AVCaptureDevice.RequestAccessForMediaType(AVAuthorizationMediaType.Video, granted => m_isAuthorized = granted);
        SetupCamera();
    }

    protected override void DisconnectHandler(UIView nativeView)
    {
        base.DisconnectHandler(nativeView);
        CleanupCamera();
    }
    
    private void SetupCamera()
    {
        try
        {
            m_captureSession = new AVCaptureSession();

            // Find the back camera
            var cameraDevice = AVCaptureDevice.GetDefaultDevice(AVMediaTypes.Video);
            if (cameraDevice == null)
            {
                Console.WriteLine("No camera found");
                return;
            }

            m_captureDeviceInput = AVCaptureDeviceInput.FromDevice(cameraDevice, out var error);

            if (error != null || m_captureDeviceInput == null)
            {
                Console.WriteLine($"Error creating capture device input: {error}");
                return;
            }

            m_captureSession.AddInput(m_captureDeviceInput);

            // Configure still image output
            m_stillImageOutput = new AVCaptureStillImageOutput();
            m_captureSession.AddOutput(m_stillImageOutput);

            // Set up the preview layer
            var previewLayer = new AVCaptureVideoPreviewLayer(m_captureSession)
            {
                Frame = PlatformView.Bounds,
                VideoGravity = AVLayerVideoGravity.ResizeAspectFill
            };

            PlatformView.Layer.AddSublayer(previewLayer);

            m_captureSession.StartRunning();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting up camera: {ex.Message}");
        }    
    }

    private void CleanupCamera()
    {
        m_captureSession?.StopRunning();
        m_captureDeviceInput?.Dispose();
        m_stillImageOutput?.Dispose();
        m_captureSession?.Dispose();    
    }
}