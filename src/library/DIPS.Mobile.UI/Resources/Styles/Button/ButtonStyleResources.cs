namespace DIPS.Mobile.UI.Resources.Styles.Button;

internal class ButtonStyleResources
{
    public static Dictionary<ButtonStyle, Style> Styles => new()
    {
        [ButtonStyle.PrimaryLarge] = ButtonTypeStyle.PrimaryLarge,
        [ButtonStyle.PrimarySmall] = ButtonTypeStyle.PrimarySmall,
        [ButtonStyle.PrimaryRoundedSmall] = ButtonTypeStyle.PrimaryRoundedSmall,
        [ButtonStyle.PrimaryRoundedLarge] = ButtonTypeStyle.PrimaryRoundedLarge,
        [ButtonStyle.SecondaryLarge] = ButtonTypeStyle.SecondaryLarge,
        [ButtonStyle.SecondarySmall] = ButtonTypeStyle.SecondarySmall,
        [ButtonStyle.SecondaryRoundedSmall] = ButtonTypeStyle.SecondaryRoundedSmall,
        [ButtonStyle.SecondaryRoundedLarge] = ButtonTypeStyle.SecondaryRoundedLarge,
        [ButtonStyle.GhostLarge] = ButtonTypeStyle.GhostLarge,
        [ButtonStyle.GhostSmall] = ButtonTypeStyle.GhostSmall,
        [ButtonStyle.GhostRoundedSmall] = ButtonTypeStyle.GhostRoundedSmall,
        [ButtonStyle.GhostRoundedLarge] = ButtonTypeStyle.GhostRoundedLarge,
    };
}