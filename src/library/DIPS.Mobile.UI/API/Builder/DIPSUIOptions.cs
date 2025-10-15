using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField.Dictation;
using DIPS.Mobile.UI.MemoryManagement;

namespace DIPS.Mobile.UI.API.Builder;

internal class DIPSUIOptions : IDIPSUIOptions
{
    public IDIPSUIOptions HandleContextMenuGlobalClicks(Action<GlobalContextMenuClickMetadata> callback)
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
    
    public IDIPSUIOptions EnableAutomaticModalHandlerDisconnection()
    {
        GCCollectionMonitor.TryAutoHandlerDisconnectModalPagesEnabled = true;

        return this;
    }
    
    public IDIPSUIOptions EnableCustomHideSoftInputOnTapped()
    {
        DUI.ShouldUseCustomHideSoftInputOnTappedImplementation = true;
        
        return this;
    }

    public IDIPSUIOptions EnableExperimentalFeature(DUI.ExperimentalFeatures feature)
    {
        DUI.EnableExperimentalFeature(feature);
        
        return this;
    }    
    
    public IDIPSUIOptions AddStartDictationDelegate(Func<IDictationConsumerDelegate, CancellationToken, Task<StartDictationResult>>? startDictationDelegate)
    {
        DUI.StartDictationDelegate = startDictationDelegate;
        
        return this;
    }
    
}