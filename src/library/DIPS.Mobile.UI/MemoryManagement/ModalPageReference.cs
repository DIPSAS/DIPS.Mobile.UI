namespace DIPS.Mobile.UI.MemoryManagement;

public class ModalPageReference : PageReference
{
    public ModalPageReference(Page? target) : base(target)
    {
        if (target is not NavigationPage navigationPage)
            return;

        PagesInsideModal = navigationPage.Navigation.NavigationStack.Select(p => new PageReference(p))
            .Reverse()
            .ToList();
    }

    public List<PageReference> PagesInsideModal { get; private set; } = [];
}