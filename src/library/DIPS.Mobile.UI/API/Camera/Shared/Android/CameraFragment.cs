using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.Hardware;
using Android.Hardware.Camera2;
using Android.Hardware.Display;
using Android.Runtime;
using Android.Views;
using AndroidX.Camera.Core;
using AndroidX.Camera.Lifecycle;
using AndroidX.Camera.Video;
using AndroidX.Camera.View;
using DIPS.Mobile.UI.API.Camera.ImageCapturing;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.CameraZoom;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Internal.Logging;
using Java.Lang;
using Java.Util.Concurrent;
using Microsoft.Maui.Platform;
using Exception = Java.Lang.Exception;
using Fragment = AndroidX.Fragment.App.Fragment;
using FragmentManager = AndroidX.Fragment.App.FragmentManager;
using ImageCapture = AndroidX.Camera.Core.ImageCapture;
using PreviewView = AndroidX.Camera.View.PreviewView;

namespace DIPS.Mobile.UI.API.Camera.Shared.Android;

//Based on : https://github.com/android/camera-samples/blob/main/CameraXBasic/app/src/main/java/com/android/example/cameraxbasic/fragments/CameraFragment.kt
public abstract class CameraFragment : Fragment
{
    private const string FragmentTag = nameof(CameraFragment);

    internal ProcessCameraProvider? CameraProvider { get; private set; }

    internal DisplayManager? DisplayManager
    {
        get
        {
            var service = Context?.GetSystemService(Context.DisplayService);
            if (service is DisplayManager displayManager)
            {
                return displayManager;
            }

            return null;
        }
    }

    public ICamera? Camera { get; private set; }
    internal ICameraControl? CameraControl => Camera?.CameraControl;
    internal ICameraInfo? CameraInfo => Camera?.CameraInfo;

    private TaskCompletionSource? m_startedTcs;

    internal PreviewView? PreviewView { get; private set; }
    private CameraPreview? m_cameraPreview;
    private int m_displayId;
    private DeviceRotationListener? m_imageEventRotationListener;
    private SurfaceOrientation m_lastOrientation; 
    protected UseCaseGroup UseCaseGroup;
    private DeviceDisplayListener? m_deviceDisplayListener;
    protected AndroidX.Camera.Core.Preview m_previewUseCase;
    private CameraFailed? m_cameraFailedDelegate;

    public new Context? Context { get; }

    public new FragmentManager? FragmentManager { get; }

    public CameraFragment()
    {
        if (DUI.GetCurrentMauiContext != null)
        {
            Context = DUI.GetCurrentMauiContext.Context;
            FragmentManager = Context?.GetFragmentManager();
        }
    }


    private bool IsFragmentStarted()
    {
        return FragmentManager?.FindFragmentByTag(FragmentTag) != null;
    }

