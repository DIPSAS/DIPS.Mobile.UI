namespace DIPS.Mobile.UI.Effects.Layout;

public partial class Layout : RoutingEffect
{
    public static int GetUniformCornerRadius(BindableObject view)
    {
        return (int)view.GetValue(UniformCornerRadiusProperty);
    }

    public static void SetUniformCornerRadius(BindableObject view, Microsoft.Maui.CornerRadius command)
    {
        view.SetValue(UniformCornerRadiusProperty, command);
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