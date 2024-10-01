using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.BottomSheets;
using Microsoft.Maui.Platform;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;

namespace DIPS.Mobile.UI.MemoryManagement;

/// <summary>
/// Use this class to monitor an object to check if it has a memory leak.
/// This class can also be used to automatic resolve memory leaks.
/// </summary>
/// <remarks>See https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/ for more information on Garbage Collection.</remarks>
public class GCCollectionMonitor
{
    private readonly List<CollectionContentTarget> m_references = [];
    private readonly VisualTreeMemoryResolver m_visualTreeMemoryResolver = new();
    private const int MsBetweenCollections = 200;
    private const int MaxCollections = 10;
    public static GCCollectionMonitor Instance { get; } = new();
    public static bool TryAutoResolveMemoryLeaksEnabled { get; internal set; }

    public void SetAdditionalResolver(Action<object> additionalResolver)
    {
        m_visualTreeMemoryResolver.SetAdditionalResolver(additionalResolver);
    }

    public CollectionContentTarget? ObserveContent(object content)
    {
        if (!DUI.IsDebug)
            return null;

        var collectionPageTarget = content.ToCollectionContentTarget();
        m_references.Add(collectionPageTarget);

        GarbageCollection.Print($@"Observing: {content.GetType().Name}");

        return collectionPageTarget;
    }

    /// <summary>
    /// Try to force garbage collecting, wait for finalizers and see if its still alive.
    /// </summary>
    /// <remarks>
    /// Remember to add the object you want to aliveness for by calling <see cref="ObserveContent"/>
    /// Will only work while debugging.
    /// Will do a Console.WriteLine of the aliveness of the object.
    /// On iOS you can see it directly in the Console window, on Android its best observed in LogCat filtered by your application and "dotnet".
    /// </remarks>
    /// <returns>true if at least one monitored object are still alive</returns>
    public async Task<bool> CheckIfMonitoredObjectsAreStillAlive(bool shouldPrintTotalMemory = true)
    {
        if (!DUI.IsDebug)
            return false;

        var shouldLookForAliveness = m_references.Count != 0;

        var anyAlive = false;

        var totalMemoryBefore = GC.GetTotalMemory(false);
        if (shouldPrintTotalMemory && shouldLookForAliveness)
        {
            GarbageCollection.Print(
                $"📈 Collections total memory before: {totalMemoryBefore} byte ({(totalMemoryBefore / (float)1024 / 1024):F2} mb)");
        }

        if (shouldLookForAliveness)
        {
            GarbageCollection.Print("Forcing garbage collection to look if monitored objects are still alive");
        }

        foreach (var collectionPageTarget in m_references.ToArray())
        {
            if (await CheckIfCollectionTargetIsAlive(collectionPageTarget, m_references))
            {
                anyAlive = true;
                
                Console.WriteLine("🧟 " + collectionPageTarget.Name + " is a zombie!");
            }
            else
            {
                Console.WriteLine("✅ " + collectionPageTarget.Name + " is not a zombie!");
            }
        }

        if (!shouldPrintTotalMemory)
            return anyAlive;

        await Task.Delay(500);

        var totalMemory = GC.GetTotalMemory(true);
        GarbageCollection.Print(
            $"📈 Collections total memory after: {totalMemory} byte ({(totalMemory / (float)1024 / 1024):F2} mb), difference: {totalMemoryBefore - totalMemory} bytes ({(totalMemoryBefore - totalMemory) / (float)1024 / 1024:F2} mb)");

        return anyAlive;
    }

