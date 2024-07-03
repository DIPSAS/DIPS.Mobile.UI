using DIPS.Mobile.UI.MemoryManagement;

namespace DIPS.Mobile.UI.Components.Shell
{
    public partial class Shell : Microsoft.Maui.Controls.Shell
    {
        private IReadOnlyCollection<WeakReference>? m_previousNavigationStack;
        private WeakReference? m_currentModalNavigationPage;

        public Shell()
        {
            Navigated += OnNavigated;
            Navigating += OnNavigating;
        }

        private void OnNavigating(object? sender, ShellNavigatingEventArgs e)
        {
            switch (e.Source)
            {
                case ShellNavigationSource.Remove:
                case ShellNavigationSource.Pop:
                case ShellNavigationSource.ShellItemChanged:
                case ShellNavigationSource.PopToRoot:
                    var currentNavigationStack = Current?.Navigation?.NavigationStack
                        .Select(p => new WeakReference(p))
                        .Reverse()
                        .ToList();

                    if (currentNavigationStack is not null)
                    {
                        m_previousNavigationStack = currentNavigationStack;
                        break;
                    }
                    
                    // Sometimes NavigationStack is empty, but CurrentPage has value
                    if (CurrentPage is not null)
                    {
                        m_previousNavigationStack = new[] { new WeakReference(CurrentPage) };
                    }
                    
                    break;
            }
        }

        private void OnNavigated(object? sender, ShellNavigatedEventArgs e)
        {
            if (e.Source is ShellNavigationSource.Push)
            {
                if (m_currentModalNavigationPage is null 
                    && Current.Navigation.ModalStack.Count > 0 
                    && Current.Navigation.ModalStack[^1] is NavigationPage navigationPage)
                {
                    // Just pushed a modal navigation page
                    m_currentModalNavigationPage = new WeakReference(navigationPage);
                    navigationPage.Popped += CurrentModalNavigationPage_OnPopped;
                }
                else if(Current.Navigation.ModalStack.Count == 1 && Current.Navigation.ModalStack[^1] is ContentPage contentPage)
                {
                    // Just pushed a regular modal page
                    contentPage.NavigatedFrom += CurrentModalPage_OnPopped;
                }
            }
            
            switch (e.Source)
            {
                case ShellNavigationSource.PopToRoot:
                case ShellNavigationSource.ShellItemChanged:
                case ShellNavigationSource.Pop:
                case ShellNavigationSource.Remove:
                    if (m_currentModalNavigationPage?.Target is NavigationPage navigationPage && !Current.Navigation.ModalStack.Contains(navigationPage))
                    {
                        // Closed the modal navigation page
                        navigationPage.Popped -= CurrentModalNavigationPage_OnPopped;
                        _ = GCCollectionMonitor.Instance.CheckIfContentAliveOrAndTryResolveLeaks(m_currentModalNavigationPage.ToCollectionContentTarget());
                        m_currentModalNavigationPage = null;
                    }

                    if (m_previousNavigationStack is not null)
                    {
                        _ = TryResolvePoppedPages(m_previousNavigationStack.ToList());
                        m_previousNavigationStack = null;
                    }
                    
                    break;
            }
        }

        private static void CurrentModalPage_OnPopped(object? sender, NavigatedFromEventArgs e)
        {
            if(sender is not ContentPage contentPage)
                return;
            
            _ = GCCollectionMonitor.Instance.CheckIfContentAliveOrAndTryResolveLeaks(
                contentPage.ToCollectionContentTarget());
        }

        private void CurrentModalNavigationPage_OnPopped(object? sender, NavigationEventArgs e)
        {
            _ = GCCollectionMonitor.Instance.CheckIfContentAliveOrAndTryResolveLeaks(
                e.Page.ToCollectionContentTarget());
        }

        public static ColorName ToolbarBackgroundColorName => ColorName.color_primary_90;
        public static ColorName ToolbarTitleTextColorName => ColorName.color_system_white;

        private async Task TryResolvePoppedPages(List<WeakReference> pages)
        {
            while (pages.Count > 0)
            {
                var page = pages[0];
                if (page.Target is null)
                {
                    pages.RemoveAt(0);
                    continue;
                }

                if (page.Target == Current.CurrentPage)
                {
                    pages.Clear();
                    break;
                }
                
                pages.RemoveAt(0);
                await GCCollectionMonitor.Instance.CheckIfContentAliveOrAndTryResolveLeaks(
                    page.Target.ToCollectionContentTarget());
            }
        }
    }
}