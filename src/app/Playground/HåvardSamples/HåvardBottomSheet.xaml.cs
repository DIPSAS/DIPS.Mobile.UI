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

    private void CloseAllBottomSheets(object sender, EventArgs e)
    {
        BottomSheetService.CloseAll();
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        new BottomSheet()
        {
            Title = "Second bottom sheet",
            Content = new Button() {Text = "Close all", Command = new Command(() => BottomSheetService.CloseAll())}
        }.Open();
    }
}