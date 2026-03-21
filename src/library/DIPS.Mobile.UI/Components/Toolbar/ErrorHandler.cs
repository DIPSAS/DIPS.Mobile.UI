using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Toolbar;

/// <summary>
/// Represents an error state for a <see cref="ToolbarTaskButton"/>.
/// When <see cref="HasError"/> is set to true, the button is replaced with an error icon.
/// </summary>
[ContentProperty(nameof(HasError))]
public class ErrorHandler : BindableObject
{
    /// <summary>
    /// <see cref="HasError"/>
    /// </summary>
    public static readonly BindableProperty HasErrorProperty = BindableProperty.Create(
        nameof(HasError),
        typeof(bool),
        typeof(ErrorHandler),
        defaultValue: false);

    /// <summary>
    /// <see cref="ErrorTappedCommand"/>
    /// </summary>
    public static readonly BindableProperty ErrorTappedCommandProperty = BindableProperty.Create(
        nameof(ErrorTappedCommand),
        typeof(ICommand),
        typeof(ErrorHandler));

    /// <summary>
    /// Indicates whether an error has occurred for this task.
    /// When true, the toolbar button is replaced with an error icon matching the AlertView error style.
    /// Set to false after the user has acknowledged the error to restore the button.
    /// </summary>
    public bool HasError
    {
        get => (bool)GetValue(HasErrorProperty);
        set => SetValue(HasErrorProperty, value);
    }

    /// <summary>
    /// A command invoked when the user taps the error icon.
    /// Use this to present an error dialog explaining what went wrong.
    /// After the user dismisses the dialog, set <see cref="HasError"/> to false so they can retry.
    /// </summary>
    public ICommand? ErrorTappedCommand
    {
        get => (ICommand?)GetValue(ErrorTappedCommandProperty);
        set => SetValue(ErrorTappedCommandProperty, value);
    }
}
