using DIPS.Mobile.UI.API.Library;

namespace Playground.HåvardSamples;

public partial class HåvardPage
{
    public HåvardPage()
    {
        InitializeComponent();
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        DUI.RemoveViewsLocatedOnTopOfPage();
    }
}