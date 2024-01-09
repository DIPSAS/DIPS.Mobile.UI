using Label = DIPS.Mobile.UI.Components.Labels.Label;

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
    public StateViewModel StateViewModel
    {
        get => (StateViewModel)GetValue(StateViewModelProperty);
        set => SetValue(StateViewModelProperty, value);
    }
    
    public static readonly BindableProperty DefaultViewProperty = BindableProperty.Create(
        nameof(DefaultView),
        typeof(IView),
        typeof(StateView),
        defaultValueCreator: CreateAndSetBindingContext<DefaultView>);
    
    public static readonly BindableProperty LoadingViewProperty = BindableProperty.Create(
        nameof(LoadingView),
        typeof(IView),
        typeof(StateView),
        defaultValueCreator: CreateAndSetBindingContext<LoadingView>);
    
    public static readonly BindableProperty ErrorViewProperty = BindableProperty.Create(
        nameof(ErrorView),
        typeof(IView),
        typeof(StateView),
        defaultValueCreator: CreateAndSetBindingContext<ErrorView>);
    
    public static readonly BindableProperty EmptyViewProperty = BindableProperty.Create(
        nameof(EmptyView),
        typeof(IView),
        typeof(StateView),
        defaultValueCreator: CreateAndSetBindingContext<EmptyView>);
    
    public static readonly BindableProperty StateViewModelProperty = BindableProperty.Create(
        nameof(StateViewModel),
        typeof(StateViewModel),
        typeof(StateView),
        defaultBindingMode:BindingMode.OneWayToSource,
        defaultValueCreator: bindable =>  bindable is not StateView stateView ? null! : new StateViewModel(stateView));

    public static readonly BindableProperty ShouldFadeBetweenStatesProperty = BindableProperty.Create(
        nameof(ShouldFadeBetweenStates),
        typeof(bool),
        typeof(StateView),
        defaultValue: true);
    
    private static View CreateAndSetBindingContext<T>(BindableObject bindableObject) where T : View, new()
    {
        if (bindableObject is not StateView stateView)
            return null!;

        var view = new T();

        view.BindingContext = view switch
        {
            Loading.StateView.ErrorView => stateView.StateViewModel.Error,
            Loading.StateView.EmptyView => stateView.StateViewModel.Empty,
            Loading.StateView.LoadingView => stateView.StateViewModel.Loading,
            Loading.StateView.DefaultView => stateView.StateViewModel.Default,
            _ => view.BindingContext
        };

        return view;
    }
}