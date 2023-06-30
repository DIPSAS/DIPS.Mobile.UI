namespace DIPS.Mobile.UI.Components.Images.ImageButton;

public partial class ImageButtonHandler : Microsoft.Maui.Handlers.ImageButtonHandler
{
    public ImageButtonHandler() : base(PropertyMapper)
    {
        AppendPropertyMapper();
    }

    private partial void AppendPropertyMapper();

    public static IPropertyMapper<ImageButton, ImageButtonHandler> PropertyMapper = new PropertyMapper<ImageButton, ImageButtonHandler>(Mapper)
    {
        [nameof(ImageButton.TintColor)] = TrySetTintColor,
        [nameof(ImageButton.AdditionalHitBoxSize)] = MapAdditionalHitBoxSize
    };

    private static partial void MapAdditionalHitBoxSize(ImageButtonHandler handler, ImageButton imageButton);

    private static partial void TrySetTintColor(ImageButtonHandler handler, ImageButton imageButton);

}