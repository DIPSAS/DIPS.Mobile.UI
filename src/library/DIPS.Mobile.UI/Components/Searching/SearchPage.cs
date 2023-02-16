using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DIPS.Mobile.UI.Extensions;
using Xamarin.Forms;
using Application = Xamarin.Forms.Application;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;
using ListView = DIPS.Mobile.UI.Components.Lists.ListView;
using NavigationPage = Xamarin.Forms.NavigationPage;

namespace DIPS.Mobile.UI.Components.Searching
{
    [ContentProperty(nameof(NoResultView))]
    public abstract partial class SearchPage : ContentPage
    {
        private readonly Grid m_grid;
        private readonly ListView m_resultListView;
        private readonly SearchBar m_searchBar;
        private CancellationTokenSource? m_searchCancellationToken;

        public SearchPage()
        {
            //Searchbar
            m_searchBar = new SearchBar() {ShowsCancelButton = true};
            m_searchBar.SetAppThemeColor(SearchBar.BarColorProperty, Shell.Shell.ToolbarBackgroundColorName);
            m_searchBar.SetAppThemeColor(Xamarin.Forms.SearchBar.CancelButtonColorProperty,
                Shell.Shell.ToolbarTitleTextColorName);
            if (Device.RuntimePlatform == Device.Android) //Colors are different on Android due to no inner white frame
            {
                m_searchBar.SetAppThemeColor(SearchBar.TextColorProperty,
                    Shell.Shell.ToolbarTitleTextColorName);
                m_searchBar.SetAppThemeColor(SearchBar.IconsColorProperty, Shell.Shell.ToolbarTitleTextColorName);
                m_searchBar.SetAppThemeColor(SearchBar.PlaceholderColorProperty,
                    Shell.Shell.ToolbarTitleTextColorName);
            }

            m_searchBar.SetBinding(Xamarin.Forms.SearchBar.PlaceholderProperty,
                new Binding(nameof(SearchPlaceholder), source: this));
            m_searchBar.TextChanged += SearchBarOnTextChanged;
            //TODO:If Mode: WhenKeyboardPressed
            m_searchBar.SearchCommand = new Command(() => OnSearchQueryChanged(m_searchBar.Text));
            m_searchBar.CancelCommand = new Command(OnCancel);
            m_searchBar.CornerRadius = 0;


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
                    new()
                    {
                        Height = GridLength.Auto
                    }, //Space for the top box view on iOS if there is no navigation page and we have to respect safe area
                    new() {Height = GridLength.Auto}, //Space for the search bar
                    new()
                    {
                        Height = GridLength.Star
                    }, //Space for the dynamic content. Hint view, empty view and listview
                },
                RowSpacing = 0
            };

            m_grid.Children.Add(m_searchBar, 0, 1);
            base.Content = m_grid;
        }

        private static void OnCancel()
        {
            Application.Current.MainPage.Navigation.PopAsync();
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
                SetSearchState(SearchStates.NeedsSearchHint);
                return;
            }

            //TODO: Implement delay if its needed from the consumer
            try
            {
                SetSearchState(SearchStates.Searching);

                var result = await ProvideSearchResult(searchQuery, m_searchCancellationToken.Token);
                if (result == null)
                {
                    return;
                }

                var resultCopy = result.ToList();
                if (!resultCopy.Any())
                {
                    SetSearchState(SearchStates.NoSearchMatch);
                    return;
                }

                m_resultListView.ItemsSource = resultCopy;
                SetSearchState(SearchStates.SearchMatched);
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
            SetSearchState(SearchStates.NeedsSearchHint);
            m_searchBar.Focus();
        }

        protected override void SafeAreaInsetsDidChange(Thickness thickness)
        {
            if (Parent is not NavigationPage) //if there is no navigation bar, it needs to move down from the safe area
            {
                //box view used on ios if safe area has to be respected
                var topBoxView = new BoxView
                {
                    BackgroundColor = m_searchBar.BackgroundColor, HeightRequest = thickness.Top
                };
                m_grid.Children.Add(topBoxView, 0, 0);
            }

            base.SafeAreaInsetsDidChange(thickness);
        }

        protected override void OnDisappearing()
        {
            m_searchBar.TextChanged -= SearchBarOnTextChanged;
            base.OnDisappearing();
        }

        private void SetSearchState(SearchStates searchStates)
        {
            if (searchStates == SearchStates.Searching)
            {
                ToggleProgressBarVisibility(true);
            }
            else
            {
                ToggleProgressBarVisibility(false);
            }

            const int contentRow = 2;

            //Remove previous view
            m_grid.RemoveChildAt(0, contentRow);

            var viewToShow = searchStates switch
            {
                SearchStates.NeedsSearchHint => HintView,
                SearchStates.SearchMatched => m_resultListView,
                SearchStates.NoSearchMatch => NoResultView,
                _ => HintView
            };
            //Add new view
            m_grid.Children.Add(viewToShow, 0, contentRow);
        }

        private void ToggleProgressBarVisibility(bool visible)
        {
            m_searchBar.IsBusy = visible;
        }
    }

    public enum SearchStates
    {
        NeedsSearchHint,
        SearchMatched,
        NoSearchMatch,
        Searching,
    }
}