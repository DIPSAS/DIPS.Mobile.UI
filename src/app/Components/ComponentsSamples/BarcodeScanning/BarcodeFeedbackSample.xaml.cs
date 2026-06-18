using Components.Resources.LocalizedStrings;
using DIPS.Mobile.UI.API.Camera;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using DuiColors = DIPS.Mobile.UI.Resources.Colors.Colors;
using DuiLabel = DIPS.Mobile.UI.Components.Labels.Label;

namespace Components.ComponentsSamples.BarcodeScanning;

public partial class BarcodeFeedbackSample
{
    private readonly BarcodeScanner m_barcodeScanner;
    private View? m_tooltipView;
    private int m_validationAttempt;

    public BarcodeFeedbackSample()
    {
        InitializeComponent();
        m_barcodeScanner = new BarcodeScanner();
    }

    private async Task Start()
    {
        try
        {
            await m_barcodeScanner.Start(new BarcodeScannerStartOptions
            {
                Preview = CameraPreview,
                OnCameraFailed = CameraFailed,
                OnBarcodeAcceptedAsync = HandleBarcodeAcceptedAsync,
                ValidateBarcodeAsync = ValidateBarcodeAsync,
                OnBarcodeRejectedAsync = HandleBarcodeRejectedAsync,
                Strategy = new ScanRectangleBarcodeScanStrategy
                {
                    WidthFraction = 0.8f,
                    HeightFraction = 0.3f
                },
                Hint = new BarcodeScannerHintOptions
                {
                    ShowAutomaticHint = true,
                    HintText = LocalizedStrings.BarcodeScannerFeedbackFocusHint,
                    Delay = TimeSpan.FromSeconds(5)
                }
            });

            m_barcodeScanner.SetTooltipView(GetTooltipView());
        }
        catch (Exception exception)
        {
            await Application.Current?.MainPage?.DisplayAlert("Failed, check console!", exception.Message, "Ok")!;
            Console.WriteLine(exception);
        }
    }

    private View GetTooltipView()
    {
        if (m_tooltipView is not null)
            return m_tooltipView;

        m_tooltipView = new DuiLabel
        {
            Text = LocalizedStrings.BarcodeScannerFeedbackTooltipText,
            Style = Styles.GetLabelStyle(LabelStyle.UI200),
            TextColor = DuiColors.GetColor(ColorName.color_text_on_button),
            HorizontalTextAlignment = TextAlignment.Center,
            LineBreakMode = LineBreakMode.WordWrap
        };

        return m_tooltipView;
    }

    private void CameraFailed(CameraException e)
    {
        App.Current.MainPage.DisplayAlert("Something failed!", e.Message, "Ok");
    }

    private Task HandleBarcodeAcceptedAsync(BarcodeScanResult barcodeScanResult)
    {
        Console.WriteLine($"Accepted barcode: {barcodeScanResult.Barcode.RawValue}");
        return Task.CompletedTask;
    }

    private async Task<BarcodeScanValidationResult> ValidateBarcodeAsync(string barcode)
    {
        await Task.Delay(150);

        var feedback = (m_validationAttempt++ % 4) switch
        {
            0 => (Message: LocalizedStrings.BarcodeScannerFeedbackAlreadyScanned, ReasonCode: "already-scanned"),
            1 => (Message: LocalizedStrings.BarcodeScannerFeedbackWrongPatient, ReasonCode: "wrong-patient"),
            2 => (Message: LocalizedStrings.BarcodeScannerFeedbackNotLabel, ReasonCode: "not-label"),
            _ => default
        };

        return feedback.Message is null
            ? BarcodeScanValidationResult.Valid()
            : BarcodeScanValidationResult.Invalid(feedback.Message, feedback.ReasonCode);
    }

    private Task HandleBarcodeRejectedAsync(BarcodeScanResult barcodeScanResult, BarcodeScanValidationResult validationResult)
    {
        Console.WriteLine($"Rejected barcode: {barcodeScanResult.Barcode.RawValue}. {validationResult.ReasonCode}");
        return Task.CompletedTask;
    }

    protected override void OnAppearing()
    {
        _ = Start();
        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        m_barcodeScanner.StopAndDispose();
        base.OnDisappearing();
    }

    private void Close(object? sender, EventArgs e)
    {
        Shell.Current.Navigation.PopModalAsync();
    }
}