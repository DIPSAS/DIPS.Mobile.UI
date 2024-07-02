using DIPS.Mobile.UI.API.Library;

namespace DIPS.Mobile.UI.MemoryManagement;

/// <summary>
/// Helper class to garbage collect.
/// </summary>
/// <remarks>See https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/ for more information on Garbage Collection.</remarks>
public static class GarbageCollection
{
    public static bool TryAutoResolveMemoryLeaksEnabled { get; internal set; }
    
    /// <summary>
    /// Prints a message to the Console with a GarbageCollection prefix.
    /// </summary>
    /// <param name="message"></param>
    public static void Print(string message)
    {
        if (!DUI.IsDebug) return;
        Console.WriteLine($@"{nameof(GarbageCollection)}: {message}");
    }

    /// <summary>
    /// Will collect and and wait for pending finalizers. The finalizers are the ~ methods that will run for objects that can be garbage collected.
    /// </summary>
    /// <remarks>This will only run in Debug as its recommended.</remarks>
    public static void CollectAndWaitForPendingFinalizers()
    {
        if (!DUI.IsDebug) return;
        Print("Force Collect");
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }

    public static GCCollectionMonitor.CollectionContentTarget ToCollectionContentTarget(this object target)
    {
        return new GCCollectionMonitor.CollectionContentTarget(target);
    }
    
}