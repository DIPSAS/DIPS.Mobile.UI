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
}