namespace DIPS.Mobile.UI.Resources.Styles.Tag;

public class TagStyleResources
{
    public static Dictionary<TagStyle, Style> Styles => new()
    {
        [TagStyle.Default] = TagTypeStyle.Default,
        [TagStyle.Danger] = TagTypeStyle.Danger,
        [TagStyle.Subtle] = TagTypeStyle.Subtle,
        [TagStyle.Success] = TagTypeStyle.Success,
        [TagStyle.Warning] = TagTypeStyle.Warning
    };
}