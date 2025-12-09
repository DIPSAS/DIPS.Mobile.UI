using DIPS.Mobile.UI.Components.Gallery;

namespace DIPS.Mobile.UI.Components.TiffViewer;

public partial class TiffViewer
{
    public static readonly BindableProperty SourceProperty = BindableProperty.Create(
        nameof(Source),
        typeof(byte[]),
        typeof(TiffViewer),
        propertyChanged: (bindable, _, _) => _ = ((TiffViewer)bindable).LoadTiffPages());

    public static readonly BindableProperty GalleryCustomizerProperty = BindableProperty.Create(
        nameof(GalleryCustomizer),
        typeof(GalleryCustomizer),
        typeof(TiffViewer),
        new GalleryCustomizer());
    
    public byte[]? Source
    {
        get => (byte[]?)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    /// <summary>
    /// Sets the customizer for the gallery's appearance.
    /// </summary>
    public GalleryCustomizer GalleryCustomizer
    {
        get => (GalleryCustomizer)GetValue(GalleryCustomizerProperty);
        set => SetValue(GalleryCustomizerProperty, value);
    }
}
