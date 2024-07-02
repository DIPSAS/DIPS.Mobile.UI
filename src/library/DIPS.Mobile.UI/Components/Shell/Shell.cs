using DIPS.Mobile.UI.MemoryManagement;

namespace DIPS.Mobile.UI.Components.Shell
{
    public partial class Shell : Microsoft.Maui.Controls.Shell
    {
        private IReadOnlyCollection<Page>? m_previousNavigationStack;

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
                    m_previousNavigationStack = Current.Navigation.NavigationStack.ToList();
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
                        _ = TryResolvePoppedPages(m_previousNavigationStack);
                        m_previousNavigationStack = null;
                        break;
            }
        }
        
        public static ColorName ToolbarBackgroundColorName => ColorName.color_primary_90;
        public static ColorName ToolbarTitleTextColorName => ColorName.color_system_white;

        private async Task TryResolvePoppedPages(IEnumerable<Page> pages)
        {
            foreach (var page in pages.Reverse())
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                if (page is null)
                {
                    continue;
                }

                if (page == Current.CurrentPage)
                {
                    break;
                }
                
                await GCCollectionMonitor.Instance.CheckIfContentAliveOrAndTryResolveLeaks(
                    page.ToCollectionContentTarget());
            }
        }
    }
}