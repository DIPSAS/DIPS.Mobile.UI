namespace DIPS.Mobile.UI.Components.Counters;

public partial class Counter
{
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(
        nameof(Value),
        typeof(int),
        typeof(Counter),
        defaultBindingMode: BindingMode.OneWay);

    public static readonly BindableProperty SecondaryValueProperty = BindableProperty.Create(
        nameof(SecondaryValue),
        typeof(int),
        typeof(Counter),
        defaultBindingMode: BindingMode.OneWay,
        propertyChanged: ((bindable, _, _) => ((Counter)bindable).OnSecondaryValueChanged()));

    public static readonly BindableProperty IsUrgentProperty = BindableProperty.Create(
        nameof(IsUrgent),
        typeof(bool),
        typeof(Counter),
        defaultBindingMode: BindingMode.OneWay,
        propertyChanged: (bindable, _, _) => ((Counter)bindable).OnIsUrgentChanged());

    public static readonly BindableProperty IsSecondaryUrgentProperty = BindableProperty.Create(
        nameof(IsSecondaryUrgent),
        typeof(bool),
        typeof(Counter),
        defaultBindingMode: BindingMode.OneWay,
        propertyChanged: (bindable, _, _) => ((Counter)bindable).OnIsSecondaryUrgentChanged());

    public static readonly BindableProperty ModeProperty = BindableProperty.Create(
        nameof(Mode),
        typeof(CounterDisplayMode),
        typeof(Counter),
        defaultBindingMode: BindingMode.OneWay,
        propertyChanged: (bindable, _, _) => ((Counter)bindable).OnModeChanged());

    public static readonly BindableProperty IsErrorProperty = BindableProperty.Create(
        nameof(IsError),
        typeof(bool),
        typeof(Counter),
        propertyChanged: (bindable, _, _) => ((Counter)bindable).OnIsErrorChanged());

    public static readonly BindableProperty IsSecondaryErrorProperty = BindableProperty.Create(
        nameof(IsSecondaryError),
        typeof(bool),
        typeof(Counter),
        propertyChanged: (bindable, _, _) => ((Counter)bindable).OnIsSecondaryErrorChanged());

    public bool IsSecondaryError
    {
        get => (bool)GetValue(IsSecondaryErrorProperty);
        set => SetValue(IsSecondaryErrorProperty, value);
    }    
    
    public bool IsError
    {
        get => (bool)GetValue(IsErrorProperty);
        set => SetValue(IsErrorProperty, value);
    }
    
    /// <summary>
    /// The mode determining how the primary and secondary counter should be displayed.
    /// </summary>
    public CounterDisplayMode Mode
    {
        get => (CounterDisplayMode)GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }
    
    /// <summary>
    /// Whether the primary value should have an urgent style.
    /// </summary>
    public bool IsUrgent
    {
        get => (bool)GetValue(IsUrgentProperty);
        set => SetValue(IsUrgentProperty, value);
    }
    
    /// <summary>
    /// Whether the secondary value should have an urgent style.
    /// </summary>
    public bool IsSecondaryUrgent
    {
        get => (bool)GetValue(IsSecondaryUrgentProperty);
        set => SetValue(IsSecondaryUrgentProperty, value);
    }
    
    /// <summary>
    /// The primary counter value.
    /// </summary>
    public int Value
    {
        get => (int)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
    
    /// <summary>
    /// The secondary counter value.
    /// </summary>
    public int SecondaryValue
    {
        get => (int)GetValue(SecondaryValueProperty);
        set => SetValue(SecondaryValueProperty, value);
    }
    
    
}