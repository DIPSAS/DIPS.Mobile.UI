namespace DIPS.Mobile.UI.MemoryManagement;

public class IgnoreMemoryLeakResolvingAttachedProperty
{
    public static readonly BindableProperty IgnoreMemoryLeakResolveProperty = BindableProperty.CreateAttached("Mode",
        typeof(bool),
        typeof(IgnoreMemoryLeakResolvingAttachedProperty),
        false);
    
    public static bool GetIgnoreMemoryLeakResolve(BindableObject view)
    {
        return (bool)view.GetValue(IgnoreMemoryLeakResolveProperty);
    }

    /// <summary>
    /// The <see cref="BindableObject"/> to ignore memory leak resolve on.
    /// <remarks> Will also ignore memory leak resolving on its children.</remarks>
    /// </summary>
    public static void SetIgnoreMemoryLeakResolve(BindableObject view, bool ignoreMemoryLeakResolve)
    {
        view.SetValue(IgnoreMemoryLeakResolveProperty, ignoreMemoryLeakResolve);
    }
}