namespace DIPS.Mobile.UI.Resources.Styles.ListItem;

internal class ListItemStyleResources
{
    public static Dictionary<ListItemStyle, Style> Styles { get; } = new()
    {
        [ListItemStyle.KeyWithInlineValue] = ListItemTitleStyle.KeyWithInlineValue,
        [ListItemStyle.KeyWithUnderlyingValue] = ListItemTitleStyle.KeyWithUnderlyingValue
    };
}