namespace DIPS.Mobile.UI.MemoryManagement;

public class PageReference : WeakReference
{
    public PageReference(object? target) : base(target)
    {
        if (target is NavigationPage navigationPage)
        {
            Name = $"{navigationPage.GetType().Name} (Root Page: {navigationPage.RootPage.GetType().Name})";
        }
        else
        {
            Name = target?.GetType().Name ?? "Undefined";
        }
    }

    public string Name { get; }
}