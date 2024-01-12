namespace DIPS.Mobile.UI.Components.Loading.StateView;

public partial class StateView
{
    /// <summary>
    /// The view to display when the <see cref="State"/> is in Default
    /// </summary>
    /// <remarks>Override this to customize to your liking, otherwise will use a default implementation</remarks>
    public IView DefaultView
    {
        get => (IView)GetValue(DefaultViewProperty);
        set => SetValue(DefaultViewProperty, value);
    }

    /// <summary>
    /// The view to display when the <see cref="State"/> is in Loading
    /// </summary>
    /// <remarks>Override this to customize to your liking, otherwise will use a default implementation</remarks>
    public IView LoadingView
    {
        get => (IView)GetValue(LoadingViewProperty);
        set => SetValue(LoadingViewProperty, value);
    }

    /// <summary>
    /// The view to display when the <see cref="State"/> is in Error
    /// </summary>
    /// <remarks>Override this to customize to your liking, otherwise will use a default implementation</remarks>
    public IView ErrorView
    {
        get => (IView)GetValue(ErrorViewProperty);
        set => SetValue(ErrorViewProperty, value);
    }

    /// <summary>
    /// The view to display when the <see cref="State"/> is in Empty
    /// </summary>
    /// <remarks>Override this to customize to your liking, otherwise will use a default implementation</remarks>
    public IView EmptyView
    {
        get => (IView)GetValue(EmptyViewProperty);
        set => SetValue(EmptyViewProperty, value);
    }

    /// <summary>
    /// Determines if the <see cref="StateView"/> should fade between the different views
    /// </summary>
    public bool ShouldFadeBetweenStates
    {
        get => (bool)GetValue(ShouldFadeBetweenStatesProperty);
        set => SetValue(ShouldFadeBetweenStatesProperty, value);
    }

    /// <summary>
    /// The view model to configure <see cref="StateView"/>
    /// </summary>
    /// <remarks><b>NB!</b> This must be made in your viewmodel</remarks>
    public StateViewModel? StateViewModel
    {
        get => (StateViewModel)GetValue(StateViewModelProperty);
        set => SetValue(StateViewModelProperty, value);
    }

    /// <summary>
    /// When the CurrentState in StateViewModel changes to the same, determines whether <see cref="OnStateChanged"/> should be called. This can be useful if <see cref="ShouldFadeBetweenStates"/> is true and you want an effect that the view has been updated for instance.
    /// </summary>
    public bool ShouldUpdateViewWhenStateSetToSame
    {
        get => (bool)GetValue(ShouldUpdateViewWhenStateChangedToSameProperty);
        set => SetValue(ShouldUpdateViewWhenStateChangedToSameProperty, value);
    }
    
    public static readonly BindableProperty DefaultViewProperty = BindableProperty.Create(
        nameof(DefaultView),
        typeof(IView),
        typeof(StateView));
    
    public static readonly BindableProperty LoadingViewProperty = BindableProperty.Create(
        nameof(LoadingView),
        typeof(IView),
        typeof(StateView),
        new LoadingView());
    
    public static readonly BindableProperty ErrorViewProperty = BindableProperty.Create(
        nameof(ErrorView),
        typeof(IView),
        typeof(StateView),
        new ErrorView());
    
    public static readonly BindableProperty EmptyViewProperty = BindableProperty.Create(
        nameof(EmptyView),
        typeof(IView),
        typeof(StateView),
        new EmptyView());

    public static readonly BindableProperty StateViewModelProperty = BindableProperty.Create(
        nameof(StateViewModel),
        typeof(StateViewModel),
        typeof(StateView),
        defaultBindingMode: BindingMode.OneWay,
        propertyChanged: (bindable, _, _) => ((StateView)bindable).OnStateViewModelChanged());

    public static readonly BindableProperty ShouldFadeBetweenStatesProperty = BindableProperty.Create(
        nameof(ShouldFadeBetweenStates),
        typeof(bool),
        typeof(StateView),
        defaultValue: true);
    
    public static readonly BindableProperty ShouldUpdateViewWhenStateChangedToSameProperty = BindableProperty.Create(
        nameof(ShouldUpdateViewWhenStateSetToSame),
        typeof(bool),
        typeof(StateView),
        false);
    
    
}