<<<<<<< HEAD:src/library/DIPS.Mobile.UI/Components/Images/ImageButton/ImageButton.Properties.cs
namespace DIPS.Mobile.UI.Components.Images.ImageButton;
=======
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Images;
>>>>>>> main:src/library/DIPS.Mobile.UI/Components/Images/ImageButton.Properties.cs

public partial class ImageButton
{
    public static readonly BindableProperty TintColorProperty = BindableProperty.Create(
        nameof(TintColor),
        typeof(Color),
        typeof(ImageButton), defaultValueCreator: (_ => Colors.GetColor(ColorName.color_system_black)));

    /// <summary>
    /// Sets the color of the image
    /// </summary>
    public Color TintColor
    {
        get => (Color)GetValue(TintColorProperty);
        set => SetValue(TintColorProperty, value);
    }
}