using System;
using DIPS.Mobile.UI.Components.Buttons;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.ContextMenu
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
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }
}