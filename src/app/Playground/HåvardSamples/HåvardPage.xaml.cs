namespace Playground.HåvardSamples;

public partial class HåvardPage
{
    public HåvardPage()
    {
        InitializeComponent();
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        var page1 = new ContentPage() { Content = new Label(){Text = "yey "}};
        var page2 = new ContentPage() { Content = new Button(){Command = new Command(() => Shell.Current.Navigation.PushModalAsync(page1))}};
        var navigationpage = new NavigationPage(page2);
        App.Current.MainPage.Navigation.PushModalAsync(navigationpage);
    }
}