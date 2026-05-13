using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

internal sealed class BarcodeScanSession
{
    private readonly BarcodeScannerStartOptions m_startOptions;
    private readonly BarcodeScanProgress? m_progress;
    private readonly BarcodeScanProgressController m_progressController;
    private readonly Func<Task> m_stopCameraAnalysisAsync;
    private bool m_hasReportedCompletion;
    private bool m_isDisposed;

    public BarcodeScanSession(BarcodeScannerStartOptions startOptions, Func<Task> stopCameraAnalysisAsync)
    {
        m_startOptions = startOptions;
        m_progress = startOptions.Completion is not null ? new BarcodeScanProgress(startOptions.Completion) : null;
        m_stopCameraAnalysisAsync = stopCameraAnalysisAsync;
        m_progressController = new BarcodeScanProgressController(m_progress);
    }

    public BarcodeScannerState State { get; private set; } = BarcodeScannerState.Idle;

    public bool CanProcessBarcode => !m_isDisposed && State == BarcodeScannerState.Scanning;

    private bool ShouldShowProgressCounter => m_progress?.ShouldShowCounter == true;

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

    public void PauseScanning()
    {
        if (m_isDisposed || State == BarcodeScannerState.Completed)
            return;

        State = BarcodeScannerState.Paused;
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
        if (m_isDisposed || State == BarcodeScannerState.Paused)
            return false;

        State = BarcodeScannerState.Validating;
        var validationResult = await ValidateBarcodeScanResultAsync(barcodeScanResult);

        if (m_isDisposed || State == BarcodeScannerState.Paused)
            return false;

        barcodeScanResult.SetValidationResult(validationResult);

        if (validationResult.IsValid)
        {
            return await AcceptBarcodeScanResultAsync(barcodeScanResult, scanRectangleOverlay);
        }

        return await RejectBarcodeScanResultAsync(barcodeScanResult, validationResult, scanRectangleOverlay);
    }

    private async Task<BarcodeScanValidationResult> ValidateBarcodeScanResultAsync(BarcodeScanResult barcodeScanResult)
    {
        if (m_startOptions.ValidateBarcodeAsync is null)
            return BarcodeScanValidationResult.Valid();

        try
        {
            return await m_startOptions.ValidateBarcodeAsync.Invoke(barcodeScanResult.Barcode.RawValue) ??
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

        m_progress?.Increment();

        await m_progressController.AnimateCounterChangedAsync();

        if (m_isDisposed)
            return false;

        await NotifyBarcodeAcceptedAsync(barcodeScanResult);

        if (m_isDisposed)
            return false;

        if (IsScanCountCompleted())
        {
            await successAnimationTask;
            if (m_isDisposed)
                return false;

            await CompleteScanCountAsync();
            return false;
        }

        await successAnimationTask;
        return !m_isDisposed && State != BarcodeScannerState.Paused;
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

        await NotifyBarcodeRejectedAsync(barcodeScanResult, validationResult);
        return !m_isDisposed && State != BarcodeScannerState.Paused;
    }

    private async Task NotifyBarcodeAcceptedAsync(BarcodeScanResult barcodeScanResult)
    {
        try
        {
            if (m_startOptions.OnBarcodeAcceptedAsync is not null)
            {
                await m_startOptions.OnBarcodeAcceptedAsync.Invoke(barcodeScanResult);
            }
        }
        catch (Exception exception)
        {
            DUILogService.LogError<BarcodeScanner>($"Barcode accepted callback failed: {exception.Message}");
        }
    }

    private async Task NotifyBarcodeRejectedAsync(BarcodeScanResult barcodeScanResult, BarcodeScanValidationResult validationResult)
    {
        try
        {
            if (m_startOptions.OnBarcodeRejectedAsync is not null)
            {
                await m_startOptions.OnBarcodeRejectedAsync.Invoke(barcodeScanResult, validationResult);
            }
        }
        catch (Exception exception)
        {
            DUILogService.LogError<BarcodeScanner>($"Barcode rejected callback failed: {exception.Message}");
        }
    }

    private async Task CompleteScanCountAsync()
    {
        if (m_isDisposed || m_hasReportedCompletion)
            return;

        m_hasReportedCompletion = true;
        State = BarcodeScannerState.Completed;

        await m_progressController.AnimateCompletedAsync();
        await m_stopCameraAnalysisAsync.Invoke();

        try
        {
            if (m_startOptions.Completion?.OnCompletedAsync is not null)
            {
                await m_startOptions.Completion.OnCompletedAsync.Invoke();
            }
        }
        catch (Exception exception)
        {
            DUILogService.LogError<BarcodeScanner>($"Barcode completion callback failed: {exception.Message}");
        }
    }

    private bool IsScanCountCompleted()
    {
        return m_progress?.IsCompleted == true;
    }
}
