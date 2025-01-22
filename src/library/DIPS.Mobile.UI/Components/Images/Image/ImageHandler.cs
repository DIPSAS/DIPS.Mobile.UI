namespace DIPS.Mobile.UI.Components.Images.Image;

public partial class ImageHandler : Microsoft.Maui.Handlers.ImageHandler
{
    public ImageHandler() : base(PropertyMapper) { }
    
    public static IPropertyMapper<Image, ImageHandler> PropertyMapper = new PropertyMapper<Image, ImageHandler>(Mapper)
    {
#if __ANDROID__
        [nameof(Image.TintColor)] = TrySetTintColor
#endif
    };

#if __ANDROID__
    private static partial void TrySetTintColor(ImageHandler handler, Image image);
#endif
}
