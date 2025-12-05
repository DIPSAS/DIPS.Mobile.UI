namespace DIPS.Mobile.UI.Components.Gallery;

public partial class Gallery
{
    public static readonly BindableProperty ImagesProperty = BindableProperty.Create(
        nameof(Images),
        typeof(IEnumerable<ImageSource>),
        typeof(Gallery));

    /// <summary>
    /// Gets or sets the collection of images to display in the gallery.
    /// </summary>
    public IEnumerable<ImageSource>? Images
    {
        get => (IEnumerable<ImageSource>?)GetValue(ImagesProperty);
        set => SetValue(ImagesProperty, value);
    }
}