    internal async Task SetupCameraAndTryStartUseCase(CameraPreview cameraPreview, UseCase useCase, CameraFailed cameraFailedDelegate)
    {
        if (IsFragmentStarted()) return;
        if (Context == null) return;

        m_startedTcs = new TaskCompletionSource();
        m_cameraFailedDelegate = cameraFailedDelegate;

        m_cameraPreview = cameraPreview;
        PreviewView = (PreviewView?)cameraPreview.PreviewView.ToPlatform(DUI.GetCurrentMauiContext!);

        CameraProvider = (ProcessCameraProvider?)await ProcessCameraProvider.GetInstance(Context).GetAsync();
        if (CameraProvider == null) return;

        //Use back camera
        var cameraSelector = CameraSelector.DefaultBackCamera;
        // var cameraSelector = new CameraSelector.Builder().RequireLensFacing((int)(LensFacing.Back)).Build();

        //Create preview use case and attach it to our MAUI view. 
        m_previewUseCase = new AndroidX.Camera.Core.Preview.Builder()
            .Build();
        m_previewUseCase.SetSurfaceProvider(PreviewView?.SurfaceProvider);
        
        await WaitForPreviewViewToInitialize();

        if (PreviewView.ViewPort == null) return;
        PreviewView.SetScaleType(PreviewView.ScaleType.FitCenter); //FillCenter is better UX, but we need to handle cropping when image is taken due to the camera viewport being larger than the preview view port.
        UseCaseGroup = new UseCaseGroup.Builder()
            .AddUseCase(m_previewUseCase)
            .AddUseCase(useCase)
            .SetViewPort(PreviewView.ViewPort)
            .Build();
        
        //Bind the camera to use cases.
        Camera = CameraProvider.BindToLifecycle(this, cameraSelector, UseCaseGroup);
        //Do configurations before starting the activity: https://developer.android.com/media/camera/camerax/configuration


        if (PreviewView.Display != null)
        {
            m_displayId = PreviewView.Display.DisplayId;
        }
        
        try
        {
            FragmentManager?.BeginTransaction().Add(this, FragmentTag).CommitAllowingStateLoss();
        }
        catch (IllegalStateException illegalStateException)
        {
            if (illegalStateException.Message != null &&
                illegalStateException.Message.Contains(
                    "FragmentManager is already executing transactions")) //This might happen if we use CommitNow(), and the fragmentmanager is executing other transactions, like closing a bottom sheet or navigating. We retry after a small amount of time if so
            {
                await Task.Delay(400);
                await SetupCameraAndTryStartUseCase(cameraPreview, useCase, cameraFailedDelegate);
            }

            throw;
        }

        await m_startedTcs.Task;
    }

    private async Task WaitForPreviewViewToInitialize()
    {
        var tries = 0;
        
        while(PreviewView?.ViewPort is null)
        {
            await Task.Delay(10);
            tries++;

            if (tries > 100)
            {
                throw new Exception("Could not initialize preview view");
            }
        }
    }

    private void UpdateOrientation(SurfaceOrientation surfaceOrientation)
    {
        if (surfaceOrientation == m_lastOrientation) return;
        var rotation = (int)surfaceOrientation;
        DUILogService.LogDebug<CameraFragment>($"Changing rotation from {m_lastOrientation} to {surfaceOrientation}");
        m_lastOrientation = surfaceOrientation;
        foreach (var useCase in UseCaseGroup.UseCases)
        {
            switch (useCase)
            {
                case AndroidX.Camera.Core.Preview preview:
                    preview.TargetRotation = rotation;
                    break;
                case ImageCapture imageCapture:
                    imageCapture.TargetRotation = rotation;
                    break;
                case VideoCapture videoCapture:
                    videoCapture.TargetRotation = rotation;
                    break;
                case ImageAnalysis imageAnalysis:
                    imageAnalysis.TargetRotation = rotation;
                    break;
            }
        }
        OrientationChanged(surfaceOrientation);
    }

    public override void OnStart()
    {
        if (DisplayManager == null) return;
        if (Activity?.RequestedOrientation != ScreenOrientation.Unspecified) //The orientation is locked by the consumer.
        {
            m_imageEventRotationListener = new DeviceRotationListener(UpdateOrientation, Context);
            m_imageEventRotationListener.Enable();    
        }
        else
        {
            m_deviceDisplayListener = new DeviceDisplayListener(UpdateOrientation, DisplayManager);
            DisplayManager?.RegisterDisplayListener(m_deviceDisplayListener, null);    
        }

        AddTapToFocus();
        AddPinchToZoom();
        AddZoomView();
        OnStarted();
        m_startedTcs?.TrySetResult();
        base.OnStart();
    }

    private void AddTapToFocus()
    {
        if(PreviewViewHandler is not null)
            PreviewViewHandler.OnTapped += PreviewViewOnTapped;
    }

    /*
     Taken from:
    - https://stackoverflow.com/questions/63202209/camerax-how-to-add-pinch-to-zoom-and-tap-to-focus-onclicklistener-and-ontouchl
    - https://developer.android.com/media/camera/camerax/configuration#focus-and-metering 
    */
    private void PreviewViewOnTapped(float x, float y)
    {
        var point = PreviewView?.MeteringPointFactory.CreatePoint(x, y);

        if(point is null)
            return;
        
        var action = new FocusMeteringAction.Builder(point, FocusMeteringAction.FlagAf | FocusMeteringAction.FlagAe)
            .SetAutoCancelDuration(5, TimeUnit.Seconds!)
            .Build();
        CameraControl?.StartFocusAndMetering(action);
    }

