using Android.Gms.Tasks;
using Android.Runtime;
using AndroidX.Camera.Core;
using AndroidX.Core.Content;
using AndroidX.Lifecycle;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning.Android;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared.Android;
using Xamarin.Google.MLKit.Vision.BarCode;
using Xamarin.Google.MLKit.Vision.Common;
using Object = Java.Lang.Object;
using Task = System.Threading.Tasks.Task;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

//Based on : https://github.com/android/camera-samples/blob/main/CameraXBasic/app/src/main/java/com/android/example/cameraxbasic/fragments/CameraFragment.kt
//and: https://developers.google.com/ml-kit/vision/barcode-scanning
public partial class BarcodeScanner : CameraFragment, IOnSuccessListener, IObserver
{
    private IBarcodeScanner m_barcodeScanner;
    private IImageProxy? m_imageProxy;

    internal partial Task PlatformStart()
    {
        return m_cameraPreview != null ? base.TryStart(m_cameraPreview, CameraUseCase.BarcodeScanning) : Task.CompletedTask;
    }

    internal partial Task PlatformStop() => base.TryStop();
    

    public override void OnStarted() => SetupBarCodeScanning();
    


    public override void OnStop()
    {
        base.OnStop();
        TryStop();
    }

 

    private void SetupBarCodeScanning()
    {
        //From docs: https://developers.google.com/ml-kit/vision/barcode-scanning/android#kotlin
        var barcodeScannerBuilder = new BarcodeScannerOptions.Builder();
        //Set formats
        barcodeScannerBuilder
            .SetBarcodeFormats(Xamarin.Google.MLKit.Vision.Barcode.Common.Barcode.FormatAllFormats);
        
        m_barcodeScanner = Xamarin.Google.MLKit.Vision.BarCode.BarcodeScanning.GetClient(barcodeScannerBuilder.Build());
        CameraController?.SetImageAnalysisAnalyzer(ContextCompat.GetMainExecutor(Context),
            ImageAnalyzer.Create(AnalyzeImage));

        //TODO: MOVE TO BASE IF UX SHOULD BE THE SAME FOR ALL USE CASES?
        if (m_cameraPreview?.Handler is CameraPreviewHandler previewHandler)
        {
            if (CameraController != null)
            {
                previewHandler.AddZoomSlider(CameraController);    
            }
        }
        
        CameraController?.ZoomState.Observe(this, this); //Observe zoom changes using LiveData pattern
    }

 

    private void AnalyzeImage(IImageProxy imageProxy)
    {
        m_imageProxy = imageProxy;
        m_barcodeScanner.Process(InputImage.FromMediaImage(imageProxy.Image, imageProxy.ImageInfo.RotationDegrees))
            .AddOnSuccessListener(ContextCompat.GetMainExecutor(Context), this);
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

    // private void DrawBarcodeRectangle(Xamarin.Google.MLKit.Vision.Barcode.Common.Barcode mlBarcode)
    // {
    //     m_previewView.Overlay?.Clear();
    //     m_previewView.Overlay?.Add(new QrCodeDrawable(mlBarcode));
    // }

    public void OnChanged(Object? value)
    {
        if (double.TryParse(value.GetPropertyValue("LinearZoom"), out var linearZoom))
        {
            if (m_cameraPreview?.Handler is CameraPreviewHandler previewHandler)
            {
                previewHandler.OnZoomChanged(linearZoom);
            }  
        }
    }
}