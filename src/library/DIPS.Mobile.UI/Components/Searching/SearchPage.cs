#if __IOS__
using DIPS.Mobile.UI.Components.Searching.iOS;
using UIKit;
#endif

using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;

namespace DIPS.Mobile.UI.Components.Searching
{
    [ContentProperty(nameof(NoResultView))]
    public abstract partial class SearchPage : ContentPage
    {
        private readonly Grid m_grid;
        private readonly CollectionView m_resultCollectionView;

        private View? m_previousView;

        public SearchPage()
        {
            //Searchbar
            SearchBar = new SearchBar { HasCancelButton = true, HasBusyIndication = true };
            SearchBar.SetAppThemeColor(SearchBar.BarColorProperty, 
                Shell.Shell.ToolbarBackgroundColorName);
            
#if __ANDROID__ //Colors are different on Android due to no inner white frame
                SearchBar.SetAppThemeColor(SearchBar.TextColorProperty,
                    Shell.Shell.ToolbarTitleTextColorName);
                SearchBar.SetAppThemeColor(SearchBar.IconsColorProperty, 
                    Shell.Shell.ToolbarTitleTextColorName);
                SearchBar.SetAppThemeColor(SearchBar.PlaceholderColorProperty,
                    Shell.Shell.ToolbarTitleTextColorName);
#else
            SearchBar.SetAppThemeColor(SearchBar.iOSSearchFieldBackgroundColorProperty, 
                BackgroundColorName);
            SearchBar.SetAppThemeColor(SearchBar.CancelButtonTextColorProperty, 
                Shell.Shell.ToolbarTitleTextColorName);
#endif

            SearchBar.SetBinding(SearchBar.PlaceholderProperty,
                new Binding(nameof(SearchPlaceholder), source: this));
            SearchBar.SetBinding(SearchBar.ShouldDelayProperty,
                new Binding(nameof(ShouldDelay), source: this));
            SearchBar.SetBinding(SearchBar.DelayProperty,
                new Binding(nameof(DelayProperty), source: this));
            SearchBar.TextChanged += SearchBarOnTextChanged;

            SearchBar.SearchCommand = new Command(() => OnSearchQueryChanged(SearchBar.Text));
            SearchBar.CancelCommand = new Command(OnCancel);


            //Result listview
            m_resultCollectionView = new CollectionView();
            m_resultCollectionView.SetBinding(ItemsView.ItemTemplateProperty,
                new Binding() {Path = nameof(ResultItemTemplate), Source = this});

            //The grid to glue it all together
            m_grid = new Grid()
            {
                RowDefinitions = new RowDefinitionCollection()
                {
                    new()
                    {
                        Height = GridLength.Auto // Space for safe area ios
                    },
                    new()
                    {
                        Height = GridLength.Auto // Space for the search bar
                    }, 
                    new()
                    {
                        Height = GridLength.Star // Space for the dynamic content. Hint view, empty view and listview
                    }, 
                },
                RowSpacing = 0
            };

            m_grid.Add(SearchBar, 0, 1);

            base.Content = m_grid;
        }

        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();

            if (Handler == null)
                return;
            
            SetSearchState(SearchStates.NeedsSearchHint);
            SearchBar.Focus();
            
#if __IOS__
            if (OperatingSystem.IsIOSVersionAtLeast(14, 1))
            {
                var safeAreaInsetsTop = UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Top;
                
                var topBoxView = new BoxView
                {
                    BackgroundColor = SearchBar.BarColor, HeightRequest = safeAreaInsetsTop
                };
                
                m_grid.Children.Insert(0, topBoxView);

                topBoxView.Margin = new Thickness(0, -safeAreaInsetsTop, 0, 0);
            }
#endif
        }

        private static void OnCancel()
        {
            Application.Current!.MainPage!.Navigation.PopAsync();
        }

        private void SearchBarOnTextChanged(object? sender, TextChangedEventArgs e)
        {
            if (SearchMode is SearchMode.WhenTextChanged)
            {
                OnSearchQueryChanged(e.NewTextValue);
            }
        }

        private async void OnSearchQueryChanged(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                m_resultCollectionView.ItemsSource =
                    new List<string>(); //Clear the previous search result to not show up the next time we start search from blank
                SetSearchState(SearchStates.NeedsSearchHint);
                return;
            }

            try
            {
                if (SearchBar.SearchCancellationToken == null)
                {
                    return;
                }

                SetSearchState(SearchStates.Searching);
                var result = await ProvideSearchResult(searchQuery, SearchBar.SearchCancellationToken.Token);
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

                m_resultCollectionView.ItemsSource = resultCopy;
                SetSearchState(SearchStates.SearchMatched);
            }
            catch (Exception e)
            {
                //TODO: What to do if the consumer has not try-catched their code?
            }
        }

        protected override void OnDisappearing()
        {
            SearchBar.TextChanged -= SearchBarOnTextChanged;
            base.OnDisappearing();
        }

        private void SetSearchState(SearchStates searchStates)
        {
            ToggleProgressBarVisibility(searchStates == SearchStates.Searching);

            const int rowChildIndex = 2;
            
            //Remove previous view
            if(m_previousView != null)
                m_grid.Remove(m_previousView);

            var viewToShow = searchStates switch
            {
                SearchStates.NeedsSearchHint => HintView,
                SearchStates.SearchMatched => m_resultCollectionView,
                SearchStates.NoSearchMatch => NoResultView,
                _ => HintView
            };

            if (viewToShow != null)
            {
                //Add new view
                m_grid.Add(viewToShow, 0, rowChildIndex);    
            }
            
            m_previousView = viewToShow;
        }

        private void ToggleProgressBarVisibility(bool visible)
        {
            SearchBar.IsBusy = visible;
        }
    }

    public enum SearchMode
    {
        /// <summary>
        /// Search is triggered whenever the search text changes
        /// </summary>
        WhenTextChanged,
        /// <summary>
        /// Search is only triggered when the user presses "Complete" on the device's keyboard
        /// </summary>
        WhenTappedComplete
    }

    public enum SearchStates
    {
        NeedsSearchHint,
        SearchMatched,
        NoSearchMatch,
        Searching,
    }
}