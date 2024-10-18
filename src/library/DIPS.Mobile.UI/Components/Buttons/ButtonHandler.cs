using IImage = Microsoft.Maui.IImage;

namespace DIPS.Mobile.UI.Components.Buttons;

public partial class ButtonHandler
{
    public ButtonHandler() : base(PropertyMapper, CommandMapper)
    {
        AppendPropertyMapper();
    }

    private partial void AppendPropertyMapper();

    public static readonly IPropertyMapper<Button, ButtonHandler> PropertyMapper =
        new PropertyMapper<Button, ButtonHandler>(Mapper)
        {
            [nameof(Button.AdditionalHitBoxSize)] = MapAdditionalHitBoxSize,
            [nameof(Button.ImageTintColor)] = MapImageTintColor,
            [nameof(Button.ImagePlacement)] = MapImageToRightSide,
            [nameof(IImage.Source)] = OverrideMapImageSource
        };

    private static partial void OverrideMapImageSource(ButtonHandler handler, Button button);

    private static partial void MapImageToRightSide(ButtonHandler handler, Button button);

    private static partial void MapImageTintColor(ButtonHandler handler, Button button);

    private static partial void MapAdditionalHitBoxSize(ButtonHandler handler, Button button);
}