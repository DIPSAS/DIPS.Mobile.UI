using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.MemoryManagement;

namespace DIPS.Mobile.UI.API.Builder;

internal class DIPSUIOptions : IDIPSUIOptions
{
    public IDIPSUIOptions HandleContextMenuLogging(Action<ContextMenuLoggingMetadata> callback)
    {
        ContextMenuEffect.SetContextMenuItemLoggingCallback(callback);

        return this;
    }

    public IDIPSUIOptions EnableAutomaticMemoryLeakResolving(Action<object>? additionalResolver = null)
    {
        GCCollectionMonitor.TryAutoResolveMemoryLeaksEnabled = true;

        if (additionalResolver is not null)
        {
            GCCollectionMonitor.Instance.SetAdditionalResolver(additionalResolver);
        }

        return this;
    }

    public IDIPSUIOptions EnableCustomHideSoftInputOnTapped()
    {
        DUI.ShouldUseCustomHideSoftInputOnTappedImplementation = true;
        
        return this;
    }
}