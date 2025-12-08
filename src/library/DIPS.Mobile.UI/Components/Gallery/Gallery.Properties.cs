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
