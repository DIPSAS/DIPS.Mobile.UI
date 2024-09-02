using DIPS.Mobile.UI.API.Camera.Permissions;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanner
{
    internal CameraPreview? m_cameraPreview;
    private Timer? m_barCodesFoundTimer;
    private List<BarcodeObservation> m_barcodeObservations = new();
    private DidFindBarcodeCallback? m_barCodeCallback;

    private void Log(string message)
    {
        DUILogService.LogDebug<BarcodeScanner>(message);
    }

    public async Task Start(CameraPreview cameraPreview, DidFindBarcodeCallback didFindBarcodeCallback)
    {
        m_cameraPreview = cameraPreview;
        m_barCodeCallback = didFindBarcodeCallback;
        if (await CameraPermissions.CanUseCamera())
        {
            Log("Permitted to use camera");
            await m_cameraPreview.HasLoaded();
            await PlatformStart();
        }
        else
        {
            Log("Not permitted to use camera");
        }
    }

    internal partial Task PlatformStart();

    public void Stop()
    {
        PlatformStop();
        m_cameraPreview = null;
        m_barCodeCallback = null;
        StopAndDisposeTimerAndResults();
    }

    internal partial void PlatformStop();

    internal void InvokeBarcodeFound(Barcode barcode)
    {
        var barcodeObservation =
            m_barcodeObservations.FirstOrDefault(observation => Equals(observation.Barcode, barcode));
        if (barcodeObservation == null)
        {
            m_barcodeObservations.Add(new BarcodeObservation(barcode, 1));
        }
        else
        {
            var numberOfDetections = barcodeObservation.Detections + 1;
            m_barcodeObservations.Remove(barcodeObservation);
            m_barcodeObservations.Add(new BarcodeObservation(barcode, numberOfDetections));
        }

        if (m_barCodesFoundTimer == null) //Start observing timer if its not already started
        {
            Log(
                $"First bar code found, observing for {BarcodeDetectionTime}ms.");
            m_barCodesFoundTimer = new Timer(_ =>
            {
                var allBarCodesOrderedByDetections =
                    m_barcodeObservations.OrderByDescending(observation => observation.Detections).ToList();
                var mostDetectedBarcodeObservation =
                    allBarCodesOrderedByDetections.FirstOrDefault();

                if (mostDetectedBarcodeObservation == null)
                    return; //Makes no sense why this would happen, but have to guard.

                mostDetectedBarcodeObservation.HasMostDetections =
                    true; //Mark the observation as the most detected barcode

                if (allBarCodesOrderedByDetections.Count > 1)
                {
                    Log("-- Observations --:");
                    foreach (var observation in allBarCodesOrderedByDetections)
                    {
                        Log($"{observation.Barcode}, detected {observation.Detections} times");
                    }
                }
                
                Log($"The most detected bar code: {mostDetectedBarcodeObservation.Barcode}");
                var barCodeResults = allBarCodesOrderedByDetections.ToList();
                StopAndDisposeTimerAndResults();
                MainThread.BeginInvokeOnMainThread(() =>
                    m_barCodeCallback?.Invoke(new BarcodeScanResult(mostDetectedBarcodeObservation.Barcode,
                        barCodeResults))); //Give the consumer the bar code, needs to invoked on main thread!
            }, null, (long)BarcodeDetectionTime, BarcodeDetectionTime);
        }
    }

    private void StopAndDisposeTimerAndResults()
    {
        m_barCodesFoundTimer?.Change(Timeout.Infinite, Timeout.Infinite); //Stop the timer
        m_barcodeObservations = []; //Reset potential bar codes for next time we scan
        m_barCodesFoundTimer = null; //Clean up
    }
}