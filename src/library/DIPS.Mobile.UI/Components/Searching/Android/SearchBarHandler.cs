using Android.Content.Res;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.Components.Searching.Android;
using DIPS.Mobile.UI.Extensions.Android;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Button = Microsoft.Maui.Controls.Button;
using AView = Android.Views.View;
using Color = Microsoft.Maui.Graphics.Color;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Components.Searching
{
    internal partial class SearchBarHandler : ViewHandler<SearchBar, AView>
    {
        private Microsoft.Maui.Controls.SearchBar InternalSearchBar { get; set; }
        private IndeterminateProgressBar ProgressBar { get; set; }
        private Button CancelButton { get; set; }
        private Grid OuterGrid { get; set; }

        private partial void Construct()
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
 
            AppendToPropertyMapper();
        }

        private static void AppendToPropertyMapper()
        {
            SearchBarPropertyMapper.Add(nameof(SearchBar.CancelCommand), MapCancelCommand);
            SearchBarPropertyMapper.Add(nameof(SearchBar.CancelCommandParameter), MapCancelCommandParameter);
            SearchBarPropertyMapper.Add(nameof(SearchBar.SearchCommand), MapSearchCommand);
            SearchBarPropertyMapper.Add(nameof(SearchBar.AndroidBusyBackgroundColor), MapAndroidBusyBackgroundColor);
            SearchBarPropertyMapper.Add(nameof(SearchBar.AndroidBusyColor), MapAndroidBusyColor);
            SearchBarPropertyMapper.Add(nameof(SearchBar.SearchCommand), MapSearchCommand);
        }

        private static void MapAndroidBusyColor(SearchBarHandler handler, SearchBar searchBar)
        {
            handler.ProgressBar.IndicatorColor = searchBar.AndroidBusyColor ??
                                             DIPS.Mobile.UI.Resources.Colors.Colors
                                                 .GetColor(ColorName.color_primary_90);
        }

        private static void MapAndroidBusyBackgroundColor(SearchBarHandler handler, SearchBar searchBar)
        {
            handler.ProgressBar.TrackColor = searchBar.AndroidBusyBackgroundColor ?? DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_neutral_30);
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
            SetHorizontalLineColor(handler, searchBar.TextColor);
           
        }

        private static void SetHorizontalLineColor(SearchBarHandler handler, Color color)
        {
            if (handler.InternalSearchBar.Handler?.PlatformView is not ViewGroup androidView)
            {
                return;
            }

            var autoCompleteTextView = androidView.FindChildView<AutoCompleteTextView>();
            
            autoCompleteTextView.BackgroundTintList = ColorStateList.ValueOf(color.ToPlatform());
        }

        private static void MapBarColor(SearchBarHandler handler, SearchBar searchBar)
        {
            handler.InternalSearchBar.BackgroundColor = searchBar.BarColor;
            handler.OuterGrid.BackgroundColor = searchBar.BarColor;

            MapAndroidBusyBackgroundColor(handler, searchBar); //Make sure the background color of the progress bar is in sync if its not set by the consumer.
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

        private static void MapText(SearchBarHandler handler, SearchBar searchBar)
        {
            handler.InternalSearchBar.Text = searchBar.Text;
        }
    }
}