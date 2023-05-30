namespace DIPS.Mobile.UI.Effects.DUIImageEffect;

public partial class DUIImageEffect : RoutingEffect
{
    public static Color GetColor(BindableObject view)
    {
        return (Color)view.GetValue(ImageColorProperty);
    }

    public static void SetColor(BindableObject view, Color color)
    {
        view.SetValue(ImageColorProperty, color);
    }

    private static void OnImageColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not View view)
            return;
        
        if (newValue is Color)
        {
            view.Effects.Add(new DUIImageEffect());
        }
        else
        {
            var toRemove = view.Effects.FirstOrDefault(e => e is DUIImageEffect);
            view.Effects.Remove(toRemove);
        }
    }
}