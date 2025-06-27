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

    public bool IsSecondaryUrgent
    {
        get => (bool)GetValue(IsSecondaryUrgentProperty);
        set => SetValue(IsSecondaryUrgentProperty, value);
    }

    public static readonly BindableProperty ModeProperty = BindableProperty.Create(
        nameof(Mode),
        typeof(CounterDisplayMode),
        typeof(Counter),
        defaultBindingMode: BindingMode.OneWay,
        propertyChanged: (bindable, _, _) => ((Counter)bindable).OnModeChanged());

    public CounterDisplayMode Mode
    {
        get => (CounterDisplayMode)GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }
    
    public bool IsUrgent
    {
        get => (bool)GetValue(IsUrgentProperty);
        set => SetValue(IsUrgentProperty, value);
    }
    
    public int SecondaryValue
    {
        get => (int)GetValue(SecondaryValueProperty);
        set => SetValue(SecondaryValueProperty, value);
    }
    
    public int Value
    {
        get => (int)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
}