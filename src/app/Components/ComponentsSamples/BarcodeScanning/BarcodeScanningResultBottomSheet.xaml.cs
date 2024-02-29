using DIPS.Mobile.UI.API.Camera.BarcodeScanning;

namespace Components.ComponentsSamples.BarcodeScanning;

public partial class BarcodeScanningResultBottomSheet
{
    public BarcodeScanningResultBottomSheet()
    {
        InitializeComponent();
    }

    public async void OpenWithBarCode(Barcode barcode, List<BarcodeObservation> barcodeObservations)
    {
        await this.Open();
        BarcodeScanResultView.Barcode = barcode;
        BarcodeScanResultView.BarcodeObservations = barcodeObservations;
        HasBarCode = true;
    }

    public bool HasBarCode { get; private set; }
}