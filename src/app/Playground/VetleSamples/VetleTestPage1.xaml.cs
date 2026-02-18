using DIPS.Mobile.UI.API.Library.Android;
using DIPS.Mobile.UI.API.Tip;
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

#if __IOS__
        var test = Platform.GetCurrentUIViewController();
#endif
        
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


    private async void Button_OnClicked(object sender, EventArgs e)
    {
        //Navigation.PushAsync(new VetleTestPage2());
        _ = Navigation.PushModalAsync(new NavigationPage(new VetleTestPage1()));
    }
    
    private async void Button_OnClicked2(object sender, EventArgs e)
    {
        SystemMessageService.Display(config =>
        {
            config.Duration = 2500;
            config.Text= "test";
        });
        await Task.Delay(1000);
        await Navigation.PopModalAsync();
    }

    private void ListItem_OnTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new VetleTestPage2());
    }

    private void Button_OnClicked2(object sender, EventArgs e)
    {
        StatusBarHandler.TrySetStatusBarColor(this, Colors.Transparent);
    }

    private void Button_OnClicked3(object sender, EventArgs e)
    {
        StatusBarHandler.TrySetStatusBarColor(this, StatusBarColor);
    }
}