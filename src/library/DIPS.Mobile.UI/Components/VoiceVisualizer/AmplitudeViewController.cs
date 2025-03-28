namespace DIPS.Mobile.UI.Components.VoiceVisualizer;

public abstract class AmplitudeViewController
{
    private bool m_isPlaying;
    
    /// <summary>
    /// Get the next amplitude to display
    /// </summary>
    /// <remarks>0 is minimum height, 1 is maximum height</remarks>
    public abstract float GetNextAmplitude();
    
    public bool IsPlaying
    {
        get => m_isPlaying;
        set
        {
            m_isPlaying = value;
            OnIsPlayingChanged?.Invoke(value);
        }
    }

    internal event Action<bool>? OnIsPlayingChanged;
}