using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Components.Gallery;

public class GalleryCustomizer : BindableObject
{
    public static readonly BindableProperty NavigateButtonsBackgroundColorProperty = BindableProperty.Create(
        nameof(NavigateButtonsBackgroundColor),
        typeof(Color),
        typeof(GalleryCustomizer),
        defaultValueCreator: _ => Colors.Black.WithAlpha(.5f));

    public static readonly BindableProperty NavigateButtonsImageTintColorProperty = BindableProperty.Create(
        nameof(NavigateButtonsImageTintColor),
        typeof(Color),
        typeof(GalleryCustomizer),
        Colors.White);

    public static readonly BindableProperty ImageCountBackgroundColorProperty = BindableProperty.Create(
        nameof(ImageCountBackgroundColor),
        typeof(Color),
        typeof(GalleryCustomizer),
        defaultValueCreator: _ => Colors.Black.WithAlpha(.5f));

    public static readonly BindableProperty ImageCountTextColorProperty = BindableProperty.Create(
        nameof(ImageCountTextColor),
        typeof(Color),
        typeof(GalleryCustomizer),
        Colors.White);

    /// <summary>
    /// Sets the color of the text displaying the current image index and image count.
    /// </summary>
    public Color ImageCountTextColor
    {
        get => (Color)GetValue(ImageCountTextColorProperty);
        set => SetValue(ImageCountTextColorProperty, value);
    }   
    
    /// <summary>
    /// Sets the background color of the image count display.
    /// </summary>
    public Color ImageCountBackgroundColor
    {
        get => (Color)GetValue(ImageCountBackgroundColorProperty);
        set => SetValue(ImageCountBackgroundColorProperty, value);
    }
    
    /// <summary>
    /// Sets the tint color of the navigate buttons' icons.
    /// </summary>
    public Color NavigateButtonsImageTintColor
    {
        get => (Color)GetValue(NavigateButtonsImageTintColorProperty);
        set => SetValue(NavigateButtonsImageTintColorProperty, value);
    }
    
    /// <summary>
    /// Sets the background color of the navigation buttons.
    /// </summary>
    public Color NavigateButtonsBackgroundColor
    {
        get => (Color)GetValue(NavigateButtonsBackgroundColorProperty);
        set => SetValue(NavigateButtonsBackgroundColorProperty, value);
    }
}