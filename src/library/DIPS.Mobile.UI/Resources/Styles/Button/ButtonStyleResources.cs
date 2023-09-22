namespace DIPS.Mobile.UI.Resources.Styles.Button;

internal class ButtonStyleResources
{
    public static Dictionary<ButtonStyle, Style> Styles => new()
    {
        [ButtonStyle.PrimaryLarge] = ButtonTypeStyle.PrimaryLarge,
        [ButtonStyle.PrimarySmall] = ButtonTypeStyle.PrimarySmall,
        [ButtonStyle.SecondaryLarge] = ButtonTypeStyle.SecondaryLarge,
        [ButtonStyle.SecondarySmall] = ButtonTypeStyle.SecondarySmall,
        [ButtonStyle.GhostLarge] = ButtonTypeStyle.GhostLarge,
        [ButtonStyle.GhostSmall] = ButtonTypeStyle.GhostSmall,
    };
}