using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField.Dictation;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField;

public partial class MultiLineInputField : IDictationConsumerDelegate
{
    private CancellationTokenSource? m_cancellationTokenSource;
    
    private bool m_isDictationActive;
    private bool m_isDictationAnimationActive;

    private async Task ToggleDictation()
    {
        if (!DUI.IsExperimentalFeatureEnabled(DUI.ExperimentalFeatures.DictationInTextFields)) return;
        if (DUI.StartDictationDelegate is null) return;
        
        if (m_isDictationActive)
        {
            await StopDictation();
            return;
        }
        
        if (m_isDictationAnimationActive) return;
        
        _ = StartDictation();
        
        m_isDictationActive = true;

        if (m_toggleDictationButton is null) return;
        
        var expandedBorderWidth = Sizes.GetSize(SizeName.stroke_large) * 1.2;
        
        m_toggleDictationButton.BorderWidth = expandedBorderWidth;
        
        m_isDictationAnimationActive = true;
        
        await PerformBreathingAnimation(1, 0.4, 2400, 
            Colors.GetColor(ColorName.color_border_button), 
            Colors.GetColor(ColorName.color_border_input_active));
    }

    private async Task StartDictation()
    {
        if (m_isDictationActive) return;

        if (!DUI.IsExperimentalFeatureEnabled(DUI.ExperimentalFeatures.DictationInTextFields)) return;

        if (DUI.StartDictationDelegate is null) return;

        var cancellationTokenSource = new CancellationTokenSource();
        m_cancellationTokenSource = cancellationTokenSource;

        await DUI.StartDictationDelegate(this, m_cancellationTokenSource.Token);
    }

    private async Task StopDictation()
    {
        if (!m_isDictationActive) return;

        if (m_cancellationTokenSource is null)
            throw new InvalidOperationException(
                $"Trying to stop dictation without a {nameof(CancellationTokenSource)} set.");

        if (m_cancellationTokenSource.IsCancellationRequested) return;
        
        this.CancelAnimations();
        
        m_isDictationActive = false;
        
        _ = AnimateDictationButtonBorderWidth(Sizes.GetSize(SizeName.stroke_medium), 300);

        await m_cancellationTokenSource.CancelAsync();
    }
    
    public void UpdateTextWithDictationResult(string textToAdd)
    {
        if (Text.Length > 1)
        {
            var lastLetterOfText = Text[Text.Length - 1];
            if (!char.IsWhiteSpace(lastLetterOfText) && !string.IsNullOrWhiteSpace(textToAdd))
            {
                Text += " ";
            }
        }
        Text += textToAdd;
    }
}