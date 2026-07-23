using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField.Dictation;
using DIPS.Mobile.UI.Internal.Logging;

namespace DIPS.Mobile.UI.Components.TextFields.Dictation;

/// <summary>
/// Owns the single active dictation session. All members must be used from the main thread.
/// </summary>
internal sealed class DictationSessionCoordinator
{
    public static DictationSessionCoordinator Current { get; } = new();

    private CancellationTokenSource? m_sessionCancellation;
    private Task<StartDictationResult>? m_activeRunningSession;
    private IDictationConsumerDelegate? m_activeConsumer;

    private DictationSessionCoordinator()
    {
    }

    /// <summary>The field that dictation results are routed to, or null when no field is targeted.</summary>
    public IDictationConsumerDelegate? CurrentConsumer { get; private set; }

    /// <summary>True while a session is running and has not been asked to stop.</summary>
    public bool IsSessionActive => m_sessionCancellation is { IsCancellationRequested: false };

    /// <summary>Raised on the main thread whenever <see cref="CurrentConsumer"/> changes.</summary>
    public event EventHandler? CurrentConsumerChanged;

    /// <summary>
    /// Starts dictation into <paramref name="consumer"/>, stopping any previous session first.
    /// </summary>
    /// <param name="consumer">The field that receives recognized text for this session.</param>
    /// <returns>
    /// A task that completes when the session ends. Its result is a clean <see cref="StartDictationResult"/> on a
    /// normal stop, or an error result when the engine reports a failure or dictation has not been configured.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="consumer"/> is null.</exception>
    /// <remarks>Must be called on the main thread.</remarks>
    public async Task<StartDictationResult> StartSession(IDictationConsumerDelegate consumer)
    {
        ArgumentNullException.ThrowIfNull(consumer);

        if (DUI.StartDictationDelegate is null)
            return new StartDictationResult("Dictation has not been configured.");

        await StopPreviousSessionAndWait();

        var sessionCancellation = new CancellationTokenSource();
        m_sessionCancellation = sessionCancellation;
        m_activeConsumer = consumer;
        
        SetCurrentConsumer(consumer);

        var mainThreadConsumer = new MainThreadDictationConsumer(consumer, sessionCancellation.Token);

        var runningSession = RunDictationSessionUntilStopped(mainThreadConsumer, sessionCancellation);
        
        m_activeRunningSession = runningSession;

        var result = await runningSession;
        
        return result;
    }

    /// <summary>Requests the active session to stop. The awaited <see cref="StartSession"/> then completes.</summary>
    public void StopSession()
    {
        var sessionCancellation = m_sessionCancellation;
        if (sessionCancellation is null)
            return;

        if (!sessionCancellation.IsCancellationRequested)
            sessionCancellation.Cancel();
    }

    /// <summary>
    /// Records that a field gained focus (Android). Moving to a different field stops a session that was running
    /// on the previous field.
    /// </summary>
    public void NotifyFieldFocused(IDictationConsumerDelegate consumer)
    {
        ArgumentNullException.ThrowIfNull(consumer);

        var sessionBelongsToAnotherField = m_activeConsumer is not null && !ReferenceEquals(m_activeConsumer, consumer);
        
        if (sessionBelongsToAnotherField)
            StopSession();

        SetCurrentConsumer(consumer);
    }

    /// <summary>
    /// Records that a field lost focus or went away (Android). Stops the session when the blurred field was the
    /// one dictating, and clears the target when it was the current target.
    /// </summary>
    public void NotifyFieldBlurred(IDictationConsumerDelegate consumer)
    {
        ArgumentNullException.ThrowIfNull(consumer);

        if (ReferenceEquals(m_activeConsumer, consumer))
            StopSession();

        if (ReferenceEquals(CurrentConsumer, consumer))
            SetCurrentConsumer(null);
    }

    private async Task StopPreviousSessionAndWait()
    {
        var previousRunningSession = m_activeRunningSession;
        
        StopSession();

        if (previousRunningSession is null)
            return;

        try
        {
            await previousRunningSession;
        }
        catch
        {
            // The previous run reports its own failure to its own caller. We just need the task to complete here,
        }
    }

    private async Task<StartDictationResult> RunDictationSessionUntilStopped(
        IDictationConsumerDelegate mainThreadConsumer, CancellationTokenSource sessionCancellation)
    {
        try
        {
            return await DUI.StartDictationDelegate!(mainThreadConsumer, sessionCancellation.Token);
        }
        catch (OperationCanceledException)
        {
            return new StartDictationResult();
        }
        catch (Exception error)
        {
            DUILogService.LogError<DictationSessionCoordinator>($"Dictation session failed: {error}");
            return new StartDictationResult(error.Message);
        }
        finally
        {
            var thisSessionIsStillCurrent = ReferenceEquals(m_sessionCancellation, sessionCancellation);
            if (thisSessionIsStillCurrent)
            {
                m_sessionCancellation = null;
                m_activeConsumer = null;
                m_activeRunningSession = null;
            }

            sessionCancellation.Dispose();
        }
    }

    private void SetCurrentConsumer(IDictationConsumerDelegate? consumer)
    {
        if (ReferenceEquals(CurrentConsumer, consumer))
            return;

        CurrentConsumer = consumer;
        
        RaiseCurrentConsumerChanged();
    }

    private void RaiseCurrentConsumerChanged()
    {
        if (MainThread.IsMainThread)
        {
            CurrentConsumerChanged?.Invoke(this, EventArgs.Empty);
            return;
        }

        MainThread.BeginInvokeOnMainThread(() => CurrentConsumerChanged?.Invoke(this, EventArgs.Empty));
    }
}
