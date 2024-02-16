using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Hardware.Camera2;
using Android.Hardware.Camera2.Params;
using Android.Media;
using Android.OS;
using Android.Views;
using DIPS.Mobile.UI.API.Library;
using Java.Lang;
using Java.Util.Concurrent;
using Playground.HåvardSamples.Scanning.Android;
using Exception = Java.Lang.Exception;
using Orientation = Android.Content.Res.Orientation;
using Size = Android.Util.Size;

namespace Playground.HåvardSamples.Scanning;

//Based on:
//- https://developers.google.com/ml-kit/vision/barcode-scanning/android
// -https://github.com/googlesamples/mlkit/tree/master/android/material-showcase/app/src/main/java/com/google/mlkit/md/barcodedetection

//Camera2 documentation : https://developer.android.com/media/camera/camera2
public partial class Scanner : CameraDevice.StateCallback, ImageReader.IOnImageAvailableListener
{
    private readonly Context m_context;
    private HandlerThread m_backgroundHandlerThread;
    private Handler m_backgroundHandler;
    private Size m_previewSize;
    private ImageReader m_imageReader;
    private Preview m_preview;
    private AutoFitTextureView m_textureView;
    private CameraManager? m_cameraManager;
    private CameraDevice? m_cameraDevice;
    private CameraCaptureSession? m_captureSession;
    private TaskCompletionSource<string> m_tcs;
    private int m_sensorOrientation;
    private bool m_flashSupported;
    private string m_cameraId;
    /**
    * A {@link Semaphore} to prevent the app from exiting before closing the camera.
    */
    private readonly Java.Util.Concurrent.Semaphore m_cameraOpenCloseLock = new(1);

    private CaptureRequest.Builder m_previewRequestBuilder;
    private CaptureRequest m_previewRequest;
    private IExecutor m_executorService;

    /**
        * Max preview width that is guaranteed by Camera2 API
        */
    private const int MaxPreviewWidth = 1920;

    /**
     * Max preview height that is guaranteed by Camera2 API
     */
    private const int MaxPreviewHeight = 1080;

    public Scanner()
    {
        m_context = DUI.GetCurrentMauiContext?.Context;
    }

    public async partial Task<string> Start(Preview preview)
    {
        StartBackgroundThread();
        m_tcs = new TaskCompletionSource<string>();
        var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
        {
            if (await Permissions.RequestAsync<Permissions.Camera>() != PermissionStatus.Granted)
            {
                return string.Empty;
            }
        }

        //Inspiration : https://www.freecodecamp.org/news/android-camera2-api-take-photos-and-videos/

        m_preview = preview;
        
        if (m_textureView == null)
        {
            if (m_preview.Handler is PreviewHandler previewHandler)
            {
                m_textureView = previewHandler.PlatformView;
            }
            else
            {
                return string.Empty;
            }
        }
        
        if (m_textureView.IsAvailable)
        {
            OpenCamera((int)m_preview.Width, (int)m_preview.Height);
        }
        else
        {
            throw new Exception("TextureView is not available..");
        }
        
        return await m_tcs.Task;
    }

    private void StartBackgroundThread()
    {
        m_executorService = Executors.NewSingleThreadExecutor(); 
        m_backgroundHandlerThread = new HandlerThread("CameraBackground");
        m_backgroundHandlerThread.Start();
        if (m_backgroundHandlerThread.Looper == null)
        {
            return;
        }

        m_backgroundHandler = new Handler(m_backgroundHandlerThread.Looper);
    }

    private void StopBackgroundThread()
    {
        m_backgroundHandlerThread?.QuitSafely();
        m_backgroundHandlerThread?.Join();
        m_backgroundHandlerThread = null;
        m_backgroundHandler = null;
    }

    public partial void Stop()
    {
        m_cameraDevice?.Close();
        m_captureSession?.Close();
        StopBackgroundThread();


        m_cameraManager?.Dispose();
        m_cameraDevice?.Dispose();
        m_captureSession?.Dispose();

        m_captureSession = null;
        m_cameraManager = null;
        m_cameraDevice = null;
    }


