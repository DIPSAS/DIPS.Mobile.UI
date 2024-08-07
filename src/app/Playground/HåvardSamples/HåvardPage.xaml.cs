using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;

namespace Playground.HåvardSamples;

public partial class HåvardPage
{
    private readonly HåvardPageViewModel m_håvardPageViewModel;

    public HåvardPage()
    {
        InitializeComponent();
        m_håvardPageViewModel = BindingContext as HåvardPageViewModel;
    }
    
    private void ContextMenuItem_OnDidClick(object sender, EventArgs e)
    {
        DialogService.ShowMessage("You tapped it", "yey!", "Ok");
    }

    private void ListItem_OnTapped(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new HåvardPage3(m_håvardPageViewModel));
    }
}