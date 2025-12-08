using System.ComponentModel;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Components.PanZoomContainer;

/// <summary>
/// A container that enables pinch-to-zoom and pan gestures on an image.
/// </summary>
[ContentProperty(nameof(Source))]
public class PanZoomContainer : View
{
    
    public static readonly BindableProperty SourceProperty = BindableProperty.Create(
        nameof(Source),
        typeof(ImageSource),
        typeof(PanZoomContainer));

    public static readonly BindableProperty IsZoomedProperty = BindableProperty.Create(
        nameof(IsZoomed),
        typeof(bool),
        typeof(PanZoomContainer),
        defaultBindingMode: BindingMode.OneWayToSource);
    
    /// <summary>
    /// The <see cref="ImageSource"/> to display inside the zoom container
    /// </summary>
    [TypeConverter(typeof(ImageSourceConverter))]
    public ImageSource? Source
    {
        get => (ImageSource?)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public bool IsZoomed
    {
        get => (bool)GetValue(IsZoomedProperty);
        set => SetValue(IsZoomedProperty, value);
    }
}
