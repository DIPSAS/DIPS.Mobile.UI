using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    public partial class BottomSheet
    {
        /// <summary>
        /// The title that will be displayed in the BottomSheet's navigation bar
        /// </summary>
        /// <remarks>Setting the title will automatically add a navigation bar to the <see cref="BottomSheet"/></remarks>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        
        /// <summary>
        /// The toolbar items to be displayed on the right side of the BottomSheet's navigation bar
        /// </summary>
        /// <remarks>Setting a <see cref="ToolbarItem"/> will automatically add a navigation bar to the <see cref="BottomSheet"/></remarks>
        public IList<ToolbarItem> ToolbarItems { get; internal set; }

        /// <summary>
        /// Determines if the bottom sheet should be sized to fit the content of the bottom sheet.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item><description>Android : Works on any OS version.</description></item>
        /// <item><description>iOS less than 15: Will always display as a full screen modal page.</description></item>
        /// <item><description>iOS less than 16: Will try to set size to half the screen, but go to full screen if the content is bigger than half the screen.</description></item>
        /// <item><description>iOS greater or equal to 16: Will set the size to fit the content.</description></item>
        /// <item><description>Do not set <see cref="IsDraggable"/> because dragging the sheet to maximize it is not supported when <see cref="ShouldFitToContent"/></description></item>
        /// </list>
        /// </remarks>
        public bool ShouldFitToContent
        {
            get => (bool)GetValue(ShouldFitToContentProperty);
            set => SetValue(ShouldFitToContentProperty, value);
        }

        /// <summary>
        /// Determines whether the <see cref="BottomSheet"/> is closeable by interacting with the <see cref="BottomSheet"/>
        /// </summary>
        /// <remarks>
        /// NB: Consumers must give the user a way to close the <see cref="BottomSheet"/> programatically.
        /// On Android a <see cref="BottomSheet"/> can be closed by using the back button, the <see cref="BottomSheet"/> will not be closed, but <see cref="OnBackButtonPressedCommand"/> will be executed, and the <see cref="BottomSheet"/> can then be programatically closed if you wish
        /// </remarks>
        public bool IsInteractiveCloseable
        {
            get => (bool)GetValue(IsInteractiveCloseableProperty);
            set => SetValue(IsInteractiveCloseableProperty, value);
        }

        /// <summary>
        /// Executed when hardware back button is pressed and <see cref="IsInteractiveCloseable"/> is set to true
        /// </summary>
        /// <remarks>Only executed on Android</remarks>
        public ICommand? OnBackButtonPressedCommand
        {
            get => (ICommand?)GetValue(OnBackButtonPressedCommandProperty);
            set => SetValue(OnBackButtonPressedCommandProperty, value);
        }

        /// <summary>
        /// Determines if the bottom sheet should have a <see cref="Components.Searching.SearchBar"/> at the top
        /// </summary>
        public bool HasSearchBar
        {
            get => (bool)GetValue(HasSearchBarProperty);
            set => SetValue(HasSearchBarProperty, value);
        }

        /// <summary>
        /// The command to be executed when the text in the search field is changed
        /// </summary>
        public ICommand SearchCommand
        {
            get => (ICommand)GetValue(SearchCommandProperty);
            set => SetValue(SearchCommandProperty, value);
        }

        /// <summary>
        /// Event raised when the text in the search field is changed
        /// </summary>
        public event EventHandler<TextChangedEventArgs> SearchTextChanged;

        /// <summary>
        /// Event raised when the <see cref="BottomSheet"/> is closed
        /// </summary>
        public event EventHandler? Closed;
        
        /// <summary>
        /// Event raised when the <see cref="BottomSheet"/> is opened
        /// </summary>
        public event EventHandler? Opened;

        /// <summary>
        /// The command to be executed when the <see cref="BottomSheet"/> is opened
        /// </summary>
        public ICommand? OpenedCommand
        {
            get => (ICommand)GetValue(OpenedCommandProperty);
            set => SetValue(OpenedCommandProperty, value);
        }
        
        /// <summary>
        /// The command to be executed when the <see cref="BottomSheet"/> is closed
        /// </summary>
        public ICommand? ClosedCommand
        {
            get => (ICommand)GetValue(ClosedCommandProperty);
            set => SetValue(ClosedCommandProperty, value);
        }
        
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(BottomSheet));
        
        public static readonly BindableProperty OpenedCommandProperty = BindableProperty.Create(
            nameof(OpenedCommand),
            typeof(ICommand),
            typeof(BottomSheet));
        
        public static readonly BindableProperty ClosedCommandProperty = BindableProperty.Create(
            nameof(ClosedCommand),
            typeof(ICommand),
            typeof(BottomSheet));

        public static readonly BindableProperty SearchCommandProperty = BindableProperty.Create(
            nameof(SearchCommand),
            typeof(ICommand),
            typeof(BottomSheet));
        
        public static readonly BindableProperty ShouldFitToContentProperty = BindableProperty.Create(
            nameof(ShouldFitToContent),
            typeof(bool),
            typeof(BottomSheet));
        
        public static readonly BindableProperty HasSearchBarProperty = BindableProperty.Create(
            nameof(HasSearchBar),
            typeof(bool),
            typeof(BottomSheet),
            propertyChanged: OnHasSearchBarChanged);
        
        public static readonly BindableProperty IsInteractiveCloseableProperty = BindableProperty.Create(
            nameof(IsInteractiveCloseable),
            typeof(bool),
            typeof(BottomSheet),
            true,
            BindingMode.OneTime);
        
        public static readonly BindableProperty OnBackButtonPressedCommandProperty = BindableProperty.Create(
            nameof(OnBackButtonPressedCommand),
            typeof(ICommand),
            typeof(BottomSheet));


#if __IOS__
        public UIKit.UIViewController? UIViewController { get; internal set; }
        public UIKit.UIViewController NavigationController { get; set; }
        public ContentPage WrappingContentPage { get; set; }

        public UIKit.UISheetPresentationController UISheetPresentationController;
#endif

#if __ANDROID__
        public Google.Android.Material.BottomSheet.BottomSheetDialog BottomSheetDialog { get; set; }
#endif
    }
}