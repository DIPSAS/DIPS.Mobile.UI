namespace DIPS.Mobile.UI.Components.Gallery;

public partial class Gallery
{
    public static readonly BindableProperty ImagesProperty = BindableProperty.Create(
        nameof(Images),
        typeof(IEnumerable<ImageSource>),
        typeof(Gallery), 
        propertyChanged: (bindable, _, _) => ((Gallery)bindable).OnImagesChanged());
    
    public static readonly BindableProperty CurrentImageIndexProperty = BindableProperty.Create(
        nameof(CurrentImageIndex),
        typeof(int),
        typeof(Gallery),
        -1,
        propertyChanged: (bindable, _, _) => ((Gallery)bindable).OnCurrentImageIndexChanged());

    public static readonly BindableProperty GalleryCustomizerProperty = BindableProperty.Create(
        nameof(GalleryCustomizer),
        typeof(GalleryCustomizer),
        typeof(Gallery),
        new GalleryCustomizer());

    public static readonly BindableProperty FadeOutOnZoomProperty = BindableProperty.Create(
        nameof(FadeOutOnZoom),
        typeof(bool),
        typeof(Gallery));

    /// <summary>
    /// Whether to fade out the UI elements when an image is zoomed in.
    /// </summary>
    public bool FadeOutOnZoom
    {
        get => (bool)GetValue(FadeOutOnZoomProperty);
        set => SetValue(FadeOutOnZoomProperty, value);
    }

    /// <summary>
    /// The customizer for the gallery's appearance.
    /// </summary>
    public GalleryCustomizer GalleryCustomizer
    {
        get => (GalleryCustomizer)GetValue(GalleryCustomizerProperty);
        set => SetValue(GalleryCustomizerProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the collection of images to display in the gallery.
    /// </summary>
    public IEnumerable<ImageSource>? Images
    {
        get => (IEnumerable<ImageSource>?)GetValue(ImagesProperty);
        set => SetValue(ImagesProperty, value);
    }

    /// <summary>
    /// Gets or sets the index of the currently displayed image in the gallery.
    /// </summary>
    public int CurrentImageIndex
    {
        get => (int)GetValue(CurrentImageIndexProperty);
        set => SetValue(CurrentImageIndexProperty, value);
    }
}
