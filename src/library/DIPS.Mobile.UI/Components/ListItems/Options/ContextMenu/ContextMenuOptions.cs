using DIPS.Mobile.UI.Components.ContextMenus;

namespace DIPS.Mobile.UI.Components.ListItems.Options.ContextMenu;

public partial class ContextMenuOptions : ListItemOptions
{
    public override void DoBind(ListItem listItem)
    {
        ContextMenuEffect.SetMode(listItem.Border, Mode);
    }
}