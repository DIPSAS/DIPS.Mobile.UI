using Components.Resources.LocalizedStrings;
using DIPS.Mobile.UI.Components.ContextMenus;

namespace Components.ComponentsSamples.ContextMenus;

[ComponentSample(nameof(LocalizedStrings.ContextMenu))]
public partial class ContextMenuSamples
{
    public ContextMenuSamples()
    {
        InitializeComponent();
    }
    
    private static void MenuItemClicked(ContextMenuItem clickedMenuItem)
    {
        //Do something
    }
        
    public Command ItemClickedCommand { get; } = new Command<ContextMenuItem>(MenuItemClicked);
}