using DIPS.Mobile.UI.Components.ContextMenus;

namespace DIPS.Mobile.UI.Components.ListItems.Options.ContextMenu;

public partial class ContextMenuOptions
{
    public static readonly BindableProperty MenuProperty = BindableProperty.Create(
        nameof(Menu),
        typeof(ContextMenus.ContextMenu),
        typeof(ContextMenuOptions));

    public static readonly BindableProperty ModeProperty = BindableProperty.Create(
        nameof(Mode),
        typeof(ContextMenuEffect.ContextMenuMode),
        typeof(ContextMenuOptions));

    public ContextMenuEffect.ContextMenuMode Mode
    {
        get => (ContextMenuEffect.ContextMenuMode)GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }
    
    public ContextMenus.ContextMenu Menu
    {
        get => (ContextMenus.ContextMenu)GetValue(MenuProperty);
        set => SetValue(MenuProperty, value);
    }
}