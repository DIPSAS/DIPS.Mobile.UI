using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Selection;

public partial class RadioButton
{
    public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(
        nameof(IsSelected),
        typeof(bool),
        typeof(RadioButton), defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, _, _) => ((RadioButton)bindable).OnIsSelectedChanged());

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }


    public static readonly BindableProperty SelectedCommandProperty = BindableProperty.Create(
        nameof(SelectedCommand),
        typeof(ICommand),
        typeof(RadioButton));

    public ICommand? SelectedCommand
    {
        get => (ICommand)GetValue(SelectedCommandProperty);
        set => SetValue(SelectedCommandProperty, value);
    }


    public static readonly BindableProperty SelectedCommandParameterProperty = BindableProperty.Create(
        nameof(SelectedCommandParameter),
        typeof(object),
        typeof(RadioButton));

    public object? SelectedCommandParameter
    {
        get => GetValue(SelectedCommandParameterProperty);
        set => SetValue(SelectedCommandParameterProperty, value);
    }

    public event EventHandler<SelectionChangedEventArgs>? SelectionChanged;
}