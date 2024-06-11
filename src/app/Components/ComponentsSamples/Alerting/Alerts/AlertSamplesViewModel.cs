using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.SystemMessage;
using DIPS.Mobile.UI.Resources.Colors;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace Components.ComponentsSamples.Alerting.Alerts;

public class AlertSamplesViewModel
{
    public AlertSamplesViewModel()
    {
        Command = new Command<string>(s=>
        {
            SystemMessageService.Display(config =>
            {
                config.Text = s;
                config.BackgroundColor = Colors.GetColor(ColorName.color_information_light);
                config.TextColor = Colors.GetColor(ColorName.color_primary_90);
            });
        });
    }

    public ICommand Command { get; }

    public string ButtonText { get; } = "See info";
}