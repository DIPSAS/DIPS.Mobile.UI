using System.Windows.Input;

namespace DIPS.Mobile.UI.API.Camera.ImageGallery.ImageGalleryThumbnails;

public partial class ImageGallery
{
    public static readonly BindableProperty ImagesProperty = BindableProperty.Create(
        nameof(Images),
        typeof(List<byte[]>),
        typeof(ImageGallery),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, _, _) => ((ImageGallery)bindable).OnImagesChanged());

    public List<byte[]> Images
    {
        get => (List<byte[]>)GetValue(ImagesProperty);
        set => SetValue(ImagesProperty, value);
    }
}