using System.ComponentModel;
using System.Windows.Input;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;

namespace Playground.HåvardSamples;

public partial class HåvardPage
{
    public HåvardPage()
    {
        InitializeComponent();
    }

    public ICommand SearchCommand { get; } = new Command<string>(s =>
    {

    });

    private void Button_OnClicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushModalAsync(new HåvardPage());
    }

    private void HåvardPage_OnLoaded(object sender, EventArgs e)
    {
        SearchBar.Focus();
    }
}