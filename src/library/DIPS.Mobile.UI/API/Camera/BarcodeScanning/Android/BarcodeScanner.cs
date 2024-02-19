using System.Diagnostics;
using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.Runtime;
using AndroidX.Camera.Core;
using AndroidX.Camera.View;
using AndroidX.Core.Content;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning.Android;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.API.Vibration;
using Microsoft.Maui.Platform;
using Xamarin.Google.MLKit.Vision.BarCode;
using Xamarin.Google.MLKit.Vision.Barcode.Common;
using Xamarin.Google.MLKit.Vision.Common;
using Fragment = AndroidX.Fragment.App.Fragment;
using FragmentManager = AndroidX.Fragment.App.FragmentManager;
using Task = System.Threading.Tasks.Task;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

//Based on : https://github.com/android/camera-samples/blob/main/CameraXBasic/app/src/main/java/com/android/example/cameraxbasic/fragments/CameraFragment.kt
//and: https://developers.google.com/ml-kit/vision/barcode-scanning
public partial class BarcodeScanner : Fragment, IOnSuccessListener
{
    private PreviewView m_previewView;
    private readonly Context m_context;
    private LifecycleCameraController m_cameraController;
    private readonly FragmentManager? m_fragmentManager;
    private IBarcodeScanner m_barcodeScanner;
    private IImageProxy? m_imageProxy;
    private string m_fragmentTag = nameof(BarcodeScanner);

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
                return true;
            }
        }
        return false;
    }
    internal partial Task PlatformStart()
    {
        if (m_fragmentManager?.FindFragmentByTag(m_fragmentTag) != null)
        {
            //Already started
            return Task.CompletedTask;
        }


        if (m_preview?.Handler is PreviewHandler previewHandler)
        {
            m_previewView = previewHandler.PlatformView;
        }

        m_cameraController = new LifecycleCameraController(m_context);
        m_cameraController.BindToLifecycle(this);
        m_previewView.Controller = m_cameraController;
        SetupBarCodeScanning();

        m_fragmentManager?.BeginTransaction().Add(this, m_fragmentTag).CommitNow();
        return Task.CompletedTask;
    }

    private void SetupBarCodeScanning()
    {
        // create BarcodeScanner object
        var options = new BarcodeScannerOptions.Builder()
            .SetBarcodeFormats(Xamarin.Google.MLKit.Vision.Barcode.Common.Barcode.FormatAllFormats)
            .Build();
        m_barcodeScanner = Xamarin.Google.MLKit.Vision.BarCode.BarcodeScanning.GetClient(options);
        
        m_cameraController.SetImageAnalysisAnalyzer(ContextCompat.GetMainExecutor(m_context),
            ImageAnalyzer.Create(AnalyzeImage));
    }

    private void AnalyzeImage(IImageProxy imageProxy)
    {
        m_imageProxy = imageProxy;
        m_barcodeScanner.Process(InputImage.FromMediaImage(imageProxy.Image, imageProxy.ImageInfo.RotationDegrees))
            .AddOnSuccessListener(ContextCompat.GetMainExecutor(m_context), this);
    }

    internal partial void PlatformStop()
    {
        m_fragmentManager?.BeginTransaction().Remove(this).CommitNow();
    }

    public void OnSuccess(Java.Lang.Object result)
    {
        if (result is JavaList list)
        {
            foreach (var obj in list)
            {
                if (obj is Xamarin.Google.MLKit.Vision.Barcode.Common.Barcode mlBarcode)
                {
                    InvokeBarcodeFound(new Barcode(mlBarcode.RawValue));
                    Console.WriteLine("Barcode displayvalue:" + mlBarcode.DisplayValue);
                    Console.WriteLine("Barcode raw value:" + mlBarcode.RawValue);
                    Console.WriteLine("Barcode BoundingBox:" + mlBarcode.BoundingBox);
                    Console.WriteLine("Barcode format:" + mlBarcode.Format);
                }
            }
        }

        m_imageProxy?.Close();
    }
}