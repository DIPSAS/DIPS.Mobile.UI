using Android.Content;
using Android.Graphics;
using Android.Hardware.Camera2;
using Android.Hardware.Camera2.Params;
using Android.Media;
using Android.OS;
using Android.Views;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Handlers;
using Playground.HåvardSamples.Scanning.Android;
using Size = Android.Util.Size;

namespace Playground.HåvardSamples.Scanning;

//Based on:
//- https://developers.google.com/ml-kit/vision/barcode-scanning/android
// -https://github.com/googlesamples/mlkit/tree/master/android/material-showcase/app/src/main/java/com/google/mlkit/md/barcodedetection
public partial class Scanner : CameraDevice.StateCallback, ImageReader.IOnImageAvailableListener, TextureView.ISurfaceTextureListener
{
    private readonly Context m_context;
    private HandlerThread m_backgroundHandlerThread;
    private Handler m_backgroundHandler;
    private Size m_previewSize;
    private ImageReader m_imageReader;
    private CaptureRequest.Builder m_captureRequestBuilder;
    private Preview m_preview;
    private TextureView m_textureView;
    private CameraManager m_cameraManager;
    private string m_cameraId;

    public Scanner()
    {
        m_context = DUI.GetCurrentMauiContext?.Context;
    }

    public async partial Task<string> Start(Preview preview)
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
        {
            if (await Permissions.RequestAsync<Permissions.Camera>() != PermissionStatus.Granted)
            {
                return string.Empty;
            }
        }

        //Inspiration : https://www.freecodecamp.org/news/android-camera2-api-take-photos-and-videos/

        m_preview = preview;
        m_textureView = new TextureView(m_context);
        m_textureView.SurfaceTextureListener = new SurfaceTextureListener();
        if (m_preview.PreviewView.Handler is not ContentViewHandler contentViewHandler) return string.Empty;
        contentViewHandler.PlatformView.AddView(m_textureView);
        
        StartBackgroundThread();

        var potentialCameraManager = m_context.GetSystemService(Context.CameraService);
        if (potentialCameraManager is not CameraManager cameraManager) return string.Empty;
        m_cameraManager = cameraManager;
        m_cameraId = null;
        foreach (var cameraId in cameraManager.GetCameraIdList())
        {
            if (m_cameraId != null) continue;

            var cameraCharacteristics = cameraManager.GetCameraCharacteristics(cameraId);
            if (cameraCharacteristics.Get(CameraCharacteristics.LensFacing) == CameraCharacteristics.LensFacing)
            {
                continue;
            }

            if (cameraCharacteristics.Get(CameraCharacteristics.ScalerStreamConfigurationMap) is StreamConfigurationMap
                streamConfigurationMap)
            {
                var sizes = streamConfigurationMap.GetOutputSizes((int)ImageFormatType.Jpeg);
                if (sizes == null) continue;
                var previewSize = sizes.MaxBy(s => s.Height * s.Width);
                m_imageReader =
                    ImageReader.NewInstance(previewSize.Width, previewSize.Height, ImageFormatType.Jpeg, 1);
                m_imageReader.SetOnImageAvailableListener(this, m_backgroundHandler);
                m_previewSize = previewSize;
            }

            m_cameraId = cameraId;
        }

        //     for (id in cameraIds) {
        //         val cameraCharacteristics = cameraManager.getCameraCharacteristics(id)
        //         //If we want to choose the rear facing camera instead of the front facing one
        //         if (cameraCharacteristics.get(CameraCharacteristics.LENS_FACING) == CameraCharacteristics.LENS_FACING_FRONT) 
        //             continue
        //     }
        //
        //     val previewSize = cameraCharacteristics.get(CameraCharacteristics.SCALER_STREAM_CONFIGURATION_MAP)!!.getOutputSizes(ImageFormat.JPEG).maxByOrNull { it.height * it.width }!!
        //         val imageReader = ImageReader.newInstance(previewSize.width, previewSize.height, ImageFormat.JPEG, 1)
        //     imageReader.setOnImageAvailableListener(onImageAvailableListener, backgroundHandler)
        //     cameraId = id
        // }
        return string.Empty;
        // if (preview.PreviewView.Handler is not ContentViewHandler contentViewHandler) return string.Empty;
        // var surfaceView = new SurfaceView(m_context)
        // {
        //     LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
        //         ViewGroup.LayoutParams.MatchParent)
        // };
        // contentViewHandler.PlatformView.AddView(surfaceView);
        //
        // var camera = new CameraSource().Start(preview, m_context);
        // // CHECK OUT OTHER VIEWS TO PREVIEW
        // camera.StartPreview();
        // camera.SetPreviewDisplay(surfaceView.Holder);

