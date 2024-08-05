using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.VetleSamples;

public partial class VetleTestPage3
{
    public VetleTestPage3()
    {
        InitializeComponent();
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        var page = Shell.Current.Navigation.NavigationStack.FirstOrDefault(page => page?.GetType() == typeof(VetleTestPage1));
        Shell.Current.Navigation.RemovePage(page);
        
        _ = Shell.Current.Navigation.PopModalAsync();
    }
}