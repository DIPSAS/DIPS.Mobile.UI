namespace DIPS.Mobile.UI.Effects.Modal;

public partial class Modal : RoutingEffect
{
    public static bool GetHasNavBar(BindableObject view)
    {
        return (bool)view.GetValue(HasNavBarProperty);
    }

    public static void SetHasNavBar(BindableObject view, bool hasNavBar)
    {
        view.SetValue(HasNavBarProperty, hasNavBar);
    }
    
    private static void OnHasNavBarChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not ContentPage view)
        {
            return;
        }
        
        if (newValue is true)
        {
            view.Effects.Add(new Modal());
        }
        else
        {
            view.Effects.Remove(view.Effects.FirstOrDefault(x => x is Modal));
        }
    }
}