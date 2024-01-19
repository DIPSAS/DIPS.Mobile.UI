using System.ComponentModel;
using DIPS.Mobile.UI.MemoryManagement;

namespace MemoryLeakTests;

public partial class App
{
    private Page? m_currentPage;
    private readonly GCCollectionMonitor m_monitor;

    public App()
    {
        m_monitor = new GCCollectionMonitor();

        InitializeComponent();
        PropertyChanged += HandleMainPageChanged;

        MainPage = new NavigationPage(new MainPage());
    }

    private void HandleMainPageChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(MainPage))
            return;

        Page? lastPage = m_currentPage;
        if (lastPage is NavigationPage lastNavPage)
            lastNavPage.Popped -= NavPageOnPopped;
        
        m_currentPage = MainPage;
        if(m_currentPage is NavigationPage currentNavPage)
            currentNavPage.Popped += NavPageOnPopped;

        if (lastPage != null)
        {
            m_monitor.Observe(lastPage);
        }
        m_monitor.CheckAliveness();
    }

    private void NavPageOnPopped(object? sender, NavigationEventArgs e)
    {
        m_monitor.Observe(e.Page);
        m_monitor.CheckAliveness();
    }
}