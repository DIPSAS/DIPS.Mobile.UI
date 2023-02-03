using System;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    public partial class BottomSheet
    {
        private bool m_shouldHaveToolbar => !string.IsNullOrEmpty(Title);

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(BottomSheet));

        /// <summary>
        /// The title of the bottom sheet for people to recognize the purpose of the sheet.
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public event EventHandler? WillClose;
        public event EventHandler? DidClose;
    }
}