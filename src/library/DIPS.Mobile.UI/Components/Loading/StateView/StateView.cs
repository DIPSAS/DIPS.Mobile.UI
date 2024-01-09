namespace DIPS.Mobile.UI.Components.Loading.StateView
{
    [ContentProperty(nameof(DefaultView))]
    public partial class StateView : ContentView
    {
        private View? m_currentViewVisible;
    
        private async Task OnStateChanged()
        {
            var viewToDisplay = GetViewByState();
            
            if (m_currentViewVisible is null)
            {
                m_currentViewVisible = viewToDisplay;
                Content = viewToDisplay;
                return;
            }

            await FadeOut(m_currentViewVisible);

            viewToDisplay.Opacity = 0;
            Content = viewToDisplay;

            await FadeIn(viewToDisplay);

            m_currentViewVisible = viewToDisplay;
        }

        private View GetViewByState()
            =>
                CurrentState switch
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
    }
}