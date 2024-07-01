using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.BottomSheets;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;

namespace DIPS.Mobile.UI.MemoryManagement;

/// <summary>
/// Use this class to monitor an object at a point where it should be garbage collected.
/// </summary>
/// <remarks>See https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/ for more information on Garbage Collection.</remarks>
public class GCCollectionMonitor
{
    private readonly List<CollectionContentTarget> m_references = [];

    private const int MsBetweenCollections = 10;
    private const int MaxCollections = 10;
    
    public static GCCollectionMonitor Instance { get; } = new();

    public CollectionContentTarget? ObserveContent(object content)
    {
        if(!DUI.IsDebug) 
            return null;
        
        var collectionPageTarget = new CollectionContentTarget(content);
        m_references.Add(collectionPageTarget);
        
        GarbageCollection.Print($@"Observing: {content.GetType().Name}");

        return collectionPageTarget;
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
            GarbageCollection.Print($"📈 Collections total memory before: {totalMemoryBefore} byte ({(totalMemoryBefore / (float)1024 / 1024):F2} mb)");
        }

        if (shouldLookForAliveness)
        {
            GarbageCollection.Print("Forcing garbage collection to look if monitored objects are still alive");
        }
        
        foreach (var collectionPageTarget in m_references.ToArray())
        {
            if(await CheckIfCollectionTargetIsAlive(collectionPageTarget, m_references))
            {
                anyAlive = true;
            }
        }

        if (!shouldPrintTotalMemory)
            return anyAlive;
        
        await Task.Delay(500);

        var totalMemory = GC.GetTotalMemory(true);
        GarbageCollection.Print($"📈 Collections total memory after: {totalMemory} byte ({(totalMemory / (float)1024 / 1024):F2} mb), difference: {totalMemoryBefore - totalMemory} bytes ({(totalMemoryBefore - totalMemory) / (float)1024 / 1024:F2} mb)");

