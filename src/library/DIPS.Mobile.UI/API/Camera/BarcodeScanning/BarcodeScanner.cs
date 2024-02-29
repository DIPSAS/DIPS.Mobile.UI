using DIPS.Mobile.UI.API.Vibration;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanner
{
    internal Preview? m_preview;
    private Action<Barcode, Dictionary<Barcode, int>>? m_didFindBarcode;
    private Timer? m_barCodesFoundTimer;
    private const int BarcodeComparisonTime = 500;
    private Dictionary<Barcode, int> m_potentialBarcodes = new();

    private void Log(string message)
    {
        Console.WriteLine($@"DIPS {nameof(BarcodeScanner)}: {message}");
    }

    public async Task Start(Preview preview, Action<Barcode, Dictionary<Barcode, int>> didFindBarcode)
    {
        m_preview = preview;
        m_didFindBarcode = didFindBarcode;
        if (await CanUseCamera())
        {
            Log("Permitted to use camera");
            await m_preview.HasLoaded();
            await PlatformStart();
        }
        else
        {
            Log("Not permitted to use camera");
        }
    }

    internal partial Task PlatformStart();
    internal partial Task<bool> CanUseCamera();

    public void Stop()
    {
        PlatformStop();
        m_preview = null;
        m_didFindBarcode = null;
        StopAndDisposeTimerAndResults();
    }

    internal partial void PlatformStop();

    internal void InvokeBarcodeFound(Barcode barcode)
    {
        var (previousBarCode, numberOfScans) = m_potentialBarcodes.FirstOrDefault(pair => Equals(pair.Key, barcode));
        if (previousBarCode == null)
        {
            m_potentialBarcodes.Add(barcode, 1);
        }
        else
        {
            m_potentialBarcodes.Remove(barcode);
            m_potentialBarcodes.Add(barcode, numberOfScans + 1);
        }

        if (m_barCodesFoundTimer == null) //Start observing timer if its not already started
        {
            Log(
                $"First bar code found, observing for {BarcodeComparisonTime}ms until returning the most scanned barcode.");
            m_barCodesFoundTimer = new Timer(_ =>
            {
                var allBarCodesOrderedByNumberOfScans =
                    m_potentialBarcodes.OrderByDescending(keyValuePair => keyValuePair.Value).ToList();
                var mostScannedBarcode =
                    allBarCodesOrderedByNumberOfScans.FirstOrDefault()
                        .Key; //Key the bar code with the highest number of scans
                if (allBarCodesOrderedByNumberOfScans.Count > 1)
                {
                    Log("-- Observations --:");
                    foreach (var keyValuePair in allBarCodesOrderedByNumberOfScans)
                    {
                        Log($"{keyValuePair.Key}, number of scans: {keyValuePair.Value}");
                    }
                }

                Log($"The most scanned bar code: {mostScannedBarcode}");
                var barCodeResults = m_potentialBarcodes.ToDictionary();
                StopAndDisposeTimerAndResults();
                MainThread.BeginInvokeOnMainThread(() => m_didFindBarcode?.Invoke(mostScannedBarcode, barCodeResults)); //Give the consumer the bar code, needs to invoked on main thread!
            }, null, (long)BarcodeComparisonTime, BarcodeComparisonTime);
        }
    }

    private void StopAndDisposeTimerAndResults()
    {
        m_barCodesFoundTimer?.Change(Timeout.Infinite, Timeout.Infinite); //Stop the timer
        m_potentialBarcodes = new Dictionary<Barcode, int>(); //Reset potential bar codes for next time we scan
        m_barCodesFoundTimer = null; //Clean up
    }
}