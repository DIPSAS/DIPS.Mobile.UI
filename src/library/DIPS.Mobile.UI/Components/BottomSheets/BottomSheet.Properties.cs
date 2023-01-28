using System;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    public partial class BottomSheet
    {
        public event EventHandler? WillClose;
        public event EventHandler? DidClose;
    }
}