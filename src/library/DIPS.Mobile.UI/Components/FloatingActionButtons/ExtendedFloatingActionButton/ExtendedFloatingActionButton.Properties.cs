using System.Windows.Input;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.FloatingActionButtons.ExtendedFloatingActionButton;

public partial class ExtendedFloatingActionButton
{
    /// <summary>
    /// Sets a title that will be beside the button
    /// </summary>s
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Sets the icon of the ImageButton
    /// </summary>
    public ImageSource Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Set rotation of the icon
    /// </summary>
    public float IconRotation
    {
        get => (float)GetValue(IconRotationProperty);
        set => SetValue(IconRotationProperty, value);
    }
    
    /// <summary>
    /// Command to be executed by the button
    /// </summary>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public Color ButtonBackgroundColor
    {
        get => (Color)GetValue(ButtonBackgroundColorProperty);
        set => SetValue(ButtonBackgroundColorProperty, value);
    }

    public static readonly BindableProperty IconRotationProperty = BindableProperty.Create(
        nameof(IconRotation),
        typeof(float),
        typeof(ExtendedFloatingActionButton));
    
    public static readonly BindableProperty ButtonBackgroundColorProperty = BindableProperty.Create(
        nameof(ButtonBackgroundColor),
        typeof(Color),
        typeof(ExtendedFloatingActionButton),
        defaultValue:Colors.GetColor(ColorName.color_primary_90));
    
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(ExtendedFloatingActionButton));
    
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(ExtendedFloatingActionButton));
    
    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon),
        typeof(ImageSource),
        typeof(ExtendedFloatingActionButton));
}