    private void OnCaptureSessionConfigureFailed(CameraCaptureSession session)
    {
    }

    private void OnCaptureSessionConfigured(CameraCaptureSession session)
    {
        if (null == m_cameraDevice)
        {
            return;
        }

        // When the session is ready, we start displaying the preview.
        m_captureSession = session;
        try
        {
            // Auto focus should be continuous for camera preview.
            m_previewRequestBuilder.Set(CaptureRequest.ControlAfMode,
                Integer.ValueOf((int)ControlAFMode.ContinuousVideo));
            // Flash is automatically enabled when necessary.
            SetAutoFlash(m_previewRequestBuilder);

            // Finally, we start displaying the camera preview.
            m_previewRequest = m_previewRequestBuilder.Build();
            m_captureSession?.SetRepeatingRequest(m_previewRequest,
                new CaptureSessionCallBack(CaptureStillPicture, RunPreCaptureSequence, PreviewingCapture), m_backgroundHandler);
        }
        catch (CameraAccessException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void PreviewingCapture(CaptureResult captureResult)
    {
        
    }

    private void RunPreCaptureSequence(CaptureResult captureResult)
    {
    }

    private void CaptureStillPicture(CaptureResult captureResult)
    {
    }

    private void SetAutoFlash(CaptureRequest.Builder requestBuilder)
    {
        if (m_flashSupported)
        {
            requestBuilder.Set(CaptureRequest.ControlAeMode, Integer.ValueOf((int)ControlAEMode.OnAutoFlash));
        }
    }

    public void OnImageAvailable(ImageReader reader)
    {
    }

    public override void OnDisconnected(CameraDevice camera)
    {
        m_cameraOpenCloseLock.Release();
        m_cameraDevice?.Close();
        m_cameraDevice = null;
    }

    public override void OnError(CameraDevice camera, CameraError error)
    {
        m_cameraOpenCloseLock.Release();
        m_cameraDevice?.Close();
        m_cameraDevice = null;
    }

    public override void OnOpened(CameraDevice cameraDevice)
    {
        // This method is called when the camera is opened.  We start camera preview here.
        m_cameraOpenCloseLock.Release();
        m_cameraDevice = cameraDevice;
        CreateCameraPreviewSession();
    }

    private void CreateCameraPreviewSession()
    {
        try
        {
            var texture = m_textureView.SurfaceTexture;
            if (texture == null || m_cameraDevice == null) return;

            // We configure the size of default buffer to be the size of camera preview we want.
            texture.SetDefaultBufferSize(m_previewSize.Width, m_previewSize.Height);

            // This is the output Surface we need to start preview.
            var previewSurface = new Surface(texture);
            var imageReaderSurface = m_imageReader.Surface;
            if (imageReaderSurface == null) return;
            

            // We set up a CaptureRequest.Builder with the output Surface.
            m_previewRequestBuilder
                = m_cameraDevice.CreateCaptureRequest(CameraTemplate.Preview);
            m_previewRequestBuilder.AddTarget(previewSurface);

            // Here, we create a CameraCaptureSession for camera preview.
            var outputConfigurations = new List<OutputConfiguration>() {new(previewSurface), new(imageReaderSurface)};
            
            //Create capture session: https://developer.android.com/media/camera/camera2/capture-sessions-requests
            //Optimalize for Preview, change this if we want to support capture.
            //From: https://developer.android.com/media/camera/camera2/capture-sessions-requests#use-stream-use-case-for-better-performance
            foreach (var outputConfiguration in outputConfigurations)
            {
                if (outputConfiguration.Surface == previewSurface)
                {
                    if (OperatingSystem.IsAndroidVersionAtLeast(33,0))
                    {
                        outputConfiguration.StreamUseCase = (long)ScalerAvailableStreamUseCases.Preview;    
                    }    
                }else if (outputConfiguration.Surface == imageReaderSurface)
                {
                    if (OperatingSystem.IsAndroidVersionAtLeast(33,0))
                    {
                        outputConfiguration.StreamUseCase = (long)ScalerAvailableStreamUseCases.StillCapture;    
                    }
                }
                
            }
            var config = new SessionConfiguration((int)SessionType.Regular, outputConfigurations, m_executorService, new CaptureStateCallback(OnCaptureSessionConfigured, OnCaptureSessionConfigureFailed));
            m_cameraDevice.CreateCaptureSession(config);
        }
        catch (CameraAccessException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    /**
    * Opens the camera specified by {@link Camera2BasicFragment#mCameraId}.
    */
    private void OpenCamera(int width, int height)
    {
        if (m_executorService == null) return;
        SetUpCameraOutputs(width, height);
        
        var activity = (Activity)m_context;
        var manager = (CameraManager)activity.GetSystemService(Context.CameraService);
        if (manager == null) return;
        try
        {
            if (!m_cameraOpenCloseLock.TryAcquire(2500, TimeUnit.Milliseconds))
            {
                throw new RuntimeException("Time out waiting to lock camera opening.");
            }

            manager.OpenCamera(m_cameraId,m_executorService, this);
        }
        catch (CameraAccessException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (InterruptedException e)
        {
            throw new RuntimeException("Interrupted while trying to lock camera opening.", e);
        }
    }


    /// <summary>
    /// Based on this: https://github.com/tomoima525/android-Camera2Basic/blob/5a79cab4ad00593682d2f917c9efbc2781437373/kotlinApp/Application/src/main/java/com/example/android/camera2basic/Camera2BasicFragment.kt#L419
    /// From this guide: https://proandroiddev.com/understanding-camera2-api-from-callbacks-part-1-5d348de65950
    /// </summary>
    

    private void SetUpCameraOutputs(int width, int height)
    {
        var activity = (Activity)m_context;
        var manager = (CameraManager)activity.GetSystemService(Context.CameraService);
        try
        {
            foreach (var cameraId in manager.GetCameraIdList())
            {
                var characteristics
                    = manager.GetCameraCharacteristics(cameraId);
                // We don't use a front facing camera.
                var facing = characteristics.Get(CameraCharacteristics.LensFacing);
                if (facing == CameraCharacteristics.LensFacing)
                {
                    continue;
                }

                if (characteristics.Get(
                        CameraCharacteristics.ScalerStreamConfigurationMap) is not StreamConfigurationMap map) continue;

                var largest = (map.GetOutputSizes((int)ImageFormatType.Jpeg)?.MaxBy(s => s.Height * s.Width));
                if (largest == null) continue;
                m_imageReader = ImageReader.NewInstance(largest.Width, largest.Height, ImageFormatType.Jpeg, 2);
                m_imageReader.SetOnImageAvailableListener(this, m_backgroundHandler);

                // Find out if we need to swap dimension to get the preview size relative to sensor
                // coordinate.
                var displayRotation = activity.WindowManager?.DefaultDisplay?.Rotation;
                //noinspection ConstantConditions
                var potentialSensorOrientation = characteristics.Get(CameraCharacteristics.SensorOrientation);
                if (potentialSensorOrientation == null) continue;
                if (!int.TryParse(potentialSensorOrientation.ToString(), out var sensorOrientation)) continue;

                m_sensorOrientation = sensorOrientation;

                var swappedDimensions = false;
                switch (displayRotation)
                {
                    case SurfaceOrientation.Rotation0:
                    case SurfaceOrientation.Rotation180:
                        if (m_sensorOrientation is 90 or 270)
                        {
                            swappedDimensions = true;
                        }

                        break;
                    case SurfaceOrientation.Rotation270:
                    case SurfaceOrientation.Rotation90:
                        if (m_sensorOrientation is 0 or 180)
                        {
                            swappedDimensions = true;
                        }

                        break;
                    case null:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }


                var displaySize = new global::Android.Graphics.Point();
                activity.WindowManager.DefaultDisplay.GetSize(displaySize);
                var rotatedPreviewWidth = width;
                var rotatedPreviewHeight = height;
                var maxPreviewWidth = displaySize.X;
                var maxPreviewHeight = displaySize.Y;

                if (swappedDimensions)
                {
                    rotatedPreviewWidth = height;
                    rotatedPreviewHeight = width;
                    maxPreviewWidth = displaySize.Y;
                    maxPreviewHeight = displaySize.X;
                }

                if (maxPreviewWidth > MaxPreviewWidth)
                {
                    maxPreviewWidth = MaxPreviewWidth;
                }

                if (maxPreviewHeight > MaxPreviewHeight)
                {
                    maxPreviewHeight = MaxPreviewHeight;
                }


                // Danger, W.R.! Attempting to use too large a preview size could  exceed the camera
                // bus' bandwidth limitation, resulting in gorgeous previews but the storage of
                // garbage capture data.
                m_previewSize = ChooseOptimalSize(map.GetOutputSizes(((int)ImageFormatType.Jpeg)),
                    rotatedPreviewWidth, rotatedPreviewHeight, maxPreviewWidth,
                    maxPreviewHeight, largest);

                // We fit the aspect ratio of TextureView to the size of preview we picked.
                var orientation = activity.Resources.Configuration.Orientation;
                if (orientation == Orientation.Landscape)
                {
                    m_textureView.SetAspectRatio(
                        m_previewSize.Width, m_previewSize.Height);
                }
                else
                {
                    m_textureView.SetAspectRatio(
                        m_previewSize.Height, m_previewSize.Width);
                }

                // Check if the flash is supported.
                var available = characteristics.Get(CameraCharacteristics.FlashInfoAvailable);
                m_flashSupported = available != null;

                m_cameraId = cameraId;
                return;
            }
        }
        catch (CameraAccessException e)
        {
            //TODO:
        }
        catch (NullPointerException e)
        {
            // Currently an NPE is thrown when the Camera2API is used but not supported on the
            // device this code runs.
            //TODO:
        }
    }

    /**
     * Given {@code choices} of {@code Size}s supported by a camera, choose the smallest one that
     * is at least as large as the respective texture view size, and that is at most as large as the
     * respective max size, and whose aspect ratio matches with the specified value. If such size
     * doesn't exist, choose the largest one that is at most as large as the respective max size,
     * and whose aspect ratio matches with the specified value.
     *
     * @param choices           The list of sizes that the camera supports for the intended output
     *                          class
     * @param textureViewWidth  The width of the texture view relative to sensor coordinate
     * @param textureViewHeight The height of the texture view relative to sensor coordinate
     * @param maxWidth          The maximum width that can be chosen
     * @param maxHeight         The maximum height that can be chosen
     * @param aspectRatio       The aspect ratio
     * @return The optimal {@code Size}, or an arbitrary one if none were big enough
     */
    private static Size ChooseOptimalSize(Size[] choices, int textureViewWidth,
        int textureViewHeight, int maxWidth, int maxHeight, Size aspectRatio)
    {
        // Collect the supported resolutions that are at least as big as the preview Surface
        var bigEnough = new List<Size>();
        // Collect the supported resolutions that are smaller than the preview Surface
        var notBigEnough = new List<Size>();
        int w = aspectRatio.Width;
        int h = aspectRatio.Height;
        foreach (var option in choices)
        {
            if (option.Width <= maxWidth && option.Height <= maxHeight &&
                option.Height == option.Width * h / w)
            {
                if (option.Width >= textureViewWidth &&
                    option.Height >= textureViewHeight)
                {
                    bigEnough.Add(option);
                }
                else
                {
                    notBigEnough.Add(option);
                }
            }
        }

        // Pick the smallest of those big enough. If there is no one big enough, pick the
        // largest of those not big enough.
        if (bigEnough.Count > 0)
        {
            return bigEnough.MinBy(s => s.Height * s.Width);
        }
        else if (notBigEnough.Count > 0)
        {
            return notBigEnough.MinBy(s => s.Height * s.Width);
        }
        else
        {
            Console.WriteLine(@"Couldn't find any suitable preview size");
            return choices[0];
        }
    }
}