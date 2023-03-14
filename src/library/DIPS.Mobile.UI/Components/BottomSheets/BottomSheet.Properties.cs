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

        public static readonly BindableProperty ShouldFitToContentProperty = BindableProperty.Create(
            nameof(ShouldFitToContent),
            typeof(bool),
            typeof(BottomSheet));

        /// <summary>
        /// Determines if the bottom sheet should fit the content of the bottom sheet.
        /// </summary>
        /// <remarks>Will work every time on Android, will only work for iOS equal or higher than 16.0. Will go to fullscreen if iOS version is less than 16.0 and the content is bigger than half the size of the screen.</remarks>
        public bool ShouldFitToContent
        {
            get => (bool)GetValue(ShouldFitToContentProperty);
            set => SetValue(ShouldFitToContentProperty, value);
        }

        public event EventHandler? WillClose;
        public event EventHandler? DidClose;
        protected virtual void OnDidClose() { }
        protected virtual void OnWillClose() { }
    }
}