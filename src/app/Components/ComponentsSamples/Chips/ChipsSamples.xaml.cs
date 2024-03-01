using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Components.ChipGroup;

namespace Components.ComponentsSamples.Chips;

public partial class ChipsSamples
{
    public ChipsSamples()
    {
        InitializeComponent();
    }

    private void Chip_OnTapped(object? sender, EventArgs e)
    {
        DialogService.ShowMessage("Chip tapped", "You tapped the chip", "OK");
    }
    
    private void Chip_OnCloseTapped(object? sender, EventArgs e)
    {
        DialogService.ShowMessage("Chip close tapped", "You tapped the chips close button", "OK");
    }

    private void ChipGroup_OnOnSelectedItemsChanged(object? sender, ChipGroupEventArgs e)
    {
    }
}