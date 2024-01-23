using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Chip;
using DIPS.Mobile.UI.Resources.Styles.InputField;
using DIPS.Mobile.UI.Resources.Styles.Label;
using DIPS.Mobile.UI.Resources.Styles.ListItem;

namespace DIPS.Mobile.UI.Resources.Styles;

public static class Styles
{
    public static Style GetButtonStyle(ButtonStyle style)
    {
        return ButtonStyleResources.Styles.TryGetValue(style, out var buttonStyle)
            ? buttonStyle
            : new Style(typeof(View));
    }

    public static Style GetChipStyle(ChipStyle style)
    {
        return ChipStyleResources.Styles.TryGetValue(style, out var chipStyle) ? chipStyle : new Style(typeof(View));
    }

    public static Style GetLabelStyle(LabelStyle style)
    {
        return LabelStyleResources.Styles.TryGetValue(style, out var labelStyle) ? labelStyle : new Style(typeof(View));
    }

    public static Style GetInputFieldStyle(InputFieldStyle style)
    {
        return InputFieldStyleResources.Styles.TryGetValue(style, out var textAreaStyle)
            ? textAreaStyle
            : new Style(typeof(View));
    }
    
    public static Style GetListItemStyle(ListItemStyle style)
    {
        return ListItemStyleResources.Styles.TryGetValue(style, out var listItemStyle)
            ? listItemStyle
            : new Style(typeof(View));
    }
}