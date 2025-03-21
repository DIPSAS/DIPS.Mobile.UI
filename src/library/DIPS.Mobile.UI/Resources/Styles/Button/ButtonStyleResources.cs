namespace DIPS.Mobile.UI.Resources.Styles.Button;

internal class ButtonStyleResources
{
    public static Dictionary<ButtonStyle, Style> Styles => new()
    {
        [ButtonStyle.PrimaryLarge] = ButtonTypeStyle.PrimaryLarge,
        [ButtonStyle.PrimarySmall] = ButtonTypeStyle.PrimarySmall,
        [ButtonStyle.PrimaryIconButtonSmall] = ButtonTypeStyle.PrimaryIconButtonSmall,
        [ButtonStyle.PrimaryIconButtonLarge] = ButtonTypeStyle.PrimaryIconButtonLarge,
        [ButtonStyle.SecondaryLarge] = ButtonTypeStyle.SecondaryLarge,
        [ButtonStyle.SecondarySmall] = ButtonTypeStyle.SecondarySmall,
        [ButtonStyle.SecondaryIconButtonSmall] = ButtonTypeStyle.SecondaryIconButtonSmall,
        [ButtonStyle.SecondaryIconButtonLarge] = ButtonTypeStyle.SecondaryIconButtonLarge,
        [ButtonStyle.GhostLarge] = ButtonTypeStyle.GhostLarge,
        [ButtonStyle.GhostSmall] = ButtonTypeStyle.GhostSmall,
        [ButtonStyle.GhostIconButtonSmall] = ButtonTypeStyle.GhostIconButtonSmall,
        [ButtonStyle.GhostIconButtonLarge] = ButtonTypeStyle.GhostIconButtonLarge,
        [ButtonStyle.CloseIconButtonSmall] = ButtonTypeStyle.CloseIconButtonSmall
    };
}