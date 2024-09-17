namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanner
{
    internal partial Task PlatformStart(BarcodeScanningSettings barcodeScanningSettings) { return Task.FromResult(string.Empty); }
    internal partial Task PlatformStop() { return Task.CompletedTask; }
}