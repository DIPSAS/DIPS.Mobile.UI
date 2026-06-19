using DIPS.Mobile.UI.Resources.Colors;
using DuiColors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Resources.Styles.SystemMessage;

internal static class SystemMessageTypeStyle
{
    internal static SystemMessageStyleColors Information => new(
        DuiColors.GetColor(ColorName.color_surface_information),
        DuiColors.GetColor(ColorName.color_text_default),
        DuiColors.GetColor(ColorName.color_border_information));

    internal static SystemMessageStyleColors Error => new(
        DuiColors.GetColor(ColorName.color_surface_danger),
        DuiColors.GetColor(ColorName.color_text_default),
        DuiColors.GetColor(ColorName.color_border_danger));

    internal static SystemMessageStyleColors Warning => new(
        DuiColors.GetColor(ColorName.color_surface_warning),
        DuiColors.GetColor(ColorName.color_text_default),
        DuiColors.GetColor(ColorName.color_border_warning));

    internal static SystemMessageStyleColors Success => new(
        DuiColors.GetColor(ColorName.color_surface_success),
        DuiColors.GetColor(ColorName.color_text_default),
        DuiColors.GetColor(ColorName.color_border_success));
}