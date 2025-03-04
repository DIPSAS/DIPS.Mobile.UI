using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
namespace DIPS.Mobile.UI.Components.Images.ImageButton;

public partial class ImageButton
{
    /// <summary>
    /// Sets the color of the image
    /// </summary>
    public Color TintColor
    {
        get => (Color)GetValue(TintColorProperty);
        set => SetValue(TintColorProperty, value);
    }

    /// <summary>
    /// Sets the value to make the hitbox of the image button larger
    /// </summary>
    public Thickness AdditionalHitBoxSize
    {
        get => (Thickness)GetValue(AdditionalHitBoxSizeProperty);
        set => SetValue(AdditionalHitBoxSizeProperty, value);
    }
    
    public static readonly BindableProperty AdditionalHitBoxSizeProperty = BindableProperty.Create(
        nameof(AdditionalHitBoxSize),
        typeof(Thickness),
        typeof(ImageButton));
    
    public static readonly BindableProperty TintColorProperty = BindableProperty.Create(
        nameof(TintColor),
        typeof(Color),
        typeof(ImageButton), defaultValueCreator: (_ => Colors.GetColor(ColorName.color_icon_default)));

}