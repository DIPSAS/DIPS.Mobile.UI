using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Alert;
using DIPS.Mobile.UI.Components.Alerting.SystemMessage;
using DIPS.Mobile.UI.MVVM;
using DIPS.Mobile.UI.Resources.Colors;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace Components.ComponentsSamples.Alerting.Alerts;

public class AlertSamplesViewModel : ViewModel
{
    private AlertView.ButtonAlignmentType m_buttonAlignment = AlertView.ButtonAlignmentType.Inline;

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

        ToggleButtonAlignmentCommand = new Command(() =>
            ButtonAlignment = ButtonAlignment is AlertView.ButtonAlignmentType.Inline
                ? AlertView.ButtonAlignmentType.Underlying
                : AlertView.ButtonAlignmentType.Inline);
    }

    public ICommand Command { get; }

    public ICommand ToggleButtonAlignmentCommand { get; }
    
    public string ButtonText { get; } = "See info";

    public AlertView.ButtonAlignmentType ButtonAlignment
    {
        get => m_buttonAlignment;
        set => RaiseWhenSet(ref m_buttonAlignment, value);
    }
}