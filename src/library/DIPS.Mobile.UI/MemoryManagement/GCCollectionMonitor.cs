using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.BottomSheets;
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

    private const int MsBetweenCollections = 200;
    private const int MaxCollections = 10;

    private Action<object>? m_additionalResolver;
    
    public static GCCollectionMonitor Instance { get; } = new();
    public bool TryAutoResolveMemoryLeaksEnabled { get; internal set; }

    public void SetAdditionalResolver(Action<object> additionalResolver)
    {
        m_additionalResolver = additionalResolver;
    }

    public CollectionContentTarget? ObserveContent(object content)
    {
        if(!DUI.IsDebug) 
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

            if (!collectionContentTarget.FlatChildrenList.Any(child => child.Target.TryGetTarget(out var target)))
            {
                GarbageCollection.Print($@"🧟 No children are infected! 🧟");
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

        foreach (var child in ((content as IVisualTreeElement)!).GetVisualChildren())
        {
            TryResolveMemoryLeaksInContent(child, false);
        }
        
        TryResolveMemoryLeak(content);
    }

    private void TryResolveMemoryLeak(object target)
    {
        try
        {
            switch (target)
            {
                case VisualElement visualElement:
                    {
                        visualElement.Effects.Clear();

                        visualElement.BindingContext = null;
                        visualElement.Parent = null;

                        switch (visualElement)
                        {
                            case ContentView contentView:
                                contentView.Content = null;
                                break;
                            case CollectionView collectionView:
                                collectionView.ItemsSource = null;
                                collectionView.ItemTemplate = null;
                                collectionView.FooterTemplate = null;
                                collectionView.HeaderTemplate = null;
                                collectionView.Footer = null;
                                collectionView.Header = null;
                                break;
                            case Border border:
                                border.Content = null;
                                break;
                            case ContentPage contentPage:
                                contentPage.Content = null;
                                break;
                            case ScrollView scrollView:
                                scrollView.Content = null;
                                break;
                        }
                        
                        visualElement.ClearLogicalChildren();

                        if (visualElement.Handler is not null)
                        {
                            if (visualElement.Handler is IDisposable disposableHandler)
                                disposableHandler.Dispose();
                            visualElement.Handler?.DisconnectHandler();
                            visualElement.Handler = null;
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
                            element.Handler?.DisconnectHandler();
                        }

                        break;
                    }
            }
            
            // Try run user-defined resolver method
            m_additionalResolver?.Invoke(target);
        }
        catch
        {
            // Should never crash the app
        }
    }

    public class CollectionContentTarget
    {
        public CollectionContentTarget(object content)
        {
            Name = content.GetType().Name;
            if (content is Element element)
            {
                if (!string.IsNullOrEmpty(element.AutomationId))
                {
                    Name += $" (automationId: {element.AutomationId})";    
                }
            }
            
            Content = new WeakReference(content);

            AddChildrenReferences((content as IVisualTreeElement)!);
        }

        private void AddChildrenReferences(IVisualTreeElement visualTreeElement)
        {
            foreach (var vte in visualTreeElement.GetVisualChildren())
            {
                AddChildrenReferences(vte);
            }
            
            AddToFlatList(visualTreeElement);
        }

        private void AddToFlatList(object monitorTarget)
        {
            string? name;
            switch (monitorTarget)
            {
                case VisualElement {Handler: not null} visualElement:
                    // TODO: Add behaviours also? 
                    name = visualElement.ToString();
                    if (!string.IsNullOrEmpty(visualElement.AutomationId))
                    {
                        name += $" (automationId: {visualElement.AutomationId}";
                    }
                    FlatChildrenList.Add(new CollectionTarget(name, visualElement.Handler));
                    AddEffectsToFlatList(visualElement.Effects);
                    break;
                case Element { Handler: not null } element:
                    // TODO: Add behaviours also?
                    name = element.ToString();
                    if (!string.IsNullOrEmpty(element.AutomationId))
                    {
                        name += $" (automationId: {element.AutomationId}";
                    }
                    FlatChildrenList.Add(new CollectionTarget(name, element.Handler));
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
            
            if(!TryAutoResolveMemoryLeaksEnabled)
                return;
            
            TryResolveMemoryLeaksInContent(target.Content.Target!);
            
            GarbageCollection.Print("🙏 Let's check if the content is garbage collected after trying to shoot all zombies 🙏");

            if (await CheckIfCollectionTargetIsAlive(target, shouldPrintTotalMemory: true))
            {
                GarbageCollection.Print("❌ The automatic resolving of memory leaks failed to resolve all memory leaks! If it says that there are no children affected, you must look into the content's handler (DisconnectHandler) or native views (Their dispose methods). It could also be an issue with Shell itself. ❌");
            }
            else
            {
                GarbageCollection.Print("✅ Looks like the automatic resolving of memory leak succeeded! 🎉🎉🎉");
            }
        }
        else if(TryAutoResolveMemoryLeaksEnabled && target?.Content.Target is not null)
        {
            // A small delay to let MAUI finish their own disposing before we try and resolve
            await Task.Delay(MsBetweenCollections);
            TryResolveMemoryLeaksInContent(target.Content.Target);
        }
    }
}