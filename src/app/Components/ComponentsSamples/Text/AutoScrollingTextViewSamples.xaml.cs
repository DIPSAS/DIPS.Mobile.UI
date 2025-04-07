using System.Text;
using System.Windows.Input;

namespace Components.ComponentsSamples.Text;

public partial class AutoScrollingTextViewSamples
{
    private string m_transcriptionText;
    private bool m_isToggled;

    public AutoScrollingTextViewSamples()
    {
        BindingContext = this;
        InitializeComponent();
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        _ = RandomTextGeneration();
    }

    private async Task RandomTextGeneration()
    {
        while (Handler is not null)
        {
            await Task.Delay(500);
            
            if(!IsToggled)
                continue;
            
            var words = RandomText.Split(' ');
            var random = new Random();
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < random.Next(1, 4); i++)
            {
                var randomWord = words[random.Next(words.Length)];
                stringBuilder.Append(randomWord + " ");
            }

            TranscriptionText += stringBuilder.ToString().Trim();
        }
    }

    public string TranscriptionText
    {
        get => m_transcriptionText;
        set
        {
            if (value == m_transcriptionText)
            {
                return;
            }

            m_transcriptionText = value;
            OnPropertyChanged();
        }
    }

    public ICommand SampleTextCommand => new Command(() =>
    {
        TranscriptionText = RandomText;
    });
    
    private const string RandomText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

    public bool IsToggled
    {
        get => m_isToggled;
        set
        {
            if (value == m_isToggled)
            {
                return;
            }

            m_isToggled = value;
            OnPropertyChanged();
        }
    }
}