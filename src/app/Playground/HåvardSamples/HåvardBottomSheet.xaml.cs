using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.BottomSheets;

namespace Playground.HåvardSamples;

public partial class HåvardBottomSheet : BottomSheet
{
    public HåvardBottomSheet()
    {
        InitializeComponent();
    }

    private void MenuItem_OnClicked(object sender, EventArgs e)
    {
        this.Close();
    }
}