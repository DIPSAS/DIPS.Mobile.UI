using System.Windows.Input;

namespace DIPS.Mobile.UI.API.Camera.ImageGallery.ImageGalleryThumbnails;

public partial class ImageGalleryThumbnails
{
    public static readonly BindableProperty ImagesProperty = BindableProperty.Create(
        nameof(Images),
        typeof(List<byte[]>),
        typeof(ImageGalleryThumbnails),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, _, _) => ((ImageGalleryThumbnails)bindable).OnImagesChanged());

    public List<byte[]> Images
    {
        get => (List<byte[]>)GetValue(ImagesProperty);
        set => SetValue(ImagesProperty, value);
    }

    public static readonly BindableProperty OnTappedImageCommandProperty = BindableProperty.Create(
        nameof(OnTappedImage),
        typeof(ICommand),
        typeof(ImageGalleryThumbnails));

    public ICommand OnTappedImageCommand
    {
        get => (ICommand)GetValue(OnTappedImageCommandProperty);
        set => SetValue(OnTappedImageCommandProperty, value);
    }
}