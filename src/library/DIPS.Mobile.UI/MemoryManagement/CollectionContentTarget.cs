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
            
        Content = new WeakReference(content);
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
        string? name;

        if (visualTreeElement is Element element)
        {
            element.Parent = null;
            element.Handler?.DisconnectHandler();
            name = element.ToString();
            if (!string.IsNullOrEmpty(element.AutomationId))
            {
                name += $" (automationId: {element.AutomationId})";
            }
            FlatVisualChildrenList.Add(new CollectionTarget(name,
                new WeakReference(visualTreeElement)));
            if (element.Handler != null)
            {
                FlatVisualChildrenList.Add(new CollectionTarget(name+ "(handler)", element.Handler));    
            }
                
            AddEffectsToFlatList(name, element.Effects);
                
            if (element.BindingContext != null)
            {
                if (!FlatBindingContextList.Any(p =>
                    {
                        p.Target.TryGetTarget(out var target);
                        return target == element.BindingContext;
                    }))
                {
                    FlatBindingContextList.Add(new CollectionTarget(element.BindingContext.ToString()!, element.BindingContext));    
                }
            }
        }
        else
        {
            try
            {
                FlatVisualChildrenList.Add(new CollectionTarget(visualTreeElement.GetType().Name,
                    new WeakReference(visualTreeElement)));
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
    public WeakReference Content { get; }
}