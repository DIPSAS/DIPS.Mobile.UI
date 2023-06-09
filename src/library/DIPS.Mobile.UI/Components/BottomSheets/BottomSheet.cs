using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    public partial class BottomSheet : ContentPage
    {
        internal static ColorName ToolbarBackgroundColorName => ColorName.color_neutral_05;
        internal static ColorName ToolbarTextColorName => ColorName.color_system_black;
        internal static ColorName ToolbarActionButtonsName => ColorName.color_primary_90;

        public BottomSheet()
        {
            this.SetAppThemeColor(BackgroundColorProperty, ToolbarBackgroundColorName);
        }
        
        public void Close()
        {
            WillClose?.Invoke(this, EventArgs.Empty);
            OnWillClose();
        }
        
    }
}