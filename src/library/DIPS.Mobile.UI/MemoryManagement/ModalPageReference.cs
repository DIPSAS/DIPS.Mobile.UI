namespace DIPS.Mobile.UI.MemoryManagement;

public class ModalPageReference : PageReference, IDisposable
{
    public ModalPageReference(Page? target) : base(target)
    {
        if (target is not NavigationPage navigationPage)
            return;
        
        PagesInsideModal = navigationPage.Navigation.NavigationStack.Select(p => new PageReference(p))
            .Reverse()
            .ToList();
        
        navigationPage.Pushed += NavigationPageOnPushed;
    }

    private void NavigationPageOnPushed(object? sender, NavigationEventArgs e)
    {
        PagesInsideModal.Add(new PageReference(e.Page));
    }

    public List<PageReference> PagesInsideModal { get; } = [];

    public void Dispose()
    {
        if (Target is not NavigationPage navigationPage)
            return;
        
        navigationPage.Pushed -= NavigationPageOnPushed;
    }
}