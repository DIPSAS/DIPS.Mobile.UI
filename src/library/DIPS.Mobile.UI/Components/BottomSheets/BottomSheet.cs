using System.Collections.ObjectModel;
using Colors = Microsoft.Maui.Graphics.Colors;
using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    public partial class BottomSheet : ContentView
    {
        internal static ColorName BackgroundColorName => ColorName.color_system_white;
        internal static ColorName ToolbarTextColorName => ColorName.color_system_black;
        internal static ColorName ToolbarActionButtonsName => ColorName.color_primary_90;

        public BottomSheet()
        {
            this.SetAppThemeColor(BackgroundColorProperty, BackgroundColorName);

            ToolbarItems = new ObservableCollection<ToolbarItem>();
        }
        
        internal SearchBar? SearchBar { get; private set; }
        
        internal bool ShouldHaveNavigationBar => !string.IsNullOrEmpty(Title) || ToolbarItems is { Count: > 0 };
        
        internal void SendClose()
        {
            Closed?.Invoke(this, EventArgs.Empty);
            ClosedCommand?.Execute(null);
            OnClosed();
        }

        internal void SendOpen()
        {
            Opened?.Invoke(this, EventArgs.Empty);
            OpenedCommand?.Execute(null);
            OnOpened();
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
        
        protected virtual void OnClosed()
        {
        }

        protected virtual void OnOpened()
        {
        }

        protected virtual void OnSearchTextChanged(string value)
        {
        }
    }
}