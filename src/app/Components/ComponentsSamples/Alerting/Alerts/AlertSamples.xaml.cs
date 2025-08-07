using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Alert;
using DIPS.Mobile.UI.Components.Alerting.SystemMessage;

namespace Components.ComponentsSamples.Alerting.Alerts;

public partial class AlertSamples
{
    public AlertSamples()
    {
        InitializeComponent();
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        foreach (var vte in this.GetVisualTreeDescendants())
        {
            if(vte is AlertView alertView)
                alertView.Animate();
        }
    }
}