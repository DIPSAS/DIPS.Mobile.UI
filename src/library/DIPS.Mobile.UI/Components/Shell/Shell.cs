using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.MemoryManagement;

namespace DIPS.Mobile.UI.Components.Shell
{
    public partial class Shell : Microsoft.Maui.Controls.Shell
    {
        private Page? m_previousPage;
        public static GCCollectionMonitor Monitor { get; } = new();

        public Shell()
        {
            Navigated += OnNavigated;
        }

        private async void OnNavigated(object? sender, ShellNavigatedEventArgs e)
        {
            if (GarbageCollection.TryAutoResolveMemoryLeaksEnabled && m_previousPage != null)
            {
                switch (e.Source)
                {
                    case ShellNavigationSource.Pop:
                    case ShellNavigationSource.PopToRoot:
                    case ShellNavigationSource.Remove:
                    case ShellNavigationSource.ShellItemChanged:
                        if (DUI.IsDebug)
                        {
                            Monitor.ObservePage(m_previousPage);
                            Console.WriteLine($"OBSERVE PAGE: {m_previousPage.GetType().Name}");

                            m_previousPage = null; //not doing this will make it live forever. Moving this one line down will also make it live forever
                            await Monitor.CheckAliveness();
                        }
                        else
                        {
                            var previousPage = m_previousPage;
                            m_previousPage = null;
                            Console.WriteLine($"TRY RESOLVE MEMORY LEAK IN PAGE: {previousPage.GetType().Name}");
                            Monitor.TryResolveMemoryLeakInPage(previousPage);
                        }
                        
                        break;
                }
            }

            if (e.Source == ShellNavigationSource.Push)
            {
                m_previousPage = Current.CurrentPage;
            }
            m_previousPage ??= CurrentPage;
        }

        public static ColorName ToolbarBackgroundColorName => ColorName.color_primary_90;
        public static ColorName ToolbarTitleTextColorName => ColorName.color_system_white;
    }
}