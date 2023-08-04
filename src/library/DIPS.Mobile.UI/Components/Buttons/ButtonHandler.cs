namespace DIPS.Mobile.UI.Components.Buttons;

public partial class ButtonHandler
{
    public ButtonHandler() : base(PropertyMapper, CommandMapper)
    {
        AppendPropertyMapper();
    }

    private partial void AppendPropertyMapper();

    public static IPropertyMapper<Button, ButtonHandler> PropertyMapper =
        new PropertyMapper<Button, ButtonHandler>(Mapper)
        {
            [nameof(Button.AdditionalHitBoxSize)] = MapAdditionalHitBoxSize,
        };

    private static partial void MapAdditionalHitBoxSize(ButtonHandler handler, Button button);
}