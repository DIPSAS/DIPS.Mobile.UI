namespace DIPS.Mobile.UI.Components.ListItems.Options.Dividers;

public partial class DividersOptions : ListItemOptions
{
    public override void SetupDefaults(ListItem listItem)
    {
        if (listItem.TopDivider is not null)
        {
            listItem.TopDivider.Margin = new Thickness(this.TopDividerMargin.Left - listItem.Padding.Left, this.TopDividerMargin.Top - listItem.Padding.Top, this.TopDividerMargin.Right - listItem.Padding.Right, this.TopDividerMargin.Bottom - listItem.Padding.Bottom);
        }

        if (listItem.BottomDivider is not null)
        {
            listItem.BottomDivider.Margin = new Thickness(this.BottomDividerMargin.Left - listItem.Padding.Left, this.BottomDividerMargin.Top - listItem.Padding.Top, this.BottomDividerMargin.Right - listItem.Padding.Right, this.BottomDividerMargin.Bottom - listItem.Padding.Bottom);
        }
    }

    protected override void DoBind(ListItem listItem)
    {
        if (listItem.TopDivider is not null)
        {
            listItem.TopDivider.SetBinding(View.MarginProperty,
                static (DividersOptions options) => options.TopDividerMargin, source: this, converter: new MinusPaddingOfListItemConverter(), converterParameter: listItem.Padding);
        }

        if (listItem.BottomDivider is not null)
        {
            listItem.BottomDivider.SetBinding(View.MarginProperty,
                static (DividersOptions options) => options.BottomDividerMargin, source: this, converter: new MinusPaddingOfListItemConverter(), converterParameter: listItem.Padding);
        }
    }
}