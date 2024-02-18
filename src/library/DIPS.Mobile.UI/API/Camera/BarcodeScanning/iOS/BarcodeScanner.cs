#nullable enable
using AVFoundation;
using CoreAnimation;
using CoreFoundation;
using CoreGraphics;
using DIPS.Mobile.UI.API.Vibration;
using Microsoft.Maui.Handlers;
using UIKit;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanner
{
    private readonly object m_isLockedForConfiguration = new object();
    private AVCaptureSession m_captureSession;

    //https://developer.apple.com/documentation/avfoundation/capture_setup/requesting_authorization_to_capture_and_save_media#2958841
    public static async Task<bool> IsAuthorized()
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

    internal partial async Task<bool> CanUseCamera() => await IsAuthorized();


    internal partial void PlatformStop()
    {
        if (m_captureSession.Running)
        {
            Task.Run(() =>
            {
                m_captureSession.StopRunning();
            });
        }
    }

    internal async partial Task PlatformStart()
    {
        m_captureSession = new AVCaptureSession();
        //Call beginConfiguration() before changing a session’s inputs or outputs, and call commitConfiguration() after making changes.
        m_captureSession.BeginConfiguration();

        // var previewLayer = new AVCaptureVideoPreviewLayer();
        //This makes sure we display the video feed
        if (m_preview?.Handler is not PreviewHandler previewHandler) return;
        var previewLayer =
            previewHandler.AddSessionToPreviewLayer(m_captureSession, AVLayerVideoGravity.ResizeAspectFill);

        //Add camera input: https://developer.apple.com/documentation/avfoundation/capture_setup/choosing_a_capture_device#2958868
        var captureDeviceDiscoverySession = AVCaptureDeviceDiscoverySession.Create(
            new[]
            {
                AVCaptureDeviceType.BuiltInWideAngleCamera,
            }, AVMediaTypes.Video, AVCaptureDevicePosition.Unspecified);
        AVCaptureDevice? captureDevice;
        if (captureDeviceDiscoverySession.Devices.Length > 0)
        {
            //TODO: Research what devices are best for bar code scanning
            captureDevice = captureDeviceDiscoverySession.Devices[0];
            if (captureDeviceDiscoverySession.Devices.Length > 1)
            {
                Console.WriteLine(
                    $@"Found {captureDeviceDiscoverySession.Devices.Length} devices to pick from. We have selected the best depth camera");
            }
        }
        else
        {
            throw new Exception($"Found no cameras to use on the back of the iPhone");
        }

        var videoDeviceInput = AVCaptureDeviceInput.FromDevice(captureDevice);
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
                    InvokeBarcodeFound(new Barcode(s.StringValue));
                }
            }), DispatchQueue.MainQueue);
            //Add bar code scanning metadata
            //https://barcode-labels.com/getting-started/barcodes/types/
            captureMetadataOutput.MetadataObjectTypes = AVMetadataObjectType.Code39Code |
                                                        AVMetadataObjectType.Code128Code |
                                                        AVMetadataObjectType.Interleaved2of5Code |
                                                        AVMetadataObjectType.UPCECode |
                                                        AVMetadataObjectType.DataMatrixCode |
                                                        AVMetadataObjectType.QRCode |
                                                        AVMetadataObjectType.EAN8Code |
                                                        AVMetadataObjectType.EAN13Code;
            var x = 0;
            var height = previewLayer.Frame.Height / 4;
            var y = (previewLayer.Frame.Height / 2) - (height / 2);
            var width = previewLayer.Frame.Width;
            var cgRect = new CGRect(x, y, width, height);
            captureMetadataOutput.RectOfInterest = previewLayer.MapToMetadataOutputCoordinates(cgRect);

            if (m_preview?.Handler is ContentViewHandler previewViewHandler)
            {
                var uiView = previewViewHandler.PlatformView;
                var layer = new CALayer();
                layer.Frame = cgRect;
                layer.BorderColor = UIColor.White.CGColor;
                layer.BorderWidth = 2;
                uiView.Layer.AddSublayer(layer);
            }
        }
        else
        {
            throw new Exception("Unable to add output");
        }

        //Commit the configuration
        m_captureSession.CommitConfiguration();
        
        SetBarCodeMinimumZoom(captureDevice,captureMetadataOutput.RectOfInterest);
        
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

        m_preview?.GestureRecognizers.Add(new TapGestureRecognizer()
        {
            Command = new Command(() =>
            {
                captureDevice.LockForConfiguration(out var error);
                if (error == null) return;
                lock (m_isLockedForConfiguration)
                {
                    var x = captureMetadataOutput.RectOfInterest.X - captureMetadataOutput.RectOfInterest.Width;
                    var y = captureMetadataOutput.RectOfInterest.Y - captureMetadataOutput.RectOfInterest.Height;
                    captureDevice.FocusPointOfInterest = new CGPoint(x, y);
                    captureDevice.FocusMode = AVCaptureFocusMode.AutoFocus;
                }

                captureDevice.UnlockForConfiguration();
            })
        });
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
        var minimumSubjectDistanceForCode =  filledCodeSize / Math.Tan(radians);
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
                    Console.WriteLine($@"Barcode scanner found type: {readableObject.Type}");
                    Console.WriteLine($@"Barcode scanner value: {stringValue}");
                    m_onSuccess.Invoke(readableObject);
                }
            }
        }
    }
}