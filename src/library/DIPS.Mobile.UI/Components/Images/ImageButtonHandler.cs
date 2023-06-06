namespace DIPS.Mobile.UI.Components.Images;

public partial class ImageButtonHandler : Microsoft.Maui.Handlers.ImageButtonHandler
{
    public ImageButtonHandler() : base(PropertyMapper)
    {
    }
    
    public static IPropertyMapper<ImageButton, ImageButtonHandler> PropertyMapper = new PropertyMapper<ImageButton, ImageButtonHandler>(Mapper)
    {
        [nameof(ImageButton.TintColor)] = TrySetTintColor
    };

    private static partial void TrySetTintColor(ImageButtonHandler handler, ImageButton imageButton);

}