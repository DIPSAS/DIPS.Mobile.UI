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

    /// <summary>
    /// Executed when the corresponding ListItem object is created, (e.g TitleLabel, SubtitleLabel)
    /// </summary>
    public abstract void SetupDefaults(ListItem listItem);
    protected abstract void DoBind(ListItem listItem);
}