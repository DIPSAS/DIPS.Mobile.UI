using System.Net;
using DIPS.Mobile.UI.Internal.Logging;
using DIPS.Mobile.UI.MemoryManagement;

namespace DIPS.Mobile.UI.Components.Shell
{
    public partial class Shell : Microsoft.Maui.Controls.Shell
    {
        private IReadOnlyCollection<WeakReference>? m_previousNavigationStack;

        /// <summary>
        /// The root page of the application.
        /// </summary>
        public static WeakReference? RootPage { get; set; }

        public static ColorName ToolbarBackgroundColorName => ColorName.color_primary_90;
        public static ColorName ToolbarTitleTextColorName => ColorName.color_system_white;

        public Shell()
        {
            Navigated += OnNavigated;
        }

        private async void OnNavigated(object? sender, ShellNavigatedEventArgs e)
        {
            switch (e.Source)
            {
                case ShellNavigationSource.PopToRoot:
                case ShellNavigationSource.ShellItemChanged:
                case ShellNavigationSource.Pop:
                case ShellNavigationSource.Remove:
                    if (m_previousNavigationStack is not null)
                    {
                        await TryResolvePoppedPages(m_previousNavigationStack.ToList(), e.Source);
                        m_previousNavigationStack = null;
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

            var currentNavigationStack = new List<WeakReference>();
            var allPagesInNavigationStack = Current?.Navigation?.NavigationStack?.Select(p => p);
            var allModalPagesInNavigationStack = Current?.Navigation?.ModalStack.Select(p => p);

            var allPagesAcrossStacks = new List<Page>();

            if (allPagesInNavigationStack != null)
            {
                allPagesAcrossStacks.AddRange(allPagesInNavigationStack);
            }

            if (allModalPagesInNavigationStack != null)
            {
                foreach (var modalPage in allModalPagesInNavigationStack)
                {
                    if (modalPage is NavigationPage navigationPage)
                    {
                        allPagesAcrossStacks.Add(navigationPage);
                        foreach (var page in navigationPage.Navigation.NavigationStack)
                        {
                            allPagesAcrossStacks.Add(page);
                        }
                    }

                    if (!allPagesAcrossStacks.Contains(modalPage))
                    {
                        allPagesAcrossStacks.Add(modalPage);
                    }
                }
            }

            if (allPagesInNavigationStack == null) return;

            foreach (var page in allPagesAcrossStacks)
            {
                currentNavigationStack.Add(new WeakReference(page));
            }


            if (e.Source == ShellNavigationSource.ShellItemChanged) //Landed on root page, or is swapping root page
            {
                if (CurrentPage is not null)
                {
                    RootPage = new WeakReference(CurrentPage);
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

        private async Task TryResolvePoppedPages(List<WeakReference> pages,
            ShellNavigationSource shellNavigatedEventArgs)
        {
            try
            {
                foreach (var page in pages)
                {
                    if (page.Target is null) //The object has already been garbage collected
                        continue;

                    if (shellNavigatedEventArgs != ShellNavigationSource.ShellItemChanged &&
                        RootPage is {Target: Page rootPage}) //Check if we should gabarge collect when swappi
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

                    if (Current.Navigation.ModalStack.Any(p =>
                            p == page.Target)) //The page is in the modal navigation stack
                    {
                        continue;
                    }

                    var potentialNavigationPageInModalStack =
                        Current.Navigation.ModalStack.FirstOrDefault(p => p is NavigationPage);
                    if (potentialNavigationPageInModalStack is NavigationPage
                        modalNavigationPage) //The modal stack includes a NavigationPage
                    {
                        if (modalNavigationPage.Navigation.NavigationStack.Any(p =>
                                p == page
                                    .Target)) //We have to check the NavigationPage stack to see if page is still visible there
                        {
                            continue;
                        }
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