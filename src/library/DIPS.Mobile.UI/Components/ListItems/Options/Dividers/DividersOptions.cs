namespace DIPS.Mobile.UI.Components.ListItems.Options.Dividers;

public partial class DividersOptions : ListItemOptions
{
    internal static void SetupDefaults(ListItem listItem)
    {
        if (listItem.TopDivider is not null)
        {
            var topDividerMargin = (Thickness)TopDividerMarginProperty.DefaultValue;
            listItem.TopDivider.Margin = new Thickness(topDividerMargin.Left - listItem.Padding.Left, 0, topDividerMargin.Right - listItem.Padding.Right, 0);
            listItem.TopDivider.TranslationY = -listItem.Padding.Top;
        }

        if (listItem.BottomDivider is not null)
        {
            var bottomDividerMargin = (Thickness)BottomDividerMarginProperty.DefaultValue;
            listItem.BottomDivider.Margin = new Thickness(bottomDividerMargin.Left - listItem.Padding.Left, 0, bottomDividerMargin.Right - listItem.Padding.Right, 0);
            listItem.BottomDivider.TranslationY = listItem.Padding.Bottom;
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