namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanner
{
    /// <summary>
    /// Gets the current scanner state.
    /// </summary>
    public BarcodeScannerState State => m_scanSession?.State ?? BarcodeScannerState.Idle;
}