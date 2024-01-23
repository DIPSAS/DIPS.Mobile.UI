using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.MemoryManagement;

namespace DIPS.Mobile.UI.Components.Shell
{
    public partial class Shell : Microsoft.Maui.Controls.Shell
    {
        private Page? m_previousPage;
        private readonly GCCollectionMonitor m_monitor;

        public Shell()
        {
            Navigated += OnNavigated;
            m_monitor = new GCCollectionMonitor();
        }

        private async void OnNavigated(object? sender, ShellNavigatedEventArgs e)
        {
            if (DUI.IsDebug && ShouldGarbageCollectPreviousPage && m_previousPage != null)
            {
                switch (e.Source)
                {
                    case ShellNavigationSource.Pop:
                    case ShellNavigationSource.PopToRoot:
                    case ShellNavigationSource.Remove:
                        m_monitor.Observe(m_previousPage);
                        m_previousPage = null; //not doing this will make it live forever. Moving this one line down will also make it live forever
                        await m_monitor.CheckAliveness();
                        break;
                }
            }

            if (e.Source == ShellNavigationSource.Push)
            {
                m_previousPage = Current.CurrentPage;
            }
        }

        public static ColorName ToolbarBackgroundColorName => ColorName.color_primary_90;
        public static ColorName ToolbarTitleTextColorName => ColorName.color_system_white;
    }
}