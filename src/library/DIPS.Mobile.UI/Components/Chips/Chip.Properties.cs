using System.ComponentModel;
using System.Windows.Input;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Chips;

public partial class Chip
{
    /// <summary>
    /// Set an icon that will be displayed on the left side of the chip.
    /// <remarks>Will be hidden if <see cref="IsToggleable"/> is true</remarks>
    /// </summary>
    [TypeConverter(nameof(ImageSourceConverter))]
    public ImageSource? CustomIcon
    {
        get => (ImageSource)GetValue(CustomIconProperty);
        set => SetValue(CustomIconProperty, value);
    }

    /// <summary>
    /// Sets the color of the <see cref="CustomIcon"/>.
    /// </summary>
    public Color CustomIconTintColor
    {
        get => (Color)GetValue(CustomIconTintColorProperty);
        set => SetValue(CustomIconTintColorProperty, value);
    }
    
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
        typeof(Chip),
        // TODO: Lisa
        Colors.GetColor(ColorName.color_neutral_90));

    /// <summary>
    /// The color of the close button that people tap to close the chip.
    /// </summary>
    public Color? CloseButtonColor
    {
        get => (Color?)GetValue(CloseButtonColorProperty);
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
    /// Sets the text color of the chip
    /// </summary>
    public Color? TitleColor
    {
        get => (Color?)GetValue(TitleColorProperty);
        set => SetValue(TitleColorProperty, value);
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
    
    public static readonly BindableProperty IsCloseableProperty = BindableProperty.Create(
        nameof(IsCloseable),
        typeof(bool),
        typeof(Chip));

    /// <summary>
    /// Determines if people should be able to interact with close button to the right of the <see cref="Title"/>.
    /// </summary>
    public bool IsCloseable
    {
        get => (bool)GetValue(IsCloseableProperty);
        set => SetValue(IsCloseableProperty, value);
    }
    
    public static readonly BindableProperty CustomIconTintColorProperty = BindableProperty.Create(
        nameof(CustomIconTintColor),
        typeof(Color),
        typeof(Chip),
        Colors.GetColor(ColorName.color_icon_default));

    public static readonly BindableProperty CustomIconProperty = BindableProperty.Create(
        nameof(CustomIcon),
        typeof(ImageSource),
        typeof(Chip));
    
    public static readonly BindableProperty CloseCommandProperty = BindableProperty.Create(
        nameof(CloseCommand),
        typeof(ICommand),
        typeof(Chip));
    
    /// <summary>
    /// Determines if this is a chip that can be toggled.
    /// When toggled on, a check icon appears left of the <see cref="Title"/>, and the <see cref="Color"/> and
    /// <see cref="TitleColor"/> changes.
    /// </summary>
    public bool IsToggleable 
    {
        get => (bool)GetValue(IsToggleableProperty);
        set => SetValue(IsToggleableProperty, value);
    }

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
    /// Indicates whether chip that is <see cref="IsToggleable"/> is toggled or not.
    /// Default <see cref="BindingMode"/> is TwoWay.
    /// </summary>
    public bool IsToggled
    {
        get => (bool)GetValue(IsToggledProperty);
        set => SetValue(IsToggledProperty, value);
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
    
    public static readonly BindableProperty TitleColorProperty = BindableProperty.Create(
        nameof(TitleColor),
        typeof(Color),
        typeof(Chip),
        Colors.GetColor(ColorName.color_neutral_90));

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
    
    public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(
        nameof(IsToggled),
        typeof(bool),
        typeof(Chip),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, _, _) => ((Chip)bindable).OnIsToggledChanged());
    
    public static readonly BindableProperty IsToggleableProperty = BindableProperty.Create(
        nameof(IsToggleable),
        typeof(bool),
        typeof(Chip));

}