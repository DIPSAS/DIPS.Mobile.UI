using System;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    public partial class BottomSheet : ContentView
    {
        private bool m_layedOut;

        protected override void OnPropertyChanged(string propertyName = null)
        {
            if (propertyName.Equals(nameof(Parent)))
            {
                if (!m_layedOut)
                {
                    LayoutContent();
                }
            }

            base.OnPropertyChanged(propertyName);
        }

        private void LayoutContent()
        {
            m_layedOut = true;
        }


        public void Close()
        {
            WillClose?.Invoke(this, EventArgs.Empty);
            OnWillClose();
        }

        internal void SendDidClose()
        {
            DidClose?.Invoke(this, EventArgs.Empty);
            OnDidClose();
        }
    }
}