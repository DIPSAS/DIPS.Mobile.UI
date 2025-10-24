namespace DIPS.Mobile.UI.MemoryManagement;

public class ModalPageReference : PageReference, IDisposable
{
    public ModalPageReference(Page? target) : base(target)
    {
        if (target is not NavigationPage navigationPage)
            return;
        
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
    
    public void Dispose()
    {
        if (Target is not NavigationPage navigationPage)
            return;
        
        navigationPage.Pushed -= NavigationPageOnPushed;
    }
}