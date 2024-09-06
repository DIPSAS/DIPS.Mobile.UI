using Android.Content;
using Android.Hardware.Camera2;
using AndroidX.Camera.Core;
using AndroidX.Camera.Lifecycle;
using AndroidX.Camera.View;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Library;
using Java.Lang;
using Java.Util.Concurrent;
using Microsoft.Maui.Platform;
using Fragment = AndroidX.Fragment.App.Fragment;
using FragmentManager = AndroidX.Fragment.App.FragmentManager;

namespace DIPS.Mobile.UI.API.Camera.Shared.Android;

//Based on : https://github.com/android/camera-samples/blob/main/CameraXBasic/app/src/main/java/com/android/example/cameraxbasic/fragments/CameraFragment.kt
public abstract class CameraFragment : Fragment
{
    private const string FragmentTag = nameof(CameraFragment);

    internal ProcessCameraProvider? CameraProvider { get; private set; }
    
    public ICamera? Camera { get; private set; }
    internal ICameraControl? CameraControl => Camera?.CameraControl;
    internal ICameraInfo? CameraInfo => Camera?.CameraInfo;

    private TaskCompletionSource? m_startedTcs;
    
    internal PreviewView? PreviewView { get; private set; }
    private CameraPreview? m_cameraPreview;
    public new Context? Context { get; }
    public new FragmentManager? FragmentManager { get; }

    protected CameraFragment()
    {
        Context = DUI.GetCurrentMauiContext?.Context;
        FragmentManager = Context?.GetFragmentManager();
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
        
        CameraProvider = (ProcessCameraProvider?) await ProcessCameraProvider.GetInstance(Context).GetAsync();
        if (CameraProvider == null) return;
        
        //Use back camera
        var cameraSelector = new CameraSelector.Builder().RequireLensFacing((int)(LensFacing.Back)).Build();
        
        //Create preview use case and attach it to our MAUI view. 
        var previewUseCase = new AndroidX.Camera.Core.Preview.Builder().Build();
        previewUseCase.SetSurfaceProvider(PreviewView.SurfaceProvider);
        
        //Bind the camera
        Camera = CameraProvider.BindToLifecycle(this, cameraSelector, previewUseCase, useCase);
        // Camera.CameraControl.Set
        //Do configurations before starting the activity: https://developer.android.com/media/camera/camerax/configuration
        
        // CameraControl.SetLinearZoom()
        // cameraProvider.Bind
        // CameraController = new LifecycleCameraController(Context);
        // var barcodeScanning = new ImageAnalysis.Builder().Build();
        // var imageCapture = new ImageCapture.Builder().Build();
        // CameraController.SetEnabledUseCases();
        // CameraController.BindToLifecycle(this);
        

        // PreviewView.Controller = CameraController;
        
        
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

    public override void OnStart()
    {
        if (m_cameraPreview?.Handler is CameraPreviewHandler previewHandler)
        {
            previewHandler.OnCameraStarted(CameraControl!);
        }
        
        OnStarted();
        m_startedTcs.TrySetResult();
        base.OnStart();
    }

    /// <summary>
    /// The fragment has started, do your thing!
    /// </summary>
    public abstract void OnStarted();
    
    internal async Task TryStop()
    {
        if(!IsFragmentStarted()) return;
        try
        {
            FragmentManager?.BeginTransaction().Remove(this).CommitAllowingStateLoss();
            CameraProvider?.Unbind();
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
}