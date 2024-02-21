using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

public partial class BarcodeMetadataView : ContentView
{
    public BarcodeMetadataView()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty BarcodeProperty = BindableProperty.Create(
        nameof(Barcode),
        typeof(Barcode),
        typeof(BarcodeMetadataView),
        propertyChanged: (bindable, _, _) =>
            ((BarcodeMetadataView)bindable).BindingContext = ((BarcodeMetadataView)bindable).BindingContext);

    public Barcode Barcode
    {
        get => (Barcode)GetValue(BarcodeProperty);
        set => SetValue(BarcodeProperty, value);
    }

    private void OnAndroidFormatTapped(object? sender, EventArgs e)
    {
        Browser.OpenAsync(
            "https://developers.google.com/android/reference/com/google/mlkit/vision/barcode/common/Barcode#FORMAT_ALL_FORMATS");
    }
}