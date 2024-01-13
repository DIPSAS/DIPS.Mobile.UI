using DIPS.Mobile.UI.Components.Alerting.Dialog;

namespace Components.ComponentsSamples.Chips;

public partial class ChipsSamples
{
    public ChipsSamples()
    {
        InitializeComponent();
        var page = App.Current.MainPage;
        if (page != null)
        {
            page.NavigatedTo += (sender, args) =>
            {

            };
        }
        
    }

    private void Chip_OnTapped(object? sender, EventArgs e)
    {
        DialogService.ShowMessage("Chip tapped", "You tapped the chip", "OK");
    }
    
    private void Chip_OnCloseTapped(object? sender, EventArgs e)
    {
        DialogService.ShowMessage("Chip close tapped", "You tapped the chips close button", "OK");
    }
}