using AVFoundation;
using CoreFoundation;
using DIPS.Mobile.UI.API.Camera.Shared.iOS;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanner : CameraSession
{
    //The rectangle that people see in the preview which we will focus on scanning bar codes in
    private AVCaptureMetadataOutput? m_captureMetadataOutput;
    private double m_rectOfInterestWidth;

    private readonly DispatchQueue m_metadataObjectsQueue = new(label: "metadata objects queue", attributes: new(), target: null);
    
#nullable disable
    private CaptureDelegate m_captureDelegate;
#nullable enable

    internal partial Task PlatformStop()
    {
        m_captureDelegate.Dispose();
        m_captureDelegate = null;
        
        return StopCameraSession();
    }

    internal partial Task PlatformStart(BarcodeScannerStartOptions startOptions, CameraFailed cameraFailedDelegate)
    {
        m_captureMetadataOutput = new AVCaptureMetadataOutput();
        m_captureDelegate = new CaptureDelegate(readableCodeObject =>
        {
            if (!string.IsNullOrEmpty(readableCodeObject.StringValue))
            {
                var scanRunId = CurrentScanRunId;
                var overlayBounds = BarcodeScanOverlayBoundsMapper.TryGetBarcodeBoundsInOverlay(
                    readableCodeObject,
                    PreviewLayer,
                    m_scanRectangleOverlay);

                InvokeBarcodeFound(new Barcode(readableCodeObject.StringValue, readableCodeObject.Type.ToString()), overlayBounds, scanRunId);
            }
        });
        return ConfigureAndStart(m_cameraPreview, null, m_captureMetadataOutput, cameraFailedDelegate);
    }

    public override void ConfigureSession()
    {
        if (m_captureMetadataOutput == null || CaptureDevice == null || PreviewLayer == null || PreviewView == null) return;

        m_cameraPreview?.SetToolbarHeights((float)PreviewView!.Frame.Height);

        m_captureMetadataOutput.SetDelegate(m_captureDelegate, m_metadataObjectsQueue);
        //Add bar code scanning metadata
        //Bar codes: https://developer.apple.com/documentation/avfoundation/avmetadataobjecttype#3801359
        //2D codes: https://developer.apple.com/documentation/avfoundation/avmetadataobjecttype#3801360
        m_captureMetadataOutput.MetadataObjectTypes = AVMetadataObjectType.CodabarCode |
                                                      AVMetadataObjectType.Code39Code |
                                                      AVMetadataObjectType.Code39Mod43Code |
                                                      AVMetadataObjectType.Code93Code |
                                                      AVMetadataObjectType.Code128Code |
                                                      AVMetadataObjectType.EAN8Code |
                                                      AVMetadataObjectType.EAN13Code |
                                                      AVMetadataObjectType.GS1DataBarCode |
                                                      AVMetadataObjectType.GS1DataBarExpandedCode |
                                                      AVMetadataObjectType.GS1DataBarLimitedCode |
                                                      AVMetadataObjectType.Interleaved2of5Code |
                                                      AVMetadataObjectType.ITF14Code |
                                                      AVMetadataObjectType.UPCECode |
                                                      AVMetadataObjectType.AztecCode |
                                                      AVMetadataObjectType.DataMatrixCode |
                                                      AVMetadataObjectType.MicroQRCode
                                                      | AVMetadataObjectType.PDF417Code
                                                      | AVMetadataObjectType.QRCode;
        
        if (m_currentStartOptions.Strategy is ScanRectangleBarcodeScanStrategy scanRectangleStrategy)
        {
            PreviewView.TryAddOrUpdateRectOfInterest(scanRectangleStrategy.WidthFraction, scanRectangleStrategy.HeightFraction);
        }
        else
        {
            PreviewView.TryAddOrUpdateRectOfInterest();
        }
        SetRecommendedZoomFactor();
    }

    // Prefer virtual back cameras so iOS starts at normal 1x wide framing but can switch to macro-capable lenses when close.
    public override AVCaptureDevice? SelectCaptureDevice() => SelectPreferredBackCaptureDevice();

    //Taken from: https://developer.apple.com/wwdc21/10047?time=117
    //and sample code from Apple: https://developer.apple.com/documentation/avfoundation/capture_setup/avcambarcode_detecting_barcodes_and_faces
    private void SetRecommendedZoomFactor()
    {
        if(CaptureDevice == null) return;
        var captureDevice = CaptureDevice;
        /*
            Optimize the user experience for scanning QR codes down to sizes of 20mm x 20mm.
            When scanning a QR code of that size, the user may need to get closer than the camera's minimum focus distance to fill the rect of interest.
            To have the QR code both fill the rect and still be in focus, we may need to apply some zoom.
        */
        var deviceMinimumFocusDistance = captureDevice.MinimumFocusDistance;

        var deviceFieldOfView = captureDevice.ActiveFormat.VideoFieldOfView;

        /*
            Given the camera horizontal field of view, we can compute the distance (mm) to make a code
            of minimumCodeSize (mm) fill the previewFillPercentage.
         */
        
        var minimumSubjectDistanceForCode = MinimumSubjectDistanceForCode(deviceFieldOfView, 50, (float)m_rectOfInterestWidth);
        if (minimumSubjectDistanceForCode < deviceMinimumFocusDistance)
        {
            var zoomFactor = deviceMinimumFocusDistance / minimumSubjectDistanceForCode;
            captureDevice.LockForConfiguration(out var error);
            if (error != null)
            {
                throw new Exception(error.ToString());
            }

            captureDevice.VideoZoomFactor = (new nfloat(zoomFactor));

            captureDevice.UnlockForConfiguration();
        }
        
        if (m_cameraPreview is { CameraZoomView: not null })
        {
            m_cameraPreview.CameraZoomView.SetZoomRatio(ToDisplayZoomRatio((float)captureDevice.VideoZoomFactor));
        }
    }

    private double MinimumSubjectDistanceForCode(float fieldOfView, float minimumCodeSize, float previewFillPercentage)
    {
        /*
            Given the camera horizontal field of view, we can compute the distance (mm) to make a code
            of minimumCodeSize (mm) fill the previewFillPercentage.
         */
        var radians = DegreesToRadians(fieldOfView / 2);
        var filledCodeSize = minimumCodeSize / previewFillPercentage;
        return filledCodeSize / Math.Tan(radians);
    }

    private double DegreesToRadians(float degrees)
    {
        return degrees * Math.PI / 180;
    }
}

public class CaptureDelegate : AVCaptureMetadataOutputObjectsDelegate
{
    private Action<AVMetadataMachineReadableCodeObject>? m_onSuccess;

    public CaptureDelegate(Action<AVMetadataMachineReadableCodeObject> onSuccess)
    {
        m_onSuccess = onSuccess;
    }
    
    public override void DidOutputMetadataObjects(AVCaptureMetadataOutput captureOutput,
        AVMetadataObject[] metadataObjects,
        AVCaptureConnection connection)
    {
        foreach (var metadataObject in metadataObjects)
        {
            if (metadataObject is AVMetadataMachineReadableCodeObject readableObject)
            {
                var stringValue = readableObject.StringValue;
                if (stringValue != null)
                {
                    MainThread.BeginInvokeOnMainThread(() => m_onSuccess?.Invoke(readableObject));
                }
            }
        }
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        m_onSuccess = null!;
    }
}