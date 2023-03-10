using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ListView = DIPS.Mobile.UI.Components.Lists.ListView;

namespace DIPS.Mobile.UI.Components.Searching
{
    public abstract partial class SearchPage
    {
        public static readonly BindableProperty EmptyContentProperty = BindableProperty.Create(
            nameof(NoResultView),
            typeof(View),
            typeof(SearchPage));

        /// <summary>
        /// The view to show to people when there is no search results.
        /// </summary>
        public View NoResultView
        {
            get => (View)GetValue(EmptyContentProperty);
            set => SetValue(EmptyContentProperty, value);
        }

        public static readonly BindableProperty HintViewProperty = BindableProperty.Create(
            nameof(HintView),
            typeof(View),
            typeof(SearchPage));

        /// <summary>
        /// The view to show to people when they have not searched for anything yet.
        /// </summary>
        public View HintView
        {
            get => (View)GetValue(HintViewProperty);
            set => SetValue(HintViewProperty, value);
        }

        [Obsolete("Setting Content property is the same as setting EmptyResultView")]
        public new View? Content { get; set; }

        public static readonly BindableProperty SearchCommandProperty = BindableProperty.Create(
            nameof(SearchCommand),
            typeof(ICommand),
            typeof(SearchPage));

        /// <summary>
        /// The command to be executed when people use the search bar to search. 
        /// </summary>
        /// <remarks>The search query gets sent as a string to the command as a CommandParameter</remarks>
        public ICommand SearchCommand
        {
            get => (ICommand)GetValue(SearchCommandProperty);
            set => SetValue(SearchCommandProperty, value);
        }

        public static readonly BindableProperty ResultViewProperty = BindableProperty.Create(
            nameof(ResultItemTemplate),
            typeof(DataTemplate),
            typeof(SearchPage));

        /// <summary>
        /// The view that people will see when they get a result from using the search bar.
        /// </summary>
        /// <remarks>This DataTemplate is used with a <see cref="ListView"/>, which means that the first item has to be a <see cref="Cell"/></remarks>
        public DataTemplate ResultItemTemplate
        {
            get => (DataTemplate)GetValue(ResultViewProperty);
            set => SetValue(ResultViewProperty, value);
        }

        public static readonly BindableProperty SearchPlaceholderProperty = BindableProperty.Create(
            nameof(SearchPlaceholder),
            typeof(string),
            typeof(SearchPage));

        /// <summary>
        /// The placeholder that is visible to people before they start searching.
        /// </summary>
        public string SearchPlaceholder
        {
            get => (string)GetValue(SearchPlaceholderProperty);
            set => SetValue(SearchPlaceholderProperty, value);
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
    }
}