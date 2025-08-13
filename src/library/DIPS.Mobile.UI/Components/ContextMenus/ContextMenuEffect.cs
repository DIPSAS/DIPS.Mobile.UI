using DIPS.Mobile.UI.Components.ListItems;

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
    
    public static readonly BindableProperty MenuBindingContextProperty = BindableProperty.CreateAttached("MenuBindingContext",
        typeof(object),
        typeof(ContextMenuEffect),
        null);

    internal static Action<GlobalContextMenuClickMetadata>? ContextMenuItemGlobalClicksCallBack { get; private set; }

    internal static void SetContextMenuItemLoggingCallback(Action<GlobalContextMenuClickMetadata> callback)
    {
        ContextMenuItemGlobalClicksCallBack = callback;
    }

    public static ContextMenuMode GetMode(BindableObject view)
    {
        return (ContextMenuMode)view.GetValue(ModeProperty);
    }

    public static void SetMode(BindableObject view, ContextMenuMode mode)
    {
        view.SetValue(ModeProperty, mode);
    }
    
    public static ContextMenu? GetMenu(BindableObject view)
    {
        return (ContextMenu?)view.GetValue(MenuProperty);
    }

    /// <summary>
    /// Provides a BindingContext for the ContextMenu
    /// </summary>
    /// <remarks>Use if you want the Context Menu to have a different <see cref="View.BindingContext"/> than the Element it is attached to</remarks>
    public static void SetMenu(BindableObject view, ContextMenu itemsSource)
    {
        view.SetValue(MenuProperty, itemsSource);
    }
    
    public static object? GetMenuBindingContext(BindableObject view)
    {
        return (object?)view.GetValue(MenuBindingContextProperty);
    }

    public static void SetMenuBindingContext(BindableObject view, object bindingContext)
    {
        view.SetValue(MenuBindingContextProperty, bindingContext);
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
            button.BindingContextChanged += View_OnBindingContextChanged;
        }
        else
        {
            var toRemove = button.Effects.FirstOrDefault(e => e is ContextMenuEffect);
            if (toRemove != null)
            {
                button.Effects.Remove(toRemove);
            }

            button.BindingContextChanged -= View_OnBindingContextChanged;
        }
    }

    private static void View_OnBindingContextChanged(object? sender, EventArgs e)
    {
        if (sender is not View view) return;

        var contextMenu = GetMenu(view);
        var menuBindingContext = GetMenuBindingContext(view);
        if (contextMenu is null)
            return;

        contextMenu.BindingContext = menuBindingContext ?? view.BindingContext;
    }

    public enum ContextMenuMode
    {
        Pressed,
        LongPressed
    }
}