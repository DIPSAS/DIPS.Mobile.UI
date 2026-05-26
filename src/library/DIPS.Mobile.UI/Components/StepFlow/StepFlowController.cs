using System.Collections.ObjectModel;
using DIPS.Mobile.UI.MVVM;

namespace DIPS.Mobile.UI.Components.StepFlow;

/// <summary>
/// Plain CLR object that owns the state of a <see cref="StepFlow"/>. The controller is
/// fully view-agnostic — it does not hold references to MAUI views — which makes it trivially
/// unit testable. The container subscribes to its events and animates accordingly.
/// </summary>
/// <remarks>
/// Consumers usually own a single <see cref="StepFlowController"/> on their view model and
/// drive the flow imperatively via <see cref="CompleteCurrent"/>, <see cref="GoTo"/> and
/// <see cref="Reset"/>.
/// </remarks>
public class StepFlowController : ViewModel
{
    private readonly ObservableCollection<StepFlowItemState> m_states = new();
    private readonly ReadOnlyObservableCollection<StepFlowItemState> m_readOnlyStates;
    private int m_currentIndex = -1;
    private int m_stepCount;

    public StepFlowController()
    {
        m_readOnlyStates = new ReadOnlyObservableCollection<StepFlowItemState>(m_states);
    }

    /// <summary>The number of steps in the flow. Set by the container when items are attached.</summary>
    public int StepCount
    {
        get => m_stepCount;
        internal set => RaiseWhenSet(ref m_stepCount, value);
    }

    /// <summary>The index of the currently <see cref="StepFlowItemState.Active"/> step, or <c>-1</c> if none.</summary>
    public int CurrentIndex
    {
        get => m_currentIndex;
        private set => RaiseWhenSet(ref m_currentIndex, value);
    }

    /// <summary>Read-only snapshot of every step's current state. Indexed by step position.</summary>
    public IReadOnlyList<StepFlowItemState> States => m_readOnlyStates;

    /// <summary><c>true</c> when every step is <see cref="StepFlowItemState.Completed"/>.</summary>
    public bool IsCompleted => m_stepCount > 0 && m_states.All(s => s == StepFlowItemState.Completed);

    /// <summary>If <c>true</c>, completing a step automatically activates the next non-completed step after <see cref="AutoAdvanceDelay"/>.</summary>
    public bool AutoAdvance { get; set; } = true;

    /// <summary>Delay between completing a step and auto-advancing. Defaults to 800 ms.</summary>
    public TimeSpan AutoAdvanceDelay { get; set; } = TimeSpan.FromMilliseconds(800);

    /// <summary>Raised when a step transitions to <see cref="StepFlowItemState.Completed"/>.</summary>
    public event EventHandler<StepFlowEventArgs>? StepCompleted;

    /// <summary>Raised when a step transitions to <see cref="StepFlowItemState.Active"/>.</summary>
    public event EventHandler<StepFlowEventArgs>? StepActivated;

    /// <summary>Raised when the last step has been completed.</summary>
    public event EventHandler? FlowCompleted;

    /// <summary>Raised whenever any step's state changes — useful for the container to update views.</summary>
    internal event EventHandler<StepFlowEventArgs>? StateChanged;

    /// <summary>
    /// Marks <see cref="CurrentIndex"/> as <see cref="StepFlowItemState.Completed"/> and, if
    /// <see cref="AutoAdvance"/> is <c>true</c>, activates the next non-completed step after
    /// <see cref="AutoAdvanceDelay"/>.
    /// </summary>
    public void CompleteCurrent()
    {
        if (m_currentIndex < 0 || m_currentIndex >= m_stepCount) return;
        Complete(m_currentIndex);
    }

    /// <summary>Marks the step at <paramref name="index"/> as <see cref="StepFlowItemState.Completed"/>.</summary>
    public void Complete(int index)
    {
        if (!IsValidIndex(index)) return;
        if (m_states[index] == StepFlowItemState.Completed) return;

        SetStateInternal(index, StepFlowItemState.Completed);

        if (m_currentIndex == index)
        {
            CurrentIndex = -1;
        }

        StepCompleted?.Invoke(this, new StepFlowEventArgs(index));

        if (IsCompleted)
        {
            FlowCompleted?.Invoke(this, EventArgs.Empty);
            return;
        }

        if (AutoAdvance)
        {
            ScheduleAutoAdvance();
        }
    }

