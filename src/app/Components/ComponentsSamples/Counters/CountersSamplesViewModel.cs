using DIPS.Mobile.UI.Components.Counters;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Counters;

public class CountersSamplesViewModel : ViewModel
{
    private int m_value = 5;
    private int m_secondaryValue;
    private CounterDisplayMode m_mode = CounterDisplayMode.Single;
    private bool m_isUrgent;
    private bool m_isSecondaryUrgent;
    private bool m_isError;
    private bool m_isSecondaryError;
    private bool m_isFlipped;

    public List<CounterDisplayMode> Modes { get; } =
    [
        CounterDisplayMode.Single,
        CounterDisplayMode.Double,
        CounterDisplayMode.Auto
    ];
    
    public int Value
    {
        get => m_value;
        set => RaiseWhenSet(ref m_value, value);
    }

    public int SecondaryValue
    {
        get => m_secondaryValue;
        set => RaiseWhenSet(ref m_secondaryValue, value);
    }

    public bool IsUrgent
    {
        get => m_isUrgent;
        set => RaiseWhenSet(ref m_isUrgent, value);
    }

    public bool IsSecondaryUrgent
    {
        get => m_isSecondaryUrgent;
        set => RaiseWhenSet(ref m_isSecondaryUrgent, value);
    }

    public bool IsError
    {
        get => m_isError;
        set => RaiseWhenSet(ref m_isError, value);
    }

    public bool IsSecondaryError
    {
        get => m_isSecondaryError;
        set => RaiseWhenSet(ref m_isSecondaryError, value);
    }

    public CounterDisplayMode Mode
    {
        get => m_mode;
        set => RaiseWhenSet(ref m_mode, value);
    }

    public bool IsFlipped
    {
        get => m_isFlipped;
        set => RaiseWhenSet(ref m_isFlipped, value);
    }
}