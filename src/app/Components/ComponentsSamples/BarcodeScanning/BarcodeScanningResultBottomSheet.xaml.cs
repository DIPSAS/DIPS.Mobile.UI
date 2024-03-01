using DIPS.Mobile.UI.API.Camera.BarcodeScanning;

namespace Components.ComponentsSamples.BarcodeScanning;

public partial class BarcodeScanningResultBottomSheet
{
    public BarcodeScanningResultBottomSheet()
    {
        InitializeComponent();
    }

    public async void OpenWithBarCode(BarcodeScanResult barcodeScanResult)
    {
        HasBarCode = true;
        BarcodeScanResultView.BarcodeScanResult = barcodeScanResult;
        await this.Open();
    }

    public bool HasBarCode { get; private set; }
}