using System.Windows.Input;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingNavigationButton.ExtendedNavigationMenuButton;

public partial class ExtendedNavigationMenuButton
{
    /// <summary>
    /// Sets a title that will be beside the button
    /// </summary>
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
    
    public int BadgeCount
    {
        get => (int)GetValue(BadgeCountProperty);
        set => SetValue(BadgeCountProperty, value);
    }

    public Color BadgeColor
    {
        get => (Color)GetValue(BadgeColorProperty);
        set => SetValue(BadgeColorProperty, value);
    }
    
    public bool Disabled
    {
        get => (bool)GetValue(DisabledProperty);
        set => SetValue(DisabledProperty, value);
    }
    
    public static readonly BindableProperty DisabledProperty = BindableProperty.Create(
        nameof(Disabled),
        typeof(bool),
        typeof(ExtendedNavigationMenuButton),
        propertyChanged: OnDisabledChanged);
    
    public static readonly BindableProperty BadgeColorProperty = BindableProperty.Create(
        nameof(BadgeColor),
        typeof(Color),
        typeof(ExtendedNavigationMenuButton),
        defaultValue: Colors.GetColor(ColorName.color_information_dark));
    
    public static readonly BindableProperty BadgeCountProperty = BindableProperty.Create(
        nameof(BadgeCount),
        typeof(int),
        typeof(ExtendedNavigationMenuButton));

    public static readonly BindableProperty IconRotationProperty = BindableProperty.Create(
        nameof(IconRotation),
        typeof(float),
        typeof(ExtendedNavigationMenuButton));
    
    public static readonly BindableProperty ButtonBackgroundColorProperty = BindableProperty.Create(
        nameof(ButtonBackgroundColor),
        typeof(Color),
        typeof(ExtendedNavigationMenuButton),
        defaultValue:Colors.GetColor(ColorName.color_primary_90));
    
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(ExtendedNavigationMenuButton));
    
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(ExtendedNavigationMenuButton));
    
    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon),
        typeof(ImageSource),
        typeof(ExtendedNavigationMenuButton));
}