    private void AddPinchToZoom()
    {
        if(PreviewViewHandler is not null)
        {
            PreviewViewHandler.OnScaled += OnScaled;
        }
    }

    private void OnScaled(float scaleRatio)
    {
        if (Camera?.CameraInfo.ZoomState.Value is not AndroidX.Camera.Core.Internal.ImmutableZoomState zoomState)
            return;

        var desiredZoomRatio = zoomState.ZoomRatio * scaleRatio;
        if(desiredZoomRatio > zoomState.MaxZoomRatio)
        {
            desiredZoomRatio = zoomState.MaxZoomRatio;
        }
        else if (desiredZoomRatio < zoomState.MinZoomRatio)
        {
            desiredZoomRatio = zoomState.MinZoomRatio;
        }
            
        CameraControl?.SetZoomRatio(desiredZoomRatio);
        m_cameraPreview?.CameraZoomView?.OnPinchToZoom(desiredZoomRatio);
    }

    private void AddZoomView()
    {
        if (Camera?.CameraInfo.ZoomState.Value is AndroidX.Camera.Core.Internal.ImmutableZoomState zoomState &&
            m_cameraPreview is not null)
        {
            m_cameraPreview.CameraZoomView =
                new CameraZoomView(zoomState.MinZoomRatio, zoomState.MaxZoomRatio, OnChangedZoomRatio);
        }
    }
    
    private void OnChangedZoomRatio(float zoomRatio)
    {
        CameraControl?.SetZoomRatio(zoomRatio);
    }

    public override void OnConfigurationChanged(Configuration newConfig)
    {
        if (PreviewView is {Display: not null})
        {
            UpdateOrientation(PreviewView.Display.Rotation);
        }
        
        base.OnConfigurationChanged(newConfig);
    }

    /// <summary>
    /// The fragment has started, do your thing!
    /// </summary>
    public abstract void OnStarted();

    internal async Task TryStop()
    {
        if (!IsFragmentStarted()) return;
        try
        {
            FragmentManager?.BeginTransaction().Remove(this).CommitAllowingStateLoss();
        }
        catch (IllegalStateException illegalStateException)
        {
            if (illegalStateException.Message != null &&
                illegalStateException.Message.Contains(
                    "FragmentManager is already executing transactions")) //This might happen if we use CommitNow(), and the fragmentmanager is executing other transactions, like closing a bottom sheet or navigating. We retry after a small amount of time if so
            {
                await Task.Delay(400);
                await TryStop();
            }
        }
    }

    public override void OnDestroy()
    {
        CameraProvider?.UnbindAll();
        CameraProvider?.Dispose();
        CameraProvider = null;
        m_cameraFailedDelegate = null;
        UnRegisterRotationEvents();
        
        base.OnDestroy();
    }

    private void UnRegisterRotationEvents()
    {
        if (PreviewViewHandler is not null)
        {
            PreviewViewHandler.OnScaled -= OnScaled;
            PreviewViewHandler.OnTapped -= PreviewViewOnTapped;
        }

        m_imageEventRotationListener?.Disable();
        m_imageEventRotationListener = null;

        if (m_deviceDisplayListener == null)
            return;

        DisplayManager?.UnregisterDisplayListener(m_deviceDisplayListener);
        m_deviceDisplayListener = null;
    }

    private PreviewViewHandler? PreviewViewHandler => m_cameraPreview?.PreviewView.Handler is not PreviewViewHandler previewViewHandler ? null : previewViewHandler;

    internal abstract void OrientationChanged(SurfaceOrientation surfaceOrientation);
    
    internal void OnCameraFailed<T>(CameraException exception, bool shouldOnlyLog=false) where T : class
    {
        DUILogService.LogError<T>(exception.Message);
        if (shouldOnlyLog) return;
        m_cameraFailedDelegate?.Invoke(exception);
    }
}