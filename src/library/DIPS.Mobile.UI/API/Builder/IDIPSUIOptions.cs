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
    IDIPSUIOptions SetContextMenuItemClickedCallback(Action<ContextMenuItem> callback);

    /// <summary>
    ///     Sets whether DIPS.Mobile.UI should try and auto-resolve memory leaks that occur during page navigation and closing of bottomsheets.
    ///     Provide a custom <see cref="Action"/> to set additional handling for custom types.
    /// </summary>
    /// <param name="additionalResolver">
    ///     Custom action to be used when resolving memory leaks. Provide an <see cref="Action{Object}"/> that cleans up or disposes
    ///     your custom views.
    ///  </param>
    IDIPSUIOptions EnableAutomaticMemoryLeakResolving(Action<object>? additionalResolver = null);
}