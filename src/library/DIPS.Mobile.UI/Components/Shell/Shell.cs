using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Shell
{
    public class Shell : Xamarin.Forms.Shell
    {
        public static readonly ColorName ToolbarBackgroundColorName = ColorName.color_primary_90;
        public static readonly ColorName ToolbarTitleTextColorName = ColorName.color_system_white;

        public Shell()
        {
            SetColors(Application.Current.RequestedTheme);
            Application.Current.RequestedThemeChanged +=
                OnRequestedThemeChanged; //Can not use AppThemeBindings because that makes the navigation page bar background flash on Android, so we listen to changes and set the color our self
        }

        private void SetColors(OSAppTheme osAppTheme)
        {
            SetBackgroundColor(this, Colors.GetColor(ToolbarBackgroundColorName, osAppTheme));
            SetTitleColor(this, Colors.GetColor(ToolbarTitleTextColorName, osAppTheme));
            SetForegroundColor(this, Colors.GetColor(ToolbarTitleTextColorName, osAppTheme));
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