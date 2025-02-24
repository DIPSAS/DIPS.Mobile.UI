using System.Diagnostics;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;
using DIPS.Mobile.UI.MemoryManagement;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pages
{
    public partial class ContentPage : Microsoft.Maui.Controls.ContentPage
    {
        // TODO: Lisa
        public static readonly ColorName BackgroundColorName = ColorName.color_neutral_10;

        private CancellationTokenSource? m_garbageCollectionCts;
        private readonly Stopwatch m_loadedTimer;

        public ContentPage()
        {
            if (DUI.IsDebug) //Always start the time, as checking for ShouldLogLoadingTime wont work in the constructor
            {
                m_loadedTimer = new Stopwatch();
                m_loadedTimer.Start();
            }

            if (Application.Current == null)
            {
                return;
            }

            SetColors(Application.Current.RequestedTheme);
            Application.Current.RequestedThemeChanged +=
                OnRequestedThemeChanged; //Can not use AppThemeBindings because that makes the navigation page bar background flash on Android, so we listen to changes and set the color our self

            Loaded += OnLoaded;
            
        }

        private void OnLoaded(object? sender, EventArgs e)
        {
            if (DUI.IsDebug)
            {
                m_loadedTimer.Stop();
                if (ShouldLogLoadingTime)
                {
                    Console.WriteLine(
                        $@"⏰ Time taken to load '{GetType().Namespace}': {m_loadedTimer.Elapsed:m\:ss\.fff}");
                }
            }
        }

        ~ContentPage()
        {
            if (!DUI.IsDebug)
            {
                return;
            }

            if (ShouldGarbageCollectAndLogWhenNavigatedTo || ShouldLogWhenGarbageCollected)
            {
                GarbageCollection.Print($"✅{GetType().Name} was garbage collected.");
            }
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

            if (DUI.IsDebug && ShouldGarbageCollectAndLogWhenNavigatedTo)
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
                                GarbageCollection.CollectAndWaitForPendingFinalizers();
                                GarbageCollection.Print($"Total memory after: {GC.GetTotalMemory(true)} byte");
                            });
                        }
                    }
                    catch
                    {
                    }
                });
            }
        }

        protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
        {
            if (DUI.IsDebug && ShouldGarbageCollectAndLogWhenNavigatedTo)
            {
                m_garbageCollectionCts?.Dispose();
                m_garbageCollectionCts = null;
            }

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