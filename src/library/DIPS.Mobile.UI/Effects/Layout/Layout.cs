namespace DIPS.Mobile.UI.Effects.Layout;

public partial class Layout : RoutingEffect
{
    public static CornerRadius GetCornerRadius(BindableObject view)
    {
        return (CornerRadius)view.GetValue(CornerRadiusProperty);
    }

    /// <summary>
    /// Sets the corner radius
    /// </summary>
    /// <remarks>Currently only supports uniform corner radius, it uses `TopLeft` for all corners</remarks>
    public static void SetCornerRadius(BindableObject view, CornerRadius cornerRadius)
    {
        view.SetValue(CornerRadiusProperty, cornerRadius);
    }

    private static void OnCornerRadiusPropertiesChanged(BindableObject bindable, object oldValue, object? newValue)
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
        view.Effects.Add(new Layout());
        
    }
    
    private static void RemoveEffects(Element view)
    {
        while (view.Effects.Any(e => e is Layout))
        {
            view.Effects.Remove(view.Effects.FirstOrDefault(e => e is Layout));
        }
    }
}