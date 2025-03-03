namespace DIPS.Mobile.UI.Effects.ListElement;

public class FirstLastElementCornerRadiusEffect : RoutingEffect
{
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.CreateAttached("CornerRadius",
        typeof(CornerRadius),
        typeof(FirstLastElementCornerRadiusEffect),
        new CornerRadius(0),
        propertyChanged: OnPropertiesChanged);
    
    public static readonly BindableProperty ListProperty = BindableProperty.CreateAttached("List",
        typeof(IReadOnlyList<object>),
        typeof(FirstLastElementCornerRadiusEffect),
        null,
        propertyChanged: OnPropertiesChanged);

    public static CornerRadius GetCornerRadius(BindableObject view)
    {
        return (CornerRadius)view.GetValue(CornerRadiusProperty);
    }

    public static void SetCornerRadius(BindableObject view, CornerRadius cornerRadius)
    {
        view.SetValue(CornerRadiusProperty, cornerRadius);
    }
    
    public static IReadOnlyList<object>? GetList(BindableObject view)
    {
        return (IReadOnlyList<object>?)view.GetValue(CornerRadiusProperty);
    }

    public static void SetList(BindableObject view, IReadOnlyList<object> list)
    {
        view.SetValue(ListProperty, list);
    }
    
    private static void OnPropertiesChanged(BindableObject bindable, object oldValue, object? newValue)
    {
        if (bindable is not View view)
            return;

        if (newValue is null)
        {
            RemoveEffects(view);
            return;
        }
        
        // Refresh
        RemoveEffects(view);
        view.Effects.Add(new FirstLastElementCornerRadiusEffect());
    }
    
    private static void RemoveEffects(Element view)
    {
        while (view.Effects.Any(e => e is FirstLastElementCornerRadiusEffect))
        {
            view.Effects.Remove(view.Effects.FirstOrDefault(e => e is FirstLastElementCornerRadiusEffect));
        }
    }
}