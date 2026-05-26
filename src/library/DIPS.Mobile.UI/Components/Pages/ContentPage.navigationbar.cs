using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using MauiShell = Microsoft.Maui.Controls.Shell;

namespace DIPS.Mobile.UI.Components.Pages
{
    public partial class ContentPage
    {
        protected override void OnParentSet()
        {
            base.OnParentSet();

            ApplyNavigationBarColors();
        }

        private void SetDefaultNavigationBarColors()
        {
            SetDefaultNavigationBarColor(
                NavigationPage.BarBackgroundColorProperty,
                GetShellBackgroundColor(this) ?? GetShellBackgroundColor(MauiShell.Current) ?? Colors.GetColor(Shell.Shell.BackgroundColorName));

            SetDefaultNavigationBarColor(
                NavigationPage.BarTextColorProperty,
                GetShellTitleColor(this) ?? GetShellTitleColor(MauiShell.Current) ?? Colors.GetColor(Shell.Shell.TitleTextColorName));
        }

        private void ApplyNavigationBarColors()
        {
            if (Parent is not NavigationPage navigationPage)
                return;

            SetDefaultNavigationBarColors();
            navigationPage.BarBackgroundColor = GetNavigationBarPropertyColor(NavigationPage.BarBackgroundColorProperty);
            navigationPage.BarTextColor = GetNavigationBarPropertyColor(NavigationPage.BarTextColorProperty);
            RefreshStatusBarTextOnPlatform(navigationPage);
        }

        private Color GetNavigationBarPropertyColor(BindableProperty navigationBarColorProperty)
        {
            return (Color)GetValue(navigationBarColorProperty);
        }

        internal static bool IsModalNavigationPage(NavigationPage navigationPage)
        {
            return MauiShell.Current?.Navigation.ModalStack.Contains(navigationPage) == true
                   || Application.Current?.Windows.Any(window => window.Page?.Navigation.ModalStack.Contains(navigationPage) == true) == true;
        }

        private void SetDefaultNavigationBarColor(BindableProperty navigationBarColorProperty, Color color)
        {
            if (IsSet(navigationBarColorProperty))
                return;

            SetValue(navigationBarColorProperty, color);
        }

        private static Color? GetShellBackgroundColor(BindableObject? bindableObject)
        {
            return bindableObject is null ? null : MauiShell.GetBackgroundColor(bindableObject);
        }

        private static Color? GetShellTitleColor(BindableObject? bindableObject)
        {
            return bindableObject is null ? null : MauiShell.GetTitleColor(bindableObject);
        }

        private partial void RefreshStatusBarTextOnPlatform(NavigationPage navigationPage);
    }
}