using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.ContextMenus;

namespace DIPS.Mobile.UI.API.Builder;

public interface IDIPSUIOptions
{
    /// <summary>
    ///     Sets a custom callback method to be invoked whenever a <see cref="ContextMenuItem"/> has been tapped by
    ///     a person.
    /// </summary>
    /// <param name="callback">The method to invoke, receiving the <see cref="ContextMenuItem"/> that was tapped.</param>
    /// <returns></returns>
    IDIPSUIOptions HandleContextMenuGlobalClicks(Action<GlobalContextMenuClickMetadata> callback);

    /// <summary>
    ///     Sets whether DIPS.Mobile.UI should try and auto-resolve memory leaks that occur during page navigation and closing of bottomsheets.
    ///     Provide a custom <see cref="Action"/> to set additional handling for custom types.
    /// </summary>
    /// <param name="additionalResolver">
    ///     Custom action to be used when resolving memory leaks. Provide an <see cref="Action{Object}"/> that cleans up or disposes
    ///     your custom views.
    ///  </param>
    IDIPSUIOptions EnableAutomaticMemoryLeakResolving(Action<object>? additionalResolver = null);

    /// <summary>
    ///     Sets whether DIPS.Mobile.UI should try and disconnect handlers of modal pages, when a modal is popped
    ///     Currently, MAUI only disconnect handlers of pages that are visible in the modal when it is popped.
    ///     Additionally, they do not disconnect handlers of pages that are just removed
    /// </summary>
    IDIPSUIOptions EnableAutomaticModalHandlerDisconnection();

    /// <summary>
    ///     Enable to try to isolate memory leaks when they are detected, because memory leaks propagates both upwards and downwards
    ///     <remarks>This can have unexpected side effects and should only be used during debugging.</remarks>
    /// </summary>
    IDIPSUIOptions EnableIsolateMemoryLeak();
    
    /// <summary>
    ///     Sets whether DIPS.Mobile.UI should use a custom implementation for hiding the soft input when a tap is detected.
    ///     Based on: https://supportcenter.devexpress.com/ticket/details/t1208656/adding-more-information-on-contentpage-hidesoftinputontapped#c43630fa-4759-4fb3-bc13-593024a70426
    /// </summary>
    IDIPSUIOptions EnableCustomHideSoftInputOnTapped();

    /// <summary>
    /// Enable a specific experimental feature.
    /// </summary>
    /// <param name="feature">The experimental feature to enable.</param>
    IDIPSUIOptions EnableExperimentalFeature(DUI.ExperimentalFeatures feature);
}