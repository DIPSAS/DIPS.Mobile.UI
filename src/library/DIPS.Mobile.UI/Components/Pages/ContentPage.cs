using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Extensions;

namespace DIPS.Mobile.UI.Components.Pages
{
    public partial class ContentPage : Microsoft.Maui.Controls.ContentPage
    {
        public static readonly ColorName BackgroundColorName = ColorName.color_neutral_30;

        public ContentPage()
        {
            this.SetAppThemeColor(BackgroundColorProperty, BackgroundColorName);
        }

        internal void SendOnContentAppearing()
        {
            OnContentAppearing();
            ContentAppearing?.Invoke(this, EventArgs.Empty);
        }
        
        internal bool ShouldUpdatePaddingWithSafeArea(Thickness thickness)
        {
            SafeAreaInsetsDidChange(thickness);
            return true;
        }
    }
}