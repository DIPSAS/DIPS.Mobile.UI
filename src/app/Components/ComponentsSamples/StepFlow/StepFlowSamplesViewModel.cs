using System.Collections.ObjectModel;
using DIPS.Mobile.UI.Components.StepFlow;
using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.MVVM.Commands;

namespace Components.ComponentsSamples.StepFlow;

public class StepFlowSamplesViewModel : ViewModel
{
    private string m_patientName = "Ola Nordmann";
    private bool m_isPatientConfirmed;
    private bool m_isScanningDone;
    private bool m_isFlowFinished;
    private bool m_autoScrollIntoView = true;

    public StepFlowSamplesViewModel()
    {
        Flow = new StepFlowController();
        Flow.FlowCompleted += OnFlowCompleted;

        ConfirmPatientCommand = new AsyncCommand(ConfirmPatient);
        AddScannedLabelCommand = new AsyncCommand(AddScannedLabel);
        FinishScanningCommand = new AsyncCommand(FinishScanning, () => ScannedLabels.Count >= 3);
        SubmitSamplingCommand = new AsyncCommand(SubmitSampling);
        ResetCommand = new AsyncCommand(ResetFlow);
    }

    public StepFlowController Flow { get; }

    public ObservableCollection<string> ScannedLabels { get; } = new();

    public string PatientName
    {
        get => m_patientName;
        set => RaiseWhenSet(ref m_patientName, value);
    }

    public bool IsPatientConfirmed
    {
        get => m_isPatientConfirmed;
        private set => RaiseWhenSet(ref m_isPatientConfirmed, value);
    }

    public bool IsScanningDone
    {
        get => m_isScanningDone;
        private set => RaiseWhenSet(ref m_isScanningDone, value);
    }

    public bool IsFlowFinished
    {
        get => m_isFlowFinished;
        private set => RaiseWhenSet(ref m_isFlowFinished, value);
    }

    public bool AutoScrollIntoView
    {
        get => m_autoScrollIntoView;
        set => RaiseWhenSet(ref m_autoScrollIntoView, value);
    }

    public AsyncCommand ConfirmPatientCommand { get; }
    public AsyncCommand AddScannedLabelCommand { get; }
    public AsyncCommand FinishScanningCommand { get; }
    public AsyncCommand SubmitSamplingCommand { get; }
    public AsyncCommand ResetCommand { get; }

    private Task ConfirmPatient()
    {
        IsPatientConfirmed = true;
        Flow.CompleteCurrent();
        return Task.CompletedTask;
    }

    private Task AddScannedLabel()
    {
        ScannedLabels.Add($"BC-{(ScannedLabels.Count + 1):000}");
        FinishScanningCommand.RaiseCanExecuteChanged();
        return Task.CompletedTask;
    }

    private Task FinishScanning()
    {
        IsScanningDone = true;
        Flow.CompleteCurrent();
        return Task.CompletedTask;
    }

    private async Task SubmitSampling()
    {
        Flow.CompleteCurrent();
        await Task.CompletedTask;
    }

    private async Task ResetFlow()
    {
        IsPatientConfirmed = false;
        IsScanningDone = false;
        IsFlowFinished = false;
        ScannedLabels.Clear();
        FinishScanningCommand.RaiseCanExecuteChanged();
        Flow.Reset();
        await Task.CompletedTask;
    }

    private void OnFlowCompleted(object? sender, EventArgs e)
    {
        IsFlowFinished = true;
        if (Application.Current?.Windows.FirstOrDefault()?.Page is { } page)
        {
            _ = page.DisplayAlert("Done", "Sampling completed!", "OK");
        }
    }
}
