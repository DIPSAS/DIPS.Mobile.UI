namespace DIPS.Mobile.UI.Components.ContextMenus;

public class ContextMenuSeparatorItem : Element, IContextMenuItem
{
    public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(
        nameof(IsVisible),
        typeof(bool),
        typeof(ContextMenuSeparatorItem),
        defaultValue: true);

    public bool IsVisible
    {
        get => (bool)GetValue(IsVisibleProperty);
        set => SetValue(IsVisibleProperty, value);
    }

    public void Dispose()
    {
        
    }
}