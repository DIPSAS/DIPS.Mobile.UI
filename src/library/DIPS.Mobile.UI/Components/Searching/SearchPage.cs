using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.Progress;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Application = Xamarin.Forms.Application;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;
using ListView = DIPS.Mobile.UI.Components.Lists.ListView;
using ProgressBar = DIPS.Mobile.UI.Components.Progress.ProgressBar;

namespace DIPS.Mobile.UI.Components.Searching
{
    [ContentProperty(nameof(EmptyResultView))]
    public abstract partial class SearchPage : ContentPage
    {
        private Grid m_grid;
        private readonly ListView m_resultListView;
        private readonly SearchBar m_searchBar;
        private CancellationTokenSource? m_searchCancellationToken;
        private readonly ProgressBar m_progressBar;

        public SearchPage()
        {
            Padding = 0;
            //Searchbar
            m_searchBar = new SearchBar();
            m_searchBar.SetAppThemeColor(BackgroundColorProperty, Shell.Shell.ToolbarBackgroundColorName);
            m_searchBar.SetAppThemeColor(Xamarin.Forms.SearchBar.PlaceholderColorProperty, ColorName.color_neutral_10);
            m_searchBar.SetAppThemeColor(Xamarin.Forms.SearchBar.CancelButtonColorProperty,
                Shell.Shell.ToolbarTitleTextColorName);
            m_searchBar.SetAppThemeColor(Xamarin.Forms.SearchBar.TextColorProperty,
                Shell.Shell.ToolbarTitleTextColorName);
            m_searchBar.SetAppThemeColor(SearchBar.IconsColorProperty,
                Shell.Shell.ToolbarTitleTextColorName);
            m_searchBar.SetBinding(Xamarin.Forms.SearchBar.PlaceholderProperty,
                new Binding(nameof(SearchPlaceholder), source: this));
            m_searchBar.TextChanged += SearchBarOnTextChanged;
            //TODO:If Mode: WhenKeyboardPressed
            m_searchBar.SearchCommand = new Command(() => OnSearchQueryChanged(m_searchBar.Text));
            m_searchBar.CancelCommand = new Command(() => Application.Current.MainPage.Navigation.PopAsync());
            m_searchBar.CornerRadius = 0;

            //Progressbar, Android only?
            m_progressBar = new ProgressBar();
            m_progressBar.Mode = ProgressBarMode.Indeterminate;
            m_progressBar.IsVisible = Device.PlatformServices.RuntimePlatform == Device.Android;
            ToggleProgressBarVisibility(false);


            //Result listview
            m_resultListView = new ListView();
            m_resultListView.SelectionMode = ListViewSelectionMode.None;
            m_resultListView.HasUnevenRows = true;
            m_resultListView.SetBinding(ListView.ItemTemplateProperty,
                new Binding() {Path = nameof(ResultItemTemplate), Source = this});

            //The grid to glue it all together
            m_grid = new Grid()
            {
                RowDefinitions = new RowDefinitionCollection()
                {
                    new() {Height = GridLength.Auto},
                    new() {Height = GridLength.Auto},
                    new() {Height = GridLength.Star},
                },
                RowSpacing = 0
            };
            m_grid.SetAppThemeColor(BackgroundProperty, ColorName.color_primary_d_90);
            m_grid.Children.Add(m_searchBar, 0, 0);
            m_grid.Children.Add(m_progressBar, 0, 1);
            base.Content = m_grid;
        }

        private void SearchBarOnTextChanged(object sender, TextChangedEventArgs e)
        {
            //TODO:If Mode:WhenPeopleType
            //TODO:If, TypeDelay
            OnSearchQueryChanged(e.NewTextValue);
        }

        private async void OnSearchQueryChanged(string searchQuery)
        {
            m_searchCancellationToken?.Cancel(); //Cancel the previous search
            m_searchCancellationToken = new CancellationTokenSource();

            if (string.IsNullOrEmpty(searchQuery))
            {
                m_resultListView.ItemsSource =
                    new List<string>(); //Clear the previous search result to not show up the next time we start search from blank
                ToggleContent(true);
                ToggleProgressBarVisibility(false);
                return;
            }

            //TODO: Implement delay if its needed from the consumer
            try
            {
                ToggleProgressBarVisibility(true);
                var result = await ProvideSearchResult(searchQuery, m_searchCancellationToken.Token);
                if (result == null)
                {
                    return;
                }

                var resultCopy = result.ToList();
                if (!resultCopy.Any())
                {
                    ToggleContent(true);
                    return;
                }

                m_resultListView.ItemsSource = resultCopy;
                ToggleContent(false);
                ToggleProgressBarVisibility(false);
            }
            catch (TaskCanceledException) //This means that people has initiated a new search
            {
                //Swallow it
            }
            catch (Exception e)
            {
                //TODO: What to do if the consumer has not try-catched their code?
            }
        }

        protected override void OnContentAppearing()
        {
            base.OnContentAppearing();
            ToggleContent(true);
            if (Device.PlatformServices.RuntimePlatform == Device.iOS)
            {
                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Left = Padding.Left;
                safeInsets.Right = Padding.Right;
                Padding = safeInsets;
            }
        }

        protected override void OnDisappearing()
        {
            m_searchBar.TextChanged -= SearchBarOnTextChanged;
            base.OnDisappearing();
        }

        private void ToggleContent(bool isEmpty)
        {
            const int contentRow = 2;
            m_grid.RemoveChildAt(0, contentRow);
            var viewToShow = isEmpty ? EmptyResultView : m_resultListView;
            viewToShow.SetAppThemeColor(BackgroundColorProperty, ContentPage.BackgroundColorName);
            m_grid.Children.Add(viewToShow, 0, contentRow);
        }

        private void ToggleProgressBarVisibility(bool visible)
        {
            m_searchBar.IsBusy = visible;
            m_progressBar.Opacity = !visible ? 0 : 1;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        
    }
}