using System.Windows.Input;
using DIPS.Mobile.UI.Components.Selection;
using SelectionChangedEventArgs = Microsoft.Maui.Controls.SelectionChangedEventArgs;

namespace DIPS.Mobile.UI.Components.ListItems.Extensions;

public partial class CheckmarkListItem 
{
    public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(
        nameof(IsSelected),
        typeof(bool),
        typeof(CheckmarkListItem), defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, _, _) => ((CheckmarkListItem)bindable).OnIsSelectedChanged());

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }


    public static readonly BindableProperty SelectedCommandProperty = BindableProperty.Create(
        nameof(SelectedCommand),
        typeof(ICommand),
        typeof(CheckmarkListItem));

    public ICommand? SelectedCommand
    {
        get => (ICommand)GetValue(SelectedCommandProperty);
        set => SetValue(SelectedCommandProperty, value);
    }


    public static readonly BindableProperty SelectedCommandParameterProperty = BindableProperty.Create(
        nameof(SelectedCommandParameter),
        typeof(object),
        typeof(CheckmarkListItem));

    public object? SelectedCommandParameter
    {
        get => GetValue(SelectedCommandParameterProperty);
        set => SetValue(SelectedCommandParameterProperty, value);
    }

    public event EventHandler<Selection.SelectionChangedEventArgs>? SelectionChanged;
}