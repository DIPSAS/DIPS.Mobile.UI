using System.Windows.Input;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Resources.Icons;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace Playground.HåvardSamples;

public partial class HåvardPage
{
    public HåvardPage()
    {
        InitializeComponent();
        
    }
    
    private void ContextMenuItem_OnDidClick(object sender, EventArgs e)
    {
        DialogService.ShowMessage("You tapped it", "yey!", "Ok");
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushModalAsync(new HåvardPage3());
    }
}