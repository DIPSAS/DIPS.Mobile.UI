using AVFoundation;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Preview.iOS;
using Foundation;
using UIKit;
using ContentView = Microsoft.Maui.Platform.ContentView;

namespace DIPS.Mobile.UI.API.Camera.Shared.iOS;

public abstract class CameraSession
{
    internal CameraPreview? m_cameraPreview;
    internal AVCaptureVideoPreviewLayer? PreviewLayer { get; private set; }
    //The the lense to be used for scanning bar codes
    internal AVCaptureDevice? CaptureDevice { get; private set; }
    
    internal PreviewView? PreviewView { get; private set; }
    
    //The session of the capture, there can only be one capture session running in an iOS app.
    private AVCaptureSession? m_captureSession;

    //The rectangle that people see in the preview which we will focus on scanning bar codes in
    
    private AVCaptureOutput? m_avCaptureOutput;
    private AVCaptureDeviceInput? m_videoDeviceInput;
    
    internal void StopCameraSession()
    {
        
        if (m_captureSession is {Running: true})
        {
            Task.Run(() =>
            {
                m_captureSession.StopRunning();
                if (m_avCaptureOutput != null)
                {
                    m_captureSession.RemoveOutput(m_avCaptureOutput);    
                }

                if (m_videoDeviceInput != null)
                {
                    m_captureSession.RemoveInput(m_videoDeviceInput);
                }
                m_captureSession = null;
            });
        }

        CaptureDevice = null;

        if (m_cameraPreview?.Handler is CameraPreviewHandler cameraPreviewHandler)
        {
            cameraPreviewHandler.Dispose();
        }
        PreviewView = null;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cameraPreview">The virtual MAUI <see cref="CameraPreview"/></param>
    /// <param name="sessionPreset">The quality of the camera session</param>
    /// <param name="avCaptureOutput">The output </param>
    /// <exception cref="Exception"></exception>
    internal async Task ConfigureAndStart(CameraPreview cameraPreview, NSString sessionPreset,
        AVCaptureOutput avCaptureOutput)
    {
        m_cameraPreview = cameraPreview;
        //This makes sure we display the video feed
        if (m_cameraPreview?.Handler is not CameraPreviewHandler previewHandler) return;

        PreviewView = (PreviewView)previewHandler.PlatformView;

        m_captureSession = new AVCaptureSession();

        //Call beginConfiguration() before changing a session’s inputs or outputs, and call commitConfiguration() after making changes.
        m_captureSession.BeginConfiguration();

        PreviewLayer =
            await previewHandler.WaitForViewLayoutAndAddSessionToPreview(m_captureSession,
                AVLayerVideoGravity.ResizeAspect);
        //Choosing build in wide angle camera, same as the sample app from Apple: AVCamBarCode: https://developer.apple.com/documentation/avfoundation/capture_setup/avcambarcode_detecting_barcodes_and_faces

        
        CaptureDevice = SelectCaptureDevice();
        if (CaptureDevice == null) throw new Exception("Unable to select an capture device.");
        
        m_videoDeviceInput = AVCaptureDeviceInput.FromDevice(CaptureDevice);
        if (m_videoDeviceInput == null) throw new Exception($"video device input is null");
        if (m_captureSession.CanAddInput(m_videoDeviceInput))
        {
            m_captureSession.AddInput(m_videoDeviceInput);
        }
        else
        {
            throw new Exception("Unable to use the back camera wide angle camera to detect bar codes");
        }

        //Set quality for best performance
        m_captureSession.SessionPreset = sessionPreset;

        //Add barcode camera output
        m_avCaptureOutput = avCaptureOutput;

        if (m_captureSession.CanAddOutput(m_avCaptureOutput))
        {
            m_captureSession.AddOutput(
                m_avCaptureOutput); //this has to be set before setting metadata objects, or else it crashes

            ConfigureSession();

            //Commit the configuration
            m_captureSession.CommitConfiguration();
            
            previewHandler.AddZoomSlider(CaptureDevice);
            previewHandler.AddPinchToZoom(CaptureDevice);

            previewHandler.AddTapToFocus(CaptureDevice);

            /*
            Setup the capture session.
            In general it is not safe to mutate an AVCaptureSession or any of its
            inputs, outputs, or connections from multiple threads at the same time.

            Why not do all of this on the main queue?
            Because AVCaptureSession.startRunning() is a blocking call which can
            take a long time. We dispatch session setup to the sessionQueue so
            that the main queue isn't blocked, which keeps the UI responsive.
            */
            await Task.Run(() =>
                {
                    m_captureSession?.StartRunning();
                }
            );
        }
        else
        {
            throw new Exception("Unable to add output");
        }
    }
    
    public abstract void ConfigureSession();

    public abstract AVCaptureDevice? SelectCaptureDevice();
}