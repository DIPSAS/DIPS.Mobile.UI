using Android.Runtime;
using Android.Views;
using AndroidX.Camera.Core;
using AndroidX.Camera.Core.ResolutionSelector;
using AndroidX.Core.Content;
using AndroidX.Lifecycle;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning.Android;
using DIPS.Mobile.UI.API.Camera.Shared.Android;
using DIPS.Mobile.UI.Observable.Android;
using Xamarin.Google.MLKit.Vision.BarCode;
using Xamarin.Google.MLKit.Vision.Common;
using Object = Java.Lang.Object;
using Task = System.Threading.Tasks.Task;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

//Based on : https://github.com/android/camera-samples/blob/main/CameraXBasic/app/src/main/java/com/android/example/cameraxbasic/fragments/CameraFragment.kt
//and: https://developers.google.com/ml-kit/vision/barcode-scanning
public partial class BarcodeScanner : CameraFragment, IObserver
{
    private IBarcodeScanner? m_barcodeScanner;
    private ImageAnalysis? m_imageAnalysisUseCase;
    private BarcodeScanningSettings? m_barcodeScanningSettings;

    internal partial Task PlatformStart(BarcodeScanningSettings barcodeScanningSettings, CameraFailed cameraFailedDelegate)
    {
        if (Context == null) return Task.CompletedTask;
        
        m_barcodeScanningSettings = barcodeScanningSettings;
        
        var resolutionSelector = new ResolutionSelector.Builder()
            .SetResolutionStrategy(ResolutionStrategy.HighestAvailableStrategy)
            .SetAspectRatioStrategy(AspectRatioStrategy.Ratio43FallbackAutoStrategy)
            .Build();
        
        m_imageAnalysisUseCase = new ImageAnalysis.Builder()
            .Build();
        m_imageAnalysisUseCase.SetAnalyzer(ContextCompat.GetMainExecutor(Context),
            ImageAnalyzer.Create(AnalyzeImage));
        return m_cameraPreview != null ? base.SetupCameraAndTryStartUseCase(m_cameraPreview, m_imageAnalysisUseCase, resolutionSelector, cameraFailedDelegate) : Task.CompletedTask;
    }

    internal partial Task PlatformStop() => base.TryStop();

    public override void OnStarted()
    {
        m_cameraPreview?.SetToolbarHeights((float)m_cameraPreview?.PreviewView.Height!);
        SetupBarCodeScanning();
    }

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
        m_barcodeScanner?.Process(InputImage.FromMediaImage(imageProxy.Image, imageProxy.ImageInfo.RotationDegrees))
            .AddOnFailureListener(ContextCompat.GetMainExecutor(Context), new OnFailureListener(e => OnCameraFailed<BarcodeScanner>(new CameraException("DidTryAnalyzeImage", e))))
            .AddOnSuccessListener(ContextCompat.GetMainExecutor(Context), new OnSuccessListener((o => OnSuccess(o, imageProxy))));
    }

    private void OnSuccess(Object result, IImageProxy imageProxy)
    {
        if (result is JavaList list)
        {
            foreach (var obj in list)
            {
                if (obj is Xamarin.Google.MLKit.Vision.Barcode.Common.Barcode mlBarcode)
                {
                    // ML Kit returns bounding boxes in the rotated coordinate space,
                    // but imageProxy.Width/Height are raw sensor dimensions.
                    // Swap when rotation is 90° or 270° so coordinates match the preview orientation.
                    var rotationDegrees = imageProxy.ImageInfo.RotationDegrees;
                    var isRotated = rotationDegrees is 90 or 270;
                    var effectiveWidth = isRotated ? imageProxy.Height : imageProxy.Width;
                    var effectiveHeight = isRotated ? imageProxy.Width : imageProxy.Height;

                    if (IsBarcodeInsideScanRectangle(mlBarcode, effectiveWidth, effectiveHeight))
                    {
                        RectF? overlayBounds = null;
                        var boundingBox = mlBarcode.BoundingBox;
                        if (boundingBox != null && effectiveWidth > 0 && effectiveHeight > 0 && m_scanRectangleOverlay is not null)
                        {
                            var normalized = new RectF(
                                boundingBox.Left / (float)effectiveWidth,
                                boundingBox.Top / (float)effectiveHeight,
                                boundingBox.Width() / (float)effectiveWidth,
                                boundingBox.Height() / (float)effectiveHeight);
                            overlayBounds = m_scanRectangleOverlay.NormalizedBoundsToOverlay(
                                normalized.X, normalized.Y, normalized.Width, normalized.Height);
                        }

                        InvokeBarcodeFound(new Barcode(mlBarcode.RawValue, mlBarcode.Format.ToString()), overlayBounds);
                    }
                }
            }
        }
        
        imageProxy.Close();
    }

    private bool IsBarcodeInsideScanRectangle(Xamarin.Google.MLKit.Vision.Barcode.Common.Barcode mlBarcode, int effectiveWidth, int effectiveHeight)
    {
        if (m_barcodeScanningSettings is not { ShowScanRectangle: true })
            return true;
        
        var boundingBox = mlBarcode.BoundingBox;
        if (boundingBox == null)
            return true;

        if (effectiveWidth == 0 || effectiveHeight == 0)
            return true;

        // Normalize barcode center to 0-1 range
        var barcodeCenterX = (boundingBox.Left + boundingBox.Right) / 2.0f / effectiveWidth;
        var barcodeCenterY = (boundingBox.Top + boundingBox.Bottom) / 2.0f / effectiveHeight;

        // Compute scan rectangle in normalized coordinates
        var rectWidthFraction = m_barcodeScanningSettings.ScanRectangleWidthFraction;
        var rectHeightFraction = m_barcodeScanningSettings.ScanRectangleHeightFraction;
        var rectLeft = (1.0f - rectWidthFraction) / 2.0f;
        var rectTop = (1.0f - rectHeightFraction) / 2.0f;
        var rectRight = rectLeft + rectWidthFraction;
        var rectBottom = rectTop + rectHeightFraction;

        return barcodeCenterX >= rectLeft && barcodeCenterX <= rectRight &&
               barcodeCenterY >= rectTop && barcodeCenterY <= rectBottom;
    }

    public void OnChanged(Object? value)
    {
    }
}