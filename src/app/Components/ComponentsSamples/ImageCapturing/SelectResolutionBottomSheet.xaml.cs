using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;

namespace Components.ComponentsSamples.ImageCapturing;

public partial class SelectResolutionBottomSheet
{
    public SelectResolutionBottomSheet()
    {
        InitializeComponent();
    }

    private async void MenuItem_OnClicked(object? sender, EventArgs e)
    {
        if(!int.TryParse(Entry.Text, out var value))
        {
            _ = DialogService.ShowMessage("Ugyldig input", "Vennligst skriv inn et gyldig heltall", "Ok");
            return;
        }
            
        ImageCaptureSample.CameraResolution = new Size(value, value);

        
        await DialogService.ShowMessage("Lagret", "Gå ut og inn av siden for å bruke den nye oppløsningen", "Ok");
        _ = Close();
    }
}