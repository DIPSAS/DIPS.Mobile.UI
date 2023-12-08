using Android;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Tasks;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using DIPS.Mobile.UI.API.Library;
using Playground.HåvardSamples.Scanning.Android;
using Xamarin.Google.MLKit.Vision.BarCode;
using Xamarin.Google.MLKit.Vision.Barcode.Common;
using Object = Java.Lang.Object;

namespace Playground.HåvardSamples.Scanning;

//Based on:
//- https://developers.google.com/ml-kit/vision/barcode-scanning/android
// -https://github.com/googlesamples/mlkit/tree/master/android/material-showcase/app/src/main/java/com/google/mlkit/md/barcodedetection
public partial class Scanner 
{
    private readonly Context m_context;

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
        
        var camera = new CameraSource().Start(preview, m_context);
        camera.StartPreview();
        camera.SetPreviewDisplay(); <-- set preview
        
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

    public partial void Stop()
    {
    }
}