        return anyAlive;
    }

    public async Task<bool> CheckIfCollectionTargetIsAlive(CollectionContentTarget collectionContentTarget,
        List<CollectionContentTarget>? references = null, bool shouldPrintTotalMemory = false)
    {
        var totalMemoryBefore = 0L;
        if (shouldPrintTotalMemory)
        {
            totalMemoryBefore = GC.GetTotalMemory(false);
            GarbageCollection.Print($"📈 Collections total memory before: {totalMemoryBefore} byte ({(totalMemoryBefore / (float)1024 / 1024):F2} mb)");
        }

        var currentCollection = 0;
        for (; currentCollection < MaxCollections; currentCollection++)
        {
            GarbageCollection.CollectAndWaitForPendingFinalizers();
            
            GarbageCollection.Print(
                $"{nameof(GCCollectionMonitor)}: Checking collection #{currentCollection} for {collectionContentTarget.Name}");

            if (collectionContentTarget.Content.IsAlive)
            {
                await Task.Delay(MsBetweenCollections);
                continue;
            }

            break;
        }

        if (!collectionContentTarget.Content.IsAlive)
        {
            GarbageCollection.Print(
                $@"✅{collectionContentTarget.Name} garbage collected after {currentCollection} collections");
            references?.Remove(collectionContentTarget);
        }
        else
        {
            GarbageCollection.Print($@"🧟 {collectionContentTarget.Name} is a zombie! Let's check if its children are infected 🧟");

            if (collectionContentTarget.FlatChildrenList.Count == 0)
            {
                GarbageCollection.Print($@"🧟 No children are infected, this probably means that something is not released in its handler 🧟");
            }

            foreach (var child in collectionContentTarget.FlatChildrenList)
            {
                if (child.Target.TryGetTarget(out var target))
                {
                    var print = $@"🧟 {child.Name} is a zombie!";

                    GarbageCollection.Print(print);
                }
            }
        }

        if (shouldPrintTotalMemory)
        {
            var totalMemory = GC.GetTotalMemory(false);
            GarbageCollection.Print($"📈 Collections total memory after: {totalMemory} byte ({(totalMemory / (float)1024 / 1024):F2} mb), difference: {totalMemoryBefore - totalMemory} bytes ({(totalMemoryBefore - totalMemory) / (float)1024 / 1024:F2} mb)");
        }

        return collectionContentTarget.Content.IsAlive;
    }

    /// <summary>
    ///     Attempts to resolve any potential memory leaks in the provided <see cref="object"/>. You should only call this
    ///     method when the provided content is not meant to be used further.
    /// </summary>
    public void TryResolveMemoryLeaksInContent(object content, bool isRoot = true)
    {
        if(isRoot)
            GarbageCollection.Print($"🔫 Let's try to shoot the zombies in {content.GetType().Name} 🧟");

        var visualTreeElement = content as IVisualTreeElement;

        if (content is ContentPage contentPage)
        {
            visualTreeElement = contentPage.Content;
        }
        else if (content is BottomSheet bottomSheet)
        {
            visualTreeElement = bottomSheet.Content;
        }
        
        if (visualTreeElement is null)
        {
            TryResolveMemoryLeak(content);
            return;
        }
        
        var children = visualTreeElement.GetVisualChildren();
        if (children.Count == 0)
        {
            TryResolveMemoryLeak(content);
            return;
        }
        
        foreach (var child in children)
        {
            TryResolveMemoryLeaksInContent(child, false);
        }
        
        TryResolveMemoryLeak(content);
    }

    private static void TryResolveMemoryLeak(object target)
    {
        switch (target)
        {
            case VisualElement visualElement:
                {
                    visualElement.Effects.Clear();
                    
                    visualElement.BindingContext = null;
                    visualElement.Parent = null;

                    visualElement.ClearLogicalChildren();
            
                    if (visualElement.Handler is not null)
                    {
                        if (visualElement.Handler is IDisposable disposableHandler)
                            disposableHandler.Dispose();
                        visualElement.Handler?.DisconnectHandler();
                    }

                    visualElement.Resources = null;
                    break;
                }
            case Element element:
                {
                    element.Effects.Clear();
                    
                    element.BindingContext = null;
                    element.Parent = null;

                    element.ClearLogicalChildren();

                    if (element.Handler is not null)
                    {
                        if (element.Handler is IDisposable disposableElementHandler)
                            disposableElementHandler.Dispose();
                        element.Handler.DisconnectHandler();
                    }

                    break;
                }
        }

        switch (target)
        {
            case ContentPage contentPage:
                contentPage.Content = null;
                break;
            case ContentView contentView:
                contentView.Content = null;
                break;
            case Border border:
                border.Content = null;
                break;
            case ScrollView scrollView:
                scrollView.Content = null;
                break;
        }
    }

    public class CollectionContentTarget
    {
        public CollectionContentTarget(object content)
        {
            Name = content.GetType().Name;
            Content = new WeakReference(content);

            if (content is ContentPage contentPage)
            {
                AddChildrenReferences(contentPage.Content);
            }

            if (content is BottomSheet bottomSheet)
            {
                AddChildrenReferences(bottomSheet.Content);
            }
        }

        private void AddChildrenReferences(object monitorTarget)
        {
            AddToFlatList(monitorTarget);
            
            if (monitorTarget is not IVisualTreeElement visualTreeElement)
                return;

            var visualChildren = visualTreeElement.GetVisualChildren();
            if (visualChildren.Count <= 0)
                return;

            foreach (var vte in visualChildren)
            {
                if (Equals(vte, monitorTarget))
                    continue;
                    
                AddChildrenReferences(vte);
            }
        }

        private void AddToFlatList(object monitorTarget)
        {
            switch (monitorTarget)
            {
                case VisualElement { Handler: not null } visualElement:
                    // TODO: Add behaviours also? 

                    FlatChildrenList.Add(new CollectionTarget(visualElement.Handler.GetType().Name, visualElement.Handler));
                    AddEffectsToFlatList(visualElement.Effects);
                    break;
                case Element { Handler: not null } element:
                    // TODO: Add behaviours also?
                    
                    FlatChildrenList.Add(new CollectionTarget(element.Handler.GetType().Name, element.Handler));
                    AddEffectsToFlatList(element.Effects);
                    break;
            }

            try
            {
                FlatChildrenList.Add(new CollectionTarget(monitorTarget.GetType().Name, new WeakReference(monitorTarget)));
            }
            catch
            {
                // We dont give a fak
            }
        }

        private void AddEffectsToFlatList(IList<Effect> effects)
        {
            foreach (var effect in effects)
            {
                FlatChildrenList.Add(new CollectionTarget(effect.GetType().Name, effect));
            }
        }

        public string Name { get; }
        public List<CollectionTarget> FlatChildrenList { get; } = [];
        public WeakReference Content { get; }
    }

    public class CollectionTarget(string name, object target)
    {
        public string Name { get; } = name;
        public WeakReference<object> Target { get; } = new(target);
    }

    public async Task CheckIfContentAliveOrAndTryResolveLeaks(CollectionContentTarget? target)
    {
        if (DUI.IsDebug)
        {
            if (target is null)
            {
                GarbageCollection.Print("Target is null, cannot check if the target is alive, aborting...");
                return;
            }
            
            if (!(await CheckIfCollectionTargetIsAlive(target, shouldPrintTotalMemory: true)))
            {
                return;
            }
            
            if(!GarbageCollection.TryAutoResolveMemoryLeaksEnabled)
                return;
            
            TryResolveMemoryLeaksInContent(target.Content.Target!);
            
            GarbageCollection.Print("🙏 Let's check if the content is garbage collected after trying to shoot all zombies 🙏");

            if (await CheckIfCollectionTargetIsAlive(target, shouldPrintTotalMemory: true))
            {
                GarbageCollection.Print("🧟 Looks like the automatic resolving of memory leak failed. Usually this means that something in the platform is not released. Maybe you did not release resources in Handlers or custom platform views? 🧟");
            }
        }
        else if(GarbageCollection.TryAutoResolveMemoryLeaksEnabled && target?.Content.Target is not null)
        {
            TryResolveMemoryLeaksInContent(target.Content.Target);
        }
    }
    
    
}