using System.Collections.ObjectModel;
using DIPS.Mobile.UI.Components.StepFlow;
using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.MVVM.Commands;

namespace Components.ComponentsSamples.StepFlow;

public class StepFlowSamplesViewModel : ViewModel
{
    private const int CompleteSamplingStepIndex = 3;

    private string m_patientName = "Ola Nordmann";
    private bool m_isPatientConfirmed;
    private bool m_isScanningDone;
    private bool m_isFlowFinished;
    private bool m_autoScrollIntoView = true;
    private bool m_canGoBack = true;
    private bool m_canActivateCompleteSamplingStep;

    public StepFlowSamplesViewModel()
    {
        Flow = new StepFlowController();
        Flow.StepActivated += OnStepActivated;
        Flow.FlowCompleted += OnFlowCompleted;

        ConfirmPatientCommand = new AsyncCommand(ConfirmPatient);
        AddScannedLabelCommand = new AsyncCommand(AddScannedLabel);
        FinishScanningCommand = new AsyncCommand(FinishScanning, () => ScannedLabels.Count >= 3);
        ConfirmSampleDetailsCommand = new AsyncCommand(ConfirmSampleDetails);
        SubmitSamplingCommand = new AsyncCommand(SubmitSampling);
        ResetCommand = new AsyncCommand(ResetFlow);
    }

    public StepFlowController Flow { get; }

    public ObservableCollection<string> ScannedLabels { get; } = new();

    public ObservableCollection<StepFlowSampleRequisitionViewModel> Requisitions
    {
        get;
        private set => RaiseWhenSet(ref field, value);
    } = [];

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

    public bool CanGoBack
    {
        get => m_canGoBack;
        set => RaiseWhenSet(ref m_canGoBack, value);
    }

    public bool CanComplete => ScannedLabels.Count >= 3;

    public bool CanActivateCompleteSamplingStep
    {
        get => m_canActivateCompleteSamplingStep;
        set => RaiseWhenSet(ref m_canActivateCompleteSamplingStep, value);
    }

    public AsyncCommand ConfirmPatientCommand { get; }
    public AsyncCommand AddScannedLabelCommand { get; }
    public AsyncCommand FinishScanningCommand { get; }
    public AsyncCommand ConfirmSampleDetailsCommand { get; }
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
        RaisePropertyChanged(nameof(CanComplete));
        return Task.CompletedTask;
    }

    private Task FinishScanning()
    {
        IsScanningDone = true;
        Flow.CompleteCurrent();
        return Task.CompletedTask;
    }

    private async Task ConfirmSampleDetails()
    {
        ClearCompleteSamplingContent();
        CanActivateCompleteSamplingStep = true;
        Flow.CompleteCurrent();
        await Task.CompletedTask;
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
        ClearCompleteSamplingContent();
        CanActivateCompleteSamplingStep = false;
        FinishScanningCommand.RaiseCanExecuteChanged();
        RaisePropertyChanged(nameof(CanComplete));
        Flow.Reset();
        await Task.CompletedTask;
    }

    private void ClearCompleteSamplingContent()
    {
        Requisitions = [];
    }

    private void ActivateCompleteSamplingStep()
    {
        // Repro detail: populate the final step dynamically from StepActivated, after the
        // StepFlowItem has started activating. Keep the template intentionally simple: one box
        // per label added in the previous step.
        Requisitions = new ObservableCollection<StepFlowSampleRequisitionViewModel>(CreateRequisitions());
    }

    private StepFlowSampleRequisitionViewModel[] CreateRequisitions() => ScannedLabels
        .Select((label, index) => new StepFlowSampleRequisitionViewModel($"{index + 1}. {label}"))
        .ToArray();

    private void OnStepActivated(object? sender, StepFlowEventArgs e)
    {
        if (e.Index == CompleteSamplingStepIndex)
        {
            ActivateCompleteSamplingStep();
        }
    }

    private void OnFlowCompleted(object? sender, EventArgs e)
    {
        IsFlowFinished = true;
        if (Application.Current?.Windows.FirstOrDefault()?.Page is { } page)
        {
            _ = page.DisplayAlertAsync("Done", "Sampling completed!", "OK");
        }
    }
}

public class StepFlowSampleRequisitionViewModel(string title)
{
    public string Title { get; } = title;
}


