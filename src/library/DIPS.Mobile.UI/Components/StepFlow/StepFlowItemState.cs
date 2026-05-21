namespace DIPS.Mobile.UI.Components.StepFlow;

/// <summary>
/// Represents the lifecycle state of a single <see cref="StepFlowItem"/> in a <see cref="StepFlow"/>.
/// </summary>
public enum StepFlowItemState
{
    /// <summary>The step is visible but cannot be interacted with.</summary>
    Disabled,

    /// <summary>The step is currently expanded and the user is working on it.</summary>
    Active,

    /// <summary>The step has been finished. Collapsed and (by default) locked from interaction.</summary>
    Completed,

    /// <summary>The step is in an error state. Visually distinct from <see cref="Active"/>.</summary>
    Error
}
