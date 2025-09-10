namespace DIPS.Mobile.UI.Components.Labels.CheckTruncatedLabel;

public partial class CheckTruncatedLabel
{
    /// <summary>
    /// Determines whether the Label is truncated or not
    /// </summary>
    public bool IsTruncated
    {
        get => (bool)GetValue(IsTruncatedProperty);
        set => SetValue(IsTruncatedProperty, value);
    }
    
    public static readonly BindableProperty IsTruncatedProperty = BindableProperty.Create(
        nameof(IsTruncated),
        typeof(bool),
        typeof(CheckTruncatedLabel),
        defaultBindingMode: BindingMode.OneWayToSource);
}