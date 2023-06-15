namespace DIPS.Mobile.UI.Components.Chips;

public partial class ChipHandler
{
    internal static string BackgroundColorAsHex = "#E5E9EB";
    public ChipHandler() : base(PropertyMapper)
    {
    }

    public static readonly IPropertyMapper<Chip, ChipHandler> PropertyMapper = new PropertyMapper<Chip, ChipHandler>(ViewMapper)
    {
        [nameof(Chip.Title)] = MapTitle,
    };

    private static partial void MapTitle(ChipHandler handler, Chip chip);
    
    private void OnChipTapped(object? sender, EventArgs e)
    {
        VirtualView.SendTapped();
    }
    
}