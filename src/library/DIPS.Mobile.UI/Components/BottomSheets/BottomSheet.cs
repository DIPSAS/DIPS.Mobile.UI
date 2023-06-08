using Colors = Microsoft.Maui.Graphics.Colors;
using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    public partial class BottomSheet : ContentView
    {
        internal SearchBar? SearchBar { get; private set; }
        
        public void Close()
        {
            WillClose?.Invoke(this, EventArgs.Empty);
            OnWillClose();
        }

        internal void SendDidClose()
        {
            DidClose?.Invoke(this, EventArgs.Empty);
            OnDidClose();
        }

        private static void OnHasSearchBarChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is not BottomSheet bottomSheet)
                return;

            if (newValue is true)
            {
                bottomSheet.SearchBar = new SearchBar { HasCancelButton = false, BackgroundColor = Colors.Transparent };
                bottomSheet.SearchBar.TextChanged += bottomSheet.OnSearchTextChanged;
            }
            else
            {
                bottomSheet.SearchBar!.TextChanged -= bottomSheet.OnSearchTextChanged;
                bottomSheet.SearchBar = null;
            }
        }

        private void OnSearchTextChanged(object? sender, TextChangedEventArgs args)
        {
            SearchTextChanged?.Invoke(SearchBar, args);
            SearchCommand?.Execute(args.NewTextValue);
            OnSearchTextChanged(args.NewTextValue);
        }

        protected virtual void OnSearchTextChanged(string value)
        {
        }
    }
}