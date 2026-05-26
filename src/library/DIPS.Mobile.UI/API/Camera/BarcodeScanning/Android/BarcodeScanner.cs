using Android.Runtime;
using Android.Views;
using AndroidX.Camera.Core;
using AndroidX.Camera.Core.ResolutionSelector;
using AndroidX.Core.Content;
using AndroidX.Lifecycle;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning.Android;
using DIPS.Mobile.UI.API.Camera.Shared.Android;
using DIPS.Mobile.UI.Observable.Android;
using Java.Util.Concurrent;
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
    private IExecutorService? m_imageAnalysisExecutor;

    internal partial Task PlatformStart(BarcodeScannerStartOptions startOptions, CameraFailed cameraFailedDelegate)
    {
        if (Context == null) return Task.CompletedTask;
        
        var resolutionSelector = new ResolutionSelector.Builder()
            .SetResolutionStrategy(ResolutionStrategy.HighestAvailableStrategy)
            .SetAspectRatioStrategy(AspectRatioStrategy.Ratio43FallbackAutoStrategy)
            .Build();
        
        m_imageAnalysisExecutor = Executors.NewSingleThreadExecutor();

        m_imageAnalysisUseCase = new ImageAnalysis.Builder()
            .SetBackpressureStrategy(ImageAnalysis.StrategyKeepOnlyLatest)
            .Build();
        m_imageAnalysisUseCase.SetAnalyzer(m_imageAnalysisExecutor,
            ImageAnalyzer.Create(AnalyzeImage));
        return m_cameraPreview != null ? base.SetupCameraAndTryStartUseCase(m_cameraPreview, m_imageAnalysisUseCase, resolutionSelector, cameraFailedDelegate) : Task.CompletedTask;
    }

    internal partial async Task PlatformStop()
    {
        m_barcodeScanner?.Close();
        m_barcodeScanner = null;
        await base.TryStop();
        m_imageAnalysisExecutor?.Shutdown();
        m_imageAnalysisExecutor = null;
    }

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
        var barcodeScanner = m_barcodeScanner;
        var imageAnalysisExecutor = m_imageAnalysisExecutor;
        var mediaImage = imageProxy.Image;
        var scanRunId = CurrentScanRunId;

        if(Context == null || barcodeScanner is null || imageAnalysisExecutor is null || mediaImage is null)
        {
            imageProxy.Close();
            return;
        }

        try
        {
            barcodeScanner.Process(InputImage.FromMediaImage(mediaImage, imageProxy.ImageInfo.RotationDegrees))
                .AddOnFailureListener(imageAnalysisExecutor, new OnFailureListener(e =>
                {
                    imageProxy.Close();
                    if (scanRunId == CurrentScanRunId)
                    {
                        MainThread.BeginInvokeOnMainThread(() => OnCameraFailed<BarcodeScanner>(new CameraException("DidTryAnalyzeImage", e)));
                    }
                }))
                .AddOnSuccessListener(imageAnalysisExecutor, new OnSuccessListener(o =>
                {
                    try
                    {
                        OnSuccess(o, imageProxy, scanRunId);
                    }
                    finally
                    {
                        imageProxy.Close();
                    }
                }));
        }
        catch (Exception exception)
        {
            imageProxy.Close();
            if (scanRunId == CurrentScanRunId)
            {
                MainThread.BeginInvokeOnMainThread(() => OnCameraFailed<BarcodeScanner>(new CameraException("DidTryAnalyzeImage", exception)));
            }
        }
    }

    private void OnSuccess(Object result, IImageProxy imageProxy, int scanRunId)
    {
        if (scanRunId != CurrentScanRunId)
            return;

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

                        InvokeBarcodeFound(new Barcode(mlBarcode.RawValue, mlBarcode.Format.ToString()), overlayBounds, scanRunId);
                    }
                }
            }
        }
    }

    private bool IsBarcodeInsideScanRectangle(Xamarin.Google.MLKit.Vision.Barcode.Common.Barcode mlBarcode, int effectiveWidth, int effectiveHeight)
    {
        if (m_currentStartOptions.Strategy is not ScanRectangleBarcodeScanStrategy scanRectangleStrategy)
            return true;

        var boundingBox = mlBarcode.BoundingBox;
        if (boundingBox == null || effectiveWidth == 0 || effectiveHeight == 0)
            return true;

        var normalizedCenterX = (boundingBox.Left + boundingBox.Right) / 2.0f / effectiveWidth;
        var normalizedCenterY = (boundingBox.Top + boundingBox.Bottom) / 2.0f / effectiveHeight;

        var rectLeft = (1.0f - scanRectangleStrategy.WidthFraction) / 2.0f;
        var rectTop = (1.0f - scanRectangleStrategy.HeightFraction) / 2.0f;
        var rectRight = rectLeft + scanRectangleStrategy.WidthFraction;
        var rectBottom = rectTop + scanRectangleStrategy.HeightFraction;

        return normalizedCenterX >= rectLeft && normalizedCenterX <= rectRight &&
               normalizedCenterY >= rectTop && normalizedCenterY <= rectBottom;
    }

    public void OnChanged(Object? value)
    {
    }
}