#nullable enable
using AVFoundation;
using CoreAnimation;
using CoreFoundation;
using CoreGraphics;
using DIPS.Mobile.UI.Effects.Touch;
using Microsoft.Maui.Handlers;
using ObjCRuntime;
using UIKit;

namespace Playground.HåvardSamples.Scanning;

public partial class Scanner
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


    public partial void Stop()
    {
        if (m_captureSession.Running)
        {
            Task.Run(() =>
            {
                m_captureSession.StopRunning();
            });
        }
    }

    public async partial Task Start(Preview preview)
    {
        if (!await IsAuthorized()) return;

        m_captureSession = new AVCaptureSession();
        //Call beginConfiguration() before changing a session’s inputs or outputs, and call commitConfiguration() after making changes.
        m_captureSession.BeginConfiguration();

        var previewLayer = new AVCaptureVideoPreviewLayer();
        //This makes sure we display the video feed
        if (preview.Handler is LayoutHandler previewHandler)
        {
            previewLayer.Frame = previewHandler.PlatformView.Bounds;
            previewLayer.Session = m_captureSession;
            previewLayer.VideoGravity = AVLayerVideoGravity.ResizeAspectFill;

            if (preview.PreviewView.Handler is ContentViewHandler previewViewHandler)
            {
                previewViewHandler.PlatformView.Layer.AddSublayer(previewLayer);
            }
        }

        //Add camera input: https://developer.apple.com/documentation/avfoundation/capture_setup/choosing_a_capture_device#2958868
        var captureDeviceDiscoverySession = AVCaptureDeviceDiscoverySession.Create(
            new[]
            {
                AVCaptureDeviceType.BuiltInTrueDepthCamera, AVCaptureDeviceType.BuiltInDualCamera,
                AVCaptureDeviceType.BuiltInWideAngleCamera
            }, AVMediaTypes.Video, AVCaptureDevicePosition.Back);
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

        //Set quality
        m_captureSession.SessionPreset = AVCaptureSession.PresetPhoto;

        //Add barcode camera output
        var captureMetadataOutput = new AVCaptureMetadataOutput();
        if (m_captureSession.CanAddOutput(captureMetadataOutput))
        {
            m_captureSession.AddOutput(
                captureMetadataOutput); //this has to be set before setting metadata objects, or else it crashes

            captureMetadataOutput.SetDelegate(new CaptureDelegate(s =>
            {
            }), DispatchQueue.MainQueue);
            //Add bar code scanning metadata
            //https://barcode-labels.com/getting-started/barcodes/types/
            captureMetadataOutput.MetadataObjectTypes = AVMetadataObjectType.Code39Code |
                                                        AVMetadataObjectType.Code128Code |
                                                        AVMetadataObjectType.Interleaved2of5Code |
                                                        AVMetadataObjectType.UPCECode |
                                                        AVMetadataObjectType.DataMatrixCode |
                                                        AVMetadataObjectType.QRCode;
            var x = 0;
            var height = previewLayer.Frame.Height / 4;
            var y = (previewLayer.Frame.Height / 2) - (height / 2);
            var width = previewLayer.Frame.Width;
            var cgRect = new CGRect(x, y, width, height);
            captureMetadataOutput.RectOfInterest = previewLayer.MapToMetadataOutputCoordinates(cgRect);

            if (preview.PreviewView.Handler is ContentViewHandler previewViewHandler)
            {
                var uiView = previewViewHandler.PlatformView;
                var layer = new CALayer();
                layer.Frame = cgRect;
                layer.BorderColor = UIColor.Blue.CGColor;
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

        preview.GestureRecognizers.Add(new TapGestureRecognizer()
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
}

public class CaptureDelegate : AVCaptureMetadataOutputObjectsDelegate
{
    private readonly Action<string> m_onSuccess;


    public CaptureDelegate(Action<string> onSuccess)
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
                    m_onSuccess.Invoke(stringValue);
                }
            }
        }
    }
}