using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.Alerting.Dialog;

namespace Components.ComponentsSamples.Alerting.Dialogs;

public partial class InputDialogSamples 
{
    public InputDialogSamples()
    {
        InitializeComponent();
    }

    private void OneInputField(object? sender, EventArgs e)
    {
        DialogService.ShowInputDialog(config =>
        {
            config.SetTitle("Test");
            config.SetDescription("Testing");
            config.AddInputField(new StringDialogInputField("Placeholder"));
        });
    }

    private void TwoInputFields(object? sender, EventArgs e)
    {
        DialogService.ShowInputDialog(config =>
        {
            config.SetTitle("Test");
            config.SetDescription("Testing");
            config.AddInputField(new StringDialogInputField("Placeholder"));
            config.AddInputField(new StringDialogInputField("Placeholder"));
        });
    }
}