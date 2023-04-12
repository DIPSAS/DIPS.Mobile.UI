using Shell = DIPS.Mobile.UI.Components.Shell.Shell;

namespace DIPS.Mobile.UI.Effects.ContextMenuEffect;

public class ContextMenuEffect : RoutingEffect
{
    public static readonly BindableProperty HasContextMenuProperty = BindableProperty.CreateAttached("HasContextMenu",
        typeof(bool),
        typeof(ContextMenuEffect),
        false,
        propertyChanged: OnHasContextMenuChanged);

    
    public static bool GetHasContextMenu(BindableObject view)
    {
        return (bool)view.GetValue(HasContextMenuProperty);
    }
    
    public static void SetHasContextMenu(BindableObject view, bool hasContextMenu)
    {
        view.SetValue(HasContextMenuProperty, hasContextMenu);
    }

    private static void OnHasContextMenuChanged(BindableObject bindableObject, object oldValue, object newValue)
    {
        if (bindableObject is not View button)
        {
            return;
        }

        var hasPopover = (bool)newValue;
        if (hasPopover)
        {
            button.Effects.Add(new ContextMenuEffect());
        }
        else
        {
            var toRemove = button.Effects.FirstOrDefault(e => e is ContextMenuEffect);
            if (toRemove != null)
            {
                button.Effects.Remove(toRemove);
            }
        }
    }
}