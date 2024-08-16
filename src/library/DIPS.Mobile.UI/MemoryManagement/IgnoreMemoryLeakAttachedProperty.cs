namespace DIPS.Mobile.UI.MemoryManagement;

public class MemoryLeaks
{
    public static readonly BindableProperty IgnoreAutomaticMemoryLeakResolvingProperty = BindableProperty.CreateAttached("Mode",
        typeof(bool),
        typeof(MemoryLeaks),
        false);
    
    public static bool GetIgnoreAutomaticMemoryLeakResolving(BindableObject view)
    {
        return (bool)view.GetValue(IgnoreAutomaticMemoryLeakResolvingProperty);
    }

    /// <summary>
    /// The <see cref="BindableObject"/> to ignore memory leak resolve on.
    /// <remarks> Will also ignore memory leak resolving on its children.</remarks>
    /// </summary>
    public static void SetIgnoreAutomaticMemoryLeakResolving(BindableObject view, bool ignoreMemoryLeakResolve)
    {
        view.SetValue(IgnoreAutomaticMemoryLeakResolvingProperty, ignoreMemoryLeakResolve);
    }
}