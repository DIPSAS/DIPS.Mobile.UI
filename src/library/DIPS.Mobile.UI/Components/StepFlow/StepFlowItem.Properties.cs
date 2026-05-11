namespace DIPS.Mobile.UI.Components.StepFlow;

public partial class StepFlowItem
{
    /// <summary>The header text shown next to the indicator.</summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>Optional smaller text rendered below the title in the header.</summary>
    public string? Subtitle
    {
        get => (string?)GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }

    /// <summary>
    /// The content rendered inside the step's body when it is <see cref="StepFlowItemState.Active"/>.
    /// This is the property that XAML default-content is assigned to.
    /// </summary>
    /// <remarks>
    /// This property intentionally hides <see cref="ContentView.Content"/>. We never expose a
    /// <c>[ContentProperty]</c> that conflicts with a layout's <c>Children</c> collection — using
    /// a ContentView shell with an explicit <c>Content</c> property avoids the reparenting trap
    /// observed in the Arena Mobil prototype.
    /// </remarks>
    public new View? Content
    {
        get => (View?)GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    /// <summary>Optional template for the leading indicator. Defaults to a numbered/check circle.</summary>
    public DataTemplate? IndicatorTemplate
    {
        get => (DataTemplate?)GetValue(IndicatorTemplateProperty);
        set => SetValue(IndicatorTemplateProperty, value);
    }

    /// <summary>When <c>true</c> (the default), tapping a completed step does nothing.</summary>
    public bool LockWhenCompleted
    {
        get => (bool)GetValue(LockWhenCompletedProperty);
        set => SetValue(LockWhenCompletedProperty, value);
    }

    /// <summary>
    /// The current lifecycle state. Driven by the owning <see cref="StepFlow"/>'s controller —
    /// consumers should not normally write this directly. Useful for read-only DataTriggers in
    /// custom <see cref="IndicatorTemplate"/> definitions.
    /// </summary>
    public StepFlowItemState State
    {
        get => (StepFlowItemState)GetValue(StateProperty);
        set => SetValue(StateProperty, value);
    }

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(StepFlowItem),
        defaultValue: string.Empty,
        propertyChanged: (b, _, _) => ((StepFlowItem)b).OnTitleChanged());

    public static readonly BindableProperty SubtitleProperty = BindableProperty.Create(
        nameof(Subtitle),
        typeof(string),
        typeof(StepFlowItem),
        defaultValue: null,
        propertyChanged: (b, _, _) => ((StepFlowItem)b).OnSubtitleChanged());

    public new static readonly BindableProperty ContentProperty = BindableProperty.Create(
        nameof(Content),
        typeof(View),
        typeof(StepFlowItem),
        propertyChanged: (b, _, _) => ((StepFlowItem)b).OnContentChanged());

    public static readonly BindableProperty IndicatorTemplateProperty = BindableProperty.Create(
        nameof(IndicatorTemplate),
        typeof(DataTemplate),
        typeof(StepFlowItem),
        propertyChanged: (b, _, _) => ((StepFlowItem)b).OnIndicatorTemplateChanged());

    public static readonly BindableProperty LockWhenCompletedProperty = BindableProperty.Create(
        nameof(LockWhenCompleted),
        typeof(bool),
        typeof(StepFlowItem),
        defaultValue: true);

    public static readonly BindableProperty StateProperty = BindableProperty.Create(
        nameof(State),
        typeof(StepFlowItemState),
        typeof(StepFlowItem),
        defaultValue: StepFlowItemState.Disabled,
        defaultBindingMode: BindingMode.OneWayToSource,
        propertyChanged: (b, oldVal, newVal) => ((StepFlowItem)b).OnStateChanged((StepFlowItemState)oldVal, (StepFlowItemState)newVal));
}
