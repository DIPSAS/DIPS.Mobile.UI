namespace DIPS.Mobile.UI.API.GarbageCollection;

/// <summary>
/// Use this class to monitor an object.
/// </summary>
public class GCCollectionMonitor
{
    private readonly List<Tuple<string, WeakReference<object>>> m_references = [];

    /// <summary>
    /// Add it for monitoring.
    /// </summary>
    /// <param name="target"></param>
    public void Observe(object target)
    {
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
#if DEBUG

        const int maxCollections = 5;
        var currentCollection = 0;
        while (++currentCollection <= maxCollections && m_references.Count != 0)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            foreach (var reference in m_references.ToArray())
            {
                if (reference.Item2.TryGetTarget(out object? target))
                {
                    if (currentCollection == maxCollections)
                    {
                        Console.WriteLine($@"🧟 {target.GetType().Name} is a zombie!");
                        m_references.Remove(reference);
                    }
                }
                else
                {
                    Console.WriteLine($@"✅{reference.Item1} released after {currentCollection} collections");
                    m_references.Remove(reference);
                }   
            }

            await Task.Delay(500);
            if (shouldPrintTotalMemory)
            {
                Console.WriteLine("Full collection total memory: " + GC.GetTotalMemory(true));    
            }
        }
#endif
    }
}