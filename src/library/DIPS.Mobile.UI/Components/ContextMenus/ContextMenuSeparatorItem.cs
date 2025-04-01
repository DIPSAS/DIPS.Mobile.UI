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

    /// <summary>
    /// The ContextMenu that the item is in
    /// </summary>
    public ContextMenu? ContextMenu { get; set; }

#if __IOS__
    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        
        if(propertyName == IsVisibleProperty.PropertyName) 
        {
            ContextMenu?.SendItemPropertiesUpdated();
        }
    }
#endif
    
    public void Dispose()
    {
        
    }
}