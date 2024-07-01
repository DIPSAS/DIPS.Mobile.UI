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

    public void ObserveContent(object content)
    {
        if(!DUI.IsDebug) 
            return;
        
        var collectionPageTarget = new CollectionContentTarget(content);
        m_references.Add(collectionPageTarget);
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
    public async Task CheckAliveness(bool shouldPrintTotalMemory = true, bool tryResolveMemoryLeaks = true)
    {
        if (!DUI.IsDebug) return;

        const int maxCollections = 10;
        var currentCollection = 0;
        var shouldLookForAliveness = m_references.Count != 0;
        const int msBetweenCollections = 200;

        var totalMemoryBefore = GC.GetTotalMemory(false);
        if (shouldPrintTotalMemory && shouldLookForAliveness)
        {
            GarbageCollection.Print($"Collections total memory before: {totalMemoryBefore} byte ({(totalMemoryBefore / (float)1024 / 1024):F2} mb)");
        }

        if (shouldLookForAliveness)
        {
            GarbageCollection.Print("Forcing garbage collection to look for aliveness");
        }
        
        while (++currentCollection <= maxCollections && m_references.Count != 0)
        {
            GarbageCollection.CollectAndWaitForPendingFinalizers();
            foreach (var collectionPageTarget in m_references.ToArray())
            {
                GarbageCollection.Print($"{nameof(GCCollectionMonitor)}: Checking collection #{currentCollection} for {collectionPageTarget.Name}");
                if (collectionPageTarget.Page.IsAlive)
                {
                    if (currentCollection == maxCollections)
                    {
                        GarbageCollection.Print($@"🧟 {collectionPageTarget.Name} is a zombie!");

                        if (tryResolveMemoryLeaks)
                            TryResolveMemoryLeakInPage(collectionPageTarget.Page.Target!);

                        foreach (var child in collectionPageTarget.FlatChildrenList)
                        {
                            if (child.Target.TryGetTarget(out var target))
                            {
                                var print = $@"🧟 {child.Name} is a zombie!";

                                object? containedIn = null;

                                child.ContainedIn?.TryGetTarget(out containedIn);
                                if (containedIn is not null)
                                {
                                    print += $" Contained in: {containedIn.GetType().Name}";
                                }
                                if (tryResolveMemoryLeaks)
                                {
                                    print += " 🔫 Lets try to shoot this zombie";
                                }
                                
                                GarbageCollection.Print(print);
                            }
                        }

                        if (tryResolveMemoryLeaks)
                        {
                            GarbageCollection.Print("Finished shooting all zombies");
                            GarbageCollection.Print("🙏 Let's check if the page is garbage collected after shooting all zombies 🙏");

                            GarbageCollection.CollectAndWaitForPendingFinalizers();
                            _ = CheckAliveness(tryResolveMemoryLeaks: false);
                            
                            return;
                        }

                        m_references.Remove(collectionPageTarget);
                    }
                }
                else
                {
                    GarbageCollection.Print($@"✅{collectionPageTarget.Name} garbage collected after {currentCollection} collections");
                    m_references.Remove(collectionPageTarget);
                }
                
                await Task.Delay(msBetweenCollections);
            }

            await Task.Delay(500);
            if (m_references.Count == 0) //Finished looking for objects
            {
                if (shouldPrintTotalMemory)
                {
                    var totalMemory = GC.GetTotalMemory(true);
                    GarbageCollection.Print($"Collections total memory after: {totalMemory} byte ({(totalMemory / (float)1024 / 1024):F2} mb), difference: {totalMemoryBefore - totalMemory} bytes ({(totalMemoryBefore - totalMemory) / (float)1024 / 1024:F2} mb)");
                }    
            }
        }
    }

    /// <summary>
    ///     Attempts to resolve any potential memory leaks in the provided <see cref="Page"/>. You should only call this
    ///     method when the provided page is not meant to be used further.
    /// </summary>
    /// <param name="page"></param>
    public void TryResolveMemoryLeakInPage(Page page)
    {
        if (DUI.IsDebug || !GarbageCollection.TryAutoResolveMemoryLeaksEnabled)
        {
            return;
        }
        
        if (page is not ContentPage {Content: IVisualTreeElement visualTreeElement})
        {
            return;
        }

        var children = visualTreeElement.GetVisualChildren();
        if (children.Count == 0)
        {
            TryResolveMemoryLeak(page);
            return;
        }
        
        foreach (var child in visualTreeElement.GetVisualChildren())
        {
            if (child is not Element {Handler: not null} element)
            {
                continue;
            }

            foreach (var effect in element.Effects)
            {
                if (effect is null)
                {
                    continue;
                }
                    
                TryResolveMemoryLeak(effect);
            }
                
            TryResolveMemoryLeak(element);
        }
    }

    private static void TryResolveMemoryLeak(object target)
    {
        TryResolveMemoryLeak(target, target.GetType().Name);
    }

    private static void TryResolveMemoryLeak(object target, string childName)
    {
        if (vte is IVisualTreeElement visualTreeElement)
        {
            foreach (var childVte in visualTreeElement.GetVisualChildren())
            {
                TryResolveMemoryLeakInPage(childVte);
            }
        }
        
        TryResolveMemoryLeak(vte);
    }
    
    private static void TryResolveMemoryLeak(object target)
    {
        switch (target)
        {
            case RoutingEffect routingEffect:
                routingEffect.Element.Effects.Clear();
                break;
            case VisualElement visualElement:
                {
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
            Page = new WeakReference(content);

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
                    AddEffectsToFlatList(visualElement.Effects, visualElement);
                    break;
                case Element { Handler: not null } element:
                    // TODO: Add behaviours also?
                    
                    FlatChildrenList.Add(new CollectionTarget(element.Handler.GetType().Name, element.Handler));
                    AddEffectsToFlatList(element.Effects, element);
                    break;
            }

            try
            {
                FlatChildrenList.Add(new CollectionTarget(monitorTarget.GetType().Name, new WeakReference(monitorTarget)));
            }
            catch
            {
            }
        }

        private void AddEffectsToFlatList(IList<Effect> effects, object containedIn)
        {
            foreach (var effect in effects)
            {
                FlatChildrenList.Add(new CollectionTarget(effect.GetType().Name, effect, containedIn));
            }
        }

        public string Name { get; }
        public List<CollectionTarget> FlatChildrenList { get; } = [];
        public WeakReference Page { get; }
    }

    public class CollectionTarget(string name, object target, object? containedIn = null)
    {
        public string Name { get; } = name;
        public WeakReference<object>? ContainedIn { get; } = containedIn is not null ? new WeakReference<object>(containedIn) : null;
        public WeakReference<object> Target { get; } = new(target);
    }
}