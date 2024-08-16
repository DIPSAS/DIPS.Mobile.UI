namespace DIPS.Mobile.UI.MemoryManagement;

public class MemoryLeaks
{
    public static readonly BindableProperty SkipAutomaticMemoryLeakResolvingProperty = BindableProperty.CreateAttached("Mode",
        typeof(bool),
        typeof(MemoryLeaks),
        false);
    
    public static bool GetSkipAutomaticMemoryLeakResolving(BindableObject view)
    {
        return (bool)view.GetValue(SkipAutomaticMemoryLeakResolvingProperty);
    }

    /// <summary>
    /// The <see cref="BindableObject"/> to ignore memory leak resolve on.
    /// <remarks> Will also ignore memory leak resolving on its children.</remarks>
    /// </summary>
    public static void SetSkipAutomaticMemoryLeakResolving(BindableObject view, bool ignoreMemoryLeakResolve)
    {
        view.SetValue(SkipAutomaticMemoryLeakResolvingProperty, ignoreMemoryLeakResolve);
    }
}