using AVFoundation;
using CoreAnimation;
using CoreFoundation;
using CoreGraphics;
using Foundation;
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
    private UITapToFocusGestureRecognizer? m_tapToFocusGestureRecognizer;


    //https://developer.apple.com/documentation/avfoundation/capture_setup/requesting_authorization_to_capture_and_save_media#2958841
    internal partial async Task<bool> CanUseCamera()
    {
        bool isAuthorized;
        var status = AVCaptureDevice.GetAuthorizationStatus(AVAuthorizationMediaType.Video);
        isAuthorized = status == AVAuthorizationStatus.Authorized;

        if (status == AVAuthorizationStatus.NotDetermined)
        {
            isAuthorized = await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVAuthorizationMediaType.Video);
        }

        return isAuthorized;
    }


    internal partial void PlatformStop()
    {
        if (m_captureSession is {Running: true})
        {
            Task.Run(() =>
            {
                m_captureSession.StopRunning();
                m_captureSession = null;
            });
        }

        m_captureDevice = null;
        m_rectOfInterest = null;
        
        if (m_tapToFocusGestureRecognizer != null)
        {
            m_tapToFocusGestureRecognizer.RemoveReferences();
            m_previewUIView?.RemoveGestureRecognizer(m_tapToFocusGestureRecognizer);
        }

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
            await previewHandler.WaitForViewLayoutAndAddSessionToPreview(m_captureSession, AVLayerVideoGravity.ResizeAspectFill);
        //Choosing build in wide angle camera, same as the sample app from Apple: AVCamBarCode: https://developer.apple.com/documentation/avfoundation/capture_setup/avcambarcode_detecting_barcodes_and_faces
        m_captureDevice = AVCaptureDevice.GetDefaultDevice(AVCaptureDeviceType.BuiltInWideAngleCamera,
            AVMediaTypes.Video, AVCaptureDevicePosition.Back);

        var videoDeviceInput = AVCaptureDeviceInput.FromDevice(m_captureDevice);
        if (videoDeviceInput == null) throw new Exception($"video device input is null");
        if (m_captureSession.CanAddInput(videoDeviceInput))
        {
            m_captureSession.AddInput(videoDeviceInput);
        }
        else
        {
            throw new Exception("Unable to add back camera as input");
        }

        //Set quality for best performance
        //Source: https://developers.google.com/ml-kit/vision/barcode-scanning/ios#performance-tips
        m_captureSession.SessionPreset = AVCaptureSession.Preset1280x720;
        
        //Add barcode camera output
        var captureMetadataOutput = new AVCaptureMetadataOutput();
        if (m_captureSession.CanAddOutput(captureMetadataOutput))
        {
            m_captureSession.AddOutput(
                captureMetadataOutput); //this has to be set before setting metadata objects, or else it crashes

            captureMetadataOutput.SetDelegate(new CaptureDelegate(s =>
            {
                if (!string.IsNullOrEmpty(s.StringValue))
                {
                    InvokeBarcodeFound(new Barcode(s.StringValue, s.Type.ToString()));
                }
            }), DispatchQueue.MainQueue);
            //Add bar code scanning metadata
            //Bar codes: https://developer.apple.com/documentation/avfoundation/avmetadataobjecttype#3801359
            //2D codes: https://developer.apple.com/documentation/avfoundation/avmetadataobjecttype#3801360
            captureMetadataOutput.MetadataObjectTypes = AVMetadataObjectType.CodabarCode |
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
            var height = previewLayer.Frame.Height / 4;
            var y = (previewLayer.Frame.Height / 2) - (height / 2);
            var width = previewLayer.Frame.Width;
            var cgRect = new CGRect(x, y, width, height);
            m_rectOfInterest = captureMetadataOutput.RectOfInterest = previewLayer.MapToMetadataOutputCoordinates(cgRect);
            
            var layer = new CALayer();
            layer.Frame = cgRect;
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

        SetBarCodeMinimumZoom(m_captureDevice, captureMetadataOutput.RectOfInterest);
        previewHandler.AddZoomSlider(m_captureDevice);

        await Task.Run(() =>
            {
                try
                {
                    m_captureSession.StartRunning();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        );
        
        m_tapToFocusGestureRecognizer = new UITapToFocusGestureRecognizer(TapToFocus);
        m_previewUIView.AddGestureRecognizer(m_tapToFocusGestureRecognizer);
    }

    private void TapToFocus(NSSet touches, UIEvent uiEvent)
    {
        if (m_previewUIView == null) return;
        if (touches.First() is not UITouch touchPoint) return;
        var screenSize = m_previewUIView.Bounds.Size;
        var focusPoint = new CGPoint(x: touchPoint.LocationInView(m_previewUIView).Y / screenSize.Height,
            y: 1.0 - touchPoint.LocationInView(m_previewUIView).X / screenSize.Width);

        if (m_captureDevice != null)
        {
            if (m_captureDevice.LockForConfiguration(out var error))
            {
                if (!string.IsNullOrEmpty(error.ToString()))
                {
                    Log(error.ToString());
                }

                if (m_captureDevice.FocusPointOfInterestSupported)
                {
                    m_captureDevice.FocusPointOfInterest = focusPoint;
                    m_captureDevice.FocusMode = AVCaptureFocusMode.AutoFocus;
                }

                if (m_captureDevice.ExposurePointOfInterestSupported)
                {
                    m_captureDevice.ExposurePointOfInterest = focusPoint;
                    m_captureDevice.ExposureMode = AVCaptureExposureMode.AutoExpose;
                }

                m_captureDevice.UnlockForConfiguration();
            }
        }
    }

    //Taken from: https://developer.apple.com/wwdc21/10047?time=117
    //and sample code from Apple: https://developer.apple.com/documentation/avfoundation/capture_setup/avcambarcode_detecting_barcodes_and_faces
    private void SetBarCodeMinimumZoom(AVCaptureDevice captureDevice, CGRect rectOfInterest)
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

        var radians = (deviceFieldOfView / 2) * Math.PI / 180;
        var filledCodeSize = 20 / rectOfInterest.Width;
        var minimumSubjectDistanceForCode = filledCodeSize / Math.Tan(radians);
        if (minimumSubjectDistanceForCode < deviceMinimumFocusDistance)
        {
            var zoomFactor = deviceMinimumFocusDistance / minimumSubjectDistanceForCode;
            try
            {
                captureDevice.LockForConfiguration(out var error);
                if (error != null)
                {
                    throw new Exception(error.ToString());
                }

                captureDevice.VideoZoomFactor = (new nfloat(zoomFactor));

                captureDevice.UnlockForConfiguration();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}

internal class UITapToFocusGestureRecognizer : UIGestureRecognizer
{
    private Action<NSSet, UIEvent>? m_onTouchesBegan;

    public UITapToFocusGestureRecognizer(Action<NSSet, UIEvent> onTouchesBegan)
    {
        m_onTouchesBegan = onTouchesBegan;
    }
    
    public override void TouchesBegan(NSSet touches, UIEvent evt)
    {
        m_onTouchesBegan?.Invoke(touches, evt);
        base.TouchesBegan(touches, evt);
    }

    internal void RemoveReferences()
    {
        m_onTouchesBegan = null;
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