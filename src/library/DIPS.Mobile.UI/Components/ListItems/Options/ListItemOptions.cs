namespace DIPS.Mobile.UI.Components.ListItems.Options;

public abstract class ListItemOptions : BindableObject
{
    /// <summary>
    /// Executed if consumer has created their own ListItemOptions
    /// </summary>
    public void Bind(ListItem listItem)
    {
        this.SetBinding(BindingContextProperty, static (ListItem listItem) => listItem.BindingContext, source: listItem);
        DoBind(listItem);
    }

    protected abstract void DoBind(ListItem listItem);

    /// <summary>
    /// Override this to clean up event subscriptions when the ListItem is disposed
    /// </summary>
    protected virtual void DoUnbind()
    {
    }

    internal void Unbind()
    {
        DoUnbind();
    }
}