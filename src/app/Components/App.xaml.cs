using System.ComponentModel;
using Components.ComponentsSamples.Pickers;
using Components.Resources.LocalizedStrings;
using Components.Services;
using DIPS.Mobile.UI.API.GarbageCollection;

namespace Components;

public partial class App : Application
{
    private readonly AppCenterService m_appCenterService;
    private readonly GCCollectionMonitor? m_monitor;
    private Page? m_currentPage;
    private readonly Shell m_shell;
    private Page m_previousPage;

    public App()
    {
#if DEBUG
        m_monitor = new GCCollectionMonitor();

#endif
        InitializeComponent();

        m_shell = new Shell();
        var tabBar = new TabBar();
        var tab = new Tab();
        PropertyChanged += HandleMainPageChanged;

        tab.Items.Add(new ShellContent()
        {
            ContentTemplate =
                new DataTemplate(() =>
                    new MainPage(
                        new List<SampleType> {SampleType.Resources, SampleType.Components}.OrderBy(s => s.ToString()),
                        REGISTER_YOUR_SAMPLES_HERE.RegisterSamples()))
        });
        tabBar.Items.Add(tab);
        m_shell.Items.Add(tabBar);
        MainPage = m_shell;
        m_shell.Navigated += ShellOnNavigated;
        m_shell.NavigatedFrom += ShellOnNavigatedFrom;

        m_appCenterService = new AppCenterService();
    }

    private void ShellOnNavigatedFrom(object? sender, NavigatedFromEventArgs e)
    {
    }

    private void ShellOnNavigated(object? sender, ShellNavigatedEventArgs e)
    {
        if (e.Source == ShellNavigationSource.Push)
        {
            m_currentPage = m_shell.CurrentPage;
        }

        if (e.Source is ShellNavigationSource.Pop or ShellNavigationSource.PopToRoot)
        {
            m_monitor?.MonitorForCollection(m_currentPage);
            m_monitor?.CollectAndCheck();
            m_currentPage = m_shell.CurrentPage;
        }
    }

    protected override void OnStart()
    {
        _ = TryGetLatestVersion();

        base.OnStart();
    }

    private async Task<bool> TryGetLatestVersion()
    {
#if DEBUG
        return true;
#endif

        var release = await m_appCenterService.GetLatestVersion();
        if (release != null)
        {
            var latestVersion = new Version(release.Version);
            var currentVersion = AppInfo.Version;
            if (currentVersion >= latestVersion)
            {
                return false;
            }

            if (Current?.MainPage == null) return true;
            var wantToDownload = await Current.MainPage.DisplayAlert(LocalizedStrings.New_version,
                LocalizedStrings.New_version_message, LocalizedStrings.Download, LocalizedStrings.Cancel);
            if (!wantToDownload)
            {
                return false;
            }

            await Launcher.OpenAsync(release.InstallUri);
            return true;
        }

        return false;
    }

    protected override void OnResume()
    {
        _ = TryGetLatestVersion();
        base.OnResume();
    }

    private void HandleMainPageChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(MainPage))
            return;

        Page? lastPage = m_currentPage;
        if (lastPage is NavigationPage lastNavPage)
            lastNavPage.Popped -= NavPageOnPopped;

        m_currentPage = MainPage;
        if (m_currentPage is NavigationPage currentNavPage)
            currentNavPage.Popped += NavPageOnPopped;


        if (lastPage != null)
            m_monitor?.MonitorForCollection(lastPage);

        m_monitor?.CollectAndCheck();
    }

    private void NavPageOnPopped(object? sender, NavigationEventArgs e)
    {
        m_monitor?.MonitorForCollection(e.Page);
        m_monitor?.CollectAndCheck();
    }
}