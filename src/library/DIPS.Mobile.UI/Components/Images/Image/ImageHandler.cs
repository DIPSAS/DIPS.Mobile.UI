namespace DIPS.Mobile.UI.Components.Images.Image;

public partial class ImageHandler : Microsoft.Maui.Handlers.ImageHandler
{
    public ImageHandler() : base(PropertyMapper)
    {
    }
    
    public static IPropertyMapper<Image, ImageHandler> PropertyMapper = new PropertyMapper<Image, ImageHandler>(Mapper)
    {
        [nameof(Image.TintColor)] = TrySetTintColor
    };

    private static partial void TrySetTintColor(ImageHandler handler, Image image);
}