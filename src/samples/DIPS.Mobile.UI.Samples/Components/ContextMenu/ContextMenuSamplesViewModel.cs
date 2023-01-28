using System.ComponentModel;
using DIPS.Mobile.UI.Components.ContextMenus;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Samples.Components.ContextMenu
{
    public class ContextMenuSamplesViewModel : INotifyPropertyChanged
    {
        public ContextMenuSamplesViewModel()
        {
        }

        private static void MenuItemClicked(ContextMenuItem clickedMenuItem)
        {
            //Do something
        }
        
        public Command ItemClickedCommand { get; } = new Command<ContextMenuItem>(MenuItemClicked);

        public event PropertyChangedEventHandler PropertyChanged;
    }
}