using Android.Content;
using AndroidX.Camera.View;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Library;
using Java.Lang;
using Microsoft.Maui.Platform;
using Fragment = AndroidX.Fragment.App.Fragment;
using FragmentManager = AndroidX.Fragment.App.FragmentManager;

namespace DIPS.Mobile.UI.API.Camera.Shared.Android;

public abstract class CameraFragment : Fragment
{
    private const string FragmentTag = nameof(CameraFragment);

    internal PreviewView PreviewView { get; private set; }
    internal LifecycleCameraController? CameraController { get; private set; }


    private TaskCompletionSource m_startedTcs;
    private CameraPreview m_cameraPreview;
    public new Context? Context { get; }
    public new FragmentManager? FragmentManager { get; }

    public CameraFragment()
    {
        Context = DUI.GetCurrentMauiContext?.Context;
        FragmentManager = Context?.GetFragmentManager();
    }
    
    private bool IsFragmentStarted()
    {
        return FragmentManager?.FindFragmentByTag(FragmentTag) != null;
    }
    
    internal async Task TryStart(CameraPreview cameraPreview, CameraUseCase cameraUseCase)
    {
        if (IsFragmentStarted())
        {
            //Already started
            return;
        }

        m_startedTcs = new TaskCompletionSource();
        m_cameraPreview = cameraPreview;
        
        if (cameraPreview.Handler is CameraPreviewHandler previewHandler)
        {
            PreviewView = previewHandler.PreviewView;
        }

        CameraController = new LifecycleCameraController(Context);
        CameraController.SetEnabledUseCases((int)cameraUseCase);
        CameraController.BindToLifecycle(this);

        PreviewView.Controller = CameraController;
        
        
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
                await TryStart(cameraPreview, cameraUseCase);
            }

            throw;
        }
        
        await m_startedTcs.Task;
    }

    public override void OnStart()
    {
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
            CameraController?.Unbind();
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

internal enum CameraUseCase
{
    BarcodeScanning = CameraController.ImageAnalysis,
    ImageCapture = CameraController.ImageCapture,
}