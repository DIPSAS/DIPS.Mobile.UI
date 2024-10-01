using AVFoundation;
using CoreMedia;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.CameraZoom;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Extensions.iOS;
using DIPS.Mobile.UI.Internal.Logging;
using Foundation;
using Microsoft.Maui.Platform;
using PreviewView = DIPS.Mobile.UI.API.Camera.Preview.iOS.PreviewView;

namespace DIPS.Mobile.UI.API.Camera.Shared.iOS;

public abstract class CameraSession
{
#nullable disable
    internal CameraPreview m_cameraPreview;
#nullable enable
    
    internal AVCaptureVideoPreviewLayer? PreviewLayer { get; private set; }
    //The lens to be used for scanning bar codes
    internal AVCaptureDevice? CaptureDevice { get; private set; }
    
    internal PreviewView? PreviewView { get; private set; }
    
    //The session of the capture, there can only be one capture session running in an iOS app.
    protected AVCaptureSession? CaptureSession;

    
    //The rectangle that people see in the preview which we will focus on scanning bar codes in
    private AVCaptureOutput? m_avCaptureOutput;
    private AVCaptureDeviceInput? m_videoDeviceInput;
    private NSObject? m_runtimeErrorObserver;
    private CameraFailed? m_cameraFailedDelegate;
    private NSObject? m_startedSessionObserver;
    private NSObject? m_sessionStoppedObserver;
    
    private TaskCompletionSource<bool>? m_sessionStartedTask;
    private TaskCompletionSource<bool>? m_sessionStoppedTask;

    internal async Task StopCameraSession()
    {
        if (CaptureSession is {Running: true})
        {
            await Task.Run(() =>
            {
                CaptureSession.StopRunning();

                if (m_avCaptureOutput != null)
                {
                    CaptureSession.RemoveOutput(m_avCaptureOutput);    
                }

                if (m_videoDeviceInput != null)
                {
                    CaptureSession.RemoveInput(m_videoDeviceInput);
                }
                CaptureSession.Dispose();
                CaptureSession = null;
            });
        }
        

        RemoveObservers();
        CaptureDevice = null;
        m_avCaptureOutput = null;

        m_sessionStartedTask?.TrySetCanceled();
        m_sessionStartedTask = null;

        m_cameraFailedDelegate = null;
        if (PreviewView is not null)
        {
            PreviewView.OnZoomChanged -= PreviewViewOnZoomChanged;
            PreviewView?.Dispose();
            PreviewView = null;    
        }

        if (m_sessionStoppedTask?.Task is not null)
        {
            await m_sessionStoppedTask.Task;
            m_sessionStoppedTask = null;
        }

        NSNotificationCenter.DefaultCenter.RemoveObserver(m_sessionStoppedObserver!);
    }

