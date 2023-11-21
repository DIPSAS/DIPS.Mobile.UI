namespace DIPS.Mobile.UI.Components.Searching
{
    public partial class SearchBar : View
    {
        public CancellationTokenSource? SearchCancellationToken { get; private set; }

        public SearchBar()
        {
            this.SetAppThemeColor(IconsColorProperty, ColorName.color_neutral_60);
            this.SetAppThemeColor(TextColorProperty, ColorName.color_neutral_60);
            this.SetAppThemeColor(iOSSearchFieldBackgroundColorProperty, ColorName.color_neutral_05);

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
            Focused?.Invoke(this, EventArgs.Empty);
        }
    }
}