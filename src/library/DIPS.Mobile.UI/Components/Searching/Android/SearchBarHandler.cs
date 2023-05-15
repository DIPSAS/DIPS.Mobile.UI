using Android.Graphics;
using Android.Widget;
using DIPS.Mobile.UI.Components.Searching.Android;
using DIPS.Mobile.UI.Extensions.Android;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Button = Microsoft.Maui.Controls.Button;
using AView = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Searching
{
    internal partial class SearchBarHandler : ViewHandler<SearchBar, AView>
    {
        private Microsoft.Maui.Controls.SearchBar InternalSearchBar { get; }
        private IndeterminateProgressBar ProgressBar { get; }
        private Button CancelButton { get; }
        private Grid OuterGrid { get; }

        public SearchBarHandler() : base(s_propertyMapper)
        {
            OuterGrid = new Grid()
            {
                ColumnDefinitions =
                    new ColumnDefinitionCollection() {new() {Width = GridLength.Star}, new() {Width = GridLength.Auto}},
                RowDefinitions =
                    new RowDefinitionCollection() {new() {Height = GridLength.Auto}, new() {Height = GridLength.Auto}},
                RowSpacing = 0,
                ColumnSpacing = 0,
            };

            //Add Maui Search bar
            InternalSearchBar = new Microsoft.Maui.Controls.SearchBar();
            OuterGrid.Add(InternalSearchBar, 0);

            //Add button to cancel the search
            CancelButton = new DIPS.Mobile.UI.Components.Buttons.Button
            {
                Text = DUILocalizedStrings.Cancel,
                BackgroundColor = Colors.Transparent,
                VerticalOptions = LayoutOptions.Center,
            };
            OuterGrid.Add(CancelButton, 1);

            //Add progressbar
            ProgressBar = new IndeterminateProgressBar();
            Grid.SetColumnSpan(ProgressBar, 2);
            OuterGrid.Add(ProgressBar, 0, 1);
        }

        protected override AView CreatePlatformView() => OuterGrid.ToContainerView(MauiContext!);


        protected override void ConnectHandler(AView platformView)
        {
            base.ConnectHandler(platformView);

            InternalSearchBar.TextChanged += SearchBarTextChanged;
            if (InternalSearchBar.Handler != null)
            {
                if (InternalSearchBar.Handler.PlatformView is MauiSearchView mauiSearchView)
                {
                    //Fixes this issue : https://github.com/dotnet/maui/issues/10823
                    mauiSearchView.MaxWidth = int.MaxValue;
                }
            }
        }

        protected override void DisconnectHandler(AView platformView)
        {
            base.DisconnectHandler(platformView);
            InternalSearchBar.TextChanged -= SearchBarTextChanged;
        }

        private void SearchBarTextChanged(object? sender, TextChangedEventArgs e)
        {
            VirtualView.Text = e.NewTextValue;
        }

        private static readonly IPropertyMapper<SearchBar, SearchBarHandler> s_propertyMapper =
            new PropertyMapper<SearchBar, SearchBarHandler>(ViewMapper)
            {
                [nameof(SearchBar.HasCancelButton)] = MapHasCancelButton,
                [nameof(SearchBar.HasBusyIndication)] = MapHasBusyIndication,
                [nameof(SearchBar.IsBusy)] = MapIsBusy,
                [nameof(SearchBar.BarColor)] = MapBarColor,
                [nameof(SearchBar.IconsColor)] = MapIconsColor,
                [nameof(SearchBar.TextColor)] = MapTextColor,
                [nameof(SearchBar.CancelButtonTextColor)] = MapCancelButtonTextColor,
                [nameof(SearchBar.PlaceholderColor)] = MapPlaceholderColor,
                [nameof(SearchBar.Placeholder)] = MapPlaceholder,
                [nameof(SearchBar.CancelCommand)] = MapCancelCommand,
                [nameof(SearchBar.CancelCommandParameter)] = MapCancelCommandParameter,
                [nameof(SearchBar.SearchCommand)] = MapSearchCommand,
            };

        private static void MapCancelButtonTextColor(SearchBarHandler handler, SearchBar searchBar)
        {
            handler.CancelButton.TextColor = searchBar.CancelButtonTextColor;
        }

        private static void MapSearchCommand(SearchBarHandler handler, SearchBar searchBar)
        {
            handler.InternalSearchBar.SearchCommand = searchBar.SearchCommand;
        }

        private static void MapHasBusyIndication(SearchBarHandler handler, SearchBar searchBar)
        {
            handler.ProgressBar.IsVisible = searchBar.HasBusyIndication;
        }

        private static void MapCancelCommandParameter(SearchBarHandler handler, SearchBar searchBar)
        {
            handler.CancelButton.CommandParameter = searchBar.CancelCommandParameter;
        }

        private static void MapCancelCommand(SearchBarHandler handler, SearchBar searchBar)
        {
            handler.CancelButton.Command = searchBar.CancelCommand;
        }

        private static void MapPlaceholderColor(SearchBarHandler handler, SearchBar searchBar)
        {
            handler.InternalSearchBar.PlaceholderColor = searchBar.PlaceholderColor;
        }

        private static void MapPlaceholder(SearchBarHandler handler, SearchBar searchbar)
        {
            handler.InternalSearchBar.Placeholder = searchbar.Placeholder;
        }

        private static void MapTextColor(SearchBarHandler handler, SearchBar searchBar)
        {
            handler.CancelButton.TextColor = searchBar.TextColor;
            handler.InternalSearchBar.TextColor = searchBar.TextColor;
        }

        private static void MapBarColor(SearchBarHandler handler, SearchBar searchBar)
        {
            handler.InternalSearchBar.BackgroundColor = searchBar.BarColor;
            handler.ProgressBar.TrackColor = searchBar.BarColor;
            handler.OuterGrid.BackgroundColor = searchBar.BarColor;
        }

        private static void MapIsBusy(SearchBarHandler handler, SearchBar searchBar)
        {
            handler.ProgressBar.IsRunning = searchBar.IsBusy;
        }

        private static void MapIconsColor(SearchBarHandler handler, SearchBar searchBar)
        {
            var internalSearchBar = handler.InternalSearchBar;

            if (searchBar.IconsColor == null ||
                internalSearchBar.Handler is not Microsoft.Maui.Handlers.SearchBarHandler mauiSearchBarHandler)
                return;

            //Set color of icons in the search bar
            foreach (var view in mauiSearchBarHandler.PlatformView.GetFlatViewHierarchyCollection())
            {
                if (view is ImageView imageView)
                {
                    imageView.SetColorFilter(searchBar.IconsColor.ToPlatform());
                }
            }

            //Change color of cursor on the search text edit to the same color as the text color
            if (mauiSearchBarHandler.QueryEditor is AutoCompleteTextView autoCompleteTextView)
            {
#pragma warning disable CS0618
                if (PorterDuff.Mode.SrcIn != null)
                {
                    autoCompleteTextView.TextCursorDrawable?.SetColorFilter(searchBar.IconsColor.ToPlatform(),
                        PorterDuff.Mode.SrcIn);
                }
#pragma warning restore CS0618
            }
        }

        private static void MapHasCancelButton(SearchBarHandler handler, SearchBar searchBar)
        {
            handler.CancelButton.IsVisible = searchBar.HasCancelButton;
        }
    }
}