using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;

namespace Playground.HåvardSamples;

public partial class HåvardPage
{
    public HåvardPage()
    {
        InitializeComponent();
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new HåvardPage2());
    }

    private void ListItem_OnTapped(object sender, EventArgs e)
    {
        
    }
}