using DIPS.Mobile.UI.MemoryManagement;

namespace DIPS.Mobile.UI.Components.Shell
{
    public partial class Shell : Microsoft.Maui.Controls.Shell
    {
        private IReadOnlyCollection<WeakReference>? m_previousNavigationStack;
        private IReadOnlyCollection<WeakReference>? m_previousNavigationBindingContextStacks;
        private WeakReference? m_currentModalNavigationPage;

        public Shell()
        {
            Navigated += OnNavigated;
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
                        _ = GCCollectionMonitor.Instance.CheckIfContentAliveOrAndTryResolveLeaks(navigationPage.ToCollectionContentTarget());
                        m_currentModalNavigationPage = null;
                    }

                    if (m_previousNavigationStack is not null)
                    {
                        _ = TryResolvePoppedPages(m_previousNavigationStack.ToList());
                        m_previousNavigationStack = null;
                    }

                    if (m_previousNavigationBindingContextStacks is not null)
                    {
                        _ = TryResolvePoppedPagesBindingContext(m_previousNavigationBindingContextStacks.ToList());
                        m_previousNavigationBindingContextStacks = null;
                    }
                    
                    break;
            }

            var currentNavigationStack = new List<WeakReference>();
            var currentNavigationBindingContextStack = new List<WeakReference>();
            var allPagesInNavigationStack = Current?.Navigation?.NavigationStack?.Select(p => p)
                .Reverse();
            if(allPagesInNavigationStack == null) return;
            
            foreach (var page in allPagesInNavigationStack)
            {
                currentNavigationStack.Add(new WeakReference(page));
                if (page?.BindingContext != null)
                {
                    currentNavigationBindingContextStack.Add(new WeakReference(page.BindingContext));    
                }
            }
                

            // If we are at the landing page, the navigation stack is 1 and its first item is null, and not the CurrentPage
            // Thus, we add the CurrentPage to our navigation stack so that it can be gc'ed
            if (currentNavigationStack?.Count == 1 && currentNavigationStack.FirstOrDefault()?.Target is null)
            {
                if (CurrentPage is not null)
                {
                    currentNavigationStack = [new WeakReference(CurrentPage)];
                    if (CurrentPage.BindingContext != null)
                    {
                        currentNavigationBindingContextStack = [new WeakReference(CurrentPage.BindingContext)];
                    }
                }
            }

            m_previousNavigationStack = currentNavigationStack;
            m_previousNavigationBindingContextStacks = currentNavigationBindingContextStack;
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

        private static async Task TryResolvePoppedPages(List<WeakReference> pages)
        {
            foreach (var page in pages)
            {
                if(page.Target is null)
                    continue;

                // Don't try to resolve memory leaks if the page is still in the NavigationStack
                if (Current.Navigation.NavigationStack.Any(p => p == page.Target))
                    continue;
                
                await GCCollectionMonitor.Instance.CheckIfContentAliveOrAndTryResolveLeaks(
                    page.Target.ToCollectionContentTarget());
            }
        }
        
        private static async Task TryResolvePoppedPagesBindingContext(List<WeakReference> pageBindingContexes)
        {
            var currentPageBindingContext = Current.CurrentPage.BindingContext;
            while (pageBindingContexes.Count > 0)
            {
                var bindingContex = pageBindingContexes[0];
                if (bindingContex.Target is null)
                {
                    pageBindingContexes.RemoveAt(0);
                    continue;
                }

                if (bindingContex.Target == currentPageBindingContext)
                {
                    pageBindingContexes.Clear();
                    break;
                }
                
                pageBindingContexes.RemoveAt(0);
                await GCCollectionMonitor.Instance.CheckIfContentAliveOrAndTryResolveLeaks(
                    bindingContex.Target.ToCollectionContentTarget());
            }
        }
    }
}