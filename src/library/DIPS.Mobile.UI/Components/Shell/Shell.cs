using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Internal.Logging;
using DIPS.Mobile.UI.MemoryManagement;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Shell;

public partial class Shell : Microsoft.Maui.Controls.Shell
{
    private IReadOnlyCollection<PageReference>? m_previousNavigationStack;
        
    private List<ModalPageReference> m_previousModalPagesStack = [];
    
    private List<PageReference> m_previousShellContentPages = [];

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

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is not null)
            return;

        Navigated -= OnNavigated;
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
                    await TryResolvePoppedPages(m_previousNavigationStack.ToList(), e.Source, m_previousShellContentPages);
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
        
        // Track all pages across all tabs/sections so we can disconnect them when shell items change.
        m_previousShellContentPages = CollectAllShellContentPages();
    }
    
    private List<PageReference> CollectAllShellContentPages()
    {
        var pages = new List<PageReference>();
        foreach (var shellItem in Items)
        {
            foreach (var section in shellItem.Items)
            {
                foreach (var content in section.Items)
                {
                    if (content is IShellContentController { Page: Page page })
                    {
                        pages.Add(new PageReference(page));
                    }
                }
                // Also collect pushed pages in the section's navigation stack (other tabs may have pushed pages)
                foreach (var page in section.Stack)
                {
                    if (page is not null && pages.All(w => w.Target != page))
                    {
                        pages.Add(new PageReference(page));
                    }
                }
            }
        }
        return pages;
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
        await Task.Delay(1000);
        
        try
        {
            foreach (var modalPage in modalPages)
            {
                GarbageCollection.Print($"--- 🪟 Attempting to check for leaks in every page that has ever been opened in modal: {modalPage.Name}, number of pages: {modalPage.WeakPages.Count}");
                
                TryAutoDisconnectModalNavigationPageHandler(modalPage);
                
                if (GCCollectionMonitor.TryAutoClearModalToolbarItemsEnabled)
                {
                    ClearToolbarItems(modalPage);
                }
                
                // The object has already been garbage collected
                if (!modalPage.IsAlive)
                {
                    GarbageCollection.Print($"{modalPage.Name} already garbage collected ✅");
                    continue;
                }

                var pageCollectionContentTargets = modalPage.RetrieveCollectionContentTargets();
                
                TryAutoDisconnectModalPageHandlers(pageCollectionContentTargets);
                
                // Workaround for dotnet/maui#34456: Clear leaked StackNavigationManager fields
                // AFTER Disconnect() has run (so it can properly unsubscribe events first)
                modalPage.ClearCapturedNavigationManagerReferences();
                
                foreach (var target in pageCollectionContentTargets)
                {
                    if (target is null || !target.IsAlive)
                    {
                        GarbageCollection.Print($"{target?.Name} inside modal was already GC'ed ✅");
                        continue;
                    }
                    
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

                GarbageCollection.Print(modalPage.IsAlive ? $"🧟 {modalPage.Name} is still alive" : $"✅ {modalPage.Name} was garbage collected");
            }
        }
        catch (Exception e)
        {
            DUILogService.LogDebug<Shell>(e.ToString());
        }
    }

    private static void TryAutoDisconnectModalNavigationPageHandler(ModalPageReference modalPage)
    {
        var modalTarget = modalPage.Target;
        if (modalTarget is NavigationPage navigationPage)
        {
            if (navigationPage.Handler is not null)
            {
                GarbageCollection.Print($"ℹ️ {modalPage.Name} did not get its handlers disconnected automatically from .NET MAUI.");

                if (GCCollectionMonitor.TryAutoHandlerDisconnectModalPagesEnabled)
                {
                    GarbageCollection.Print("🔧 Disconnecting handler manually...");
                    navigationPage.DisconnectHandlers();
                }
            }
            else
            {
                GarbageCollection.Print($"ℹ️ {modalPage.Name} had its handlers disconnected automatically from .NET MAUI.");
            }
        }
    }

    private static void TryAutoDisconnectModalPageHandlers(List<CollectionContentTarget?> pageTargets)
    {
        foreach (var target in pageTargets)
        {
            if (target?.Content.TryGetTarget(out var contentPageTarget) ?? false)
            {
                if (contentPageTarget is not ContentPage { Handler: not null } contentPage)
                {
                    GarbageCollection.Print($"ℹ️ {target.Name} had its handlers disconnected automatically from .NET MAUI.");
                    continue;
                }

                GarbageCollection.Print($"ℹ️ {target.Name} did not get its handlers disconnected automatically.");
                
                if (!GCCollectionMonitor.TryAutoHandlerDisconnectModalPagesEnabled)
                    continue;

                GarbageCollection.Print("🔧 Disconnecting handler manually...");
                contentPage.DisconnectHandlers();
            }
        }
    }

    /// <summary>
    /// Clears ToolbarItems on modal pages to break the reference chain from native toolbar infrastructure
    /// back to the page. Without this, the native navigation bar retains toolbar item handlers which root
    /// the page, and any x:Name'd elements remain alive through the generated code-behind fields.
    /// TODO: May remove this method when https://github.com/dotnet/maui/issues/34892 is merged
    /// </summary>
    private static void ClearToolbarItems(ModalPageReference modalPage)
    {
        GarbageCollection.Print("ℹ️ Clearing ToolbarItems...");
        
        foreach (var weakPage in modalPage.WeakPages)
        {
            if (weakPage.Target is Page page)
            {
                page.ToolbarItems.Clear();
            }
        }
        
        if (modalPage.Target is Page modalRoot)
        { 
            modalRoot.ToolbarItems.Clear();
        }
    }

    private async static Task TryResolvePoppedPages(List<PageReference> pages,
        ShellNavigationSource shellNavigatedEventArgs,
        List<PageReference> previousShellContentPages)
    {

        var pagesToMonitor = pages;
        
        if (shellNavigatedEventArgs is ShellNavigationSource.ShellItemChanged)
        {
            pagesToMonitor = previousShellContentPages;
            
            // We need a delay here, because it takes some time for Shell to animate to the new root page.
            // Causing it to be still visible, disconnecting the handler while the page is visible, will cause a crash.
            // We set a delay of 5 seconds to be 100% sure that the animation is done, even though we could use a lower delay.
            GarbageCollection.Print("Changed root page, will wait for 5 seconds before trying to resolve/monitor memory leaks");
            await Task.Delay(5000);
            
            // Workaround: When Shell items change, MAUI disconnects the page's handler but not its
            // child components' handlers. Disconnect content handlers for ALL pages across all tabs,
            // not just the active tab's navigation stack.
            // TODO: Remove when this is merged https://github.com/dotnet/maui/issues/34898
            // NOTE: Extracted to a non-async method to avoid the compiler hoisting strong references
            // to ContentPage as state machine fields, which would root the pages during the GC monitoring loop.
            DisconnectShellContentPageHandlers(previousShellContentPages);
        }
            
        try
        {
            foreach (var page in pagesToMonitor)
            {
                if (!page.IsAlive) //The object has already been garbage collected
                {
                    GarbageCollection.Print($"✅ {page.Name} already garbage collected");
                    continue;
                }

                // Check if page is still in navigation stack - do this in a limited scope
                var shouldSkip = false;
                {
                    var pageTarget = page.Target;
                    
                    if (shellNavigatedEventArgs != ShellNavigationSource.ShellItemChanged &&
                        RootPage is {Target: Page rootPage}) //Check if we should garbage collect when swapping
                    {
                        if (pageTarget == rootPage)
                        {
                            shouldSkip = true;
                        }
                    }

                    //Don't try to resolve memory leaks for the following cases, because the page is still visible.
                    if (!shouldSkip && Current?.Navigation?.NavigationStack != null && pageTarget != null)
                    {
                        shouldSkip = Current.Navigation.NavigationStack.Contains(pageTarget);
                    }
                    // pageTarget and rootPage go out of scope here
                }
                
                if (shouldSkip)
                    continue;

                // Create CollectionContentTarget in a limited scope
                CollectionContentTarget? collectionContentTarget = null;
                {
                    var pageTarget = page.Target;
                    if (pageTarget != null)
                    {
                        collectionContentTarget = pageTarget.ToCollectionContentTarget();
                    }
                    // pageTarget goes out of scope here
                }
                
                await GCCollectionMonitor.Instance.CheckIfObjectIsAliveAndTryResolveLeaks(collectionContentTarget);
            }
        }
        catch (Exception e)
        {
            DUILogService.LogDebug<Shell>(e.ToString());
        }
    }

    private static void DisconnectShellContentPageHandlers(List<PageReference> previousShellContentPages)
    {
        var pageNames = new List<string>();
        foreach (var pageRef in previousShellContentPages)
        {
            if (pageRef.Target is not ContentPage { Content: not null } contentPage)
                continue;
            
            pageNames.Add(contentPage.GetType().Name);
#if __ANDROID__
            contentPage.DisconnectHandlers();
#endif
            contentPage.Content.DisconnectHandlers();
        }
        
        GarbageCollection.Print($"🔧 Disconnected content handlers for: {string.Join(", ", pageNames)}");
    }
}