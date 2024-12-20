using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Loading.StateView
{
    [ContentProperty(nameof(DefaultView))]
    public partial class StateView : Grid
    {
        private View? m_currentViewVisible;
        private State? m_lastState;

        public StateView()
        {
            Add(new Label
            {
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Style = Styles.GetLabelStyle(LabelStyle.Header500),
                Text = "You have not created a StateViewModel in your ViewModel or not bound to it"
            });
        }
        
        private async void OnStateChanged(State state)
        {
            var viewToDisplayAndItsViewModel = GetViewAndViewModelByState(state);

            if (m_currentViewVisible is null)
            {
                m_currentViewVisible = viewToDisplayAndItsViewModel.Item1;
                SetContent(viewToDisplayAndItsViewModel.Item1, ShouldWrapRefreshViewAroundView(viewToDisplayAndItsViewModel.Item2));
                return;
            }
            
            if (!ShouldUpdateViewWhenStateSetToSame && m_lastState == state)
                return;

            m_lastState = state;

            await FadeOut(m_currentViewVisible);

            viewToDisplayAndItsViewModel.Item1.Opacity = 0;
            SetContent(viewToDisplayAndItsViewModel.Item1, ShouldWrapRefreshViewAroundView(viewToDisplayAndItsViewModel.Item2));

            await FadeIn(viewToDisplayAndItsViewModel.Item1);

            m_currentViewVisible = viewToDisplayAndItsViewModel.Item1;
        }

        private void SetContent(View view, bool wrapRefreshViewAround)
        {
            if (wrapRefreshViewAround)
            {
                var refreshView = new RefreshView();
                refreshView.SetBinding(Microsoft.Maui.Controls.RefreshView.CommandProperty, static (StateViewModel stateViewModel) => stateViewModel.RefreshCommand, source: StateViewModel);
                refreshView.SetBinding(Microsoft.Maui.Controls.RefreshView.IsRefreshingProperty, static (StateViewModel stateViewModel) => stateViewModel.IsRefreshing, source: StateViewModel);
                refreshView.Content = view;
                refreshView.Unloaded += RefreshViewOnUnloaded;
                Clear();
                Add(refreshView);
            }
            else
            {
                Clear();
                Add(view);
            }
        }

        private void RefreshViewOnUnloaded(object? sender, EventArgs e)
        {
            if (sender is not RefreshView refreshView)
                return;

            refreshView.Command = null;
            refreshView.Handler?.DisconnectHandler();
            refreshView.Unloaded -= RefreshViewOnUnloaded;
        }

        private bool ShouldWrapRefreshViewAroundView(IRefreshableViewModel? refreshAbleViewModel) =>
            refreshAbleViewModel?.HasRefreshView ?? false;

        private (View, IRefreshableViewModel?) GetViewAndViewModelByState(State state)
            =>
                state switch
                {
                    State.Default => ((View)DefaultView, StateViewModel?.Default),
                    State.Loading => ((View)LoadingView!, null),
                    State.Error => ((View)ErrorView!, StateViewModel?.Error),
                    State.Empty => ((View)EmptyView!, StateViewModel?.Empty),
                    _ => throw new ArgumentOutOfRangeException()
                };

        private async Task FadeOut(IView? view)
        {
            if (view is View viewToFade)
            {
                if (ShouldFadeBetweenStates)
                {
                    await viewToFade.FadeTo(0);
                }
                else
                {
                    viewToFade.Opacity = 0;
                }
            }
        }
        
        private async Task FadeIn(IView? view)
        {
            if (view is View viewToFade)
            {
                if (ShouldFadeBetweenStates)
                {
                    await viewToFade.FadeTo(1);
                }
                else
                {
                    viewToFade.Opacity = 1;
                }
            }
        }

        private void OnStateViewModelChanged()
        {
            if(StateViewModel is null)
                return;

            ErrorView ??= new ErrorView { BindingContext = StateViewModel.Error };
            LoadingView ??= new LoadingView { BindingContext = StateViewModel.Loading };
            EmptyView ??= new EmptyView { BindingContext = StateViewModel.Empty };
            
            StateViewModel.OnStateChanged += OnStateChanged;
            
            OnStateChanged(StateViewModel.CurrentState);
        }

        protected override void OnHandlerChanging(HandlerChangingEventArgs args)
        {
            base.OnHandlerChanging(args);

            if (args.NewHandler is null && StateViewModel is not null)
            {
                StateViewModel.OnStateChanged -= OnStateChanged;
            }
        }
    }
}