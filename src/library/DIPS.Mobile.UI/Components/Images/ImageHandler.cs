namespace DIPS.Mobile.UI.Components.Images;

public partial class ImageHandler
{
    public ImageHandler() : base(PropertyMapper)
    {
        
    }
    
    public static IPropertyMapper<Image, ImageHandler> PropertyMapper = new PropertyMapper<Image, ImageHandler>(ViewMapper)
    {
        [nameof(Image.iOSProperties)] = TrySetSystemImage,
        [nameof(Image.AndroidProperties)] = TrySetSystemImage,
        [nameof(Image.Color)] = TrySetImageColor
    };

    private static partial void TrySetSystemImage(ImageHandler imageHandler, Image image);

    private static partial void TrySetImageColor(ImageHandler imageHandler, Image image);

}