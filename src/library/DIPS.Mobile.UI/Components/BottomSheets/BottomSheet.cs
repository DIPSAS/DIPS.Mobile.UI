using System.Collections.ObjectModel;
using Colors = Microsoft.Maui.Graphics.Colors;
using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    public partial class BottomSheet : ContentView
    {
        internal const int BottomBarHeight = 120;
        internal static ColorName BackgroundColorName => ColorName.color_system_white;
        internal static ColorName ToolbarTextColorName => ColorName.color_system_black;
        internal static ColorName ToolbarActionButtonsName => ColorName.color_primary_90;

        public BottomSheet()
        {
            this.SetAppThemeColor(BackgroundColorProperty, BackgroundColorName);

            ToolbarItems = new ObservableCollection<ToolbarItem>();
            BottombarButtons = new ObservableCollection<Button>();

            SearchBar = new SearchBar {HasCancelButton = false, BackgroundColor = Colors.Transparent};
            SearchBar.TextChanged += OnSearchTextChanged;
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

        internal SearchBar SearchBar { get; private set; }

        internal bool ShouldHaveNavigationBar => !string.IsNullOrEmpty(Title) || ToolbarItems is {Count: > 0};

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

        protected override void OnHandlerChanging(HandlerChangingEventArgs args)
        {
            base.OnHandlerChanging(args);
            if (args.NewHandler == null) //Disconnect
            {
                SearchBar.TextChanged -= OnSearchTextChanged;
            }
        }

        internal Border CreateBottomBar()
        {
            var border = new Border
            {
                Padding = Sizes.GetSize(SizeName.size_3),
                StrokeThickness = 0,
                VerticalOptions = LayoutOptions.End,
                HeightRequest = BottomBarHeight,
                Background = new LinearGradientBrush()
                {
                    EndPoint = new Point(0, 1),
                    GradientStops = new GradientStopCollection()
                    {
                        new() {Color = this.BackgroundColor.WithAlpha(0), Offset = 0.00f},
                        new() {Color = this.BackgroundColor, Offset = 0.22f}
                    }
                }
            };
            var grid = new Grid
            {
                ColumnSpacing = Sizes.GetSize(SizeName.size_2), VerticalOptions = LayoutOptions.End,
                RowDefinitions = new RowDefinitionCollection(){new(GridLength.Star)}
            };
            foreach (var button in BottombarButtons)
            {
                grid.AddColumnDefinition(new ColumnDefinition(GridLength.Star));
                grid.Add(button, grid.ColumnDefinitions.Count - 1);
            }

            border.Content = grid;
            border.BindingContext = BindingContext;
            return border;
        }
    }
}