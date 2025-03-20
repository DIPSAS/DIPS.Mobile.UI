using System.Windows.Input;
using DIPS.Mobile.UI.Components.BottomSheets.Header;
using Button = Microsoft.Maui.Controls.Button;

namespace DIPS.Mobile.UI.Components.BottomSheets;

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
    /// The bottom bar items to be displayed in the bottom of the bottom sheet.
    /// </summary>
    public IList<Button> BottombarButtons { get; internal set; }

    /// <summary>
    /// Determines if the bottom sheet has bottom bar buttons
    /// </summary>
    public bool HasBottomBarButtons => BottombarButtons.Any();

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
    /// <remarks></remarks>
    /// <remarks>
    /// <b>Only executed on Android</b><br/><br/>
    /// An action is sent as a CommandParameter, if the Action is executed, the <see cref="BottomSheet"/> will be closed.
    /// </remarks>
    /// <code>
    /// public ICommand CloseButtonCommand => new Command(action =>
    /// {
    ///     // Here you can display a confirmation dialog
    ///     if(shouldClose)
    ///         Close();
    /// });
    /// </code>
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
    /// Determines whether the search bar should be focused when opening the bottom sheet
    /// </summary>
    /// <remarks>Only valid when <see cref="HasSearchBar"/> is true</remarks>
    public bool ShouldAutoFocusSearchBar
    {
        get => (bool)GetValue(ShouldAutoFocusSearchBarProperty);
        set => SetValue(ShouldAutoFocusSearchBarProperty, value);
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

    internal event Action<Positioning>? OnPositioningChanged;

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
        
    public Positioning Positioning
    {
        get => (Positioning)GetValue(PositioningProperty);
        set
        {
            SetValue(PositioningProperty, value);
            OnPositioningChanged?.Invoke(value);
        }
    }
    
    public BottomSheetHeaderBehavior? BottomSheetHeaderBehavior
    {
        get => (BottomSheetHeaderBehavior?)GetValue(BottomSheetHeaderBehaviorProperty);
        set => SetValue(BottomSheetHeaderBehaviorProperty, value);
    }
        
    /// <summary>
    /// Whether the <see cref="BottomSheet"/> should be draggable
    /// <remarks>Setting this to false will remove the handle</remarks>
    /// </summary>
    public bool IsDraggable
    {
        get => (bool)GetValue(IsDraggableProperty);
        set => SetValue(IsDraggableProperty, value);
    }
        
    public static readonly BindableProperty IsDraggableProperty = BindableProperty.Create(
        nameof(IsDraggable),
        typeof(bool),
        typeof(BottomSheet),
        true);
        
    public static readonly BindableProperty BottomSheetHeaderBehaviorProperty = BindableProperty.Create(
        nameof(BottomSheetHeaderBehavior),
        typeof(BottomSheetHeaderBehavior),
        typeof(BottomSheet),
        propertyChanged: (bindable, _, newValue) =>
        {
            if(newValue is not null)
                ((BindableObject)newValue).BindingContext = bindable.BindingContext;
        });

    public static readonly BindableProperty PositioningProperty = BindableProperty.Create(
        nameof(Positioning),
        typeof(Positioning),
        typeof(BottomSheet));
        
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
        
    public static readonly BindableProperty HasSearchBarProperty = BindableProperty.Create(
        nameof(HasSearchBar),
        typeof(bool),
        typeof(BottomSheet));
        
    public static readonly BindableProperty IsInteractiveCloseableProperty = BindableProperty.Create(
        nameof(IsInteractiveCloseable),
        typeof(bool),
        typeof(BottomSheet),
        true);
        
    public static readonly BindableProperty OnBackButtonPressedCommandProperty = BindableProperty.Create(
        nameof(OnBackButtonPressedCommand),
        typeof(ICommand),
        typeof(BottomSheet));

    public static readonly BindableProperty ShouldAutoFocusSearchBarProperty = BindableProperty.Create(
        nameof(ShouldAutoFocusSearchBar),
        typeof(bool),
        typeof(BottomSheet));

#if __IOS__
        public iOS.BottomSheetViewController ViewController { get; set; }
#endif

#if __ANDROID__
    public Google.Android.Material.BottomSheet.BottomSheetDialog BottomSheetDialog { get; set; }
    public Google.Android.Material.BottomSheet.BottomSheetBehavior BottomSheetBehavior { get; set; }
    public Android.BottomSheetFragment BottomSheetFragment { get; set; }
    public global::Android.Widget.RelativeLayout RootLayout { get; set; }
#endif
}