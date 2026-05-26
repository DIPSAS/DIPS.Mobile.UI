using DIPS.Mobile.UI.API.Camera.BarcodeScanning;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.Sizes;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Label;

namespace Playground.VetleSamples;

/// <summary>
/// Reproduces the Arena Mobile SamplingPatientBarcodeResultStrategy scanner layout.
/// Opens as a modal NavigationPage with:
///   - Top toolbar: patient banner placeholder
///   - Tooltip: instruction label
///   - Bottom toolbar: "or" label + button
///   - ScanRectangleBarcodeScanStrategy
///
/// Use this to verify that top/bottom toolbar sizing works correctly.
/// </summary>
public partial class SamplingPatientScanRepro
{
    private readonly BarcodeScanner m_barcodeScanner;

    public SamplingPatientScanRepro()
    {
        InitializeComponent();
        m_barcodeScanner = new BarcodeScanner();

        ToolbarItems.Add(new ToolbarItem
        {
            Text = "Avbryt",
            Command = new Command(() => Shell.Current.Navigation.PopModalAsync())
        });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = StartScanning();
    }

    protected override void OnDisappearing()
    {
        m_barcodeScanner.StopAndDispose();
        base.OnDisappearing();
    }

    private async Task StartScanning()
    {
        try
        {
            await m_barcodeScanner.Start(new BarcodeScannerStartOptions
            {
                Preview = CameraPreview,
                OnCameraFailed = e => DisplayAlert("Kamera feilet", e.Message, "OK"),
                OnBarcodeAcceptedAsync = OnBarcodeAccepted,
                Strategy = new ScanRectangleBarcodeScanStrategy(),
            });

            // Add top content — simulates the minimized patient banner
            var patientBanner = new Frame
            {
                BackgroundColor = Color.FromArgb("#333333"),
                CornerRadius = 8,
                Padding = new Thickness(12, 8),
                Margin = new Thickness(Sizes.GetSize(SizeName.size_3), 0),
                VerticalOptions = LayoutOptions.Start,
                Content = new Label
                {
                    Text = "Ola Nordmann — 01019012345",
                    TextColor = Microsoft.Maui.Graphics.Colors.White,
                    FontSize = 14,
                }
            };
            CameraPreview.AddTopToolbarView(patientBanner);

            // Add tooltip — simulates the scan instruction
            m_barcodeScanner.SetTooltipView(new DIPS.Mobile.UI.Components.Labels.Label
            {
                Text = "Skann armbåndet til pasienten",
                Style = Styles.GetLabelStyle(LabelStyle.Body300),
                TextColor = Microsoft.Maui.Graphics.Colors.White,
                HorizontalTextAlignment = TextAlignment.Center,
            });

            // Add bottom content — simulates the "or enter birth number" section
            var bottomContent = new VerticalStackLayout
            {
                Children =
                {
                    new DIPS.Mobile.UI.Components.Labels.Label
                    {
                        Text = "eller",
                        Style = Styles.GetLabelStyle(LabelStyle.Body300),
                        TextColor = Microsoft.Maui.Graphics.Colors.White,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Margin = new Thickness(0, Sizes.GetSize(SizeName.size_4)),
                    },
                    new DIPS.Mobile.UI.Components.Buttons.Button
                    {
                        Text = "Tast inn fødselsnummer",
                        TextColor = Microsoft.Maui.Graphics.Colors.White,
                        Style = Styles.GetButtonStyle(ButtonStyle.DefaultLarge),
                        HorizontalOptions = LayoutOptions.Fill,
                        Margin = new Thickness(Sizes.GetSize(SizeName.size_3), 0),
                    }
                }
            };
            CameraPreview.AddBottomToolbarView(bottomContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SamplingPatientScanRepro] Start failed: {ex}");
        }
    }

    private Task OnBarcodeAccepted(BarcodeScanResult result)
    {
        m_barcodeScanner.PauseScanning(resetOverlay: false);

        var sheet = new BottomSheet { Title = "Skanneresultat" };
        sheet.Content = new VerticalStackLayout
        {
            Padding = 16,
            Children =
            {
                new Label { Text = $"Strekkode: {result.Barcode.RawValue}", Margin = new Thickness(0, 0, 0, 8) },
                new Label { Text = $"Format: {result.Barcode.Format}" },
            }
        };

        sheet.Closed += (_, _) =>
        {
            m_barcodeScanner.ResumeScanning();
        };

        sheet.Open();
        return Task.CompletedTask;
    }
}
