using AVFoundation;
using DIPS.Mobile.UI.API.Camera.Extensions.iOS;
using DIPS.Mobile.UI.API.Camera.ImageCapturing;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.CameraZoom;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Preview.iOS;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Internal.Logging;
using Foundation;
using Microsoft.Maui.Platform;
using SystemConfiguration;
using UIKit;
using ContentView = Microsoft.Maui.Platform.ContentView;
using PreviewView = DIPS.Mobile.UI.API.Camera.Preview.iOS.PreviewView;

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
    private NSObject? m_runtimeErrorObserver;
    private CameraFailed? m_cameraFailedDelegate;

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

        RemoveObservers();
        CaptureDevice = null;

        if (PreviewView is null)
            return;

        PreviewView.OnZoomChanged -= PreviewViewOnZoomChanged;
        PreviewView?.Dispose();
        PreviewView = null;
        m_cameraFailedDelegate = null;
    }

    private void RemoveObservers()
    {
        if (m_runtimeErrorObserver != null)
        {
            NSNotificationCenter.DefaultCenter.RemoveObserver(m_runtimeErrorObserver);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cameraPreview">The virtual MAUI <see cref="CameraPreview"/></param>
    /// <param name="sessionPreset">The quality of the camera session</param>
    /// <param name="avCaptureOutput">The output </param>
    /// <exception cref="Exception"></exception>
    internal async Task ConfigureAndStart(CameraPreview cameraPreview, NSString sessionPreset,
        AVCaptureOutput avCaptureOutput, CameraFailed cameraFailedDelegate)
    {
        m_cameraFailedDelegate = cameraFailedDelegate;
        m_cameraPreview = cameraPreview;
        
        //This makes sure we display the video feed
        PreviewView = (PreviewView?)cameraPreview.PreviewView.ToPlatform(DUI.GetCurrentMauiContext!);
        PreviewView.OnZoomChanged += PreviewViewOnZoomChanged;

        m_captureSession = new AVCaptureSession();

        AddObservers();

        //Call beginConfiguration() before changing a session’s inputs or outputs, and call commitConfiguration() after making changes.
        m_captureSession.BeginConfiguration();
        
        CaptureDevice = SelectCaptureDevice();
        if (CaptureDevice == null) throw new Exception("Unable to select an capture device.");
        
        PreviewLayer =
            await PreviewView?.WaitForViewLayoutAndAddSessionToPreview(CaptureDevice, m_captureSession,
                AVLayerVideoGravity.ResizeAspect)!;
        
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

        AddCameraZoomView();
        
        if (m_captureSession.CanAddOutput(m_avCaptureOutput))
        {
            m_captureSession.AddOutput(
                m_avCaptureOutput); //this has to be set before setting metadata objects, or else it crashes
            
            ConfigureSession();

            //Commit the configuration
            m_captureSession.CommitConfiguration();
            
            PreviewView.AddPinchToZoom(CaptureDevice);
            PreviewView.AddTapToFocus(CaptureDevice);
            PreviewView.OnTapToFocus += PreviewViewOnOnTapToFocus;

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

    private void AddCameraZoomView()
    {
        if (m_cameraPreview is null || CaptureDevice is null)
            return;

        if (m_cameraPreview?.CameraZoomView is not null)
        {
            m_cameraPreview.CameraZoomView.IsVisible = true;
            m_cameraPreview.CameraZoomView?.SetZoomRatio((float)CaptureDevice.VideoZoomFactor);
        }
        else if(m_cameraPreview is not null)
        {
            m_cameraPreview.CameraZoomView = new CameraZoomView((float)CaptureDevice.MinAvailableVideoZoomFactor,
                (int)Math.Min(CaptureDevice.MaxAvailableVideoZoomFactor, PreviewView.MaxZoomRatio),
                OnChangedZoomRatio);
        }
    }

    private void PreviewViewOnOnTapToFocus(float x, float y)
    {
        var percentX = x / (float)m_cameraPreview?.PreviewView.Width!;
        var percentY = y / (float)m_cameraPreview?.PreviewView.Height!;
        m_cameraPreview?.AddFocusIndicator(percentX, percentY);
    }

    private void PreviewViewOnZoomChanged(float zoomRatio)
    {
        m_cameraPreview?.CameraZoomView?.OnPinchToZoom(zoomRatio);
    }

    private void AddObservers()
    {
        m_runtimeErrorObserver = NSNotificationCenter.DefaultCenter.AddObserver(AVCaptureSession.RuntimeErrorNotification, OnSessionRuntimeError, m_captureSession);
    }

    private void OnSessionRuntimeError(NSNotification notification)
    {
        if (notification.UserInfo == null)
        {
            return;
        }

        var error = (NSError)notification.UserInfo[AVCaptureSession.ErrorKey];
        OnCameraFailed<CameraSession>(new CameraException("OnSessionRuntimeError", new Exception(error.ToExceptionMessage())));
    }

    private void OnChangedZoomRatio(float zoomRatio)
    {
        if (CaptureDevice is null || !CaptureDevice.LockForConfiguration(out var configurationLockError))
            return;

        try
        {
            CaptureDevice.RampToVideoZoom(zoomRatio, 5.0f);
        }
        catch (Exception e)
        {
            OnCameraFailed<CameraSession>(new CameraException("OnChangedZoomRatio", e), true);
        }
        finally
        {
            CaptureDevice.UnlockForConfiguration();
        }
    }

    public abstract void ConfigureSession();

    public abstract AVCaptureDevice? SelectCaptureDevice();
    
    internal void OnCameraFailed<T>(CameraException exception, bool shouldOnlyLog = false) where T : class
    {
        DUILogService.LogError<T>(exception.Message);
        if (shouldOnlyLog) return;
        m_cameraFailedDelegate?.Invoke(exception);
    }
}