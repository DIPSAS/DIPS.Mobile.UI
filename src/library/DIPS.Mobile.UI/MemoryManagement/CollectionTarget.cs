namespace DIPS.Mobile.UI.MemoryManagement;

public class CollectionTarget(string name, object target)
{
    public string Name { get; } = name;
    public WeakReference<object> Target { get; } = new(target);
}