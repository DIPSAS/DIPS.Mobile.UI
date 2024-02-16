using System.Diagnostics;
using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.Runtime;
using AndroidX.Camera.Core;
using AndroidX.Camera.View;
using AndroidX.Core.Content;
using DIPS.Mobile.UI.API.Camera.Scanning.Android;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using Xamarin.Google.MLKit.Vision.BarCode;
using Xamarin.Google.MLKit.Vision.Barcode.Common;
using Xamarin.Google.MLKit.Vision.Common;
using Fragment = AndroidX.Fragment.App.Fragment;
using FragmentManager = AndroidX.Fragment.App.FragmentManager;

namespace DIPS.Mobile.UI.API.Camera.Scanning;

//Based on : https://github.com/android/camera-samples/blob/main/CameraXBasic/app/src/main/java/com/android/example/cameraxbasic/fragments/CameraFragment.kt
//and: https://developers.google.com/ml-kit/vision/barcode-scanning
public partial class Scanner : Fragment, IOnSuccessListener
{
    private PreviewView m_previewView;
    private TaskCompletionSource<string> m_tcs;
    private readonly Context m_context;
    private LifecycleCameraController m_cameraController;
    private readonly FragmentManager? m_fragmentManager;
    private IBarcodeScanner m_barcodeScanner;
    private IImageProxy? m_imageProxy;

    public Scanner()
    {
        m_context = DUI.GetCurrentMauiContext?.Context;
        m_fragmentManager = m_context.GetFragmentManager();
    }

    public partial async Task<string> Start(Preview preview)
    {
        m_tcs = new TaskCompletionSource<string>();

        var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
        {
            if (await Permissions.RequestAsync<Permissions.Camera>() != PermissionStatus.Granted)
            {
                return string.Empty;
            }
        }

        if (preview.Handler is PreviewHandler previewHandler)
        {
            m_previewView = previewHandler.PlatformView;
        }

        m_cameraController = new LifecycleCameraController(m_context);
        m_cameraController.BindToLifecycle(this);
        m_previewView.Controller = m_cameraController;
        SetupBarCodeScanning();

        m_fragmentManager.BeginTransaction().Add(this, nameof(Scanner)).Commit();
        return await m_tcs.Task;
    }

    private void SetupBarCodeScanning()
    {
        // create BarcodeScanner object
        var options = new BarcodeScannerOptions.Builder()
            .SetBarcodeFormats(Barcode.FormatAllFormats)
            .Build();
        m_barcodeScanner = BarcodeScanning.GetClient(options);

        m_cameraController.SetImageAnalysisAnalyzer(ContextCompat.GetMainExecutor(m_context),
            ImageAnalyzer.Create(AnalyzeImage));
    }

    private void AnalyzeImage(IImageProxy imageProxy)
    {
        m_imageProxy = imageProxy;
        m_barcodeScanner.Process(InputImage.FromMediaImage(imageProxy.Image, imageProxy.ImageInfo.RotationDegrees))
            .AddOnSuccessListener(ContextCompat.GetMainExecutor(m_context), this);
    }

    public partial void Stop()
    {
        m_fragmentManager.BeginTransaction().Remove(this).Commit();
    }

    public void OnSuccess(Java.Lang.Object result)
    {
        if(result is JavaList list)
        {
            foreach (var obj in list)
            {
                if (obj is Barcode barcode)
                {
                    m_tcs.TrySetResult(barcode.RawValue);
                    Console.WriteLine("Barcode displayvalue:" + barcode.DisplayValue);
                    Console.WriteLine("Barcode raw value:" + barcode.RawValue);
                    Console.WriteLine("Barcode BoundingBox:" + barcode.BoundingBox);
                    Console.WriteLine("Barcode format:" + barcode.Format);
                }
            }
        }
        m_imageProxy?.Close();
    }
}