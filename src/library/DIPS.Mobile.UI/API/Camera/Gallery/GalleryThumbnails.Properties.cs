using System.Windows.Input;
using DIPS.Mobile.UI.API.Camera.ImageCapturing;

namespace DIPS.Mobile.UI.API.Camera.Gallery;

public partial class GalleryThumbnails
{
    public static readonly BindableProperty ImagesProperty = BindableProperty.Create(
        nameof(Images),
        typeof(List<CapturedImage>),
        typeof(GalleryThumbnails),
        defaultBindingMode: BindingMode.TwoWay,
        defaultValue: new List<CapturedImage>(),
        propertyChanged: (bindable, _, _) => ((GalleryThumbnails)bindable).OnImagesChanged());

    public List<CapturedImage> Images
    {
        get => (List<CapturedImage>)GetValue(ImagesProperty);
        set => SetValue(ImagesProperty, value);
    }

    public ICommand? CameraButtonTappedCommand
    {
        get => (ICommand)GetValue(CameraButtonTappedCommandProperty);
        set => SetValue(CameraButtonTappedCommandProperty, value);
    }
    
    public static readonly BindableProperty CameraButtonTappedCommandProperty = BindableProperty.Create(
        nameof(CameraButtonTappedCommand),
        typeof(ICommand),
        typeof(GalleryThumbnails));
    
    public event EventHandler? CameraButtonTapped;
}