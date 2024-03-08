using Android.Content;
using Android.Gms.Tasks;
using Android.Runtime;
using AndroidX.Camera.Core;
using AndroidX.Camera.View;
using AndroidX.Core.Content;
using AndroidX.Lifecycle;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning.Android;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Library;
using Java.Lang;
using Microsoft.Maui.Platform;
using Xamarin.Google.MLKit.Vision.BarCode;
using Xamarin.Google.MLKit.Vision.Common;
using Fragment = AndroidX.Fragment.App.Fragment;
using FragmentManager = AndroidX.Fragment.App.FragmentManager;
using Object = Java.Lang.Object;
using Task = System.Threading.Tasks.Task;
using TaskCompletionSource = System.Threading.Tasks.TaskCompletionSource;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

//Based on : https://github.com/android/camera-samples/blob/main/CameraXBasic/app/src/main/java/com/android/example/cameraxbasic/fragments/CameraFragment.kt
//and: https://developers.google.com/ml-kit/vision/barcode-scanning
public partial class BarcodeScanner : Fragment, IOnSuccessListener, IObserver
{
    private PreviewView m_previewView;
    private readonly Context m_context;
    private LifecycleCameraController m_cameraController;
    private readonly FragmentManager? m_fragmentManager;
    private IBarcodeScanner m_barcodeScanner;
    private IImageProxy? m_imageProxy;
    private string m_fragmentTag = nameof(BarcodeScanner);
    private TaskCompletionSource m_startedTcs;

    public BarcodeScanner()
    {
        m_context = DUI.GetCurrentMauiContext?.Context;
        m_fragmentManager = m_context.GetFragmentManager();
    }

    internal partial async Task<bool> CanUseCamera()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
        {
            if (await Permissions.RequestAsync<Permissions.Camera>() != PermissionStatus.Granted)
            {
                return false;
            }
        }

        return true;
    }

    internal partial Task PlatformStart()
    {
        if (IsFragmentStarted())
        {
            //Already started
            return Task.CompletedTask;
        }

        m_startedTcs = new TaskCompletionSource();

        if (m_cameraPreview?.Handler is CameraPreviewHandler previewHandler)
        {
            m_previewView = previewHandler.PreviewView;
        }

        m_cameraController = new LifecycleCameraController(m_context);
        m_cameraController.SetEnabledUseCases(CameraController.ImageAnalysis);
        m_cameraController.BindToLifecycle(this);
        
        m_previewView.Controller = m_cameraController;
        
        TryStart();

        return m_startedTcs.Task;
    }

    private bool IsFragmentStarted()
    {
        return m_fragmentManager?.FindFragmentByTag(m_fragmentTag) != null;
    }

    public override void OnStart()
    {
        SetupBarCodeScanning();
        m_startedTcs.TrySetResult();
        
        base.OnStart();
    }

    public override void OnAttach(Context context)
    {
        base.OnAttach(context);
    }


    public override void OnStop()
    {
        base.OnStop();
        TryStop();
    }

    private async void TryStart()
    {
        try
        {
            m_fragmentManager?.BeginTransaction().Add(this, m_fragmentTag).CommitAllowingStateLoss();
        }
        catch (IllegalStateException illegalStateException)
        {
            if (illegalStateException.Message != null &&
                illegalStateException.Message.Contains(
                    "FragmentManager is already executing transactions")) //This might happen if we use CommitNow(), and the fragmentmanager is executing other transactions, like closing a bottom sheet or navigating. We retry after a small amount of time if so
            {
                await Task.Delay(400);
                TryStart();
            }

            throw;
        }
    }

    private void SetupBarCodeScanning()
    {
        //From docs: https://developers.google.com/ml-kit/vision/barcode-scanning/android#kotlin
        var barcodeScannerBuilder = new BarcodeScannerOptions.Builder();
        //Set formats
        barcodeScannerBuilder
            .SetBarcodeFormats(Xamarin.Google.MLKit.Vision.Barcode.Common.Barcode.FormatAllFormats);
        
        m_barcodeScanner = Xamarin.Google.MLKit.Vision.BarCode.BarcodeScanning.GetClient(barcodeScannerBuilder.Build());
        m_cameraController.SetImageAnalysisAnalyzer(ContextCompat.GetMainExecutor(m_context),
            ImageAnalyzer.Create(AnalyzeImage));

        if (m_cameraPreview?.Handler is CameraPreviewHandler previewHandler)
        {
            previewHandler.AddZoomSlider(m_cameraController);
        }
        
        m_cameraController.ZoomState.Observe(this, this); //Observe zoom changes using LiveData pattern
    }

 

    private void AnalyzeImage(IImageProxy imageProxy)
    {
        m_imageProxy = imageProxy;
        m_barcodeScanner.Process(InputImage.FromMediaImage(imageProxy.Image, imageProxy.ImageInfo.RotationDegrees))
            .AddOnSuccessListener(ContextCompat.GetMainExecutor(m_context), this);
    }

    internal partial void PlatformStop() => TryStop();

    private async void TryStop()
    {
        if(!IsFragmentStarted()) return;
        try
        {
            m_fragmentManager?.BeginTransaction().Remove(this).CommitAllowingStateLoss();
            m_cameraController.Unbind();
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
                TryStop();
            }
        }
       
    }

    public void OnSuccess(Java.Lang.Object result)
    {
        if (result is JavaList list)
        {
            foreach (var obj in list)
            {
                if (obj is Xamarin.Google.MLKit.Vision.Barcode.Common.Barcode mlBarcode)
                {
                    //DrawBarcodeRectangle(mlBarcode); //TODO: Fix this, does not work. It draws wrong
                    InvokeBarcodeFound(new Barcode(mlBarcode.RawValue, mlBarcode.Format.ToString()));
                }
            }
        }

        m_imageProxy?.Close();
    }

    private void DrawBarcodeRectangle(Xamarin.Google.MLKit.Vision.Barcode.Common.Barcode mlBarcode)
    {
        m_previewView.Overlay?.Clear();
        m_previewView.Overlay?.Add(new QrCodeDrawable(mlBarcode));
    }

    public void OnChanged(Object? value)
    {
        if (double.TryParse(value.GetPropertyValue("LinearZoom"), out var linearZoom))
        {
            if (m_cameraPreview?.Handler is CameraPreviewHandler previewHandler)
            {
                previewHandler.OnZoomChanged(linearZoom, m_cameraController);
            }  
        }
    }
}