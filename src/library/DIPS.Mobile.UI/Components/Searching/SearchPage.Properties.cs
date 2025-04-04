using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Searching
{
    public abstract partial class SearchPage
    {
        /// <summary>
        /// The view to show to people when there is no search results.
        /// </summary>
        public View? NoResultView
        {
            get => (View)GetValue(EmptyContentProperty);
            set => SetValue(EmptyContentProperty, value);
        }

        /// <summary>
        /// The view to show to people when they have not searched for anything yet.
        /// </summary>
        public View? HintView
        {
            get => (View)GetValue(HintViewProperty);
            set => SetValue(HintViewProperty, value);
        }

        /// <summary>
        /// The footer to show at the bottom at all times
        /// </summary>
        public View FooterView
        {
            get => (View)GetValue(FooterViewProperty);
            set => SetValue(FooterViewProperty, value);
        }

        [Obsolete("Setting Content property is the same as setting EmptyResultView")]
        public new View? Content { get; set; }

        /// <summary>
        /// The command to be executed when people use the search bar to search. 
        /// </summary>
        /// <remarks>The search query gets sent as a string to the command as a CommandParameter</remarks>
        public ICommand SearchCommand
        {
            get => (ICommand)GetValue(SearchCommandProperty);
            set => SetValue(SearchCommandProperty, value);
        }

        /// <summary>
        /// The command to be executed when people press the cancel button
        /// </summary>
        /// <remarks>Defaults to close the modal</remarks>
        public ICommand CancelCommand
        {
            get => (ICommand)GetValue(CancelCommandProperty);
            set => SetValue(CancelCommandProperty, value);
        }

        /// <summary>
        /// The view that people will see when they get a result from using the search bar.
        /// </summary>
        /// <remarks>This DataTemplate is used with a <see cref="ListView"/>, which means that the first item has to be a <see cref="Cell"/></remarks>
        public DataTemplate ResultItemTemplate
        {
            get => (DataTemplate)GetValue(ResultViewProperty);
            set => SetValue(ResultViewProperty, value);
        }

        /// <summary>
        /// The placeholder that is visible to people before they start searching.
        /// </summary>
        public string SearchPlaceholder
        {
            get => (string)GetValue(SearchPlaceholderProperty);
            set => SetValue(SearchPlaceholderProperty, value);
        }

        /// <summary>
        /// Whether the invocation of <see cref="ProvideSearchResult"/> should be delayed according to <see cref="Delay"/>.
        /// </summary>
        public bool ShouldDelay
        {
            get => (bool)GetValue(ShouldDelayProperty);
            set => SetValue(ShouldDelayProperty, value);
        }

        /// <summary>
        /// Determines if the page should automatically focus the search bar when it loads.
        /// </summary>
        public bool ShouldAutoFocus
        {
            get => (bool)GetValue(ShouldAutoFocusProperty);
            set => SetValue(ShouldAutoFocusProperty, value);
        }

        /// <summary>
        /// The amount of delay before invocation of <see cref="ProvideSearchResult"/> in milliseconds. Is only in effect if <see cref="ShouldDelay"/>
        /// is true.
        /// </summary>
        public int Delay
        {
            get => (int)GetValue(DelayProperty);
            set => SetValue(DelayProperty, value);
        }

        /// <summary>
        /// Defines when a search should be triggered.
        /// <see cref="Searching.SearchMode.WhenTextChanged"/> = search is triggered when a character is typed or removed from the search text
        /// <see cref="Searching.SearchMode.WhenTappedComplete"/> = search is triggered only when user presses complete on the device's keyboard
        /// </summary>
        public SearchMode SearchMode
        {
            get => (SearchMode)GetValue(SearchModeProperty);
            set => SetValue(SearchModeProperty, value);
        }
        
        /// <summary>
        /// Whether autocorrect should be enabled on the search bar.
        /// </summary>
        public bool IsAutocorrectEnabled 
        {
            get => (bool)GetValue(IsAutocorrectEnabledProperty);
            set => SetValue(IsAutocorrectEnabledProperty, value);
        }

        /// <summary>
        /// The method to return the result that people will see when they use the search bar in the search page.
        /// </summary>
        /// <param name="searchQuery">The query provided from people when they use the search bar in the search page.</param>
        /// <param name="searchCancellationToken">The cancellation token that will be cancelled when people search during a search. Do use this in your asynchronous awaits for the best user experience. </param>
        /// <returns>A list of objects to display, the <see cref="ResultItemTemplate"/> is responsible of displaying the result.</returns>
        /// <remarks>Doing this asynchronously will display a spinner for people to see during the task. If people start searching while your asynchronous calls are being executed, the <see cref="searchCancellationToken"/> will be cancelled. This means that you have to use it for your asynchronous calls.  </remarks>
        public abstract Task<IEnumerable<object>?> ProvideSearchResult(string searchQuery,
            CancellationToken searchCancellationToken);
        
        /// <summary>
        /// Event to be raised when the <see cref="SearchBar"/> was focused.
        /// </summary>
        public event EventHandler<EventArgs> SearchBarFocused;

        /// <summary>
        /// Auto hides the last divider (If consumer has divider in its ItemTemplate to separate items)
        /// </summary>
        public bool AutoHideLastDivider
        {
            get => (bool)GetValue(AutoHideLastDividerProperty);
            set => SetValue(AutoHideLastDividerProperty, value);
        }

        public SearchBar SearchBar { get; }
        
        public static readonly BindableProperty AutoHideLastDividerProperty = BindableProperty.Create(
            nameof(AutoHideLastDivider),
            typeof(bool),
            typeof(SearchPage),
            defaultBindingMode: BindingMode.OneTime,
            propertyChanged: (bindable, _, newValue) =>
            {
                if (newValue is bool boolean && bindable is SearchPage searchPage)
                {
                    UI.Effects.Layout.Layout.SetAutoHideLastDivider(searchPage.m_resultCollectionView, boolean);
                }
            });
        
        public static readonly BindableProperty CancelCommandProperty = BindableProperty.Create(
            nameof(CancelCommand),
            typeof(ICommand),
            typeof(SearchPage),
            defaultValue: new Command(OnCancel),
            propertyChanged: (bindable, _, _) => ((SearchPage)bindable).OnCancelCommandChanged());
        
        public static readonly BindableProperty SearchModeProperty = BindableProperty.Create(
            nameof(SearchMode),
            typeof(SearchMode),
            typeof(SearchPage),
            SearchMode.WhenTextChanged,
            propertyChanged: (bindable, _, _) => ((SearchPage)bindable).OnSearchModeChanged());
        
        public static readonly BindableProperty DelayProperty = BindableProperty.Create(
            nameof(Delay),
            typeof(int),
            typeof(SearchPage),
            defaultValue:SearchBar.DelayProperty.DefaultValue);
        
        public static readonly BindableProperty ShouldDelayProperty = BindableProperty.Create(
            nameof(ShouldDelay),
            typeof(bool),
            typeof(SearchPage),
            defaultValue:SearchBar.ShouldDelayProperty.DefaultValue);
        
        public static readonly BindableProperty SearchPlaceholderProperty = BindableProperty.Create(
            nameof(SearchPlaceholder),
            typeof(string),
            typeof(SearchPage));
        
        public static readonly BindableProperty ResultViewProperty = BindableProperty.Create(
            nameof(ResultItemTemplate),
            typeof(DataTemplate),
            typeof(SearchPage));
        
        public static readonly BindableProperty SearchCommandProperty = BindableProperty.Create(
            nameof(SearchCommand),
            typeof(ICommand),
            typeof(SearchPage));
        
        public static readonly BindableProperty HintViewProperty = BindableProperty.Create(
            nameof(HintView),
            typeof(View),
            typeof(SearchPage));
        
        public static readonly BindableProperty EmptyContentProperty = BindableProperty.Create(
            nameof(NoResultView),
            typeof(View),
            typeof(SearchPage));
        
        public static readonly BindableProperty ShouldAutoFocusProperty = BindableProperty.Create(
            nameof(ShouldAutoFocus),
            typeof(bool),
            typeof(SearchPage), defaultValue: true);
        
        public static readonly BindableProperty FooterViewProperty = BindableProperty.Create(
            nameof(FooterView),
            typeof(View),
            typeof(SearchPage),
            propertyChanged: (bindable, _, _) => ((SearchPage)bindable).OnFooterViewChanged());
        
        public static readonly BindableProperty IsAutocorrectEnabledProperty = BindableProperty.Create(
            nameof(IsAutocorrectEnabled),
            typeof(bool),
            typeof(SearchPage),
            true);
    }
}