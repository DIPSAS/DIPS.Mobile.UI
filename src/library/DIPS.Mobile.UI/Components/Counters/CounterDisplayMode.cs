namespace DIPS.Mobile.UI.Components.Counters;

public enum CounterDisplayMode
{
    /// <summary>
    ///     Only display the primary value.
    /// </summary>
    Single,
    /// <summary>
    ///     Display both primary and secondary values.
    /// </summary>
    Double,
    /// <summary>
    ///     Display the primary value, and the secondary value only if it is not 0.
    /// </summary>
    Auto
}