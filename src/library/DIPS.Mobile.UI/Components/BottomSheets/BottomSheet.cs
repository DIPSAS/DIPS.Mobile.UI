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
            if (!m_shouldHaveToolbar) //Leave the Content alone
            {
                return;
            }

            var consumerContent = Content;
            //Wrap it all in a Grid
            var theGrid = new Grid()
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new() {Height = GridLength.Auto,}, new() {Height = GridLength.Star,}
                }
            };

            var toolbar = new Grid()
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition {Width = GridLength.Star},
                    new ColumnDefinition {Width = GridLength.Auto},
                    new ColumnDefinition {Width = GridLength.Auto}
                }
            };
            
            theGrid.Children.Add(consumerContent, 0,0);
            theGrid.Children.Add(consumerContent, 0,1);
            Content = theGrid;
        }


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