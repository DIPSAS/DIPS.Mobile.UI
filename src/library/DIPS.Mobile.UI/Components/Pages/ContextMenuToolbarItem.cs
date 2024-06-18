using DIPS.Mobile.UI.Components.ContextMenus;

namespace DIPS.Mobile.UI.Components.Pages;

public class ContextMenuToolbarItem : ToolbarItem
{
    public static readonly BindableProperty ContextMenuProperty = BindableProperty.Create(
        nameof(ContextMenu),
        typeof(ContextMenu),
        typeof(ContextMenuToolbarItem));

    public ContextMenu ContextMenu
    {
        get => (ContextMenu)GetValue(ContextMenuProperty);
        set => SetValue(ContextMenuProperty, value);
    }

    protected override void OnBindingContextChanged()
    {
        ContextMenu.BindingContext = BindingContext;
        base.OnBindingContextChanged();
    }
}