using System.ComponentModel;
using System.Windows.Input;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton.NavigationMenuButton;

internal partial class NavigationMenuButton
{
    /// <summary>
    /// Sets the icon
    /// </summary>
    [TypeConverter(nameof(ImageSourceConverter))]
    public ImageSource Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Set the button's background color
    /// </summary>
    public Color ButtonBackgroundColor
    {
        get => (Color)GetValue(ButtonBackgroundColorProperty);
        set => SetValue(ButtonBackgroundColorProperty, value);
    }

    /// <summary>
    /// Set the icon's rotation
    /// </summary>
    public float IconRotation
    {
        get => (float)GetValue(IconRotationProperty);
        set => SetValue(IconRotationProperty, value);
    }

    /// <summary>
    /// Set the command
    /// </summary>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// Set the badge count
    /// <remarks>If the badge count is zero, the badge will not be visible</remarks>
    /// </summary>
    public int BadgeCount
    {
        get => (int)GetValue(BadgeCountProperty);
        set => SetValue(BadgeCountProperty, value);
    }

    /// <summary>
    /// Set the color of the badge
    /// </summary>
    public Color BadgeColor
    {
        get => (Color)GetValue(BadgeColorProperty);
        set => SetValue(BadgeColorProperty, value);
    }

    public static readonly BindableProperty BadgeColorProperty = BindableProperty.Create(
        nameof(BadgeColor),
        typeof(Color),
        typeof(NavigationMenuButton),
        defaultValue: Colors.GetColor(ColorName.color_information_dark));
    
    public static readonly BindableProperty BadgeCountProperty = BindableProperty.Create(
        nameof(BadgeCount),
        typeof(int),
        typeof(NavigationMenuButton),
        propertyChanged: OnBadgeCountChanged);
    
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(NavigationMenuButton));
    
    public static readonly BindableProperty IconRotationProperty = BindableProperty.Create(
        nameof(IconRotation),
        typeof(float),
        typeof(NavigationMenuButton));

    public static readonly BindableProperty ButtonBackgroundColorProperty = BindableProperty.Create(
        nameof(ButtonBackgroundColor),
        typeof(Color),
        typeof(NavigationMenuButton),
        defaultValue:Colors.GetColor(ColorName.color_primary_90));
    
    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon),
        typeof(ImageSource),
        typeof(NavigationMenuButton));
}