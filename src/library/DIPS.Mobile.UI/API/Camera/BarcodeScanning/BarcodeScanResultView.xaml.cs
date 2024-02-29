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

    public static readonly BindableProperty BarcodeProperty = BindableProperty.Create(
        nameof(Barcode),
        typeof(Barcode),
        typeof(BarcodeScanResultView));

    public Barcode Barcode
    {
        get => (Barcode)GetValue(BarcodeProperty);
        set => SetValue(BarcodeProperty, value);
    }

    public static readonly BindableProperty BarcodeObservationsProperty = BindableProperty.Create(
        nameof(BarcodeObservations),
        typeof(List<BarcodeObservation>),
        typeof(BarcodeScanResultView), defaultValue:new List<BarcodeObservation>(), propertyChanged: ((bindable, _, _) => ((BarcodeScanResultView)bindable).OnBarcodeObservationsChanged()));

    private bool m_hasMultipleObservations;

    private void OnBarcodeObservationsChanged()
    {
        HasMultipleObservations = BarcodeObservations.Count > 1;
    }

    public bool HasMultipleObservations
    {
        get => m_hasMultipleObservations;
        set
        {
            m_hasMultipleObservations = value;
            OnPropertyChanged();
        }
    }

    public List<BarcodeObservation> BarcodeObservations
    {
        get => (List<BarcodeObservation>)GetValue(BarcodeObservationsProperty);
        set => SetValue(BarcodeObservationsProperty, value);
    }

    private void OnAndroidFormatTapped(object? sender, EventArgs e)
    {
        Browser.OpenAsync(
            "https://developers.google.com/android/reference/com/google/mlkit/vision/barcode/common/Barcode#FORMAT_ALL_FORMATS");
    }
}