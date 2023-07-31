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
        if (FloatingNavigationButtonService.IsShowing())
        {
            FloatingNavigationButtonService.Hide();
        }
        else
        {
            FloatingNavigationButtonService.Show();
        }
    }
}