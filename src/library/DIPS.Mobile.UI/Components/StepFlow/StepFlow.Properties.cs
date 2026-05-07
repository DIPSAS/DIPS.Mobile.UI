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
}
