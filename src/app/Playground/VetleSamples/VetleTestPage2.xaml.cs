using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.VetleSamples;

public partial class VetleTestPage2
{
    public VetleTestPage2()
    {
        InitializeComponent();
    }

    private void NavigationListItem_OnTapped(object sender, EventArgs e)
    {
        var test = Shell.Current.Navigation.ModalStack;
    }
}