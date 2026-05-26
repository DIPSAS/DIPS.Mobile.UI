using DIPS.Mobile.UI.API.Camera.Preview;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanner
{
    internal CameraPreview m_cameraPreview;
    internal partial Task PlatformStart(BarcodeScannerStartOptions startOptions, CameraFailed cameraFailedDelegate) { return Task.FromResult(string.Empty); }
    internal partial Task PlatformStop() { return Task.CompletedTask; }
}