using DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField.Dictation;

namespace DIPS.Mobile.UI.Components.TextFields.Dictation;

/// <summary>
/// Wraps the field that receives dictation so every result is delivered on the main thread, and results that
/// arrive after the session was superseded or stopped are dropped. The app speech engine may stream results
/// from a background thread.
/// </summary>
internal sealed class MainThreadDictationConsumer : IDictationConsumerDelegate
{
    private readonly IDictationConsumerDelegate m_field;
    private readonly CancellationToken m_sessionToken;

    public MainThreadDictationConsumer(IDictationConsumerDelegate field, CancellationToken sessionToken)
    {
        ArgumentNullException.ThrowIfNull(field);

        m_field = field;
        m_sessionToken = sessionToken;
    }

    public void UpdateTextWithDictationResult(string textToAdd)
    {
        if (m_sessionToken.IsCancellationRequested)
            return;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (m_sessionToken.IsCancellationRequested)
                return;

            m_field.UpdateTextWithDictationResult(textToAdd);
        });
    }
}
