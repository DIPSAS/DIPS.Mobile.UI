namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanner
{
    internal partial Task PlatformStart() { return Task.FromResult(string.Empty);}
    internal partial void PlatformStop(){}
}