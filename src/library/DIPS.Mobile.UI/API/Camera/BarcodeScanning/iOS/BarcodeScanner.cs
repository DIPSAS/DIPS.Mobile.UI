using AVFoundation;
using CoreAnimation;
using CoreFoundation;
using CoreGraphics;
using CoreMedia;
using DIPS.Mobile.UI.API.Camera.iOS;
using Foundation;
using Microsoft.Maui.Controls.Shapes;
using UIKit;
using ContentView = Microsoft.Maui.Platform.ContentView;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanner
{
    //The session of the capture, there can only be one capture session running in an iOS app.
    private AVCaptureSession? m_captureSession;

    //The the lense to be used for scanning bar codes
    private AVCaptureDevice? m_captureDevice;

    //The rectangle that people see in the preview which we will focus on scanning bar codes in
    private CGRect? m_rectOfInterest;
    private ContentView? m_previewUIView;
    private float m_rectOfInterestFillPercentage;
    private AVCaptureMetadataOutput? m_captureMetadataOutput;
    private AVCaptureDeviceInput? m_videoDeviceInput;

    //https://developer.apple.com/documentation/avfoundation/capture_setup/requesting_authorization_to_capture_and_save_media#2958841
    internal partial async Task<bool> CanUseCamera()
    {
        return await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVAuthorizationMediaType.Video);
    }


    internal partial void PlatformStop()
    {
        if (m_captureSession is {Running: true})
        {
            Task.Run(() =>
            {
                m_captureSession.StopRunning();
                if (m_captureMetadataOutput != null)
                {
                    m_captureSession.RemoveOutput(m_captureMetadataOutput);    
                }

                if (m_videoDeviceInput != null)
                {
                    m_captureSession.RemoveInput(m_videoDeviceInput);
                }
                m_captureSession = null;
            });
        }

        m_captureDevice = null;
        m_rectOfInterest = null;

        if (m_preview?.Handler is not PreviewHandler previewHandler) return;
        previewHandler.RemoveZoomSlider();
        previewHandler.RemovePinchToZoom();
        previewHandler.RemoveTouchToFocus();

        m_previewUIView = null;
    }

    internal async partial Task PlatformStart()
    {
        //This makes sure we display the video feed
        if (m_preview?.Handler is not PreviewHandler previewHandler) return;

        m_previewUIView = previewHandler.PlatformView;

        m_captureSession = new AVCaptureSession();
        
        //Call beginConfiguration() before changing a session’s inputs or outputs, and call commitConfiguration() after making changes.
        m_captureSession.BeginConfiguration();

        var previewLayer =
            await previewHandler.WaitForViewLayoutAndAddSessionToPreview(m_captureSession,
                AVLayerVideoGravity.ResizeAspectFill);
        //Choosing build in wide angle camera, same as the sample app from Apple: AVCamBarCode: https://developer.apple.com/documentation/avfoundation/capture_setup/avcambarcode_detecting_barcodes_and_faces
        m_captureDevice = AVCaptureDevice.GetDefaultDevice(AVCaptureDeviceType.BuiltInWideAngleCamera,
            AVMediaTypes.Video, AVCaptureDevicePosition.Back);
        if (m_captureDevice == null) return;
        
        m_videoDeviceInput = AVCaptureDeviceInput.FromDevice(m_captureDevice);
        if (m_videoDeviceInput == null) throw new Exception($"video device input is null");
        if (m_captureSession.CanAddInput(m_videoDeviceInput))
        {
            m_captureSession.AddInput(m_videoDeviceInput);
        }
        else
        {
            throw new Exception("Unable to use the back camera wide angle camera to detect bar codes");
        }

        //Set quality for best performance
        //Source: https://developers.google.com/ml-kit/vision/barcode-scanning/ios#performance-tips where this is mentioned
        //But we've followed sample code from Apple: https://developer.apple.com/documentation/avfoundation/capture_setup/avcambarcode_detecting_barcodes_and_faces
        m_captureSession.SessionPreset = AVCaptureSession.PresetHigh;

        //Add barcode camera output
        m_captureMetadataOutput = new AVCaptureMetadataOutput();
        if (m_captureSession.CanAddOutput(m_captureMetadataOutput))
        {
            m_captureSession.AddOutput(
                m_captureMetadataOutput); //this has to be set before setting metadata objects, or else it crashes

            m_captureMetadataOutput.SetDelegate(new CaptureDelegate(s =>
            {
                if (!string.IsNullOrEmpty(s.StringValue))
                {
                    InvokeBarcodeFound(new Barcode(s.StringValue, s.Type.ToString()));
                }
            }), DispatchQueue.MainQueue);
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

            
            var x = 0;
            m_rectOfInterestFillPercentage = 0.5f;
            var height = previewLayer.Frame.Height * m_rectOfInterestFillPercentage;
            var y = (previewLayer.Frame.Height / 2) - (height / 2);
            var width = previewLayer.Frame.Width;
            var regionOfInterest = new CGRect(x, y, width, height);
            m_rectOfInterest = m_captureMetadataOutput.RectOfInterest =
                previewLayer.MapToMetadataOutputCoordinates(regionOfInterest);

            var layer = new CAShapeLayer();
            layer.FillRule = new NSString(FillRule.EvenOdd.ToString());
            layer.FillColor = UIColor.Black.CGColor;
            layer.Opacity = 0.6f;
            
            layer.Frame = regionOfInterest;
            layer.BorderColor = UIColor.White.CGColor;
            layer.BorderWidth = 2;
            m_previewUIView.Layer.AddSublayer(layer);

        }
        else
        {
            throw new Exception("Unable to add output");
        }

        //Commit the configuration
        m_captureSession.CommitConfiguration();

        SetRecommendedZoomFactor(m_captureDevice);
        previewHandler.SetFocusPoint(m_preview.Width / 2, m_preview.Height / 2, m_captureDevice, out var error);
        if (error != null)
        {
            Log(error);
        }
        
        previewHandler.AddZoomSlider(m_captureDevice);
        previewHandler.AddPinchToZoom(m_captureDevice);
        previewHandler.AddTapToFocus(m_captureDevice);
        
        await StartSession();
    }

    private async Task StartSession()
    {
        await Task.Run(() =>
            {
                m_captureSession?.StartRunning();
            }
        );
    }

  
    
    //Taken from: https://developer.apple.com/wwdc21/10047?time=117
    //and sample code from Apple: https://developer.apple.com/documentation/avfoundation/capture_setup/avcambarcode_detecting_barcodes_and_faces
    private void SetRecommendedZoomFactor(AVCaptureDevice captureDevice)
    {
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
        
        var minimumSubjectDistanceForCode = MinimumSubjectDistanceForCode(deviceFieldOfView, 20, m_rectOfInterestFillPercentage);
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
                    m_onSuccess.Invoke(readableObject);
                }
            }
        }
    }
}