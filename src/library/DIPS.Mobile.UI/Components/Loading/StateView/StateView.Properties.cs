using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Loading.StateView;

public partial class StateView
{
    public State CurrentState
    {
        get => (State)GetValue(CurrentStateProperty);
        set => SetValue(CurrentStateProperty, value);
    }

    public IView DefaultView
    {
        get => (IView)GetValue(DefaultViewProperty);
        set => SetValue(DefaultViewProperty, value);
    }

    public IView LoadingView
    {
        get => (IView)GetValue(LoadingViewProperty);
        set => SetValue(LoadingViewProperty, value);
    }

    public IView ErrorView
    {
        get => (IView)GetValue(ErrorViewProperty);
        set => SetValue(ErrorViewProperty, value);
    }

    public IView EmptyView
    {
        get => (IView)GetValue(EmptyViewProperty);
        set => SetValue(EmptyViewProperty, value);
    }

    public bool ShouldFadeBetweenStates
    {
        get => (bool)GetValue(ShouldFadeBetweenStatesProperty);
        set => SetValue(ShouldFadeBetweenStatesProperty, value);
    }

    public StateViewModel StateViewModel
    {
        get => (StateViewModel)GetValue(StateViewModelProperty);
        set => SetValue(StateViewModelProperty, value);
    }
    
    public static readonly BindableProperty DefaultViewProperty = BindableProperty.Create(
        nameof(DefaultView),
        typeof(IView),
        typeof(StateView),
        defaultValue: new Label{ Text = "Remember to set the default view", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center});
    
    public static readonly BindableProperty LoadingViewProperty = BindableProperty.Create(
        nameof(LoadingView),
        typeof(IView),
        typeof(StateView),
        defaultValue: new ActivityIndicator
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            IsRunning = true
        });
    
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
        defaultValue: new StateViewModel(),
        BindingMode.OneWayToSource);
    
    public static readonly BindableProperty ShouldFadeBetweenStatesProperty = BindableProperty.Create(
        nameof(ShouldFadeBetweenStates),
        typeof(bool),
        typeof(StateView),
        defaultValue: true);
    
    public static readonly BindableProperty CurrentStateProperty = BindableProperty.Create(
        nameof(CurrentState),
        typeof(State),
        typeof(StateView),
        propertyChanged: (bindable, _, _) => _ = ((StateView)bindable).OnStateChanged(),
        defaultBindingMode: BindingMode.OneWay,
        defaultValue: State.None);

    private static View CreateAndSetBindingContext<T>(BindableObject bindableObject) where T : View, new()
    {
        if (bindableObject is not StateView stateView)
            return null!;

        var view = new T();

        view.BindingContext = view switch
        {
            Loading.StateView.ErrorView => stateView.StateViewModel.Error,
            Loading.StateView.EmptyView => stateView.StateViewModel.Empty,
            _ => view.BindingContext
        };

        return view;
    }
}