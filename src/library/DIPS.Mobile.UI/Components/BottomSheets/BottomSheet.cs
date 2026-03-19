using System.Collections.ObjectModel;
using DIPS.Mobile.UI.Components.BottomSheets.Header;
using DIPS.Mobile.UI.Internal;

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
        /// Action delegate for platform-specific search focus. Set by platform handlers/controllers.
        /// </summary>
        internal Action? FocusSearchAction { get; set; }
        
        /// <summary>
        /// Action delegate for platform-specific search unfocus. Set by platform handlers/controllers.
        /// </summary>
        internal Action? UnfocusSearchAction { get; set; }
        
        /// <summary>
        /// Focuses the native search field.
        /// </summary>
        internal void FocusSearch() => FocusSearchAction?.Invoke();
        
        /// <summary>
        /// Unfocuses the native search field.
        /// </summary>
        internal void UnfocusSearch() => UnfocusSearchAction?.Invoke();

        /// <summary>
        /// Called by native search controllers when the search text changes.
        /// Routes the change through the existing events and virtual method.
        /// </summary>
        internal void OnNativeSearchTextChanged(string newValue, string oldValue)
        {
            SearchTextChanged?.Invoke(this, new TextChangedEventArgs(oldValue, newValue));
            SearchCommand?.Execute(newValue);
            OnSearchTextChanged(newValue);
        }

        /// <summary>
        /// Called by the native search field when it gains focus.
        /// </summary>
        internal void OnSearchFieldFocused()
        {
            if (Positioning is Positioning.Large)
                return;
            
            Positioning = Positioning.Large;
        }

        /// <summary>
        /// Called by the native search field when it loses focus.
        /// </summary>
        internal void OnSearchFieldUnfocused()
        {
            Positioning = Positioning.Medium;
        }

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
                FocusSearchAction = null;
                UnfocusSearchAction = null;
            }
        }
    }
}
