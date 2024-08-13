namespace DIPS.Mobile.UI.MemoryManagement;

public class PageReference : WeakReference
{
    public PageReference(object? target) : base(target)
    {
        Name = target?.GetType().Name ?? "Undefined";
    }

    public string Name { get; }
}