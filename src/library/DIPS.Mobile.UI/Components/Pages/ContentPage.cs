using DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pages
{
    public partial class ContentPage : Microsoft.Maui.Controls.ContentPage
    {
        public static readonly ColorName BackgroundColorName = ColorName.color_neutral_10;

        private CancellationTokenSource? m_garbageCollectionCts;

        public ContentPage()
        {
            if (Application.Current == null)
            {
                return;
            }

            SetColors(Application.Current.RequestedTheme);
            Application.Current.RequestedThemeChanged +=
                OnRequestedThemeChanged; //Can not use AppThemeBindings because that makes the navigation page bar background flash on Android, so we listen to changes and set the color our self
        }

        ~ContentPage()
        {
#if DEBUG
            if (ShouldGarbageCollectAndLogWhenNavigatedTo)
            {
                Console.WriteLine($"Called finalizer an instance of {GetType()}. Title of page is {Title}");
            }   
#endif
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            HasAppeared = true;

            HideOrShowFloatingNavigationMenu();
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
#if DEBUG
            if (ShouldGarbageCollectAndLogWhenNavigatedTo)
            {
                m_garbageCollectionCts = new CancellationTokenSource();
                Task.Run(async () =>
                {
                    try
                    {
                        while (true)
                        {
                            m_garbageCollectionCts.Token.ThrowIfCancellationRequested();
                            await Task.Delay(2000);
                            await MainThread.InvokeOnMainThreadAsync(() =>
                            {
                                Console.WriteLine("Force GC");
                                GC.Collect();
                                GC.WaitForPendingFinalizers();
                                Console.WriteLine("Full collection total memory: " + GC.GetTotalMemory(true));
                            });
                        }
                    }
                    catch
                    {
                    }
                });
            }
#endif
        }

        protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
        {
            m_garbageCollectionCts?.Dispose();
            m_garbageCollectionCts = null;
            base.OnNavigatingFrom(args);
        }

        private void HideOrShowFloatingNavigationMenu()
        {
            if (!HasAppeared)
                return;

            if (ShouldHideFloatingNavigationMenuButton)
            {
                FloatingNavigationButtonService.Hide();
            }
            else
            {
                FloatingNavigationButtonService.Show();
            }
        }

        private void SetColors(AppTheme osAppTheme)
        {
            this.SetAppThemeColor(BackgroundColorProperty, BackgroundColorName);

            Microsoft.Maui.Controls.Shell.SetBackgroundColor(this,
                Colors.GetColor(Shell.Shell.ToolbarBackgroundColorName, osAppTheme));
            Microsoft.Maui.Controls.Shell.SetTitleColor(this,
                Colors.GetColor(Shell.Shell.ToolbarTitleTextColorName, osAppTheme));
            Microsoft.Maui.Controls.Shell.SetForegroundColor(this,
                Colors.GetColor(Shell.Shell.ToolbarTitleTextColorName, osAppTheme));
        }


        private void OnRequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
        {
            SetColors(e.RequestedTheme);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            HasAppeared = false;

            if (Application.Current != null)
            {
                Application.Current.RequestedThemeChanged -= OnRequestedThemeChanged;
            }
        }
    }
}