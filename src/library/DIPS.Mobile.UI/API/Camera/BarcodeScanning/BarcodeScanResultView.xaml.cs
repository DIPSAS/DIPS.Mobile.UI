using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeScanResultView : ContentView
{
    public BarcodeScanResultView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public static readonly BindableProperty BarcodeScanResultProperty = BindableProperty.Create(
        nameof(BarcodeScanResult),
        typeof(BarcodeScanResult),
        typeof(BarcodeScanResultView));

    public BarcodeScanResult BarcodeScanResult
    {
        get => (BarcodeScanResult)GetValue(BarcodeScanResultProperty);
        set => SetValue(BarcodeScanResultProperty, value);
    }

    private void OnAndroidFormatTapped(object? sender, EventArgs e)
    {
        Browser.OpenAsync(
            "https://developers.google.com/android/reference/com/google/mlkit/vision/barcode/common/Barcode#FORMAT_ALL_FORMATS");
    }
}