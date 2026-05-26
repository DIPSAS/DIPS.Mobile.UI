namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

internal sealed class BarcodeScanProgress
{
    public BarcodeScanProgress(BarcodeScanCompletionOptions completionOptions)
    {
        RequiredCount = completionOptions.RequiredCount;
        var initialCount = Math.Max(0, completionOptions.InitialCount);
        CurrentCount = RequiredCount is > 0 ? Math.Min(initialCount, RequiredCount.Value) : initialCount;
    }

    public int? RequiredCount { get; }

    public int CurrentCount { get; private set; }

    public bool ShouldShowCounter => RequiredCount is > 0;

    public bool IsCompleted => RequiredCount is > 0 && CurrentCount >= RequiredCount.Value;

    public event Action? ProgressChanged;

    public void Increment()
    {
        CurrentCount++;
        ProgressChanged?.Invoke();
    }
}