    public async Task<bool> CheckIfCollectionTargetIsAlive(CollectionContentTarget collectionContentTarget,
        List<CollectionContentTarget>? references = null, bool shouldPrintTotalMemory = false)
    {
        GarbageCollection.Print($"Checking if {collectionContentTarget.Name} has memory leaks...");
        var totalMemoryBefore = 0L;
        if (shouldPrintTotalMemory)
        {
            totalMemoryBefore = GC.GetTotalMemory(false);
            GarbageCollection.Print(
                $"📈 GC Memory before: {totalMemoryBefore} byte ({(totalMemoryBefore / (float)1024 / 1024):F2} mb)");
        }

        var currentCollection = 0;
        GarbageCollection.Print($"Checking {MaxCollections - 1} GC collections.");
        for (; currentCollection < MaxCollections; currentCollection++)
        {
            GarbageCollection.CollectAndWaitForPendingFinalizers();
            await Task.Delay(MsBetweenCollections);
        }

        var allVisualChildrenThatLives =
            collectionContentTarget.FlatVisualChildrenList.FindAll(c => c.Target.TryGetTarget(out var target));
        var allBindingContextsThatLives =
            collectionContentTarget.FlatBindingContextList.FindAll(c => c.Target.TryGetTarget(out var target));
        var totalNumberOfLeaks = allVisualChildrenThatLives.Count + allBindingContextsThatLives.Count;

        if (totalNumberOfLeaks > 0)
        {
            GarbageCollection.Print($"Number of leaks: {totalNumberOfLeaks}");
        }

        if (allVisualChildrenThatLives.Count > 0)
        {
            GarbageCollection.Print($"---- Visual children zombies of {collectionContentTarget.Name}: ----");
        }

        foreach (var child in allVisualChildrenThatLives)
        {
            if (child.Target.TryGetTarget(out var target))
            {
                GarbageCollection.Print($@"- 🧟 {child.Name} is a zombie!");
            }
        }

        if (allBindingContextsThatLives.Count > 0)
        {
            GarbageCollection.Print($"---- Binding Context zombies of {collectionContentTarget.Name}: ----");
        }

        foreach (var child in allBindingContextsThatLives)
        {
            if (child.Target.TryGetTarget(out var target))
            {
                GarbageCollection.Print($@"- 🧟 {child.Name} is a zombie!");
            }
        }

        if (shouldPrintTotalMemory)
        {
            var totalMemory = GC.GetTotalMemory(false);
            GarbageCollection.Print(
                $"📈 GC Memory after: {totalMemory} byte ({(totalMemory / (float)1024 / 1024):F2} mb), difference: {totalMemoryBefore - totalMemory} bytes ({(totalMemoryBefore - totalMemory) / (float)1024 / 1024:F2} mb)");
        }

        var hasMemoryLeaks = collectionContentTarget.Content.IsAlive || allBindingContextsThatLives.Count > 0 ||
                             allVisualChildrenThatLives.Count > 0;
        
        return hasMemoryLeaks;
    }

    public async Task CheckIfObjectIsAliveAndTryResolveLeaks(CollectionContentTarget? target)
    {
        // A small delay to let MAUI finish their own disposing before we try and resolve
        await Task.Delay(MsBetweenCollections);
        
        if (DUI.IsDebug)
        {
            if (target is null)
            {
                return;
            }

            if (TryAutoResolveMemoryLeaksEnabled)
            {
                if (target.Content.Target != null)
                {
                    TryResolveMemoryLeaksInContent(target.Content.Target);
                }

                if (await CheckIfCollectionTargetIsAlive(target, shouldPrintTotalMemory: true))
                {
                    GarbageCollection.Print(
                        $"❌ There is memory leaks after checking {target?.Name}. See https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/Performance#tips-and-tricks for help resolving the leaks. ");
                }
                else
                {
                    GarbageCollection.Print($"✅ No memory leaks in {target.Name}! 🎉🎉🎉");
                }
            }
            else
            {
                if (!(await CheckIfCollectionTargetIsAlive(target, shouldPrintTotalMemory: true)))
                {
                    GarbageCollection.Print($"✅ No memory leaks when checking {target.Name}");
                }    
            }
        }
        else if (TryAutoResolveMemoryLeaksEnabled && target?.Content.Target is not null)
        {
            TryResolveMemoryLeaksInContent(target.Content.Target);
        }
    }

    /// <summary>
    ///     Attempts to resolve any potential memory leaks in the provided <see cref="object"/>. You should only call this
    ///     method when the provided content is not meant to be used further.
    /// </summary>
    public void TryResolveMemoryLeaksInContent(object content, bool isRoot = true)
    {
        if(content is BindableObject bindableObject)
        {
            if (MemoryLeaks.GetSkipAutomaticMemoryLeakResolving(bindableObject))
            {
                GarbageCollection.Print($"⏭️ Skipping automatic memory leak resolving for {content.GetType().Name}");
                return;
            }
        }
        
        if (isRoot)
            GarbageCollection.Print("Auto resolving memory leaks...");
        
        if (content is IVisualTreeElement visualTreeElement)
        {
            foreach (var child in visualTreeElement.GetVisualChildren())
            {
                TryResolveMemoryLeaksInContent(child, false);
            }
        }

        m_visualTreeMemoryResolver.TryResolveMemoryLeak(content);
    }
}