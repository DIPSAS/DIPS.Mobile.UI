namespace DIPS.Mobile.UI.Components.Chips;

public partial class ChipHandler
{
    private IconName CloseIconName => IconName.close_line;
    public ChipHandler() : base(PropertyMapper)
    {
    }

    public static readonly IPropertyMapper<Chip, ChipHandler> PropertyMapper = new PropertyMapper<Chip, ChipHandler>(ViewMapper)
    {
        [nameof(Chip.Title)] = MapTitle,
        [nameof(Chip.HasCloseButton)] = MapHasCloseButton,
        [nameof(Chip.Color)] = MapColor,
        [nameof(Chip.CloseButtonColor)] = MapCloseButtonColor,
        [nameof(Chip.CornerRadius)] = MapCornerRadius,
        [nameof(Chip.BorderWidth)] = MapBorderWidth,
        [nameof(Chip.BorderColor)] = MapBorderColor,
    };

    private static partial void MapBorderColor(ChipHandler handler, Chip chip);

    private static partial void MapBorderWidth(ChipHandler handler, Chip chip);

    private static partial void MapCornerRadius(ChipHandler handler, Chip chip);

    private static partial void MapColor(ChipHandler handler, Chip chip);

    private static partial void MapTitle(ChipHandler handler, Chip chip);
    private static partial void MapHasCloseButton(ChipHandler handler, Chip chip);
    private static partial void MapCloseButtonColor(ChipHandler handler, Chip chip);
    
    internal void OnChipTapped()
    {
        VirtualView.SendTapped();
    }

    internal void OnCloseTapped()
    {
        VirtualView.SendCloseTapped();
    }
    
}