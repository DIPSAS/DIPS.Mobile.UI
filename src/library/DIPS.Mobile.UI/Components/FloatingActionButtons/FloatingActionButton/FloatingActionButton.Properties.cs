using System.Windows.Input;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingActionButton;

public partial class FloatingActionButton
{
    /// <summary>
    /// Sets the icon
    /// </summary>
    public ImageSource Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public Color ButtonBackgroundColor
    {
        get => (Color)GetValue(ButtonBackgroundColorProperty);
        set => SetValue(ButtonBackgroundColorProperty, value);
    }

    public float IconRotation
    {
        get => (float)GetValue(IconRotationProperty);
        set => SetValue(IconRotationProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public int BadgeCount
    {
        get => (int)GetValue(BadgeCountProperty);
        set => SetValue(BadgeCountProperty, value);
    }
    
    public static readonly BindableProperty BadgeCountProperty = BindableProperty.Create(
        nameof(BadgeCount),
        typeof(int),
        typeof(FloatingActionButton),
        propertyChanged: OnBadgeCountChanged);
    
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(FloatingActionButton));
    
    public static readonly BindableProperty IconRotationProperty = BindableProperty.Create(
        nameof(IconRotation),
        typeof(float),
        typeof(FloatingActionButton));

    public static readonly BindableProperty ButtonBackgroundColorProperty = BindableProperty.Create(
        nameof(ButtonBackgroundColor),
        typeof(Color),
        typeof(FloatingActionButton),
        defaultValue:Colors.GetColor(ColorName.color_primary_90));
    
    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon),
        typeof(ImageSource),
        typeof(FloatingActionButton));
}