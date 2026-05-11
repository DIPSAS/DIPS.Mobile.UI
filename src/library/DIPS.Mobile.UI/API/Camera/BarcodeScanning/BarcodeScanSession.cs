using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

internal sealed class BarcodeScanSession
{
    private readonly BarcodeScanningSettings m_settings;
    private readonly BarcodeScanProgressController m_progressController;
    private readonly Action m_stopScanning;
    private bool m_hasReportedCompletion;
    private bool m_isDisposed;

    public BarcodeScanSession(BarcodeScanningSettings settings, Action stopScanning)
    {
        m_settings = settings;
        m_stopScanning = stopScanning;
        m_progressController = new BarcodeScanProgressController(settings);
    }

    public BarcodeScannerState State { get; private set; } = BarcodeScannerState.Idle;

    public bool CanProcessBarcode => !m_isDisposed && State == BarcodeScannerState.Scanning;

    public bool ShouldShowProgressCounter => m_settings.RequiredScanCount.GetValueOrDefault() > 0;

    public void AttachProgressCounter(CameraPreview cameraPreview)
    {
        m_progressController.Attach(cameraPreview);
    }

    public void Start()
    {
        m_hasReportedCompletion = false;
        State = BarcodeScannerState.Scanning;
    }

    public void ResumeScanning()
    {
        if (m_isDisposed || State == BarcodeScannerState.Completed)
            return;

        State = BarcodeScannerState.Scanning;
    }

    public bool TryBeginProcessing()
    {
        if (!CanProcessBarcode)
            return false;

        State = BarcodeScannerState.Validating;
        return true;
    }

    public void Dispose()
    {
        m_isDisposed = true;
        State = BarcodeScannerState.Idle;
        m_progressController.Detach();
    }

    public async Task<bool> ProcessBarcodeScanResultAsync(BarcodeScanResult barcodeScanResult, BarcodeScanRectangleOverlay? scanRectangleOverlay)
    {
        if (m_isDisposed)
            return false;

        State = BarcodeScannerState.Validating;
        var validationResult = await ValidateBarcodeScanResultAsync(barcodeScanResult);

        if (m_isDisposed)
            return false;

        return validationResult.IsValid
            ? await AcceptBarcodeScanResultAsync(barcodeScanResult, scanRectangleOverlay)
            : await RejectBarcodeScanResultAsync(barcodeScanResult, validationResult, scanRectangleOverlay);
    }

    private async Task<BarcodeScanValidationResult> ValidateBarcodeScanResultAsync(BarcodeScanResult barcodeScanResult)
    {
        if (m_settings.ValidateBarcodeAsync is null)
            return BarcodeScanValidationResult.Valid();

        try
        {
            return await m_settings.ValidateBarcodeAsync.Invoke(barcodeScanResult.Barcode.RawValue) ??
                   BarcodeScanValidationResult.Invalid();
        }
        catch (Exception exception)
        {
            DUILogService.LogError<BarcodeScanner>($"Barcode validation failed: {exception.Message}");
            return BarcodeScanValidationResult.Invalid();
        }
    }

    private async Task<bool> AcceptBarcodeScanResultAsync(BarcodeScanResult barcodeScanResult, BarcodeScanRectangleOverlay? scanRectangleOverlay)
    {
        State = BarcodeScannerState.SuccessAnimating;

        var successAnimationTask = scanRectangleOverlay?.PlaySuccessAndResetAsync() ?? Task.CompletedTask;
        if (ShouldShowProgressCounter && scanRectangleOverlay is not null)
        {
            var counterCenter = m_progressController.GetCounterCenterRelativeTo(scanRectangleOverlay);
            await scanRectangleOverlay.PlayCollectionTokenAsync(counterCenter);
        }

        if (m_isDisposed)
            return false;

        m_settings.CurrentScanCount++;
        await m_progressController.AnimateCounterChangedAsync();

        if (m_isDisposed)
            return false;

        await NotifyValidBarcodeScannedAsync(barcodeScanResult);

        if (m_isDisposed)
            return false;

        if (IsRequiredScanCountCompleted())
        {
            await successAnimationTask;
            if (m_isDisposed)
                return false;

            await CompleteRequiredScanCountAsync();
            return false;
        }

        await successAnimationTask;
        return !m_isDisposed;
    }

    private async Task<bool> RejectBarcodeScanResultAsync(BarcodeScanResult barcodeScanResult, BarcodeScanValidationResult validationResult, BarcodeScanRectangleOverlay? scanRectangleOverlay)
    {
        State = BarcodeScannerState.FailureAnimating;

        if (scanRectangleOverlay is not null)
        {
            await scanRectangleOverlay.PlayFailureAndResetAsync(validationResult.ErrorMessage);
        }

        if (m_isDisposed)
            return false;

        await NotifyInvalidBarcodeScannedAsync(barcodeScanResult, validationResult);
        return !m_isDisposed;
    }

    private async Task NotifyValidBarcodeScannedAsync(BarcodeScanResult barcodeScanResult)
    {
        try
        {
            m_settings.OnValidBarcodeScanned?.Invoke(barcodeScanResult);
            if (m_settings.OnValidBarcodeScannedAsync is not null)
            {
                await m_settings.OnValidBarcodeScannedAsync.Invoke(barcodeScanResult);
            }
        }
        catch (Exception exception)
        {
            DUILogService.LogError<BarcodeScanner>($"Barcode accepted callback failed: {exception.Message}");
        }
    }

    private async Task NotifyInvalidBarcodeScannedAsync(BarcodeScanResult barcodeScanResult, BarcodeScanValidationResult validationResult)
    {
        try
        {
            if (m_settings.OnInvalidBarcodeScannedAsync is not null)
            {
                await m_settings.OnInvalidBarcodeScannedAsync.Invoke(barcodeScanResult, validationResult);
            }
        }
        catch (Exception exception)
        {
            DUILogService.LogError<BarcodeScanner>($"Barcode rejected callback failed: {exception.Message}");
        }
    }

    private async Task CompleteRequiredScanCountAsync()
    {
        if (m_isDisposed || m_hasReportedCompletion)
            return;

        m_hasReportedCompletion = true;
        State = BarcodeScannerState.Completed;

        await m_progressController.AnimateCompletedAsync();

        try
        {
            if (m_settings.OnRequiredScanCountCompletedAsync is not null)
            {
                await m_settings.OnRequiredScanCountCompletedAsync.Invoke();
            }
        }
        catch (Exception exception)
        {
            DUILogService.LogError<BarcodeScanner>($"Barcode completion callback failed: {exception.Message}");
        }

        if (m_settings.StopScanningWhenCompleted)
        {
            m_stopScanning.Invoke();
        }
    }

    private bool IsRequiredScanCountCompleted()
    {
        return m_settings.RequiredScanCount is > 0 &&
               m_settings.CurrentScanCount >= m_settings.RequiredScanCount.Value;
    }
}