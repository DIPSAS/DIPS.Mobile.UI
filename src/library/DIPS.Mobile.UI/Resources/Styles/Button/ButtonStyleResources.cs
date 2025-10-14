namespace DIPS.Mobile.UI.Resources.Styles.Button;

internal class ButtonStyleResources
{
    public static Dictionary<ButtonStyle, Style> Styles => new()
    {
        [ButtonStyle.CallToActionLarge] = ButtonTypeStyle.CallToActionLarge,
        [ButtonStyle.CallToActionSmall] = ButtonTypeStyle.CallToActionSmall,
        [ButtonStyle.CallToActionIconButtonSmall] = ButtonTypeStyle.CallToActionButtonSmall,
        [ButtonStyle.CallToActionButtonLarge] = ButtonTypeStyle.CallToActionButtonLarge,
        [ButtonStyle.DefaultLarge] = ButtonTypeStyle.DefaultLarge,
        [ButtonStyle.DefaultSmall] = ButtonTypeStyle.DefaultSmall,
        [ButtonStyle.DefaultIconButtonSmall] = ButtonTypeStyle.DefaultIconButtonSmall,
        [ButtonStyle.DefaultIconButtonLarge] = ButtonTypeStyle.DefaultIconButtonLarge,
        [ButtonStyle.GhostLarge] = ButtonTypeStyle.GhostLarge,
        [ButtonStyle.GhostSmall] = ButtonTypeStyle.GhostSmall,
        [ButtonStyle.GhostIconButtonSmall] = ButtonTypeStyle.GhostIconButtonSmall,
        [ButtonStyle.GhostIconButtonLarge] = ButtonTypeStyle.GhostIconButtonLarge,
        [ButtonStyle.CloseIconButtonSmall] = ButtonTypeStyle.CloseIconButtonSmall,
        [ButtonStyle.DefaultFloatingIconButton] = ButtonTypeStyle.DefaultFloatingIconButton,
        [ButtonStyle.DefaultFloatingButton] = ButtonTypeStyle.DefaultFloatingButton
    };
}