using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.ListItems.Extensions;

public partial class RadioButtonListItem
{
    public static readonly BindableProperty HasHapticsProperty = BindableProperty.Create(
        nameof(HasHaptics),
        typeof(bool),
        typeof(CheckmarkListItem), defaultValue:true);

    /// <summary>
    /// Determines if the phone should stimulate the sense of touch and motion by the use of vibration when people tap the radio button.
    /// </summary>
    public bool HasHaptics
    {
        get => (bool)GetValue(HasHapticsProperty);
        set => SetValue(HasHapticsProperty, value);
    }
    
    public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(
        nameof(IsSelected),
        typeof(bool),
        typeof(RadioButtonListItem), defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, _, _) => ((RadioButtonListItem)bindable).OnIsSelectedChanged());

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }


    public static readonly BindableProperty SelectedCommandProperty = BindableProperty.Create(
        nameof(SelectedCommand),
        typeof(ICommand),
        typeof(RadioButtonListItem));

    public ICommand? SelectedCommand
    {
        get => (ICommand)GetValue(SelectedCommandProperty);
        set => SetValue(SelectedCommandProperty, value);
    }


    public static readonly BindableProperty SelectedCommandParameterProperty = BindableProperty.Create(
        nameof(SelectedCommandParameter),
        typeof(object),
        typeof(RadioButtonListItem));

    public object? SelectedCommandParameter
    {
        get => GetValue(SelectedCommandParameterProperty);
        set => SetValue(SelectedCommandParameterProperty, value);
    }

    public event EventHandler<Selection.SelectionChangedEventArgs>? SelectionChanged;
}