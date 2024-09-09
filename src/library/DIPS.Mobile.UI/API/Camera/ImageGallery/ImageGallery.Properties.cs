using System.Windows.Input;

namespace DIPS.Mobile.UI.API.Camera.ImageGallery;

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

    public ICommand CameraButtonTappedCommand
    {
        get => (ICommand)GetValue(CameraButtonTappedCommandProperty);
        set => SetValue(CameraButtonTappedCommandProperty, value);
    }
    
    public static readonly BindableProperty CameraButtonTappedCommandProperty = BindableProperty.Create(
        nameof(CameraButtonTappedCommand),
        typeof(ICommand),
        typeof(ImageGallery));
}