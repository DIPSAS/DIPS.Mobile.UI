using System;
using DIPS.Mobile.UI.API.Tip;
using Microsoft.Maui.Platform;

namespace Components.ComponentsSamples.Tip;

public partial class TipSamples
{
    public TipSamples()
    {
        InitializeComponent();
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        TipService.Show(TipMessage.Text, ButtonWithTip);
    }
}