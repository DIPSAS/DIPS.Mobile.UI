using System.ComponentModel;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using ContextMenuItem = DIPS.Mobile.UI.Components.ContextMenus.ContextMenuItem;

namespace Components.ComponentsSamples.ContextMenu;

public class ContextMenuSamplesViewModel : INotifyPropertyChanged
{
    public ContextMenuSamplesViewModel()
    {
    }

    private static void MenuItemClicked(ContextMenuItem clickedMenuItem)
    {
        DialogService.ShowMessage("Tapped", "You tapped an context menu item", "Ok");
        //Do something
    }
        
    public Command ItemClickedCommand { get; } = new Command<ContextMenuItem>(MenuItemClicked);

    private bool m_isOptionChecked;
    public bool IsOptionChecked
    {
        get => m_isOptionChecked;
        set
        {
            m_isOptionChecked = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsOptionChecked)));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}