        // var options = new BarcodeScannerOptions.Builder()
        //     .SetBarcodeFormats(
        //         Barcode.FormatQrCode,
        //         Barcode.FormatDataMatrix,
        //         Barcode.FormatCode39,
        //         Barcode.FormatCode128,
        //         Barcode.FormatItf,
        //         Barcode.FormatUpcE)
        //     .Build();
        // var options = new GmsBarcodeScannerOptions.Builder()
        //     .SetBarcodeFormats(
        //         Barcode.FormatQrCode,
        //         Barcode.FormatDataMatrix,
        //         Barcode.FormatCode39, 
        //         Barcode.FormatCode128, 
        //         Barcode.FormatItf,
        //         Barcode.FormatUpcE)
        //     .EnableAutoZoom()
        //     .Build();
        //
        // var scanner = GmsBarcodeScanning.GetClient(m_context);
        //
        // try
        // {
        //     m_moduleInstallClient.AreModulesAvailable(m_optionalApiClient).AddOnSuccessListener(this).AddOnFailureListener(this);
        //     
        //     var scanning = await scanner.StartScan();
        //     if (scanning is Barcode barcode)
        //     {
        //         return barcode.RawValue;
        //     }
        // }
        // catch (MlKitException mlKitException)
        // {
        //     if (mlKitException.Message.Contains("Waiting for the Barcode UI module to be downloaded."))
        //     {
        //         
        //     }    
        // }
        //
        // catch (Exception e)
        // {
        //
        // }

        return string.Empty;
    }

    private void StartBackgroundThread()
    {
        m_backgroundHandlerThread = new HandlerThread("CameraVideoThread");
        m_backgroundHandlerThread.Start();
        if (m_backgroundHandlerThread.Looper == null)
        {
            return;
        }

        m_backgroundHandler = new Handler(m_backgroundHandlerThread.Looper);
    }

    private void StopBackgroundThread()
    {
        m_backgroundHandlerThread.QuitSafely();
        m_backgroundHandlerThread.Join();
    }

    public partial void Stop()
    {
        StopBackgroundThread();
    }

    
    private void OnCaptureSessionConfigureFailed(CameraCaptureSession session)
    {
        
    }

    private void OnCaptureSessionConfigured(CameraCaptureSession session)
    {
        session.SetRepeatingRequest(m_captureRequestBuilder.Build(), null, m_backgroundHandler);
    }
    
    public void OnImageAvailable(ImageReader reader)
    {
    }

    public override void OnDisconnected(CameraDevice camera)
    {
    }

    public override void OnError(CameraDevice camera, CameraError error)
    {
    }

    public override void OnOpened(CameraDevice cameraDevice)
    {
        var surfaceTexture = m_textureView.SurfaceTexture;
        if (surfaceTexture == null) return;
        surfaceTexture.SetDefaultBufferSize(m_previewSize.Width, m_previewSize.Height);
        var previewSurface = new Surface(surfaceTexture);
        m_captureRequestBuilder = cameraDevice.CreateCaptureRequest(CameraTemplate.Preview);
        m_captureRequestBuilder.AddTarget(previewSurface);
        
        cameraDevice.CreateCaptureSession(new List<Surface>() {previewSurface, m_imageReader.Surface}, new CaptureStateCallback(OnCaptureSessionConfigured, OnCaptureSessionConfigureFailed), null);
    }

    public void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height)
    {
        m_cameraManager.OpenCamera(m_cameraId, this, m_backgroundHandler);
    }

    public bool OnSurfaceTextureDestroyed(SurfaceTexture surface)
    {
        return true;
    }

    public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
    {
    }

    public void OnSurfaceTextureUpdated(SurfaceTexture surface)
    {
    }
}