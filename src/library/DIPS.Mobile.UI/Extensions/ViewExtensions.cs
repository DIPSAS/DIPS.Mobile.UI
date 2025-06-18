using System.Reflection;

namespace DIPS.Mobile.UI.Extensions;

public static class ViewExtensions
{
    static PropertyInfo DataTemplateIdPropertyInfo;

    internal static string GetDataTemplateId(this DataTemplate dataTemplate)
    {
        DataTemplateIdPropertyInfo ??= dataTemplate.GetType().GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic);

        return DataTemplateIdPropertyInfo.GetValue(dataTemplate)?.ToString();

    }

    static MethodInfo removeLogicalChildMethod = null;

    internal static void RemoveLogicalChild(this Element parent, IView view)
    {
        if (view is Element elem)
        {
            removeLogicalChildMethod ??= GetLogicalChildMethod(parent, "RemoveLogicalChildInternal", "RemoveLogicalChild");
            removeLogicalChildMethod?.Invoke(parent, new[] { elem });
        }
    }

    static MethodInfo addLogicalChildMethod = null;

    internal static void AddLogicalChild(this Element parent, IView view)
    {
        if (view is Element elem)
        {
            addLogicalChildMethod ??= GetLogicalChildMethod(parent, "AddLogicalChildInternal", "AddLogicalChild");
            addLogicalChildMethod?.Invoke(parent, new[] { elem });
        }
    }

    static MethodInfo GetLogicalChildMethod(Element parent, string internalName, string publicName)
    {
        var internalMethod = parent.GetType().GetMethod(
            internalName,
            BindingFlags.Instance | BindingFlags.NonPublic,
            new[] { typeof(Element) });

        if (internalMethod is null)
        {
            internalMethod = parent.GetType().GetMethod(
                publicName,
                BindingFlags.Instance | BindingFlags.Public,
                new[] { typeof(Element) });
        }

        return internalMethod;
    }
    
    public static T? FindParentOfType<T>(this Element element, bool includeThis = false)
        where T : IElement
    {
        if (includeThis && element is T view)
            return view;

        foreach (var parent in element.GetParentsPath())
        {
            if (parent is T parentView)
                return parentView;
        }

        return default;
    }
    
    /// <summary>
    /// Uses breadth first search, so the child that are closest to the root will be found if a match is found
    /// </summary>
    public static T? FindChildOfTypeClosestToRoot<T>(this View? view)
    {
        if (view is null) 
            return default;
    
        var queue = new Queue<View>();
        queue.Enqueue(view);

        while (queue.Count > 0)
        {
            var currentView = queue.Dequeue();

            if (currentView is T matchView)
                return matchView;

            foreach (var visualTreeElement in (currentView as IVisualTreeElement).GetVisualChildren())
            {
                queue.Enqueue(visualTreeElement as View);
            }
        }

        return default;
    }
    
    public static List<T> FindAllChildrenOfType<T>(this View? view)
    {
        var results = new List<T>();

        if (view is null)
            return results;

        var queue = new Queue<View>();
        queue.Enqueue(view);

        while (queue.Count > 0)
        {
            var currentView = queue.Dequeue();

            if (currentView is T matchView)
                results.Add(matchView);

            if (currentView is IVisualTreeElement visualTreeElement)
            {
                foreach (var child in visualTreeElement.GetVisualChildren())
                {
                    if (child is View childView)
                        queue.Enqueue(childView);
                }
            }
        }

        return results;
    }

    
    public static IEnumerable<Element> GetParentsPath(this Element self)
    {
        var current = self;

        while (!IsApplicationOrNull(current.RealParent))
        {
            current = current.RealParent;
            yield return current;
        }
    }
    
    public static bool IsApplicationOrNull(object? element) =>
        element is null or IApplication;
    
    public static async Task<bool> HeightTo(this View view, double height, uint duration = 250, Easing easing = null)
    {
        var tcs = new TaskCompletionSource<bool>();

        var heightAnimation = new Animation(x => view.HeightRequest = x, view.Height, height);
        heightAnimation.Commit(view, "HeightAnimation", 10, duration, easing, (finalValue, finished) => { tcs.SetResult(finished); });

        return await tcs.Task;
    }

    public static async Task<bool> WidthTo(this View view, double width, uint duration = 250, Easing easing = null)
    {
        var tcs = new TaskCompletionSource<bool>();

        var heightAnimation = new Animation(x => view.WidthRequest = x, view.Width, width);
        heightAnimation.Commit(view, "WidthAnimation", 10, duration, easing, (finalValue, finished) => { tcs.SetResult(finished); });

        return await tcs.Task;
    }
}
