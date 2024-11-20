using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;

namespace Components.AccessibilitySamples.ListItem;

public partial class ListItemAccessibilitySamples
{
    public ListItemAccessibilitySamples()
    {
        InitializeComponent();


        
    }

    public ICommand Test { get; }

    private void ListItem_OnTapped(object? sender, EventArgs e)
    {
        DialogService.ShowMessage("Ko", "Ko", "Ok");
    }
}