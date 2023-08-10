namespace DIPS.Mobile.UI.Components.Chips;

public partial class ChipHandler
{
    public ChipHandler() : base(PropertyMapper)
    {
    }

    public static readonly IPropertyMapper<Chip, ChipHandler> PropertyMapper = new PropertyMapper<Chip, ChipHandler>(ViewMapper)
    {
        [nameof(Chip.Title)] = MapTitle,
        [nameof(Chip.HasCloseButton)] = MapHasCloseButton,
        [nameof(Chip.Color)] = MapColor,
        [nameof(Chip.CloseButtonColor)] = MapCloseButtonColor,
    };

    private static partial void MapColor(ChipHandler handler, Chip chip);

    private static partial void MapTitle(ChipHandler handler, Chip chip);
    private static partial void MapHasCloseButton(ChipHandler handler, Chip chip);
    private static partial void MapCloseButtonColor(ChipHandler handler, Chip chip);
    
    private void OnChipTapped()
    {
        VirtualView.SendTapped();
    }

    private void OnCloseTapped()
    {
        VirtualView.SendCloseTapped();
    }
    
}