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
        Shell.Current.Navigation.PushAsync(new HåvardPage2());
    }

    private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        
    }

    private void SegmentControl_OnOnSelected(object sender, EventArgs e)
    {
        
    }
}