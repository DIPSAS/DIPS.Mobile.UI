using DIPS.Mobile.UI.Components.ContextMenus;

namespace DIPS.Mobile.UI.API.Builder;

internal class DIPSUIOptions : IDIPSUIOptions
{
    public IDIPSUIOptions SetContextMenuItemClickedCallback(Action<ContextMenuItem> callback)
    {
        ContextMenuEffect.SetContextMenuItemClickedCallback(callback);

        return this;
    }
}