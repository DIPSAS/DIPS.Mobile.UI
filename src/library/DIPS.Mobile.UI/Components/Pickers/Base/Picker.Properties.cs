using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Pickers.Base
{
    public abstract partial class Picker
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(Picker));

        /// <summary>
        /// The title of the context of what people are picking.
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        
        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create(
            nameof(IsOpen),
            typeof(bool),
            typeof(DatePicker));

        /// <summary>
        /// Determines if the picker should be open for people to start picking.
        /// </summary>
        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        public abstract void Open();
        public abstract void Close();
    }
}