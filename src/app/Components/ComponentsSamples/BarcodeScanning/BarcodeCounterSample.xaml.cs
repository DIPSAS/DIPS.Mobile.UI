using DIPS.Mobile.UI.API.Camera;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace Components.ComponentsSamples.BarcodeScanning;

public partial class BarcodeCounterSample
{
    private readonly BarcodeScanner m_barcodeScanner;
    private int m_scanCount;
    private Label? m_counterLabel;

    public BarcodeCounterSample()
    {
        InitializeComponent();
        m_barcodeScanner = new BarcodeScanner();
    }

    private async Task Start()
    {
        try
        {
            await m_barcodeScanner.Start(CameraPreview, DidFindBarcode, CameraFailed, settings =>
            {
                settings.ShowScanRectangle = true;
                settings.ScanRectangleWidthFraction = 0.8f;
                settings.ScanRectangleHeightFraction = 0.3f;
            });

            m_counterLabel = new Label
            {
                Text = "Scanned: 0",
                Style = Styles.GetLabelStyle(LabelStyle.UI200),
                TextColor = Colors.White,
                HorizontalTextAlignment = TextAlignment.Center
            };

            CameraPreview.AddTopToolbarView(new VerticalStackLayout
            {
                Spacing = 4,
                Children =
                {
                    new Label
                    {
                        Text = "Barcode counter",
                        Style = Styles.GetLabelStyle(LabelStyle.UI200),
                        TextColor = Colors.White,
                        HorizontalTextAlignment = TextAlignment.Center
                    },
                    m_counterLabel
                }
            });
        }
        catch (Exception exception)
        {
            await Application.Current?.MainPage?.DisplayAlert("Failed, check console!", exception.Message, "Ok")!;
            Console.WriteLine(exception);
        }
    }

    private void CameraFailed(CameraException e)
    {
        App.Current.MainPage.DisplayAlert("Something failed!", e.Message, "Ok");
    }

    private void DidFindBarcode(BarcodeScanResult barcodeScanResult)
    {
        m_scanCount++;
        if (m_counterLabel is not null)
        {
            m_counterLabel.Text = $"Scanned: {m_scanCount}";
        }
    }

    protected override void OnAppearing()
    {
        _ = Start();
        base.OnAppearing();
    }

    private void Close(object? sender, EventArgs e)
    {
        m_barcodeScanner.StopAndDispose();
        Shell.Current.Navigation.PopModalAsync();
    }
}
