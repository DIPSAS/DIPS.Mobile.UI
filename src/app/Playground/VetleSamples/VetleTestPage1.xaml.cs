using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Components.Alerting.SystemMessage;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;
using DIPS.Mobile.UI.Resources.Icons;
using ImageButton = DIPS.Mobile.UI.Components.Images.ImageButton;

namespace Playground.VetleSamples;

public partial class VetleTestPage1 
{
    public VetleTestPage1()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        //FloatingNavigationButtonService.TryHideOrShowFloatingNavigationButton(this);
    }

    public void AddButton(object sender, EventArgs eventArgs)
    {
    }

    private void NavigationListItem_OnTapped(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//VetlePage");
    }

    private void Test(object sender, EventArgs e)
    {
        new TestBottomSheetFitToContent().Open();
    }

    private void Lol(object sender, EventArgs e)
    {
        new BottomSheetWithToolbar().Open();
    }


    private void Element_OnHandlerChanging(object sender, HandlerChangingEventArgs e)
    {
       
    }


    private void Button_OnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new VetleTestPage2());
    }
}