using Android.Content;
using Android.Hardware;
using Android.Hardware.Camera2;
using Android.Hardware.Display;
using Android.Runtime;
using Android.Views;
using AndroidX.Camera.Core;
using AndroidX.Camera.Lifecycle;
using AndroidX.Camera.Video;
using AndroidX.Camera.View;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Internal.Logging;
using Java.Lang;
using Java.Util.Concurrent;
using Microsoft.Maui.Platform;
using Fragment = AndroidX.Fragment.App.Fragment;
using FragmentManager = AndroidX.Fragment.App.FragmentManager;
using PreviewView = AndroidX.Camera.View.PreviewView;

namespace DIPS.Mobile.UI.API.Camera.Shared.Android;

//Based on : https://github.com/android/camera-samples/blob/main/CameraXBasic/app/src/main/java/com/android/example/cameraxbasic/fragments/CameraFragment.kt
public abstract class CameraFragment : Fragment
{
    private const string FragmentTag = nameof(CameraFragment);

    internal ProcessCameraProvider? CameraProvider { get; private set; }
    internal DisplayManager? DisplayManager { get; set; }
    public ICamera? Camera { get; private set; }
    internal ICameraControl? CameraControl => Camera?.CameraControl;
    internal ICameraInfo? CameraInfo => Camera?.CameraInfo;

    private TaskCompletionSource? m_startedTcs;

    internal PreviewView? PreviewView { get; private set; }
    private CameraPreview? m_cameraPreview;
    private UseCase[] m_useCase;
    private int m_displayId;
    private DeviceRotationListener? m_imageEventRotationListener;
    private SurfaceOrientation m_lastOrientation;

    public new Context? Context { get; }

    public new FragmentManager? FragmentManager { get; }

    public CameraFragment()
    {
        if (DUI.GetCurrentMauiContext != null)
        {
            Context = DUI.GetCurrentMauiContext.Context;
            FragmentManager = Context?.GetFragmentManager();
            var service = Context?.GetSystemService(Context.DisplayService);
            if (service is DisplayManager displayManager)
            {
                DisplayManager = displayManager;
            }
        }
    }


    private bool IsFragmentStarted()
    {
        return FragmentManager?.FindFragmentByTag(FragmentTag) != null;
    }

    internal async Task SetupCameraAndTryStartUseCase(CameraPreview cameraPreview, UseCase useCase)
    {
        if (IsFragmentStarted()) return;
        if (Context == null) return;

        m_startedTcs = new TaskCompletionSource();

        m_cameraPreview = cameraPreview;
        if (cameraPreview.Handler is not CameraPreviewHandler previewHandler) return;
        PreviewView = previewHandler.PreviewView;

        CameraProvider = (ProcessCameraProvider?)await ProcessCameraProvider.GetInstance(Context).GetAsync();
        if (CameraProvider == null) return;

        //Use back camera
        var cameraSelector = new CameraSelector.Builder().RequireLensFacing((int)(LensFacing.Back)).Build();

        //Create preview use case and attach it to our MAUI view. 
        var previewUseCase = new AndroidX.Camera.Core.Preview.Builder().Build();
        previewUseCase.SetSurfaceProvider(PreviewView.SurfaceProvider);

        //Bind the camera
        m_useCase = [previewUseCase, useCase];
        Camera = CameraProvider.BindToLifecycle(this, cameraSelector, previewUseCase, useCase);
        //Do configurations before starting the activity: https://developer.android.com/media/camera/camerax/configuration


        if (PreviewView.Display != null)
        {
            m_displayId = PreviewView.Display.DisplayId;
        }

        m_imageEventRotationListener = new DeviceRotationListener(UpdateRotation, Context);
        m_imageEventRotationListener.Enable();

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
                await SetupCameraAndTryStartUseCase(cameraPreview, useCase);
            }

            throw;
        }

        await m_startedTcs.Task;
    }


    private void UpdateRotation(SurfaceOrientation surfaceOrientation)
    {
        if (surfaceOrientation == m_lastOrientation) return;
        var rotation = (int)surfaceOrientation;
        m_lastOrientation = surfaceOrientation;
        foreach (var useCase in m_useCase)
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
        RotationChanged(surfaceOrientation);
    }

    public override void OnStart()
    {
        if (m_cameraPreview?.Handler is CameraPreviewHandler previewHandler)
        {
            previewHandler.OnCameraStarted(CameraControl!);
        }

        OnStarted();
        m_startedTcs?.TrySetResult();
        base.OnStart();
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
            CameraProvider?.Unbind();
            DisplayManager = null;
            m_imageEventRotationListener?.Disable();
            m_imageEventRotationListener = null;
            if (m_cameraPreview?.Handler is CameraPreviewHandler previewHandler)
            {
                previewHandler.RemoveZoomSlider();
            }
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


    internal abstract void RotationChanged(SurfaceOrientation surfaceOrientation);
}

//Based on : https://developer.android.com/media/camera/camerax/orientation-rotation#orientation-event-listener-setup
internal class DeviceRotationListener(Action<SurfaceOrientation>? orientationChanged, Context? context)
    : OrientationEventListener(context)
{
    private Action<SurfaceOrientation>? m_orientationChanged = orientationChanged;

    public override void OnOrientationChanged(int orientation)
    {
        switch (orientation)
        {
            case > 45 and <= 135:
                m_orientationChanged?.Invoke(SurfaceOrientation.Rotation270);
                break;
            case > 135 and <= 225:
                m_orientationChanged?.Invoke(SurfaceOrientation.Rotation180);
                break;
            case > 225 and <= 315:
                m_orientationChanged?.Invoke(SurfaceOrientation.Rotation90);
                break;
            default:
                m_orientationChanged?.Invoke(SurfaceOrientation.Rotation0);
                break;
        }
    }

    protected override void Dispose(bool disposing)
    {
        m_orientationChanged = null;
        base.Dispose(disposing);
    }
}