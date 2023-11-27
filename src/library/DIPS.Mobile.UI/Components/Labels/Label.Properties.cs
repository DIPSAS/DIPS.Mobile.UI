using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Labels;

public partial class Label
{
    public static readonly BindableProperty IsTruncatedProperty = BindableProperty.Create(
        nameof(IsTruncated),
        typeof(bool),
        typeof(Label),
        defaultBindingMode: BindingMode.OneWayToSource);

    public bool IsTruncated
    {
        get => (bool)GetValue(IsTruncatedProperty);
        set => SetValue(IsTruncatedProperty, value);
    }
}