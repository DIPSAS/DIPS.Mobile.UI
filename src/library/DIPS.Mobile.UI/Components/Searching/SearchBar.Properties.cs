using System.Windows.Input;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Components.Searching
{
    public partial class SearchBar
    {
        /// <summary>
        /// Determines whether the keyboard should be closed when return key tapped
        /// </summary>
        public bool ShouldCloseKeyboardOnReturnKeyTapped
        {
            get => (bool)GetValue(ShouldCloseKeyboardOnSearchProperty);
            set => SetValue(ShouldCloseKeyboardOnSearchProperty, value);
        }
        
        /// <summary>
        /// iOS: Sets the text of the return key
        /// Android: Sets the icon of the return key
        /// </summary>
        public ReturnType ReturnKeyType
        {
            get => (ReturnType)GetValue(ReturnKeyTypeProperty);
            set => SetValue(ReturnKeyTypeProperty, value);
        }
        
        /// <summary>
        /// Sets the color of the icons that people see in the search bar.
        /// </summary>
        public Color? IconsColor
        {
            get => (Color)GetValue(IconsColorProperty);
            set => SetValue(IconsColorProperty, value);
        }

        /// <summary>
        /// Displays a busy indication for people to see while they wait for the search bars results.
        /// </summary>
        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        /// <summary>
        /// The background color of the indeterminate busy indicator below the search bar that is visible when <see cref="IsBusy"/> is set to true.
        /// </summary>
        public Color? AndroidBusyBackgroundColor
        {
            get => (Color)GetValue(AndroidBusyBackgroundColorProperty);
            set => SetValue(AndroidBusyBackgroundColorProperty, value);
        }

        /// <summary>
        /// The color of the indeterminate busy indicator below the search bar that is visible when <see cref="IsBusy"/> is set to true.
        /// </summary>
        public Color? AndroidBusyColor
        {
            get => (Color)GetValue(AndroidBusyColorProperty);
            set => SetValue(AndroidBusyColorProperty, value);
        }

        /// <summary>
        /// Indicates that the busy indication should be visible for people when you use <see cref="IsBusy"/> property to indicate that the search bar is busy.
        /// </summary>
        public bool HasBusyIndication
        {
            get => (bool)GetValue(HasBusyIndicationProperty);
            set => SetValue(HasBusyIndicationProperty, value);
        }

        /// <summary>
        /// Indicates that the cancel button should be visible for people to use to cancel the search.
        /// </summary>
        public bool HasCancelButton
        {
            get => (bool)GetValue(HasCancelButtonProperty);
            set => SetValue(HasCancelButtonProperty, value);
        }

        /// <summary>
        /// The command to be executed when people tap the cancel button.
        /// </summary>
        public ICommand CancelCommand
        {
            get => (ICommand)GetValue(CancelCommandProperty);
            set => SetValue(CancelCommandProperty, value);
        }

        /// <summary>
        /// The parameter to be sent with the <see cref="CancelCommand"/> when people tap the cancel button.
        /// </summary>
        public object CancelCommandParameter
        {
            get => (object)GetValue(CancelCommandParameterProperty);
            set => SetValue(CancelCommandParameterProperty, value);
        }

        /// <summary>
        /// The placeholder for people to see before searching.
        /// </summary>
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        /// <summary>
        /// The text that people search for when using the search bar.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// The color of the text for people to see.
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// The color of the text in the cancel button.
        /// </summary>
        public Color CancelButtonTextColor
        {
            get => (Color)GetValue(CancelButtonTextColorProperty);
            set => SetValue(CancelButtonTextColorProperty, value);
        }

        /// <summary>
        /// Event that gets invoked when people search using the search bar.
        /// </summary>
        public event EventHandler<TextChangedEventArgs> TextChanged;

        /// <summary>
        /// The search command that gets invoked when people has tapped the search button in the keyboard.
        /// </summary>
        public ICommand? SearchCommand
        {
            get => (ICommand)GetValue(SearchCommandProperty);
            set => SetValue(SearchCommandProperty, value);
        }

        public static readonly BindableProperty ClearTextCommandProperty = BindableProperty.Create(
            nameof(ClearTextCommand),
            typeof(ICommand),
            typeof(SearchBar));

        
        /// <summary>
        /// Executed when people has tapped the image that clears the text.
        /// </summary>
        /// <remarks><see cref="TextChanged"/> and <see cref="Text"/> will be raised when this happens as well.</remarks>
        public ICommand? ClearTextCommand
        {
            get => (ICommand)GetValue(ClearTextCommandProperty);
            set => SetValue(ClearTextCommandProperty, value);
        }

        /// <summary>
        /// The background color of the textfield.
        /// </summary>
        public Color? iOSSearchFieldBackgroundColor
        {
            get => (Color)GetValue(iOSSearchFieldBackgroundColorProperty);
            set => SetValue(iOSSearchFieldBackgroundColorProperty, value);
        }

        /// <summary>
        /// The background color of the search bar.
        /// </summary>
        public Color? BarColor
        {
            get => (Color)GetValue(BarColorProperty);
            set => SetValue(BarColorProperty, value);
        }

        /// <summary>
        /// The color of the place holder in the search bar.
        /// </summary>
        public Color? PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
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
        /// The amount of delay before invocation of <see cref="ProvideSearchResult"/> in milliseconds. Is only in effect if <see cref="ShouldDelay"/>
        /// is true.
        /// </summary>
        public int Delay
        {
            get => (int)GetValue(DelayProperty);
            set => SetValue(DelayProperty, value);
        }
        
        /// <summary>
        /// Event to be raised when the <see cref="SearchBar"/> was focused.
        /// </summary>
        public new event EventHandler<EventArgs> Focused;
        
        public static readonly BindableProperty IconsColorProperty = BindableProperty.Create(
            nameof(IconsColor),
            typeof(Color),
            typeof(SearchBar));
        
        public static readonly BindableProperty HasCancelButtonProperty = BindableProperty.Create(
            nameof(HasCancelButton),
            typeof(bool),
            typeof(SearchBar));

        public static readonly BindableProperty HasBusyIndicationProperty = BindableProperty.Create(
            nameof(HasBusyIndication),
            typeof(bool),
            typeof(SearchBar));
        
        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(
            nameof(IsBusy),
            typeof(bool),
            typeof(SearchBar));
        
        public static readonly BindableProperty AndroidBusyBackgroundColorProperty = BindableProperty.Create(
            nameof(AndroidBusyBackgroundColor),
            typeof(Color),
            typeof(SearchBar));
        
        public static readonly BindableProperty AndroidBusyColorProperty = BindableProperty.Create(
            nameof(AndroidBusyColor),
            typeof(Color),
            typeof(SearchBar));
        
        public static readonly BindableProperty CancelCommandProperty = BindableProperty.Create(
            nameof(CancelCommand),
            typeof(ICommand),
            typeof(SearchBar));
        
        public static readonly BindableProperty CancelCommandParameterProperty = BindableProperty.Create(
            nameof(CancelCommandParameter),
            typeof(object),
            typeof(SearchBar));
        
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(SearchBar), propertyChanged: ((bindable, value, newValue) => ((SearchBar)bindable).OnTextChanged((string)newValue, (string)value)), defaultBindingMode:BindingMode.TwoWay);
        
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(SearchBar));
        
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(SearchBar),
            defaultValue: DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_neutral_90));
        
        public static readonly BindableProperty SearchCommandProperty = BindableProperty.Create(
            nameof(SearchCommand),
            typeof(ICommand),
            typeof(SearchBar));
        
        public static readonly BindableProperty iOSSearchFieldBackgroundColorProperty = BindableProperty.Create(
            nameof(iOSSearchFieldBackgroundColor),
            typeof(Color),
            typeof(SearchBar));
        
        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(
            nameof(PlaceholderColor),
            typeof(Color),
            typeof(SearchBar),
            defaultValue: DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_neutral_60));

        public static readonly BindableProperty CancelButtonTextColorProperty = BindableProperty.Create(
            nameof(CancelButtonTextColor),
            typeof(Color),
            typeof(SearchBar));
        
        public static readonly BindableProperty BarColorProperty = BindableProperty.Create(
            nameof(BarColor),
            typeof(Color),
            typeof(SearchBar), defaultValue: Colors.Transparent);
        
        public static readonly BindableProperty DelayProperty = BindableProperty.Create(
            nameof(Delay),
            typeof(int),
            typeof(SearchPage),
            500);
        
        public static readonly BindableProperty ShouldDelayProperty = BindableProperty.Create(
            nameof(ShouldDelay),
            typeof(bool),
            typeof(SearchPage),
            false);
        
        public static readonly BindableProperty ReturnKeyTypeProperty = BindableProperty.Create(
            nameof(ReturnKeyType),
            typeof(ReturnType),
            typeof(SearchBar),
            ReturnType.Search);
        
        public static readonly BindableProperty ShouldCloseKeyboardOnSearchProperty = BindableProperty.Create(
            nameof(ShouldCloseKeyboardOnReturnKeyTapped),
            typeof(bool),
            typeof(SearchBar),
            true);

    }
}