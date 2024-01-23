using Playground.EirikSamples;
using Playground.HåvardSamples;
using Playground.SanderSamples;
using Playground.VetleSamples;

namespace Playground;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void GoToVetle(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new VetlePage());
    }

    private void GoToHåvard(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new HåvardPage());
    }

    private void GoToSander(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new SanderPage());
    }

    private void GoToEirik(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new EirikPage());

    }
}