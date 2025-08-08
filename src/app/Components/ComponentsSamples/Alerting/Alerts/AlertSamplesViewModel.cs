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
    private string m_title = "A Short Title";

    public AlertSamplesViewModel()
    {
        Command = new Command<string>(s=>
        {
            SystemMessageService.Display(config =>
            {
                config.Text = s;
                config.BackgroundColor = Colors.GetColor(ColorName.color_surface_information);
                config.TextColor = Colors.GetColor(ColorName.color_text_action);
            });
        });

        ToggleButtonAlignmentCommand = new Command(() =>
            ButtonAlignment = ButtonAlignment is AlertView.ButtonAlignmentType.Inline
                ? AlertView.ButtonAlignmentType.Underlying
                : AlertView.ButtonAlignmentType.Inline);

        SwitchTitleCommand = new Command(() =>
            Title = Title == "A Short Title" ? "A Very Long Title That Should Move The Button" : "A Short Title");
    }

    public ICommand Command { get; }
    public ICommand SwitchTitleCommand { get; }

    public string Title
    {
        get => m_title;
        private set => RaiseWhenSet(ref m_title, value);
    }

    public ICommand ToggleButtonAlignmentCommand { get; }
    
    public string ButtonText { get; } = "See info";

    public AlertView.ButtonAlignmentType ButtonAlignment
    {
        get => m_buttonAlignment;
        set => RaiseWhenSet(ref m_buttonAlignment, value);
    }

    public ICommand AnimateCommand => new Command(AlertViewService.TriggerAnimation);
}