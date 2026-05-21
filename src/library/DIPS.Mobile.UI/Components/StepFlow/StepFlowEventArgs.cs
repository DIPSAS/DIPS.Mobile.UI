namespace DIPS.Mobile.UI.Components.StepFlow;

/// <summary>
/// Event arguments raised by <see cref="StepFlowController"/> when a step transitions state.
/// </summary>
public class StepFlowEventArgs : EventArgs
{
    /// <summary>The zero-based index of the step that changed.</summary>
    public int Index { get; }

    public StepFlowEventArgs(int index)
    {
        Index = index;
    }
}
