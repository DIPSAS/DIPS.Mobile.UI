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
            
            SearchBar = new SearchBar { HasCancelButton = false, BackgroundColor = Colors.Transparent };
            SearchBar.TextChanged += OnSearchTextChanged;
            Unloaded += OnUnLoaded;
        }
        
        /// <summary>
        /// <see cref="BottomSheetService.Close"/>
        /// </summary>
        /// <param name="animated"></param>
        /// <returns></returns>
        public Task Close(bool animated = true)
        {
            return BottomSheetService.Close(this, animated);
        }

        /// <summary>
        /// <see cref="BottomSheetService.Open"/>
        /// </summary>
        /// <returns></returns>
        public Task Open()
        {
            return BottomSheetService.Open(this);
        }

        private void OnUnLoaded(object? sender, EventArgs e)
        {
            Unloaded -= OnUnLoaded;
            SearchBar.TextChanged -= OnSearchTextChanged;
        }
        
        internal SearchBar SearchBar { get; private set; }
        
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