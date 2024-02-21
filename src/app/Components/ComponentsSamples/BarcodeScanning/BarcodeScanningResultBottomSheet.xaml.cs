using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning;

namespace Components.ComponentsSamples.BarcodeScanning;

public partial class BarcodeScanningResultBottomSheet
{
    public BarcodeScanningResultBottomSheet()
    {
        InitializeComponent();
    }

    public void OpenWithBarCode(Barcode barcode)
    {
        BindingContext = barcode;      
        _ = this.Open();
    }
}