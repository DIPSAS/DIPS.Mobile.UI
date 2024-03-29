namespace DIPS.Mobile.UI.Components.ListItems.Options;

public abstract class ListItemOptions : BindableObject
{
    public void Bind(ListItem listItem)
    {
        SetBinding(BindingContextProperty, new Binding(source: listItem, path:nameof(BindingContext)));
        DoBind(listItem);
    }
    
    public abstract void DoBind(ListItem listItem);
}