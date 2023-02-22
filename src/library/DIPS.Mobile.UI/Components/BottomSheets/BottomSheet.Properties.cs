using System;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    public partial class BottomSheet
    {
        public static readonly BindableProperty IsDraggableProperty = BindableProperty.Create(
            nameof(IsDraggable),
            typeof(bool),
            typeof(BottomSheet), defaultValue:true);

        /// <summary>
        /// Determines if people can drag the bottom sheet.
        /// </summary>
        /// <remarks>Setting this to true will add a drag handle to the top of the bottom sheet.</remarks>
        public bool IsDraggable
        {
            get => (bool)GetValue(IsDraggableProperty);
            set => SetValue(IsDraggableProperty, value);
        }

        public event EventHandler? WillClose;
        public event EventHandler? DidClose;
        protected virtual void OnDidClose() { }
        protected virtual void OnWillClose() { }
    }
}