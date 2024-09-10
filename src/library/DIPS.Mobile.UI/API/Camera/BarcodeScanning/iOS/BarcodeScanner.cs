using AVFoundation;
using CoreAnimation;
using CoreFoundation;
using CoreGraphics;
using CoreMedia;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Camera.Shared.iOS;
using Foundation;
using Microsoft.Maui.Controls.Shapes;
using UIKit;
using ContentView = Microsoft.Maui.Platform.ContentView;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanner : CameraSession
{
    //The rectangle that people see in the preview which we will focus on scanning bar codes in
    private AVCaptureMetadataOutput? m_captureMetadataOutput;
    private double m_rectOfInterestWidth;

    private DispatchQueue m_metadataObjectsQueue = new(label: "metadata objects queue", attributes: new(), target: null);

    internal partial Task PlatformStop()
    {
        StopCameraSession();
        return Task.CompletedTask;
    }

    internal partial Task PlatformStart()
    {
        if (m_cameraPreview == null) return Task.CompletedTask;
        m_captureMetadataOutput = new AVCaptureMetadataOutput();
        return ConfigureAndStart(m_cameraPreview, AVCaptureSession.PresetHigh, m_captureMetadataOutput);
    }

    public override void ConfigureSession()
    {
        if (m_captureMetadataOutput == null || CaptureDevice == null || PreviewLayer == null || PreviewUIView == null) return;
        
         m_captureMetadataOutput.SetDelegate(new CaptureDelegate(s =>
            {
                if (!string.IsNullOrEmpty(s.StringValue))
                {
                    InvokeBarcodeFound(new Barcode(s.StringValue, s.Type.ToString()));
                }
            }), m_metadataObjectsQueue);
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
            
            
            var formatDimensions = ((CMVideoFormatDescription)CaptureDevice.ActiveFormat.FormatDescription).Dimensions;
            m_rectOfInterestWidth = formatDimensions.Height / (double)formatDimensions.Width;
            var rectOfInterestHeight = 1.0;
            var xCoordinate = (1.0 - m_rectOfInterestWidth) / 2.0;
            var yCoordinate = (1.0 - rectOfInterestHeight) / 2.0;
            var initialRectOfInterest = new CGRect(x: xCoordinate, y: yCoordinate, width: m_rectOfInterestWidth,
                height: rectOfInterestHeight);
            m_captureMetadataOutput.RectOfInterest = initialRectOfInterest;

            var rectOfInterestToLayerCoordinates = PreviewLayer.MapToLayerCoordinates(initialRectOfInterest);
            var layer = new CAShapeLayer();
            layer.FillRule = new NSString(FillRule.EvenOdd.ToString());
            layer.FillColor = UIColor.Black.CGColor;
            layer.Opacity = 0.6f;
            
            layer.Frame = rectOfInterestToLayerCoordinates;
            layer.BorderColor = UIColor.White.CGColor;
            layer.BorderWidth = 2;
            PreviewUIView.Layer.AddSublayer(layer);
            
            SetRecommendedZoomFactor();
            
    }
    
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
    private readonly Action<AVMetadataMachineReadableCodeObject> m_onSuccess;


    public CaptureDelegate(Action<AVMetadataMachineReadableCodeObject> onSuccess)
    {
        m_onSuccess = onSuccess;
    }

    public override void DidOutputMetadataObjects(AVCaptureMetadataOutput captureOutput,
        AVMetadataObject[] metadataObjects,
        AVCaptureConnection connection)
    {
        // Check if metadataObjects array is not empty
        if (metadataObjects.Length == 0)
        {
            Console.WriteLine(@"No metadata objects detected");
        }


        // Iterate through the metadata objects
        foreach (var metadataObject in metadataObjects)
        {
            if (metadataObject is AVMetadataMachineReadableCodeObject readableObject)
            {
                var stringValue = readableObject.StringValue;
                if (stringValue != null)
                {
                    MainThread.BeginInvokeOnMainThread(() =>m_onSuccess.Invoke(readableObject));
                }
            }
        }
    }
}