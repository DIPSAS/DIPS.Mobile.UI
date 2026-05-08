namespace DIPS.Mobile.UI.Components.StepFlow;

public partial class StepFlow
{
    /// <summary>
    /// The controller that owns the flow's state. If <c>null</c>, the container creates a default
    /// controller automatically. Consumers should normally supply their own.
    /// </summary>
    public StepFlowController? Controller
    {
        get => (StepFlowController?)GetValue(ControllerProperty);
        set => SetValue(ControllerProperty, value);
    }

    /// <summary>
    /// When <c>true</c>, tapping a <see cref="StepFlowItemState.Disabled"/> step's header activates
    /// it. Defaults to <c>false</c> — the controller normally drives activation.
    /// </summary>
    public bool AllowDirectStepActivation
    {
        get => (bool)GetValue(AllowDirectStepActivationProperty);
        set => SetValue(AllowDirectStepActivationProperty, value);
    }

    /// <summary>
    /// When <c>true</c> (the default), the <see cref="StepFlow"/> automatically scrolls its
    /// closest ancestor <see cref="Microsoft.Maui.Controls.ScrollView"/> so that the newly
    /// activated <see cref="StepFlowItem"/> is pinned to the top of the scroller (using
    /// <see cref="ScrollToPosition.Start"/>) — ensuring the freshly expanded body is fully
    /// visible. If no ancestor <see cref="Microsoft.Maui.Controls.ScrollView"/> exists, this
    /// property has no effect.
    /// </summary>
    public bool AutoScrollIntoView
    {
        get => (bool)GetValue(AutoScrollIntoViewProperty);
        set => SetValue(AutoScrollIntoViewProperty, value);
    }

    /// <summary>Raised when the controller's <see cref="StepFlowController.FlowCompleted"/> fires.</summary>
    public event EventHandler? FlowCompleted;

    public static readonly BindableProperty ControllerProperty = BindableProperty.Create(
        nameof(Controller),
        typeof(StepFlowController),
        typeof(StepFlow),
        propertyChanged: (b, oldVal, newVal) => ((StepFlow)b).OnControllerChanged(oldVal as StepFlowController, newVal as StepFlowController));

    public static readonly BindableProperty AllowDirectStepActivationProperty = BindableProperty.Create(
        nameof(AllowDirectStepActivation),
        typeof(bool),
        typeof(StepFlow),
        defaultValue: false);

    public static readonly BindableProperty AutoScrollIntoViewProperty = BindableProperty.Create(
        nameof(AutoScrollIntoView),
        typeof(bool),
        typeof(StepFlow),
        defaultValue: true);
}
