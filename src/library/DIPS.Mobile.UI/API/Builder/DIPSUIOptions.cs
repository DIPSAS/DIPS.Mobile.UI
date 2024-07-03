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

    public IDIPSUIOptions EnableAutomaticMemoryLeakResolving(Action<object>? additionalResolver = null)
    {
        GCCollectionMonitor.Instance.TryAutoResolveMemoryLeaksEnabled = true;

        if (additionalResolver is not null)
        {
            GCCollectionMonitor.Instance.SetAdditionalResolver(additionalResolver);
        }

        return this;
    }
}