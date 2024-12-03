namespace DIPS.Mobile.UI.Components.ListItems.Options.Dividers;

public partial class DividersOptions : ListItemOptions
{
    public override void DoBind(ListItem listItem)
    {
        if (listItem.TopDivider is not null)
        {
            listItem.TopDivider.SetBinding(View.MarginProperty,
                static (DividersOptions options) => options.TopDividerMargin, source: this);
        }

        if (listItem.BottomDivider is not null)
        {
            listItem.BottomDivider.SetBinding(View.MarginProperty,
                static (DividersOptions options) => options.BottomDividerMargin, source: this);
        }
    }
}