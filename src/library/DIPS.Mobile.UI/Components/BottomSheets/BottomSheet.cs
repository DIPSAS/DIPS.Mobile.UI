using System.Collections.ObjectModel;
using DIPS.Mobile.UI.Components.BottomSheets.Header;
using DIPS.Mobile.UI.Internal;
using Colors = Microsoft.Maui.Graphics.Colors;
using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    public partial class BottomSheet : ContentView
    {
        internal const double BottomBarHeight = 120;

        internal static ColorName BackgroundColorName => ColorName.color_surface_default;
        internal static ColorName ToolbarTextColorName => ColorName.color_text_default;
        internal static ColorName ToolbarActionButtonsName => ColorName.color_text_action;

        public BottomSheet()
        {
            this.SetAppThemeColor(BackgroundColorProperty, BackgroundColorName);

            BottombarButtons = new ObservableCollection<Button>();

            BottomSheetHeaderBehavior = new BottomSheetHeaderBehavior();

            SearchBar = new SearchBar { AutomationId = "SearchBar".ToDUIAutomationId<BottomSheet>(), HasCancelButton = false, BackgroundColor = Colors.Transparent, ReturnKeyType = ReturnType.Done };
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
        
        public Grid CreateBottomBar()
        {
            var grid = new Grid
            {
                AutomationId = "BottomBarGrid".ToDUIAutomationId<BottomSheet>(),
                ColumnSpacing = Sizes.GetSize(SizeName.content_margin_small), 
                RowDefinitions = [new RowDefinition(GridLength.Star)],
                Padding = new Thickness(Sizes.GetSize(SizeName.content_margin_medium), Sizes.GetSize(SizeName.content_margin_medium), Sizes.GetSize(SizeName.content_margin_medium), Sizes.GetSize(SizeName.page_margin_xlarge)),
                Background = new LinearGradientBrush
                {
                    EndPoint = new Point(0, 1),
                    GradientStops =
                    [
                        new GradientStop { Color = BackgroundColor.WithAlpha(0), Offset = .0f },
                        new GradientStop { Color = BackgroundColor, Offset = .25f }
                    ]
                }
            };
       
            foreach (var button in BottombarButtons)
            {
                grid.AddColumnDefinition(new ColumnDefinition(GridLength.Star));
                var index = grid.ColumnDefinitions.Count - 1;
                grid.Add(button, index);
                button.AutomationId = $"BottomBarButton{index}".ToDUIAutomationId<BottomSheet>();
            }
        
            grid.BindingContext = BindingContext;
        
            return grid;
        }

        protected override void OnHandlerChanging(HandlerChangingEventArgs args)
        {
            base.OnHandlerChanging(args);
            if (args.NewHandler == null) //Disconnect
            {
                SearchBar.TextChanged -= OnSearchTextChanged;
            }
        }
    }
}