namespace DIPS.Mobile.UI.Components.ListItems.Options;

public abstract class ListItemOptions : BindableObject
{
    public void Bind(ListItem listItem)
    {
        this.SetBinding(BindingContextProperty, static (ListItem listItem) => listItem.BindingContext, source: listItem);
        DoBind(listItem);
    }
    
    public abstract void DoBind(ListItem listItem);
}