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
    IDIPSUIOptions HandleContextMenuLogging(Action<ContextMenuLoggingMetadata> callback);

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
    ///     Sets whether DIPS.Mobile.UI should use a custom implementation for hiding the soft input when a tap is detected.
    ///     Based on: https://supportcenter.devexpress.com/ticket/details/t1208656/adding-more-information-on-contentpage-hidesoftinputontapped#c43630fa-4759-4fb3-bc13-593024a70426
    /// </summary>
    IDIPSUIOptions EnableCustomHideSoftInputOnTapped();
}