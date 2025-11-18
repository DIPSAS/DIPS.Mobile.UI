namespace DIPS.Mobile.UI.Resources.Styles.Button;

internal class ButtonStyleResources
{
    public static Dictionary<ButtonStyle, Style> Styles => new()
    {
        [ButtonStyle.CallToActionLarge] = ButtonTypeStyle.CallToActionLarge,
        [ButtonStyle.CallToActionSmall] = ButtonTypeStyle.CallToActionSmall,
        [ButtonStyle.CallToActionIconSmall] = ButtonTypeStyle.CallToActionIconSmall,
        [ButtonStyle.CallToActionIconLarge] = ButtonTypeStyle.CallToActionIconLarge,
        [ButtonStyle.DefaultLarge] = ButtonTypeStyle.DefaultLarge,
        [ButtonStyle.DefaultSmall] = ButtonTypeStyle.DefaultSmall,
        [ButtonStyle.DefaultIconSmall] = ButtonTypeStyle.DefaultIconSmall,
        [ButtonStyle.DefaultIconLarge] = ButtonTypeStyle.DefaultIconLarge,
        [ButtonStyle.GhostLarge] = ButtonTypeStyle.GhostLarge,
        [ButtonStyle.GhostSmall] = ButtonTypeStyle.GhostSmall,
        [ButtonStyle.GhostIconSmall] = ButtonTypeStyle.GhostIconSmall,
        [ButtonStyle.GhostIconLarge] = ButtonTypeStyle.GhostIconLarge,
        [ButtonStyle.CloseIconSmall] = ButtonTypeStyle.CloseIconSmall,
        [ButtonStyle.DefaultFloatingIconLarge] = ButtonTypeStyle.DefaultFloatingIconLarge,
        [ButtonStyle.DefaultFloatingLarge] = ButtonTypeStyle.DefaultFloatingLarge
    };
}