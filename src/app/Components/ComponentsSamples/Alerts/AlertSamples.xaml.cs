using Components.ComponentsSamples.Alerts.SystemMessages;

namespace Components.ComponentsSamples.Alerts;

public partial class AlertSamples
{
    public AlertSamples()
    {
        InitializeComponent();
    }

    private void NavigationListItem_OnTapped(object? sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new SystemMessageSamples());
    }
}