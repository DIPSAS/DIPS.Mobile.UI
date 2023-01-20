using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Pages
{
    public class NavigationPage : Xamarin.Forms.NavigationPage
    {
        public static readonly ColorName BackgroundColorName = ColorName.color_primary_light_primary_80;
        public static readonly ColorName BarBackgroundColorName = ColorName.color_primary_light_primary_100;
        public static readonly ColorName BarTextColorName = ColorName.color_system_white;
        public NavigationPage(Xamarin.Forms.ContentPage contentPage) : base(contentPage)
        {
            SetColors(Application.Current.RequestedTheme);
            Application.Current.RequestedThemeChanged +=
                OnRequestedThemeChanged; //Can not use AppThemeBindings because that makes the navigation page bar background flash on Android, so we listen to changes and set the color our self
        }

        private void SetColors(OSAppTheme osAppTheme)
        {
            BackgroundColor = Colors.GetColor(BackgroundColorName, osAppTheme);
            BarBackgroundColor = Colors.GetColor(BarBackgroundColorName, osAppTheme);
            BarTextColor = Colors.GetColor(BarTextColorName, osAppTheme);
        }

        private void OnRequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            SetColors(e.RequestedTheme);
        }

        protected override void OnDisappearing()
        {
            Application.Current.RequestedThemeChanged += OnRequestedThemeChanged;
        }
    }
}