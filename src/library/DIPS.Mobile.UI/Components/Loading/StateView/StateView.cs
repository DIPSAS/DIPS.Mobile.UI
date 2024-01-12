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
        
        public async void OnStateChanged(State state)
        {
            var viewToDisplay = GetViewByState(state);
            
            if (m_currentViewVisible is null)
            {
                m_currentViewVisible = viewToDisplay;
                Content = viewToDisplay;
                return;
            }
            
            if (!ShouldUpdateViewWhenStateSetToSame && m_lastState == state)
                return;

            m_lastState = state;

            await FadeOut(m_currentViewVisible);

            viewToDisplay.Opacity = 0;
            Content = viewToDisplay;

            await FadeIn(viewToDisplay);

            m_currentViewVisible = viewToDisplay;
        }

        private View GetViewByState(State state)
            =>
                state switch
                {
                    State.Default => (View)DefaultView,
                    State.Loading => (View)LoadingView,
                    State.Error => (View)ErrorView,
                    State.Empty => (View)EmptyView,
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

            if (ErrorView is ErrorView errorView)
            {
                errorView.BindingContext = StateViewModel.Error;
            }

            if (LoadingView is LoadingView loadingView)
            {
                loadingView.BindingContext = StateViewModel.Loading;
            }

            if (EmptyView is EmptyView emptyView)
            {
                emptyView.BindingContext = StateViewModel.Empty;
            }
            
            StateViewModel.OnStateChanged -= OnStateChanged;
            StateViewModel.OnStateChanged += OnStateChanged;
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