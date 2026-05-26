namespace DIPS.Mobile.UI.MemoryManagement;

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
                
            if (content is IVisualTreeElement treeElement)
            {
                AddChildrenReferences(treeElement);    
            }
        }
            
        Content = new WeakReference<object>(content);
    }

    private void AddChildrenReferences(IVisualTreeElement visualTreeElement)
    {
        foreach (var vte in visualTreeElement.GetVisualChildren())
        {
            AddChildrenReferences(vte);
        }
            
        AddToFlatList(visualTreeElement);
    }

    private void AddToFlatList(IVisualTreeElement visualTreeElement)
    {
        string name;

        if (visualTreeElement is Element element)
        {
            name = element.ToString() ?? element.GetType().Name;
            if (!string.IsNullOrEmpty(element.AutomationId))
            {
                name += $" (automationId: {element.AutomationId})";
            }
            FlatVisualChildrenList.Add(new CollectionTarget(name, visualTreeElement));
            if (element.Handler != null)
            {
                FlatVisualChildrenList.Add(new CollectionTarget(element.Handler.GetType().Name + $"(Attached to {name})", element.Handler));    
            }
                
            AddEffectsToFlatList(name, element.Effects);
            
            // Skip strings: .NET interns strings as static GC roots, so a WeakReference to a string
            // always survives GC.Collect() and would be reported as a false-positive zombie.
            if (element.BindingContext is not null && element.BindingContext is not string)
            {
                if (!FlatBindingContextList.Any(p =>
                    {
                        p.Target.TryGetTarget(out var target);
                        return target == element.BindingContext;
                    }))
                    FlatBindingContextList.Add(new CollectionTarget(element.BindingContext.ToString()!, element.BindingContext));    
            }
        }
        else
        {
            try
            {
                FlatVisualChildrenList.Add(new CollectionTarget(visualTreeElement.GetType().Name, visualTreeElement));
            }
            catch
            {
                // We dont give a fak
            }
        }
    }

    private void AddEffectsToFlatList(string name, IList<Effect> effects)
    {
        foreach (var effect in effects)
        {
            FlatVisualChildrenList.Add(new CollectionTarget($"{effect.GetType().Name} (Attached to: {name})", effect));
        }
    }

    public string Name { get; }
    public List<CollectionTarget> FlatVisualChildrenList { get; } = [];
    public List<CollectionTarget> FlatBindingContextList { get; } = [];
    public WeakReference<object> Content { get; }
    public bool IsAlive => FlatVisualChildrenList.Any(c => c.Target.TryGetTarget(out _)) || FlatBindingContextList.Any(c => c.Target.TryGetTarget(out _));
}