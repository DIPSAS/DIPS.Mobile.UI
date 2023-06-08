using System.Windows.Input;
using Colors = Microsoft.Maui.Graphics.Colors;
using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    public partial class BottomSheet
    {
        
        /// <summary>
        /// Determines if the bottom sheet should be sized to fit the content of the bottom sheet.
        ///
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item><description>Android : Works on any OS version.</description></item>
        /// <item><description>iOS less than 15: Will always display as a full screen modal page.</description></item>
        /// <item><description>iOS less than 16: Will try to set size to half the screen, but go to full screen if the content is bigger than half the screen.</description></item>
        /// <item><description>iOS greater or equal to 16: Will set the size to fit the content.</description></item>
        /// /// <item><description>Do not set <see cref="IsDraggable"/> because dragging the sheet to maximize it is not supported when <see cref="ShouldFitToContent"/></description></item>
        /// </list>
       /// </remarks>
        public bool ShouldFitToContent
        {
            get => (bool)GetValue(ShouldFitToContentProperty);
            set => SetValue(ShouldFitToContentProperty, value);
        }

        /// <summary>
        /// Determines if the bottom sheet should have a <see cref="Components.Searching.SearchBar"/> at the top
        /// </summary>
        public bool HasSearchBar
        {
            get => (bool)GetValue(HasSearchBarProperty);
            set => SetValue(HasSearchBarProperty, value);
        }

        public static readonly BindableProperty SearchCommandProperty = BindableProperty.Create(
            nameof(SearchCommand),
            typeof(ICommand),
            typeof(BottomSheet));

        public ICommand SearchCommand
        {
            get => (ICommand)GetValue(SearchCommandProperty);
            set => SetValue(SearchCommandProperty, value);
        }

        public event EventHandler<TextChangedEventArgs> SearchTextChanged;

        internal SearchBar? SearchBar { get; private set; }
        
        public static readonly BindableProperty ShouldFitToContentProperty = BindableProperty.Create(
            nameof(ShouldFitToContent),
            typeof(bool),
            typeof(BottomSheet));
        
        public static readonly BindableProperty HasSearchBarProperty = BindableProperty.Create(
            nameof(HasSearchBar),
            typeof(bool),
            typeof(BottomSheet),
            propertyChanged: OnHasSearchBarChanged);

        public event EventHandler? WillClose;
        public event EventHandler? DidClose;
        protected virtual void OnDidClose() { }
        protected virtual void OnWillClose() { }
    }
}