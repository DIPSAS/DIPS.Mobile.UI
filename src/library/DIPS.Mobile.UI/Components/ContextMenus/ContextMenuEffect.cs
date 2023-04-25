namespace DIPS.Mobile.UI.Components.ContextMenus;

public class ContextMenuEffect : RoutingEffect
{
    
    /// <summary>
    /// Set the ContextMenu mode
    /// </summary>
    /// <remarks>Default is <see cref="ContextMenuMode.Pressed"/></remarks>
    public static readonly BindableProperty ModeProperty = BindableProperty.CreateAttached("Mode",
        typeof(ContextMenuMode),
        typeof(ContextMenuEffect),
        ContextMenuMode.Pressed);

    /// <summary>
    /// Configures the ContextMenu
    /// </summary>
    public static readonly BindableProperty MenuProperty = BindableProperty.CreateAttached("Menu",
        typeof(ContextMenu),
        typeof(ContextMenuEffect),
        null,
        propertyChanged:OnHasMenuChanged);


    public static ContextMenuMode GetMode(BindableObject view)
    {
        return (ContextMenuMode)view.GetValue(ModeProperty);
    }

    public static void SetMode(BindableObject view, ContextMenuMode mode)
    {
        view.SetValue(ModeProperty, mode);
    }
    
    public static ContextMenu GetMenu(BindableObject view)
    {
        return (ContextMenu)view.GetValue(MenuProperty);
    }

    public static void SetMenu(BindableObject view, ContextMenu itemsSource)
    {
        view.SetValue(MenuProperty, itemsSource);
    }

    private static void OnHasMenuChanged(BindableObject bindableObject, object oldValue, object newValue)
    {
        if (bindableObject is not View button)
        {
            return;
        }

        var shouldHaveContextMenu = newValue != null;
        if (shouldHaveContextMenu)
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

    public enum ContextMenuMode
    {
        Pressed,
        LongPressed
    }
}