using DIPS.Mobile.UI.Components;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Searching
{
    public partial class SearchBar : View, IReloadFocusPreservable
    {
        private bool m_hasFocus;

        public CancellationTokenSource? SearchCancellationToken { get; private set; }

        public SearchBar()
        {
#if __ANDROID__
            this.SetBinding(BackgroundColorProperty, static (SearchBar searchBar) => searchBar.BarColor, source: this);
#endif
            
            Unloaded += OnUnloaded;
        }


        public new void Focus()
        {
            if (Handler is SearchBarHandler searchBarHandler)
            {
                searchBarHandler.Focus();
            }
        }

        public new void Unfocus()
        {
            if (Handler is SearchBarHandler searchBarHandler)
            {
                searchBarHandler.UnFocus();
            }
        }

        private void OnUnloaded(object? sender, EventArgs e)
        {
            Unfocus();
            Unloaded -= OnUnloaded;
        }


        private async void OnTextChanged(string newTextValue, string oldTextValue)
        {
            SearchCancellationToken?.Cancel(); //Cancel the previous search
            SearchCancellationToken = new CancellationTokenSource();

            try
            {
                if (ShouldDelay && Delay > 0)
                {
                    await Task.Delay(Delay, SearchCancellationToken.Token);
                }

                TextChanged?.Invoke(this, new TextChangedEventArgs(oldTextValue, newTextValue));
            }
            catch (TaskCanceledException) //This means that people has initiated a new search
            {
                //Swallow it
            }
        }

        internal void SendFocused()
        {
            m_hasFocus = true;
            Focused?.Invoke(this, EventArgs.Empty);
        }

        internal void SendUnfocused()
        {
            m_hasFocus = false;
            Unfocused?.Invoke(this, EventArgs.Empty);
        }

        bool IReloadFocusPreservable.HasPreservedFocus => ShouldPreserveFocusOnCollectionViewReload && m_hasFocus;

        bool IReloadFocusPreservable.TryRestoreFocus()
        {
            if (!ShouldPreserveFocusOnCollectionViewReload || Handler is not SearchBarHandler)
                return false;

            Focus();
            return true;
        }
    }
}