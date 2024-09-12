using Android.Gms.Extensions;
using Android.Gms.Tasks;
using Android.Hardware.Display;
using Android.Runtime;
using Android.Views;
using AndroidX.Camera.Core;
using AndroidX.Core.Content;
using AndroidX.Lifecycle;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning.Android;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Preview.Android.Slider;
using DIPS.Mobile.UI.API.Camera.Shared.Android;
using DIPS.Mobile.UI.Observable.Android;
using Xamarin.Google.MLKit.Vision.BarCode;
using Xamarin.Google.MLKit.Vision.Common;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Object = Java.Lang.Object;
using Task = System.Threading.Tasks.Task;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

//Based on : https://github.com/android/camera-samples/blob/main/CameraXBasic/app/src/main/java/com/android/example/cameraxbasic/fragments/CameraFragment.kt
//and: https://developers.google.com/ml-kit/vision/barcode-scanning
public partial class BarcodeScanner : CameraFragment, IObserver
{
    private IBarcodeScanner? m_barcodeScanner;
    private ImageAnalysis? m_imageAnalysisUseCase;
    private CameraZoomSlider? m_slider;

    internal partial Task PlatformStart()
    {
        if (Context == null) return Task.CompletedTask;
        m_imageAnalysisUseCase = new ImageAnalysis.Builder()
            .Build();
        m_imageAnalysisUseCase.SetAnalyzer(ContextCompat.GetMainExecutor(Context),
            ImageAnalyzer.Create(AnalyzeImage));
        return m_cameraPreview != null ? base.SetupCameraAndTryStartUseCase(m_cameraPreview, m_imageAnalysisUseCase) : Task.CompletedTask;
    }

    internal partial Task PlatformStop() => base.TryStop();

    public override void OnStarted() => SetupBarCodeScanning();
    internal override void OrientationChanged(SurfaceOrientation surfaceOrientation)
    {
        
    }
    private void SetupBarCodeScanning()
    {
        //From docs: https://developers.google.com/ml-kit/vision/barcode-scanning/android#kotlin
        var barcodeScannerBuilder = new BarcodeScannerOptions.Builder();
        //Set formats
        barcodeScannerBuilder
            .SetBarcodeFormats(Xamarin.Google.MLKit.Vision.Barcode.Common.Barcode.FormatAllFormats);
        
        m_barcodeScanner = Xamarin.Google.MLKit.Vision.BarCode.BarcodeScanning.GetClient(barcodeScannerBuilder.Build());
        
        CameraInfo?.ZoomState.Observe(this, this); //Observe zoom changes using LiveData pattern
    }

    private void AnalyzeImage(IImageProxy imageProxy)
    {
        if(Context == null) return;
        //TODO: Simplify with await and make sure to run on main thread
        m_barcodeScanner?.Process(InputImage.FromMediaImage(imageProxy.Image, imageProxy.ImageInfo.RotationDegrees)).AddOnSuccessListener(ContextCompat.GetMainExecutor(Context), new OnSuccessListener((o => OnSuccess(o, imageProxy))));
    }

    private void OnSuccess(Object result, IImageProxy imageProxy)
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
        
        imageProxy.Close();
    }

    // private void DrawBarcodeRectangle(Xamarin.Google.MLKit.Vision.Barcode.Common.Barcode mlBarcode)
    // {
    //     m_previewView.Overlay?.Clear();
    //     m_previewView.Overlay?.Add(new QrCodeDrawable(mlBarcode));
    // }

    public void OnChanged(Object? value)
    {
        if (double.TryParse(value.GetPropertyValue("LinearZoom"), out var linearZoom) && m_slider is not null)
        {
            m_slider.ZoomLevel = linearZoom;
        }
    }
}