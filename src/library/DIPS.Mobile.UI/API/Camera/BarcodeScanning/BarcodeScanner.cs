using DIPS.Mobile.UI.API.Vibration;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanner
{
    internal Preview? m_preview;
    private Action<Barcode>? m_didFindBarcode;

    public async Task Start(Preview preview, Action<Barcode> didFindBarcode)
    {
        m_preview = preview;
        m_didFindBarcode = didFindBarcode;
        if (!await CanUseCamera()) return;
        await PlatformStart();
    }

    internal partial Task PlatformStart();
    internal partial Task<bool> CanUseCamera();

    public void Stop()
    {
        m_preview = null;
        m_didFindBarcode = null;
        PlatformStop();
    }
    
    internal partial void PlatformStop();

    internal void InvokeBarcodeFound(Barcode barcode)
    {
        VibrationService.SelectionChanged();
        m_didFindBarcode?.Invoke(barcode);
    }

}