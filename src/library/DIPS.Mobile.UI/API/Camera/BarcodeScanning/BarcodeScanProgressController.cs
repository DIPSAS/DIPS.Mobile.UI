using DIPS.Mobile.UI.API.Camera.Preview;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

internal sealed class BarcodeScanProgressController
{
    private readonly BarcodeScanningSettings m_settings;
    private CameraPreview? m_cameraPreview;
    private BarcodeScanProgressView? m_progressView;

    public BarcodeScanProgressController(BarcodeScanningSettings settings)
    {
        m_settings = settings;
        m_settings.ProgressChanged += OnProgressChanged;
    }

    public void Attach(CameraPreview cameraPreview)
    {
        m_cameraPreview = cameraPreview;
        UpdateVisibility();
    }

    public void Detach()
    {
        if (!MainThread.IsMainThread)
        {
            MainThread.BeginInvokeOnMainThread(Detach);
            return;
        }

        m_settings.ProgressChanged -= OnProgressChanged;
        RemoveProgressView();
        m_cameraPreview = null;
    }

    public async Task AnimateCounterChangedAsync()
    {
        UpdateCounterText();

        if (m_progressView is null)
            return;

        await m_progressView.AnimateCounterChangedAsync();
    }

    public async Task AnimateCompletedAsync()
    {
        UpdateCounterText();

        if (m_progressView is null)
            return;

        await m_progressView.AnimateCompletedAsync();
    }

    public Point? GetCounterCenterRelativeTo(VisualElement relativeTo)
    {
        return m_progressView?.GetCounterCenterRelativeTo(relativeTo);
    }

    private void OnProgressChanged()
    {
        if (!MainThread.IsMainThread)
        {
            MainThread.BeginInvokeOnMainThread(OnProgressChanged);
            return;
        }

        UpdateVisibility();
        UpdateCounterText();
    }

    private void UpdateVisibility()
    {
        if (m_cameraPreview is null)
            return;

        if (!MainThread.IsMainThread)
        {
            MainThread.BeginInvokeOnMainThread(UpdateVisibility);
            return;
        }

        if (ShouldShowProgressCounter)
        {
            AddProgressView();
        }
        else
        {
            RemoveProgressView();
        }
    }

    private void AddProgressView()
    {
        if (m_progressView is not null)
            return;

        m_progressView = new BarcodeScanProgressView();
        m_cameraPreview?.AddBottomToolbarView(m_progressView);
        UpdateCounterText();
    }

    private void RemoveProgressView()
    {
        m_cameraPreview?.RemoveBottomToolbarView(m_progressView);
        m_progressView = null;
    }

    private void UpdateCounterText()
    {
        if (m_progressView is not null && m_settings.RequiredScanCount is > 0)
        {
            m_progressView.UpdateCounter(m_settings.CurrentScanCount, m_settings.RequiredScanCount.Value);
        }
    }

    private bool ShouldShowProgressCounter => m_settings.RequiredScanCount.GetValueOrDefault() > 0;
}