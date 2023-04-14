using DIPS.Mobile.UI.Components.ContextMenus;

namespace DIPS.Mobile.UI.Effects.PopoverEffect;

public class PopoverEffect : RoutingEffect
{
    public static readonly BindableProperty HasPopoverProperty = BindableProperty.CreateAttached("HasPopover",
        typeof(bool),
        typeof(PopoverEffect),
        false,
        propertyChanged: OnHasPopoverChanged);
    
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.CreateAttached("ItemsSource",
        typeof(ContextMenu),
        typeof(PopoverEffect),
        null);

    public static ContextMenu GetItemsSource(BindableObject view)
    {
        return (ContextMenu)view.GetValue(ItemsSourceProperty);
    }

    public static void SetItemsSource(BindableObject view, ContextMenu itemsSource)
    {
        view.SetValue(ItemsSourceProperty, itemsSource);
    }
    
    public static bool GetHasPopover(BindableObject view)
    {
        return (bool)view.GetValue(HasPopoverProperty);
    }
    
    public static void SetHasPopover(BindableObject view, bool hasPopover)
    {
        view.SetValue(HasPopoverProperty, hasPopover);
    }

    public static void OnHasPopoverChanged(BindableObject bindableObject, object oldValue, object newValue)
    {
        if(bindableObject is not View view)
            return;

        var hasPopover = (bool)newValue;
        if (hasPopover)
        {
            view.Effects.Add(new PopoverEffect());
        }
        else
        {
            var toRemove = view.Effects.FirstOrDefault(e => e is PopoverEffect);
            if (toRemove != null)
            {
                view.Effects.Remove(toRemove);
            }
        }
    }
    
}