    private void RemoveObservers()
    {
        if (m_runtimeErrorObserver != null)
        {
            NSNotificationCenter.DefaultCenter.RemoveObserver(m_runtimeErrorObserver);
        }

        if (m_startedSessionObserver != null)
        {
            NSNotificationCenter.DefaultCenter.RemoveObserver(m_startedSessionObserver);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cameraPreview">The virtual MAUI <see cref="CameraPreview"/></param>
    /// <param name="avCaptureOutput">The output </param>
    /// <exception cref="Exception"></exception>
    internal async Task ConfigureAndStart(CameraPreview cameraPreview, int? maxHeightOrWidth,
        AVCaptureOutput avCaptureOutput, CameraFailed cameraFailedDelegate)
    {
        m_cameraFailedDelegate = cameraFailedDelegate;
        m_cameraPreview = cameraPreview;
        m_sessionStartedTask = new TaskCompletionSource<bool>();
        m_sessionStoppedTask = new TaskCompletionSource<bool>();
        //This makes sure we display the video feed
        PreviewView = (PreviewView?)cameraPreview.PreviewView.ToPlatform(DUI.GetCurrentMauiContext!);
        PreviewView!.OnZoomChanged += PreviewViewOnZoomChanged;

        CaptureSession = new AVCaptureSession();

        AddObservers();

        //Call beginConfiguration() before changing a session’s inputs or outputs, and call commitConfiguration() after making changes.
        CaptureSession.BeginConfiguration();
        
        CaptureDevice = SelectCaptureDevice();
        
        if (CaptureDevice == null) throw new Exception("Unable to select an capture device.");
        
        PreviewLayer =
            await PreviewView?.WaitForViewLayoutAndAddSessionToPreview(CaptureDevice, CaptureSession,
                AVLayerVideoGravity.ResizeAspect)!;
        
        m_videoDeviceInput = AVCaptureDeviceInput.FromDevice(CaptureDevice);
        if (m_videoDeviceInput == null) throw new Exception($"video device input is null");
        if (CaptureSession.CanAddInput(m_videoDeviceInput))
        {
            CaptureSession.AddInput(m_videoDeviceInput);
        }
        else
        {
            throw new Exception("Unable to use the back camera wide angle camera to detect bar codes");
        }
        
        // Set quality based on target resolution
        if (maxHeightOrWidth is not null)
        {
            try
            {
                if (!CaptureDevice.LockForConfiguration(out var configurationLockError))
                    return;

                CaptureDevice.ActiveFormat = GetCompatibleFormat(maxHeightOrWidth.Value);

                CaptureDevice.UnlockForConfiguration();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        else
        {
            CaptureSession.SessionPreset = AVCaptureSession.PresetPhoto;
        }
        
        m_avCaptureOutput = avCaptureOutput;

        AddCameraZoomView();
        
        if (CaptureSession.CanAddOutput(m_avCaptureOutput))
        {
            CaptureSession.AddOutput(
                m_avCaptureOutput); //this has to be set before setting metadata objects, or else it crashes
            
            ConfigureSession();

            //Commit the configuration
            CaptureSession.CommitConfiguration();
            
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
                    CaptureSession?.StartRunning();
                }
            );
        }
        else
        {
            throw new Exception("Unable to add output");
        }
    }

    private AVCaptureDeviceFormat GetCompatibleFormat(int targetHeight)
    {
        var selectedVideoFormat = CaptureDevice?.Formats[0];
        foreach (var format in CaptureDevice?.Formats!)
        {
            if (format.FormatDescription is not CMVideoFormatDescription videoFormatDescription)
                continue;

            var isFourThreeRatio = Math.Abs(((float)videoFormatDescription.Dimensions.Height / videoFormatDescription.Dimensions.Width) - 0.75) < 0.01f;
            if(!isFourThreeRatio)
                continue;
            
            if (videoFormatDescription.Dimensions.Width <= targetHeight)
            {
                if (selectedVideoFormat?.FormatDescription is CMVideoFormatDescription selectedVideoFormatDescription)
                {
                    // Just take the first format of a resolution
                    if(selectedVideoFormatDescription.Dimensions.Width == videoFormatDescription.Dimensions.Width && selectedVideoFormatDescription.Dimensions.Height == videoFormatDescription.Dimensions.Height)
                        continue;
                }
                
                selectedVideoFormat = format;
            }
            else
            {
                break;
            }
        }

        return selectedVideoFormat!;
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
        if (m_cameraPreview != null)
        {
            m_cameraPreview.HasZoomed = true;
        }
        m_cameraPreview?.CameraZoomView?.OnPinchToZoom(zoomRatio);
    }

    private void AddObservers()
    {
        m_runtimeErrorObserver = NSNotificationCenter.DefaultCenter.AddObserver(AVCaptureSession.RuntimeErrorNotification, OnSessionRuntimeError, CaptureSession);
        m_startedSessionObserver = NSNotificationCenter.DefaultCenter.AddObserver(AVCaptureSession.DidStartRunningNotification, OnStartedRunning, CaptureSession);
        m_sessionStoppedObserver = NSNotificationCenter.DefaultCenter.AddObserver(AVCaptureSession.DidStopRunningNotification, OnSessionStopped, CaptureSession);
    }

    private void OnSessionStopped(NSNotification obj)
    {
        m_sessionStoppedTask?.TrySetResult(true);
    }

    private void OnStartedRunning(NSNotification obj)
    {
        m_sessionStartedTask?.TrySetResult(true);
    }

    internal Task<bool> HasStartedSession() => m_sessionStartedTask?.Task ?? Task.FromResult(false);

    private void OnSessionRuntimeError(NSNotification notification)
    {
        if (notification.UserInfo == null)
        {
            return;
        }

        var error = (NSError)notification.UserInfo[AVCaptureSession.ErrorKey];
        OnCameraFailed<CameraSession>(new CameraException("OnSessionRuntimeError", new Exception(error.ToExceptionMessage()), error.LocalizedDescription, error.LocalizedRecoverySuggestion));
    }

    private void OnChangedZoomRatio(float zoomRatio)
    {
        if (CaptureDevice is null || !CaptureDevice.LockForConfiguration(out var configurationLockError))
            return;

        try
        {
            CaptureDevice.RampToVideoZoom(zoomRatio, 5.0f);
            if (m_cameraPreview != null)
            {
                m_cameraPreview.HasZoomed = true;
            }
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