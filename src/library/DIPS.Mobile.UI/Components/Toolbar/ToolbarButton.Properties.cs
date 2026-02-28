using System.ComponentModel;
using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Toolbar;

public partial class ToolbarButton
{
    /// <summary>
    /// <see cref="Title"/>
    /// </summary>
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(ToolbarButton));

    /// <summary>
    /// <see cref="Icon"/>
    /// </summary>
    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon),
        typeof(ImageSource),
        typeof(ToolbarButton));

    /// <summary>
    /// <see cref="Command"/>
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(ToolbarButton));

    /// <summary>
    /// <see cref="CommandParameter"/>
    /// </summary>
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        nameof(CommandParameter),
        typeof(object),
        typeof(ToolbarButton));

    /// <summary>
    /// <see cref="IsEnabled"/>
    /// </summary>
    public static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(
        nameof(IsEnabled),
        typeof(bool),
        typeof(ToolbarButton),
        defaultValue: true);

    /// <summary>
    /// The title of the toolbar button, used as the accessibility label.
    /// </summary>
    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// The icon to display in the toolbar button.
    /// </summary>
    [TypeConverter(nameof(ImageSourceConverter))]
    public ImageSource? Icon
    {
        get => (ImageSource?)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// The command to execute when the button is tapped.
    /// </summary>
    public ICommand? Command
    {
        get => (ICommand?)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// The parameter to pass to <see cref="Command"/> when the button is tapped.
    /// </summary>
    public object? CommandParameter
    {
        get => (object?)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    /// <summary>
    /// Determines whether the button is enabled.
    /// </summary>
    public bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }
}
