namespace DIPS.Mobile.UI.Components.Images.ImageButton;

public partial class ImageButtonHandler : Microsoft.Maui.Handlers.ImageButtonHandler
{
    public ImageButtonHandler() : base(PropertyMapper) { }
    
    public static IPropertyMapper<ImageButton, ImageButtonHandler> PropertyMapper = new PropertyMapper<ImageButton, ImageButtonHandler>(Mapper)
    {
        [nameof(ImageButton.AdditionalHitBoxSize)] = MapAdditionalHitBoxSize,
#if __ANDROID__
        [nameof(ImageButton.TintColor)] = TrySetTintColor
#endif
    };

    private static partial void MapAdditionalHitBoxSize(ImageButtonHandler handler, ImageButton imageButton);

#if __ANDROID__
    private static partial void TrySetTintColor(ImageButtonHandler handler, ImageButton imageButton);
#endif

}