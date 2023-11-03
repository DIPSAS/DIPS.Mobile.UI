namespace DIPS.Mobile.UI.Resources.Styles.Chip;

internal static class ChipStyleResources
{
    public static Dictionary<ChipStyle, Style> Styles { get; } = new()
    {
        [ChipStyle.Input] = InputStyle.Current, 
        [ChipStyle.EmptyInput] = EmptyInputStyle.Current,
        [ChipStyle.Toggle] = ToggleStyle.ToggledOff,
    };
}