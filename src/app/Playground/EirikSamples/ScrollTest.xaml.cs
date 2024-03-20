using DIPS.Mobile.UI.Components.Alerting.Dialog;

namespace Playground.EirikSamples;

public partial class ScrollTest
{
    public ScrollTest()
    {
        InitializeComponent();
    }
    
    private void Header_OnTapped(object sender, EventArgs e)
    {
        DialogService.ShowMessage("You tapped it!", "Great job!", "Thanks!");
    }
}