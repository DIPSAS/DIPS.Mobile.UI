using DIPS.Mobile.UI.Components.Lists;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Internal;
using DIPS.Mobile.UI.Resources.Colors;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Searching
{
    [ContentProperty(nameof(NoResultView))]
    public abstract partial class SearchPage : ContentPage
    {
        private readonly Grid m_grid;
        private readonly CollectionView m_resultCollectionView;

        private View? m_previousView;
        private View? m_footerView;

        public SearchPage()
        {
            
            //Searchbar
            SearchBar = new SearchBar { AutomationId = "SearchBar".ToDUIAutomationId<SearchPage>(), HasCancelButton = true, HasBusyIndication = true, ShouldCloseKeyboardOnReturnKeyTapped = true };
            SearchBar.SetAppThemeColor(SearchBar.BarColorProperty, ColorName.color_surface_default);
            
#if __ANDROID__ //Colors are different on Android due to no inner white frame
                SearchBar.SetAppThemeColor(SearchBar.TextColorProperty,
                    ColorName.color_text_default);
                SearchBar.SetAppThemeColor(SearchBar.IconsColorProperty, 
                    ColorName.color_icon_default);
                SearchBar.SetAppThemeColor(SearchBar.PlaceholderColorProperty,
                    ColorName.color_text_subtle);
#else
            SearchBar.SetAppThemeColor(SearchBar.SearchFieldBackgroundColorProperty, 
                BackgroundColorName);
            SearchBar.SetAppThemeColor(SearchBar.CancelButtonTextColorProperty, 
                ColorName.color_text_action);
#endif
            SearchBar.SetBinding(SearchBar.ReturnKeyTypeProperty, static (SearchPage searchPage) => searchPage.SearchMode, source: this);
            SearchBar.SetBinding(SearchBar.PlaceholderProperty, static (SearchPage searchPage) => searchPage.SearchPlaceholder, source: this);
            SearchBar.SetBinding(SearchBar.ShouldDelayProperty, static (SearchPage searchPage) => searchPage.ShouldDelay, source: this);
            SearchBar.SetBinding(SearchBar.DelayProperty, static (SearchPage searchPage) => searchPage.Delay, source: this);
            SearchBar.SetBinding(SearchBar.IsAutocorrectEnabledProperty, static (SearchPage searchPage) => searchPage.IsAutocorrectEnabled, source: this);
            
            SearchBar.TextChanged += SearchBarOnTextChanged;

            SearchBar.SearchCommand = new Command(() => OnSearchQueryChanged(SearchBar.Text));
            SearchBar.ClearTextCommand = new Command(TextWasClearedFromClick);
            SearchBar.CancelCommand = CancelCommand;


            //Result listview
            m_resultCollectionView = new CollectionView
            {
                AutomationId = "ResultCollectionView".ToDUIAutomationId<SearchPage>(),
                HeaderTemplate = new DataTemplate(() => new Grid { Padding = new Thickness(0, Sizes.GetSize(SizeName.content_margin_large), 0, 0) })
            };

            m_resultCollectionView.Scrolled += OnCollectionViewScrolled;
            m_resultCollectionView.SetBinding(ItemsView.ItemTemplateProperty, static (SearchPage searchPage) => searchPage.ResultItemTemplate, source: this);

            //The grid to glue it all together
            m_grid = new Grid()
            {
                AutomationId = "Grid".ToDUIAutomationId<SearchPage>(),
                RowDefinitions = new RowDefinitionCollection()
                {
                    new()
                    {
                        Height = GridLength.Auto // Space for the search bar
                    }, 
                    new()
                    {
                        Height = GridLength.Star // Space for the dynamic content. Hint view, empty view and listview
                    }, 
                    new()
                    {
                        Height = GridLength.Auto // Footer
                    }
                },
                RowSpacing = 0
            };

            m_grid.Add(SearchBar, 0, 0);

            OnSearchModeChanged();
            
            base.Content = m_grid;
        }

        private void OnCollectionViewScrolled(object? sender, ItemsViewScrolledEventArgs e)
        {
#if __ANDROID__ //Scrolled gets kicked off when you change the collections item source for some reason, so we have to detect if its a scroll or not
                if (m_resultCollectionView.Handler is CollectionViewHandler {PlatformView.ScrollState: 0}) return; //0 is idle
#endif
               
            SearchBar.Unfocus();
        }

        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();
            
            if (Handler is null)
                return;
            
            SetSearchState(SearchStates.NeedsSearchHint);
            if (ShouldAutoFocus)
            {
                SearchBar.Focus();    
            }

            SearchBar.Focused += OnSearchBarFocused;
        }

        private void OnSearchBarFocused(object? sender, EventArgs eventArgs)
        {
            SearchBarFocused?.Invoke(this, EventArgs.Empty);
        }

        private void TextWasClearedFromClick()
        {
            if (SearchMode is SearchMode.WhenTappedComplete)
            {
                OnSearchQueryChanged(string.Empty);
            }
        }


        private static void OnCancel()
        {
            Application.Current?.Windows[0].Navigation.PopAsync();
        }

        /// <inheritdoc />
        protected override bool OnBackButtonPressed()
        {
            if (CancelCommand.CanExecute(null))
            {
                CancelCommand.Execute(null);
                return true;
            }

            return base.OnBackButtonPressed();
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
            SearchBar.Unfocus();
            base.OnDisappearing();
        }

        private void SetSearchState(SearchStates searchStates)
        {
            ToggleProgressBarVisibility(searchStates == SearchStates.Searching);

            const int rowChildIndex = 1;
            
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

        private void OnCancelCommandChanged()
        {
            SearchBar.CancelCommand = CancelCommand;
        }

        private void OnSearchModeChanged()
        {
            SearchBar.ReturnKeyType = SearchMode == SearchMode.WhenTextChanged ?
                ReturnType.Done :
                ReturnType.Search;
        }

        private void OnScrollableHeaderChanged()
        {
            m_resultCollectionView.Header = ScrollableHeader;
        }

        private void OnFooterViewChanged()
        {
            if (m_footerView is not null)
                m_grid.Remove(m_footerView);

            m_footerView = FooterView;
            m_footerView.VerticalOptions = LayoutOptions.End;
            m_grid.Add(FooterView, 0, 2);
        }

        protected override void OnHandlerChanging(HandlerChangingEventArgs args)
        {
            base.OnHandlerChanging(args);
            
            if (args.NewHandler is null)
            {
                m_resultCollectionView.Scrolled -= OnCollectionViewScrolled;
                SearchBar.Focused -= OnSearchBarFocused;
            }
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