using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Internal.Logging;
using DIPS.Mobile.UI.MemoryManagement;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Shell;

public partial class Shell : Microsoft.Maui.Controls.Shell
{
    private IReadOnlyCollection<PageReference>? m_previousNavigationStack;
        
    private List<ModalPageReference> m_previousModalPagesStack = [];

    /// <summary>
    /// The root page of the application.
    /// </summary>
    public static PageReference? RootPage { get; set; }
        
    public static ColorName TitleTextColorName => ColorName.color_text_default;
    public static ColorName ForegroundColorName => ColorName.color_icon_action;
    public static ColorName BackgroundColorName => ColorName.color_background_default;

    public Shell()
    {
        Navigated += OnNavigated;
            
        SetBackgroundColor(this, Colors.GetColor(BackgroundColorName));
        SetForegroundColor(this, Colors.GetColor(ForegroundColorName));
        SetTitleColor(this, Colors.GetColor(TitleTextColorName));
            
        SetTabBarBackgroundColor(this, Colors.GetColor(ColorName.color_background_default));
        SetTabBarTitleColor(this, Colors.GetColor(ColorName.color_text_action));
        SetTabBarUnselectedColor(this, Colors.GetColor(ColorName.color_icon_subtle));

        SetNavBarHasShadow(this, false);
    }

    private async void OnNavigated(object? sender, ShellNavigatedEventArgs e)
    {
        switch (e.Source)
        {
            case ShellNavigationSource.PopToRoot:
            case ShellNavigationSource.ShellItemChanged:
            case ShellNavigationSource.Pop:
            case ShellNavigationSource.Remove:
                        
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
            
        await CheckIfModalIsRemoved();

        foreach (var page in Current.Navigation?.ModalStack ?? [])
        {
            var existingPage = m_previousModalPagesStack.FirstOrDefault(p => p.Target == page);
            if (existingPage is null)
            {
                m_previousModalPagesStack.Add(new ModalPageReference(page));
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

    private async Task CheckIfModalIsRemoved()
    {
        var currentModalStack = Current.Navigation.ModalStack.ToList();
        
        var removedModals = m_previousModalPagesStack
            .Where(prevModal => prevModal.Target != null && !currentModalStack.Contains(prevModal.Target))
            .ToList();
        
        if (removedModals.Count > 0)
        {
            await TryResolvePoppedModalPages(removedModals);
            
            // Remove the handled modals from tracking
            foreach (var removedModal in removedModals)
            {
                removedModal.Dispose();
                m_previousModalPagesStack.Remove(removedModal);
            }
        }
    }

    private static async Task TryResolvePoppedModalPages(List<ModalPageReference> modalPages)
    {
        // A small delay to wait for MAUI to disconnect handlers
        await Task.Delay(100);
        
        try
        {
            foreach (var modalPage in modalPages)
            {
                GarbageCollection.Print($"🪟 Attempting to check for leaks in every page that has ever been opened in modal: {modalPage.Name}, number of pages: {modalPage.WeakPages.Count}");
                
                // The object has already been garbage collected
                if (modalPage.Target is null)
                {
                    GarbageCollection.Print($"{modalPage.Name} already garbage collected ✅");
                    continue;
                }

                var pageCollectionContentTargets = modalPage.RetrieveCollectionContentTargets();
                
                TryAutoDisconnectModalHandlers(pageCollectionContentTargets);
                
                foreach (var target in pageCollectionContentTargets)
                {
                    if (target is null || !target.IsAlive)
                    {
                        GarbageCollection.Print($"{target?.Name} inside modal was already GC'ed ✅");
                        continue;
                    }

                    GarbageCollection.Print($"Checking page inside modal: {target.Name}");
                    await GCCollectionMonitor.Instance.CheckIfObjectIsAliveAndTryResolveLeaks(target);
                }

                if (DUI.IsDebug)
                {
                    var alivePages = pageCollectionContentTargets.Where(t => t is not null && t.IsAlive).ToList();
                    var garbageCollectedPages = pageCollectionContentTargets.Where(t => t is null || !t.IsAlive).ToList();
                    
                    var alivePageNames = string.Join(", ", alivePages.Select(p => p!.Name));
                    var gcPageNames = string.Join(", ", garbageCollectedPages.Select(p => p?.Name ?? "null"));
                    
                    GarbageCollection.Print($"📊 Summary for modal {modalPage.Name}:");
                    GarbageCollection.Print($"✅ {garbageCollectedPages.Count} pages garbage collected: [{gcPageNames}]");
                    GarbageCollection.Print($"🧟 {alivePages.Count} pages still alive: [{alivePageNames}]");
                }
                
                if (modalPage.IsAlive)
                {
                    GarbageCollection.Print("Checking the actual modal navigation page...");
                    await GCCollectionMonitor.Instance.CheckIfObjectIsAliveAndTryResolveLeaks(
                        modalPage.Target.ToCollectionContentTarget());
                }
                else
                {
                    GarbageCollection.Print("The actual modal navigation page was garbage collected ✅");
                }
            }
        }
        catch (Exception e)
        {
            DUILogService.LogDebug<Shell>(e.ToString());
        }
    }

    private static void TryAutoDisconnectModalHandlers(List<CollectionContentTarget?> pageTargets)
    {
        if (!GCCollectionMonitor.TryAutoHandlerDisconnectModalPagesEnabled)
        {
            GarbageCollection.Print("Auto disconnect handlers for modals is not enabled.");
            return;
        }
        
        foreach (var target in pageTargets)
        {
            if (target?.Content.TryGetTarget(out var contentPageTarget) ?? false)
            {
                if (contentPageTarget is not ContentPage { Handler: not null } contentPage)
                {
                    continue;
                }

                GarbageCollection.Print($"ℹ️ {target.Name} did not get its handlers disconnected automatically. Disconnecting handlers...");
                contentPage.DisconnectHandlers();
            }
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
            GarbageCollection.Print("Changed root page, will wait for 5 seconds before trying to resolve/monitor memory leaks");
            await Task.Delay(5000);
        }
            
        try
        {
            foreach (var page in pages)
            {
                if (page.Target is null) //The object has already been garbage collected
                {
                    GarbageCollection.Print($"{page.Name} already garbage collected");
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