using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Searching
{
    public partial class SearchBar
    {
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(SearchBar), propertyChanged:OnCornerRadiusChanged);

        /// <summary>
        /// Sets the corner radius of the background of the searchbar.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty IconsColorProperty = BindableProperty.Create(
            nameof(IconsColor),
            typeof(Color),
            typeof(SearchBar));

        /// <summary>
        /// Sets the color of the icons that people see in the search bar.
        /// </summary>
        public Color IconsColor
        {
            get => (Color)GetValue(IconsColorProperty);
            set => SetValue(IconsColorProperty, value);
        }

        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(
            nameof(IsBusy),
            typeof(bool),
            typeof(SearchBar));

        /// <summary>
        /// Displays a busy indication for people to see while they wait for the search bars results.
        /// </summary>
        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        public static readonly BindableProperty ShowsCancelButtonProperty = BindableProperty.Create(
            nameof(HasCancelButton),
            typeof(bool),
            typeof(SearchBar), propertyChanged:OnShowsCancelButtonChanged);

        public static readonly BindableProperty HasBusyIndicationProperty = BindableProperty.Create(
            nameof(HasBusyIndication),
            typeof(bool),
            typeof(SearchBar));

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
            get => (bool)GetValue(ShowsCancelButtonProperty);
            set => SetValue(ShowsCancelButtonProperty, value);
        }

        public static readonly BindableProperty CancelCommandProperty = BindableProperty.Create(
            nameof(CancelCommand),
            typeof(ICommand),
            typeof(SearchBar));

        /// <summary>
        /// The command to be executed when people tap the cancel button.
        /// </summary>
        public ICommand CancelCommand
        {
            get => (ICommand)GetValue(CancelCommandProperty);
            set => SetValue(CancelCommandProperty, value);
        }

        public static readonly BindableProperty CancelCommandParameterProperty = BindableProperty.Create(
            nameof(CancelCommandParameter),
            typeof(object),
            typeof(SearchBar));

        /// <summary>
        /// The parameter to be sent with the <see cref="CancelCommand"/> when people tap the cancel button.
        /// </summary>
        public object CancelCommandParameter
        {
            get => (object)GetValue(CancelCommandParameterProperty);
            set => SetValue(CancelCommandParameterProperty, value);
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(SearchBar));

        /// <summary>
        /// The placeholder for people to see before searching.
        /// </summary>
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(SearchBar), propertyChanged: (bindable, value, newValue) =>
            {
                if (bindable is SearchBar searchBar && value is string oldTextValue && newValue is string newTextValue)
                {
                    searchBar.TextChanged?.Invoke(searchBar, new TextChangedEventArgs(oldTextValue, newTextValue));
                }
            });

        /// <summary>
        /// The text that people search for when using the search bar.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(SearchBar));

        /// <summary>
        /// The color of the text for people to see.
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty CancelButtonColorProperty = BindableProperty.Create(
            nameof(CancelButtonColor),
            typeof(Color),
            typeof(SearchBar));

        /// <summary>
        /// The color of the text in the cancel button to see.
        /// </summary>
        public Color CancelButtonColor
        {
            get => (Color)GetValue(CancelButtonColorProperty);
            set => SetValue(CancelButtonColorProperty, value);
        }

        /// <summary>
        /// Event that gets invoked when people search using the search bar.
        /// </summary>
        public event EventHandler<TextChangedEventArgs> TextChanged;

        public static readonly BindableProperty SearchCommandProperty = BindableProperty.Create(
            nameof(SearchCommand),
            typeof(ICommand),
            typeof(SearchBar));

        /// <summary>
        /// The search command that gets invoked when people has tapped the search button in the keyboard.
        /// </summary>
        public ICommand SearchCommand
        {
            get => (ICommand)GetValue(SearchCommandProperty);
            set => SetValue(SearchCommandProperty, value);
        }

        public static readonly BindableProperty BarColorProperty = BindableProperty.Create(
            nameof(BarColor),
            typeof(Color),
            typeof(SearchBar));

        /// <summary>
        /// The background color of the search bar.
        /// </summary>
        public Color BarColor
        {
            get => (Color)GetValue(BarColorProperty);
            set => SetValue(BarColorProperty, value);
        }

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(
            nameof(PlaceholderColor),
            typeof(Color),
            typeof(SearchBar));

        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }
    }
}