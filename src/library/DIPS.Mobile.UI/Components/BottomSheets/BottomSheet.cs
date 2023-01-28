using System;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    public partial class BottomSheet : ContentView
    {
        public void Close()
        {
            WillClose?.Invoke(this, EventArgs.Empty);
        }

        internal void SendDidClose()
        {
            DidClose?.Invoke(this, EventArgs.Empty);
        }
    }
}