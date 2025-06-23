using DIPS.Mobile.UI.Internal.Logging;
using DIPS.Mobile.UI.MemoryManagement;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Shell
{
    public partial class Shell : Microsoft.Maui.Controls.Shell
    {
        private IReadOnlyCollection<PageReference>? m_previousNavigationStack;
        
        private List<ModalPageReference> m_previousModalPages = [];

        /// <summary>
        /// The root page of the application.
        /// </summary>
        public static PageReference? RootPage { get; set; }

        public static ColorName ToolbarTitleTextColorName => ColorName.color_text_default;
        public static ColorName ToolbarForegroundColorName => ColorName.color_icon_action;
        public static ColorName ToolbarBackgroundColorName => ColorName.color_background_default;

        public Shell()
        {
            Navigated += OnNavigated;
            
            SetTabBarBackgroundColor(this, Colors.GetColor(ColorName.color_surface_subtle));
            SetTabBarTitleColor(this, Colors.GetColor(ColorName.color_text_action));
            SetTabBarUnselectedColor(this, Colors.GetColor(ColorName.color_icon_subtle));
            
        }

        private async void OnNavigated(object? sender, ShellNavigatedEventArgs e)
        {
            switch (e.Source)
            {
                case ShellNavigationSource.PopToRoot:
                case ShellNavigationSource.ShellItemChanged:
                case ShellNavigationSource.Pop:
                case ShellNavigationSource.Remove:
                    
                    if (m_previousModalPages.Count > 0)
                    {
                        await TryResolvePoppedModalPages(m_previousModalPages.ToList());
                        m_previousModalPages = [];
                    }
                        
                    if(m_previousNavigationStack is not null)
                    {
                        await TryResolvePoppedPages(m_previousNavigationStack.ToList(), e.Source);
                    }

                    break;
                case ShellNavigationSource.Unknown:
                    break;
                case ShellNavigationSource.Push:
                    break;
                case ShellNavigationSource.Insert:
                    break;
                case ShellNavigationSource.ShellSectionChanged:
                    break;
                case ShellNavigationSource.ShellContentChanged:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            foreach (var page in Current?.Navigation?.ModalStack ?? [])
            {
                var existingPage = m_previousModalPages.FirstOrDefault(p => p.Target == page);
                if (existingPage is null)
                {
                    m_previousModalPages.Add(new ModalPageReference(page));
                }
            }
            
            var allPagesInNavigationStack = Current?.Navigation?.NavigationStack?.Select(p => p);
            if (allPagesInNavigationStack == null) 
                return;

            var currentNavigationStack = allPagesInNavigationStack
                .Select(page => new PageReference(page))
                .ToList();

            if (e.Source == ShellNavigationSource.ShellItemChanged) //Landed on root page, or is swapping root page
            {
                if (CurrentPage is not null)
                {
                    RootPage = new PageReference(CurrentPage);
                    currentNavigationStack = [RootPage];
                }
            }

            currentNavigationStack.Reverse(); //To get the latest first

            if (currentNavigationStack[^1].Target ==
                null) //Update the root page as it gets nullified by MAUI using Shell for each navigation...
            {
                if (RootPage != null)
                {
                    currentNavigationStack.Remove(currentNavigationStack[^1]);
                    currentNavigationStack.Add(RootPage);
                }
            }

            m_previousNavigationStack = currentNavigationStack;
        }

        private static async Task TryResolvePoppedModalPages(List<ModalPageReference> modalPages)
        {
            var modalPagesInStack = Current?.Navigation?.ModalStack.Select(p => p).ToList() ?? [];
            
            try
            {
                foreach (var page in modalPages)
                {
                    page.Dispose();
                    
                    // The object has already been garbage collected
                    if (page.Target is null)
                    {
                        DUILogService.LogDebug<Shell>($"{page.Name} already garbage collected");
                        continue;
                    }

                    // The modal page was not popped
                    if (modalPagesInStack.Contains(page.Target))
                        continue;
                    
                    // We first try resolve the root page, and check if it is GC'ed
                    await GCCollectionMonitor.Instance.CheckIfObjectIsAliveAndTryResolveLeaks(
                        page.Target.ToCollectionContentTarget());
                    
                    // If the modal is a NavigationPage, we also check the pages inside the NavigationPage, AFTER we have checked the root page
                    foreach (var pageInsideModal in page.PagesInsideModal)
                    {
                        if (pageInsideModal.Target is null)
                        {
                            DUILogService.LogDebug<Shell>($"{pageInsideModal.Name} inside modal was already garbage collected");
                            continue;
                        }
                        
                        await GCCollectionMonitor.Instance.CheckIfObjectIsAliveAndTryResolveLeaks(
                            pageInsideModal.Target?.ToCollectionContentTarget());
                    }
                }
            }
            catch (Exception e)
            {
                DUILogService.LogDebug<Shell>(e.ToString());
            }
        }
        
        private async static Task TryResolvePoppedPages(List<PageReference> pages,
            ShellNavigationSource shellNavigatedEventArgs)
        {

            if (shellNavigatedEventArgs is ShellNavigationSource.ShellItemChanged)
            {
                // We need a delay here, because it takes some time for Shell to animate to the new root page.
                // Causing it to be still visible, disconnecting the handler while the page is visible, will cause a crash.
                // We set a delay of 5 seconds to be 100% sure that the animation is done, even though we could use a lower delay.
                DUILogService.LogDebug<Shell>("Changed root page, will wait for 5 seconds before trying to resolve/monitor memory leaks");
                await Task.Delay(5000);
            }
            
            try
            {
                foreach (var page in pages)
                {
                    if (page.Target is null) //The object has already been garbage collected
                    {
                        DUILogService.LogDebug<Shell>($"{page.Name} already garbage collected");
                        continue;
                    }

                    if (shellNavigatedEventArgs != ShellNavigationSource.ShellItemChanged &&
                        RootPage is {Target: Page rootPage}) //Check if we should garbage collect when swapping
                    {
                        if (page.Target == rootPage)
                        {
                            continue;
                        }
                    }

                    //Don't try to resolve memory leaks for the following cases, because the page is still visible.
                    if (Current.Navigation.NavigationStack.Any(p =>
                            p == page.Target)) //The page is in the navigation stack
                    {
                        continue;
                    }

                    await GCCollectionMonitor.Instance.CheckIfObjectIsAliveAndTryResolveLeaks(
                        page.Target?.ToCollectionContentTarget());
                }
            }
            catch (Exception e)
            {
                DUILogService.LogDebug<Shell>(e.ToString());
            }
        }
    }
}