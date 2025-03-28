using DIPS.Mobile.UI.Components.VoiceVisualizer;
using DIPS.Mobile.UI.Resources.Colors;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Enum = System.Enum;

namespace Components.ComponentsSamples.AmplitudeView;

public partial class AmplitudeViewSamples
{
    private DIPS.Mobile.UI.Components.VoiceVisualizer.AmplitudeView m_amplitudeView;
    private Controller m_controller;
    private string? m_placeholderAmplitudeColorString;
    private string? m_amplitudeColorString;
    private string m_fadeColorString;

    public AmplitudeViewSamples()
    {
        ColorList = new List<string>(DIPS.Mobile.UI.Extensions.Enum.ToList<ColorName>().Select(c => c.ToString()));
        
        InitializeComponent();
    }

    public List<string> ColorList { get; set; }

    public string? AmplitudeColorString
    {
        get => m_amplitudeColorString;
        set
        {
            if (value == m_amplitudeColorString)
            {
                return;
            }

            m_amplitudeColorString = value;
            OnPropertyChanged();
        }
    }

    public string? PlaceholderAmplitudeColorString
    {
        get => m_placeholderAmplitudeColorString;
        set
        {
            if (value == m_placeholderAmplitudeColorString)
            {
                return;
            }

            m_placeholderAmplitudeColorString = value;
            OnPropertyChanged();
        }
    }

    public string FadeColorString
    {
        get => m_fadeColorString;
        set
        {
            if (value == m_fadeColorString)
            {
                return;
            }

            m_fadeColorString = value;
            OnPropertyChanged();
        }
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        if (Handler is null)
            return;

        m_controller = new Controller();
        
        m_amplitudeView = new DIPS.Mobile.UI.Components.VoiceVisualizer.AmplitudeView
        {
            Controller = m_controller,
            FadeColor = BackgroundColor,
            HeightRequest = HeightStepper.Value
        };
        Container.Add(m_amplitudeView);
        FpsStepper.Value = m_amplitudeView.FramesPerSecond;
        SampleRateStepper.Value = m_amplitudeView.SampleRate;
        TimerSwitch.IsToggled = m_amplitudeView.HasTimer;
    }

    private void RefreshAmplitudeView()
    {
        Container.Remove(m_amplitudeView);
        m_amplitudeView.DisconnectHandlers();
        
        m_amplitudeView = new DIPS.Mobile.UI.Components.VoiceVisualizer.AmplitudeView
        {
            FadeColor = BackgroundColor,
            Controller = m_controller,
            FramesPerSecond = (int)FpsStepper.Value,
            SampleRate = (int)SampleRateStepper.Value,
            HasTimer = TimerSwitch.IsToggled,
            HeightRequest = HeightStepper.Value
        };

        if (Enum.TryParse<ColorName>(AmplitudeColorString, out var amplitudeColor))
        {
            m_amplitudeView.AmplitudeColor = Colors.GetColor(amplitudeColor);            
        }

        if (Enum.TryParse<ColorName>(PlaceholderAmplitudeColorString, out var placeholderAmplitudeColor))
        {
            m_amplitudeView.PlaceholderAmplitudeColor = Colors.GetColor(placeholderAmplitudeColor);
        } 
        
        if (Enum.TryParse<ColorName>(FadeColorString, out var fadeColor))
        {
            m_amplitudeView.FadeColor = Colors.GetColor(fadeColor);
        }
        
        Container.Add(m_amplitudeView);
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        RefreshAmplitudeView();
        RefreshPausePlayButton();
    }

    private void PausePlay(object? sender, EventArgs e)
    {
        m_controller.IsRunning = !m_controller.IsRunning;
        RefreshPausePlayButton();
    }

    private void RefreshPausePlayButton()
    {
        PausePlayButton.Title = m_controller.IsRunning ? "Pause" : "Play";
    }
}

public class Controller : AmplitudeViewController
{
    public override float GetNextAmplitude()
    {
        return (float)new Random().NextDouble();
    }
}