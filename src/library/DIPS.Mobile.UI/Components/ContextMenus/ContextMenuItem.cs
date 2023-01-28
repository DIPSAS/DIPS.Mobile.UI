using System;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.ContextMenus
{
    /// <summary>
    /// A context menu item to use in a context menu
    /// </summary>
    public partial class ContextMenuItem : BindableObject
    {
        internal void SendClicked(ContextMenuButton contextMenuButton)
        {
            contextMenuButton.SendClicked(this);
            Command?.Execute(CommandParameter);
            DidClick?.Invoke(this, EventArgs.Empty);
        }
    }
}