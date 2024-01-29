using DIPS.Mobile.UI.Components.ContextMenus;

namespace DIPS.Mobile.UI.API.Builder;

public interface IDIPSUIOptions
{
    IDIPSUIOptions SetContextMenuItemClickedCallback(Action<ContextMenuItem> callback);
}