    /// <summary>
    /// Activates the step at <paramref name="index"/>. No-op if the step is
    /// <see cref="StepFlowItemState.Disabled"/> or <see cref="StepFlowItemState.Completed"/>.
    /// </summary>
    public void GoTo(int index)
    {
        if (!IsValidIndex(index)) return;
        var state = m_states[index];
        if (state == StepFlowItemState.Completed) return;

        ActivateInternal(index);
    }

    /// <summary>Resets the flow: step 0 becomes <see cref="StepFlowItemState.Active"/>, the rest <see cref="StepFlowItemState.Disabled"/>.</summary>
    public void Reset()
    {
        if (m_stepCount == 0) return;
        for (var i = 0; i < m_stepCount; i++)
        {
            SetStateInternal(i, i == 0 ? StepFlowItemState.Active : StepFlowItemState.Disabled);
        }
        CurrentIndex = 0;
        StepActivated?.Invoke(this, new StepFlowEventArgs(0));
    }

    /// <summary>Explicitly sets a step's state. Use sparingly — prefer <see cref="GoTo"/>/<see cref="Complete"/>.</summary>
    public void SetState(int index, StepFlowItemState state)
    {
        if (!IsValidIndex(index)) return;
        if (m_states[index] == state) return;

        SetStateInternal(index, state);

        if (state == StepFlowItemState.Active)
        {
            // Enforce single-active invariant.
            for (var i = 0; i < m_stepCount; i++)
            {
                if (i != index && m_states[i] == StepFlowItemState.Active)
                {
                    SetStateInternal(i, StepFlowItemState.Disabled);
                }
            }
            CurrentIndex = index;
            StepActivated?.Invoke(this, new StepFlowEventArgs(index));
        }
        else if (state == StepFlowItemState.Completed)
        {
            if (m_currentIndex == index) CurrentIndex = -1;
            StepCompleted?.Invoke(this, new StepFlowEventArgs(index));
            if (IsCompleted) FlowCompleted?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>Re-applies the initial state (step 0 active, rest disabled). Called by the container after items attach.</summary>
    internal void Initialize(int stepCount)
    {
        StepCount = stepCount;
        m_states.Clear();
        for (var i = 0; i < stepCount; i++)
        {
            m_states.Add(i == 0 ? StepFlowItemState.Active : StepFlowItemState.Disabled);
        }
        CurrentIndex = stepCount > 0 ? 0 : -1;
        RaisePropertyChanged(nameof(States));
        RaisePropertyChanged(nameof(IsCompleted));
        if (stepCount > 0)
        {
            StepActivated?.Invoke(this, new StepFlowEventArgs(0));
        }
    }

    private void ScheduleAutoAdvance()
    {
        var capturedIndex = m_currentIndex;
        _ = Task.Run(async () =>
        {
            await Task.Delay(AutoAdvanceDelay);
            // Only auto-advance if no other navigation has happened in the meantime.
            if (m_currentIndex != capturedIndex) return;
            var next = FindNextEligible();
            if (next < 0) return;
            // Marshal to the main thread so view subscribers run on the UI thread.
            if (Application.Current?.Dispatcher is { } dispatcher)
            {
                dispatcher.Dispatch(() => ActivateInternal(next));
            }
            else
            {
                ActivateInternal(next);
            }
        });
    }

    private int FindNextEligible()
    {
        for (var i = 0; i < m_stepCount; i++)
        {
            if (m_states[i] != StepFlowItemState.Completed) return i;
        }
        return -1;
    }

    private void ActivateInternal(int index)
    {
        for (var i = 0; i < m_stepCount; i++)
        {
            if (i == index)
            {
                if (m_states[i] != StepFlowItemState.Active)
                    SetStateInternal(i, StepFlowItemState.Active);
            }
            else if (m_states[i] == StepFlowItemState.Active)
            {
                SetStateInternal(i, StepFlowItemState.Disabled);
            }
        }
        CurrentIndex = index;
        StepActivated?.Invoke(this, new StepFlowEventArgs(index));
    }

    private void SetStateInternal(int index, StepFlowItemState state)
    {
        m_states[index] = state;
        StateChanged?.Invoke(this, new StepFlowEventArgs(index));
        RaisePropertyChanged(nameof(States));
        RaisePropertyChanged(nameof(IsCompleted));
    }

    private bool IsValidIndex(int index) => index >= 0 && index < m_stepCount;
}
