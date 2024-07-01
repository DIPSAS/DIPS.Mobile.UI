using DIPS.Mobile.UI.MemoryManagement;

namespace DIPS.Mobile.UI.Components.Shell
{
    public partial class Shell : Microsoft.Maui.Controls.Shell
    {
        private Page? m_previousPage;

        public Shell()
        {
            Navigated += OnNavigated;
        }

        private void OnNavigated(object? sender, ShellNavigatedEventArgs e)
        {
            if (m_previousPage != null)
            {
                switch (e.Source)
                {
                    case ShellNavigationSource.Pop:
                    case ShellNavigationSource.PopToRoot:
                    case ShellNavigationSource.Remove:
                    case ShellNavigationSource.ShellItemChanged:
                        _ = GCCollectionMonitor.Instance.CheckIfContentAliveOrAndTryResolveLeaks(m_previousPage.ToCollectionContentTarget());
                        m_previousPage = null;
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