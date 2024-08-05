using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Mobile.UI.Resources.Icons;

namespace Playground.VetleSamples;

public partial class VetleTestPage2
{
    public VetleTestPage2()
    {
        InitializeComponent();
    }

    private void NavigationListItem_OnTapped(object sender, EventArgs e)
    {
        
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushModalAsync(new VetleTestPage3());
    }
}