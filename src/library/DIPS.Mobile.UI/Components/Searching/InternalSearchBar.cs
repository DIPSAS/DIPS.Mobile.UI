using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Searching
{
    internal class InternalSearchBar : Microsoft.Maui.Controls.SearchBar
    {

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(SearchBar));

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

        public void SendCancelTapped()
        {
            CancelCommand?.Execute(CancelCommandParameter);
        }
        
        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(
            nameof(IsBusy),
            typeof(bool),
            typeof(SearchBar));

        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }
        
        public static readonly BindableProperty ShowsCancelButtonProperty = BindableProperty.Create(
            nameof(ShowsCancelButton),
            typeof(bool),
            typeof(SearchBar), defaultValue: true);

        /// <summary>
        /// Indicates that the cancel button should be visible for people to use to cancel the search.
        /// </summary>
        public bool ShowsCancelButton
        {
            get => (bool)GetValue(ShowsCancelButtonProperty);
            set => SetValue(ShowsCancelButtonProperty, value);
        }
        
        public static readonly BindableProperty HasBusyIndicationProperty = BindableProperty.Create(
            nameof(HasBusyIndication),
            typeof(bool),
            typeof(SearchBar));

        public bool HasBusyIndication
        {
            get => (bool)GetValue(HasBusyIndicationProperty);
            set => SetValue(HasBusyIndicationProperty, value);
        }
    }
}