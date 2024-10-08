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
}
