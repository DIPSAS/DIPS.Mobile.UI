namespace DIPS.Mobile.UI.MemoryManagement;

/// <summary>
/// Helper class to garbage collect.
/// </summary>
/// <remarks>See https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/ for more information on Garbage Collection.</remarks>
public static class GarbageCollection
{
    /// <summary>
    /// Prints a message to the Console with a GarbageCollection prefix.
    /// </summary>
    /// <param name="message"></param>
    public static void Print(string message)
    {
        Console.WriteLine($@"{nameof(GarbageCollection)}: {message}");
    }

    /// <summary>
    /// Will collect and and wait for pending finalizers. The finalizers are the ~ methods that will run for objects that can be garbage collected.
    /// </summary>
    /// <remarks>This will only run in Debug as its recommended.</remarks>
    public static void CollectAndWaitForPendingFinalizers()
    {
#if DEBUG
        Print("Force Collect");
        GC.Collect();
        GC.WaitForPendingFinalizers();
#endif
    }
}