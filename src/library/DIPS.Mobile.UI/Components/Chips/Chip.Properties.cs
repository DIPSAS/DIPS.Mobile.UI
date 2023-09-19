using System.Windows.Input;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Chips;

public partial class Chip
{
    /// <summary>
    /// The color of the chip.
    /// </summary>
    public Color? Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }
    
    /// <summary>
    /// The radius of the corners of the chip.
    /// </summary>
    public int CornerRadius
    {
        get => (int)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    
    /// <summary>
    /// The width of the border of the chip.
    /// </summary>
    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    /// <summary>
    /// The color of the borders of the chip.
    /// </summary>
    public Color? BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    public static readonly BindableProperty CloseButtonColorProperty = BindableProperty.Create(
        nameof(CloseButtonColor),
        typeof(Color),
        typeof(Chip));

    /// <summary>
    /// The color of the close button that people tap to close the chip.
    /// </summary>
    public Color? CloseButtonColor
    {
        get => (Color)GetValue(CloseButtonColorProperty);
        set => SetValue(CloseButtonColorProperty, value);
    }

    /// <summary>
    /// Sets the text inside of the chip that people will see
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// The command to execute when people tap the chip.
    /// </summary>
    public ICommand? Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// The command parameter to pass to the command when it executes when people tap the chip.
    /// </summary>
    public object? CommandParameter
    {
        get => (object)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
    
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius),
        typeof(int),
        typeof(Chip));
    
    public static readonly BindableProperty HasCloseButtonProperty = BindableProperty.Create(
        nameof(HasCloseButton),
        typeof(bool),
        typeof(Chip));

    /// <summary>
    /// Determines if people should be able to interact with close button to the right of the <see cref="Title"/>.
    /// </summary>
    public bool HasCloseButton
    {
        get => (bool)GetValue(HasCloseButtonProperty);
        set => SetValue(HasCloseButtonProperty, value);
    }

    public static readonly BindableProperty CloseCommandProperty = BindableProperty.Create(
        nameof(CloseCommand),
        typeof(ICommand),
        typeof(Chip));

    /// <summary>
    /// The command to be executed when people tap the close button.
    /// </summary>
    public ICommand? CloseCommand
    {
        get => (ICommand)GetValue(CloseCommandProperty);
        set => SetValue(CloseCommandProperty, value);
    }

    public static readonly BindableProperty CloseCommandParameterProperty = BindableProperty.Create(
        nameof(CloseCommandParameter),
        typeof(object),
        typeof(Chip));

    /// <summary>
    /// The parameter to be passed to the <see cref="CloseCommand"/> when people tap the close button.
    /// </summary>
    public object? CloseCommandParameter
    {
        get => (object)GetValue(CloseCommandParameterProperty);
        set => SetValue(CloseCommandParameterProperty, value);
    }

    /// <summary>
    /// The event to be invoked when the chip is tapped.
    /// </summary>
    public event EventHandler? Tapped;

    /// <summary>
    /// The event to invoked when the chips close button is tapped.
    /// </summary>
    public event EventHandler? CloseTapped;

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        nameof(CommandParameter),
        typeof(object),
        typeof(Chip));

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(Chip));

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(Chip));
    
    public static readonly BindableProperty ColorProperty = BindableProperty.Create(
        nameof(Color),
        typeof(Color),
        typeof(Chip));

    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
        nameof(BorderColor),
        typeof(Color),
        typeof(Chip));

    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(
        nameof(BorderWidth),
        typeof(double),
        typeof(Chip));

}