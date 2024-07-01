using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.MemoryManagement;

namespace DIPS.Mobile.UI.API.Builder;

internal class DIPSUIOptions : IDIPSUIOptions
{
    public IDIPSUIOptions SetContextMenuItemClickedCallback(Action<ContextMenuItem> callback)
    {
        ContextMenuEffect.SetContextMenuItemClickedCallback(callback);

        return this;
    }

    public IDIPSUIOptions SetAutoResolveMemoryLeaksEnabled(bool enabled = true)
    {
        GarbageCollection.TryAutoResolveMemoryLeaksEnabled = true;

        return this;
    }
}