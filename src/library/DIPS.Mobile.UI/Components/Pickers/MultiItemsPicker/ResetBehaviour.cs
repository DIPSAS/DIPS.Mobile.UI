using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Pickers.MultiItemsPicker;

public class ResetBehaviour : BindableObject
{
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(ResetBehaviour));

    /// <summary>
    /// Custom command when the reset button is pressed. Set this if you need to set a custom value when the list is reset.
    /// If no command has been set, it will simply clear all selected items.
    /// </summary>
    public ICommand? Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
}