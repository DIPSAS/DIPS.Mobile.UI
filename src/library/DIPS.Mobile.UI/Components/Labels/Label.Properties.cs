using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Labels;

public partial class Label
{
    public static readonly BindableProperty IsEllipsizedProperty = BindableProperty.Create(
        nameof(IsEllipsized),
        typeof(bool),
        typeof(Label),
        defaultBindingMode: BindingMode.TwoWay);

    public bool IsEllipsized
    {
        get => (bool)GetValue(IsEllipsizedProperty);
        set => SetValue(IsEllipsizedProperty, value);
    }
}