namespace DIPS.Mobile.UI.Components.ListItems.Options.Dividers;

public partial class DividersOptions : ListItemOptions
{
    public override void DoBind(ListItem listItem)
    {
        listItem.TopDivider?.SetBinding(View.MarginProperty, new Binding(nameof(TopDividerMargin), source: this));
        listItem.BottomDivider?.SetBinding(View.MarginProperty, new Binding(nameof(BottomDividerMargin), source: this));
    }
}