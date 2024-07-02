using DIPS.Mobile.UI.MemoryManagement;

namespace DIPS.Mobile.UI.Components.Shell
{
    public partial class Shell : Microsoft.Maui.Controls.Shell
    {
        private IReadOnlyCollection<Page?>? m_previousNavigationStack;

        public Shell()
        {
            Navigated += OnNavigated;
            Navigating += OnNavigating;
        }

        private void OnNavigating(object? sender, ShellNavigatingEventArgs e)
        {
            if (Current?.Navigation?.NavigationStack is null) return;
            
            switch (e.Source)
            {
                case ShellNavigationSource.Remove:
                case ShellNavigationSource.Pop:
                case ShellNavigationSource.ShellItemChanged:
                case ShellNavigationSource.PopToRoot:
                    m_previousNavigationStack = Current.Navigation.NavigationStack.Reverse().ToList();
                    break;
            }
        }

        private void OnNavigated(object? sender, ShellNavigatedEventArgs e)
        {
            if (m_previousNavigationStack is null) return;
                switch (e.Source)
                {
                    case ShellNavigationSource.PopToRoot:
                    case ShellNavigationSource.ShellItemChanged:
                    case ShellNavigationSource.Pop:
                    case ShellNavigationSource.Remove:
                        _ = TryResolvePoppedPages(m_previousNavigationStack.ToList());
                        m_previousNavigationStack = null;
                        break;
            }
        }
        
        public static ColorName ToolbarBackgroundColorName => ColorName.color_primary_90;
        public static ColorName ToolbarTitleTextColorName => ColorName.color_system_white;

        private async Task TryResolvePoppedPages(List<Page?> pages)
        {
            while (pages.Count > 0)
            {
                var page = new WeakReference(pages[0]);
                if (page.Target is null)
                {
                    pages.RemoveAt(0);
                    continue;
                }

                if (page.Target == Current.CurrentPage)
                {
                    pages.RemoveAt(0);
                    continue;
                }
                
                pages.RemoveAt(0);
                await GCCollectionMonitor.Instance.CheckIfContentAliveOrAndTryResolveLeaks(
                    page.Target.ToCollectionContentTarget());
            }
        }
    }
}