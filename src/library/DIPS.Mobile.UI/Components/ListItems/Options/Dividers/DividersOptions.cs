namespace DIPS.Mobile.UI.Components.ListItems.Options.Dividers;

public partial class DividersOptions : ListItemOptions
{
    internal static void SetupDefaults(ListItem listItem)
    {
        if (listItem.TopDivider is not null)
        {
            var topDividerMargin = (Thickness)TopDividerMarginProperty.DefaultValue;
            listItem.TopDivider.Margin = new Thickness(topDividerMargin.Left - listItem.Padding.Left, topDividerMargin.Top - listItem.Padding.Top, topDividerMargin.Right - listItem.Padding.Right, topDividerMargin.Bottom - listItem.Padding.Bottom);
        }

        if (listItem.BottomDivider is not null)
        {
            var bottomDividerMargin = (Thickness)BottomDividerMarginProperty.DefaultValue;
            listItem.BottomDivider.Margin = new Thickness(bottomDividerMargin.Left - listItem.Padding.Left, bottomDividerMargin.Top - listItem.Padding.Top, bottomDividerMargin.Right - listItem.Padding.Right, bottomDividerMargin.Bottom - listItem.Padding.Bottom);
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