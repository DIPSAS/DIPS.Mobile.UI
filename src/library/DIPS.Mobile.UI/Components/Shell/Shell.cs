using DIPS.Mobile.UI.Resources.Colors;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Shell
{
    public class Shell : Microsoft.Maui.Controls.Shell
    {
        public static readonly ColorName ToolbarBackgroundColorName = ColorName.color_primary_90;
        public static readonly ColorName ToolbarTitleTextColorName = ColorName.color_system_white;

        public Shell()
        {
            SetColors(Application.Current.RequestedTheme);
            Application.Current.RequestedThemeChanged +=
                OnRequestedThemeChanged; //Can not use AppThemeBindings because that makes the navigation page bar background flash on Android, so we listen to changes and set the color our self
        }

        private void SetColors(AppTheme osAppTheme)
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