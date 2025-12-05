using System.ComponentModel;
using Microsoft.Maui.Graphics.Converters;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Images.Image;

public partial class Image
{
    public static readonly BindableProperty TintColorProperty = BindableProperty.Create(
        nameof(TintColor),
        typeof(Color),
        typeof(Image));

    /// <summary>
    /// Sets the color of the image
    /// </summary>
    [TypeConverter(typeof(ColorTypeConverter))]
    public Color? TintColor
    {
        get => (Color)GetValue(TintColorProperty);
        set => SetValue(TintColorProperty, value);
    }


}