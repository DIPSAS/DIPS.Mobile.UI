using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Mobile.UI.API.TabBadge;

namespace Playground;

public partial class TestTab
{
    public TestTab()
    {
        InitializeComponent();
    }

    private void Stepper_OnValueChanged(object sender, ValueChangedEventArgs e)
    {
        TabBadgeService.SetCount(1, (int)e.NewValue);
    }

    private void ListItem_OnTapped(object sender, EventArgs e)
    {
        TabBadgeService.SetColor(1, Colors.Blue);
    }
}