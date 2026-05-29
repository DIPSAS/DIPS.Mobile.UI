using System.Collections.ObjectModel;
using DIPS.Mobile.UI.API.Diagnostics;
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

        /// <summary>
        /// Pushes new content onto the bottom sheet's internal navigation stack.
        /// The content will be displayed with an animated transition and a back button to return.
        /// </summary>
        /// <param name="content">The view to display.</param>
        /// <param name="title">The title to display in the navigation bar for the pushed content.</param>
public async Task PushAsync(View content, string? title = null)
{
    var entry = new BottomSheetNavigationEntry(content, title);
    NavigationStack.Push(entry);
    try
    {
        await PlatformPushAsync(content, title);
    }
    catch
    {
        NavigationStack.Pop();
        throw;
    }
}

        /// <summary>
        /// Pops the current content from the bottom sheet's internal navigation stack and returns to the previous content.
        /// </summary>
        public async Task PopAsync()
        {
            if (NavigationStack.Count == 0) return;
            var popped = NavigationStack.Pop();
            await PlatformPopAsync(popped);
        }

        /// <summary>
        /// Whether there is content that can be popped from the navigation stack.
        /// </summary>
        public bool CanPopNavigation => NavigationStack.Count > 0;
        
        internal Stack<BottomSheetNavigationEntry> NavigationStack { get; } = new();

        /// <summary>
        /// Called by platform code when the user interactively pops (e.g., iOS swipe-back gesture).
        /// Keeps the managed navigation stack in sync.
        /// </summary>
        internal void HandleInteractivePop(View content)
        {
            if (NavigationStack.TryPeek(out var top) && top.Content == content)
            {
                NavigationStack.Pop();
            }
        }
        
        private partial Task PlatformPushAsync(View content, string? title);
        private partial Task PlatformPopAsync(BottomSheetNavigationEntry popped);

        internal SearchBar SearchBar { get; private set; }

        internal void SendClose()
        {
            LayoutDiagnosticsService.EndSnapshot();
            
            Closed?.Invoke(this, EventArgs.Empty);
            ClosedCommand?.Execute(null);
            OnClosed();
        }

        internal void SendOpen()
        {
            LayoutDiagnosticsService.BeginSnapshot($"BottomSheet: {GetType().Name}");
            
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
                },
                SafeAreaEdges = SafeAreaEdges.None
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