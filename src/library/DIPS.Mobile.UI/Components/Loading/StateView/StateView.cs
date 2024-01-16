using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Loading.StateView
{
    [ContentProperty(nameof(DefaultView))]
    public partial class StateView : ContentView
    {
        private View? m_currentViewVisible;
        private State? m_lastState;

        public StateView()
        {
            Content = new Label
            {
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Style = Styles.GetLabelStyle(LabelStyle.Header500),
                Text = "You have not created a StateViewModel in your ViewModel or not bound to it"
            };
        }
        
        private async void OnStateChanged(State state)
        {
            var viewToDisplayAndItsViewModel = GetViewAndViewModelByState(state);

            if (ShouldWrapRefreshViewAroundView(viewToDisplayAndItsViewModel.Item2))
            {
                var refreshView = new RefreshView { Content = viewToDisplayAndItsViewModel.Item1 };
                refreshView.SetBinding(Microsoft.Maui.Controls.RefreshView.CommandProperty, new Binding(nameof(StateViewModel.RefreshCommand), source: StateViewModel));
                refreshView.SetBinding(Microsoft.Maui.Controls.RefreshView.IsRefreshingProperty, new Binding(nameof(StateViewModel.IsRefreshing), source: StateViewModel));
                viewToDisplayAndItsViewModel.Item1 = refreshView;
            }
            
            if (m_currentViewVisible is null)
            {
                m_currentViewVisible = viewToDisplayAndItsViewModel.Item1;
                Content = viewToDisplayAndItsViewModel.Item1;
                return;
            }
            
            if (!ShouldUpdateViewWhenStateSetToSame && m_lastState == state)
                return;

            m_lastState = state;

            await FadeOut(m_currentViewVisible);

            viewToDisplayAndItsViewModel.Item1.Opacity = 0;
            Content = viewToDisplayAndItsViewModel.Item1;

            await FadeIn(viewToDisplayAndItsViewModel.Item1);

            m_currentViewVisible = viewToDisplayAndItsViewModel.Item1;
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

                if (m_currentViewVisible is RefreshView refreshView)
                {
                    refreshView.Command = null;
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