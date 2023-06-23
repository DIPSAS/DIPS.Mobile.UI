using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Toolbars.Android;

internal partial class DUIToolbar
{
    public Page? PageConnectedTo { get; set; }
    public Color TitleColor { get; set; } = Colors.GetColor(ColorName.color_system_white);
    public Color ToolbarItemsColor { get; set; } = Colors.GetColor(ColorName.color_system_white);
}