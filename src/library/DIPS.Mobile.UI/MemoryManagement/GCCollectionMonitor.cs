using DIPS.Mobile.UI.API.Library;

namespace DIPS.Mobile.UI.MemoryManagement;

/// <summary>
/// Use this class to monitor an object at a point where it should be garbage collected.
/// </summary>
/// <remarks>See https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/ for more information on Garbage Collection.</remarks>
public class GCCollectionMonitor
{
    private readonly List<Tuple<string, WeakReference<object>>> m_references = [];

    /// <summary>
    /// Add it for monitoring.
    /// </summary>
    /// <param name="target"></param>
    public void Observe(object target)
    {
        if (!DUI.IsDebug) return;
        var targetType = target.GetType().Name;
        m_references.Add(new Tuple<string, WeakReference<object>>(targetType, new WeakReference<object>(target)));
    }

    /// <summary>
    /// Try to force garbage collecting, wait for finalizers and see if its still alive.
    /// </summary>
    /// <remarks>
    /// Remember to add the object you want to aliveness for by calling <see cref="Observe"/>
    /// Will only work while debugging.
    /// Will do a Console.WriteLine of the aliveness of the object.
    /// On iOS you can see it directly in the Console window, on Android its best observed in LogCat filtered by your application and "dotnet".
    /// </remarks>
    public async void CheckAliveness(bool shouldPrintTotalMemory = true)
    {
        if (!DUI.IsDebug) return;

        const int maxCollections = 5;
        var currentCollection = 0;
        var shouldLookForAliveness = m_references.Count != 0;
        
        if (shouldPrintTotalMemory && shouldLookForAliveness)
        {
            GarbageCollection.Print($"Collections total memory before: {GC.GetTotalMemory(true)} byte");
        }

        if (shouldLookForAliveness)
        {
            GarbageCollection.Print("Forcing garbage collection to look for aliveness");
        }
        
        
        while (++currentCollection <= maxCollections && m_references.Count != 0)
        {
            GarbageCollection.CollectAndWaitForPendingFinalizers();
            foreach (var reference in m_references.ToArray())
            {
                GarbageCollection.Print($"{nameof(GCCollectionMonitor)}: Checking collection #{currentCollection} for objects");
                if (reference.Item2.TryGetTarget(out var target))
                {
                    if (currentCollection == maxCollections)
                    {
                        GarbageCollection.Print($@"🧟 {target.GetType().Name} is a zombie!");
                        m_references.Remove(reference);
                    }
                }
                else
                {
                    GarbageCollection.Print($@"✅{reference.Item1} released after {currentCollection} collections");
                    m_references.Remove(reference);
                }
            }

            await Task.Delay(500);
            if (m_references.Count == 0) //Finished looking for objects
            {
                if (shouldPrintTotalMemory)
                {
                    GarbageCollection.Print($"Collections total memory after: {GC.GetTotalMemory(true)} byte");
                }    
            }
        }
    }
}