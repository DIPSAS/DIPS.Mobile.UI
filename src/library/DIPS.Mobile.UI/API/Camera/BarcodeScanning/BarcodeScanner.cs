using DIPS.Mobile.UI.API.Vibration;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanner
{
    internal Preview? m_preview;
    private Action<Barcode>? m_didFindBarcode;

    private void Log(string message)
    {
        Console.WriteLine($@"DIPS {nameof(BarcodeScanner)}: {message}");
    }
    
    public async Task Start(Preview preview, Action<Barcode> didFindBarcode)
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
    }
    
    internal partial void PlatformStop();

    internal void InvokeBarcodeFound(Barcode barcode)
    {
        m_didFindBarcode?.Invoke(barcode);
    }

}