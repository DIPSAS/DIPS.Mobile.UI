namespace DIPS.Mobile.UI.Components.VoiceVisualizer;

public abstract class AmplitudeViewController
{
    private bool m_isRunning;
    
    /// <summary>
    /// Get the next amplitude to display
    /// </summary>
    /// <remarks>0 is minimum height, 1 is maximum height</remarks>
    public abstract float GetNextAmplitude();
    
    public bool IsRunning
    {
        get => m_isRunning;
        set
        {
            m_isRunning = value;
            OnIsPlayingChanged?.Invoke(value);
        }
    }

    /// <summary>
    /// How many milliseconds that has elapsed
    /// </summary>
    public float ElapsedMilliseconds { get; internal set; }
    
    internal event Action<bool>? OnIsPlayingChanged;
}