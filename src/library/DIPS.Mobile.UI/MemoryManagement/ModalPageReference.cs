namespace DIPS.Mobile.UI.MemoryManagement;

public class ModalPageReference : PageReference, IDisposable
{
    private readonly object? m_capturedNavigationManager;

    public ModalPageReference(Page? target) : base(target)
    {
        if (target is not NavigationPage navigationPage)
        {
            // Modal page without NavigationPage - track it directly
            if (target is not null)
                WeakPages.Add(new PageReference(target));
            return;
        }
        
        // Capture StackNavigationManager while handler is still connected (workaround for dotnet/maui#34456)
        m_capturedNavigationManager = StackNavigationManagerWorkaround.CaptureNavigationManager(navigationPage);
        
        WeakPages.Add(new PageReference(navigationPage.RootPage));
        navigationPage.Pushed += NavigationPageOnPushed;
    }

    private void NavigationPageOnPushed(object? sender, NavigationEventArgs e)
    {
        if(Target is NavigationPage navigationPage)
            WeakPages.Insert(0, new PageReference(navigationPage.CurrentPage));
    }

    public List<PageReference> WeakPages { get; } = [];

    public List<CollectionContentTarget?> RetrieveCollectionContentTargets()
    {
        return WeakPages
            .Select(pageReference => pageReference.Target?.ToCollectionContentTarget())
            .ToList();
    }

    /// <summary>
    /// Clears leaked StackNavigationManager fields (workaround for dotnet/maui#34456).
    /// Must be called before GC checks, not in Dispose (which runs after).
    /// </summary>
    internal void ClearCapturedNavigationManagerReferences()
    {
        StackNavigationManagerWorkaround.ClearLeakedReferences(m_capturedNavigationManager);
    }
    
    public void Dispose()
    {
        if (Target is not NavigationPage navigationPage)
            return;
        
        navigationPage.Pushed -= NavigationPageOnPushed;
    }
}