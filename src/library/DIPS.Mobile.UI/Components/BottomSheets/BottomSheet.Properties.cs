using DIPS.Mobile.UI.Components.BottomSheets.ToolbarConfiguration;
using Colors = Microsoft.Maui.Graphics.Colors;
using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    public partial class BottomSheet
    {

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
        /// Determines if the bottom sheet should have a <see cref="Components.Searching.SearchBar"/> at the top
        /// </summary>
        public bool HasSearchBar
        {
            get => (bool)GetValue(HasSearchBarProperty);
            set => SetValue(HasSearchBarProperty, value);
        }
        
        public SearchBar SearchBar { get; } = new() { HasCancelButton = false, BackgroundColor = Colors.Transparent };
        
        public static readonly BindableProperty ShouldFitToContentProperty = BindableProperty.Create(
            nameof(ShouldFitToContent),
            typeof(bool),
            typeof(BottomSheet));
        
        public static readonly BindableProperty HasSearchBarProperty = BindableProperty.Create(
            nameof(HasSearchBar),
            typeof(bool),
            typeof(BottomSheet));

        public event EventHandler? WillClose;
        protected virtual void OnWillClose() { }
    }
}