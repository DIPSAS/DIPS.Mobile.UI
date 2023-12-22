using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using DIPS.Mobile.UI.Components.Searching.Android;
using DIPS.Mobile.UI.Extensions.Android;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Xamarin.Google.Crypto.Tink.Shaded.Protobuf;
using Button = Microsoft.Maui.Controls.Button;
using AView = Android.Views.View;
using Color = Microsoft.Maui.Graphics.Color;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Components.Searching
{
    internal partial class SearchBarHandler : ViewHandler<SearchBar, AView>
    {
        private ImageView? RemoveTextImageView =>
            AutoCompleteTextView is {Parent: ViewGroup viewGroup}
                ? viewGroup.FindChildView<ImageView>() ?? null
                : null;

        private AutoCompleteTextView? AutoCompleteTextView =>
            InternalSearchBar.Handler?.PlatformView is not ViewGroup androidView
                ? null
                : androidView.FindChildView<AutoCompleteTextView>() ?? null;

        internal Microsoft.Maui.Controls.SearchBar InternalSearchBar { get; set; }
        private IndeterminateProgressBar ProgressBar { get; set; }
        private Button CancelButton { get; set; }
        private VerticalStackLayout OuterVerticalStackLayout { get; } = new() {Spacing = 0};

        private partial void Construct()
        {
            var grid = new Grid()
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
            grid.Add(InternalSearchBar, 0);

            //Add button to cancel the search
            CancelButton = new DIPS.Mobile.UI.Components.Buttons.Button
            {
                Text = DUILocalizedStrings.Cancel,
                VerticalOptions = LayoutOptions.Center,
                Style = Styles.GetButtonStyle(ButtonStyle.GhostLarge)
            };
            grid.Add(CancelButton, 1);

            OuterVerticalStackLayout.Add(grid);

            //Add progressbar
            ProgressBar = new IndeterminateProgressBar();
            OuterVerticalStackLayout.Add(ProgressBar);

            AppendToPropertyMapper();
        }

        private static void AppendToPropertyMapper()
        {
            SearchBarPropertyMapper.Add(nameof(SearchBar.CancelCommand), MapCancelCommand);
            SearchBarPropertyMapper.Add(nameof(SearchBar.CancelCommandParameter), MapCancelCommandParameter);
            SearchBarPropertyMapper.Add(nameof(SearchBar.AndroidBusyBackgroundColor), MapAndroidBusyBackgroundColor);
            SearchBarPropertyMapper.Add(nameof(SearchBar.AndroidBusyColor), MapAndroidBusyColor);
        }

        private static void MapAndroidBusyColor(SearchBarHandler handler, SearchBar searchBar)
        {
            handler.ProgressBar.IndicatorColor = searchBar.AndroidBusyColor ??
                                                 DIPS.Mobile.UI.Resources.Colors.Colors
                                                     .GetColor(ColorName.color_primary_90);
        }

        private static void MapAndroidBusyBackgroundColor(SearchBarHandler handler, SearchBar searchBar)
        {
            handler.ProgressBar.TrackColor = searchBar.AndroidBusyBackgroundColor ??
                                             DIPS.Mobile.UI.Resources.Colors.Colors
                                                 .GetColor(ColorName.color_neutral_30);
        }

        protected override AView CreatePlatformView() => OuterVerticalStackLayout.ToContainerView(MauiContext!);


        protected override void ConnectHandler(AView platformView)
        {
            base.ConnectHandler(platformView);


            if (InternalSearchBar.Handler != null)
            {
                if (InternalSearchBar.Handler.PlatformView is MauiSearchView mauiSearchView)
                {
                    //Fixes this issue : https://github.com/dotnet/maui/issues/10823
                    mauiSearchView.MaxWidth = int.MaxValue;
                }

                InternalSearchBar.SearchCommand = new Command(() =>
                {
                    if (VirtualView.ShouldCloseKeyboardOnReturnKeyTapped)
                    {
                        // An ugly workaround to hide keyboard on Android
                        InternalSearchBar.IsEnabled = false;
                        InternalSearchBar.IsEnabled = true;
                        UnFocus();
                    }

                    VirtualView.SearchCommand?.Execute(null);

                });
            }
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            InternalSearchBar.TextChanged += SearchBarTextChanged;
            if (RemoveTextImageView != null)
            {
                RemoveTextImageView.Click += OnClearTextClicked;
            }

            CancelButton.Clicked += OnCancelClicked;
            InternalSearchBar.Focused += OnInternalSearchBarFocused;
        }

        private void OnCancelClicked(object? sender, EventArgs e)
        {
            UnFocus();
        }

        protected override void DisconnectHandler(AView platformView)
        {
            base.DisconnectHandler(platformView);
            UnsubscribeToEvents();
        }

        private void UnsubscribeToEvents()
        {
            InternalSearchBar.TextChanged -= SearchBarTextChanged;
            if (RemoveTextImageView != null)
            {
                RemoveTextImageView.Click -= OnClearTextClicked;
            }

            InternalSearchBar.Focused -= OnInternalSearchBarFocused;
            CancelButton.Clicked -= OnCancelClicked;
        }


        private void OnClearTextClicked(object? sender, EventArgs e)
        {
            VirtualView.Text = string.Empty;
            VirtualView.ClearTextCommand?.Execute(null);
        }

        private void SearchBarTextChanged(object? sender, TextChangedEventArgs e)
        {
            VirtualView.Text = e.NewTextValue;
        }

        private static void MapCancelButtonTextColor(SearchBarHandler handler, SearchBar searchBar)
        {
            handler.CancelButton.TextColor = searchBar.CancelButtonTextColor;
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
            if (handler.AutoCompleteTextView != null)
            {
                handler.AutoCompleteTextView.BackgroundTintList = ColorStateList.ValueOf(color.ToPlatform());
            }
        }

        private static void MapBarColor(SearchBarHandler handler, SearchBar searchBar)
        {
            handler.InternalSearchBar.BackgroundColor = searchBar.BarColor;
            handler.OuterVerticalStackLayout.BackgroundColor = searchBar.BarColor;

            MapAndroidBusyBackgroundColor(handler,
                searchBar); //Make sure the background color of the progress bar is in sync if its not set by the consumer.
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
        
        private static void MapReturnKeyType(SearchBarHandler handler, SearchBar searchBar)
        {
            if (handler.InternalSearchBar.Handler.PlatformView is MauiSearchView mauiSearchView)
            {
                mauiSearchView.ImeOptions = (int)(searchBar.ReturnKeyType == ReturnType.Done
                    ? ImeAction.Done
                    : ImeAction.Search);
            }

        }

        public partial void Focus()
            => InternalSearchBar.Focus();

        public partial void UnFocus()
            => InternalSearchBar.Unfocus();
    }
}