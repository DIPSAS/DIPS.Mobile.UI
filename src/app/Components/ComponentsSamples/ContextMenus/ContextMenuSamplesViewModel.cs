using System.ComponentModel;
using ContextMenuItem = DIPS.Mobile.UI.Components.ContextMenus.ContextMenuItem;

namespace Components.ComponentsSamples.ContextMenu;

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