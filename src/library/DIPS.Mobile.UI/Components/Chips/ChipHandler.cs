using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;

namespace DIPS.Mobile.UI.Components.Chips;

public partial class ChipHandler
{
    private IconName CloseIconName => IconName.close_line;
    private IconName ToggledIconName => IconName.check_line;
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
        [nameof(Chip.Style)] = MapStyle,
        [nameof(Chip.TitleColor)] = MapTitleColor,
    };

    private static partial void MapBorderColor(ChipHandler handler, Chip chip);

    private static partial void MapBorderWidth(ChipHandler handler, Chip chip);

    private static partial void MapCornerRadius(ChipHandler handler, Chip chip);

    private static partial void MapColor(ChipHandler handler, Chip chip);

    private static partial void MapTitle(ChipHandler handler, Chip chip);
    private static partial void MapHasCloseButton(ChipHandler handler, Chip chip);
    private static partial void MapCloseButtonColor(ChipHandler handler, Chip chip);
    private static partial void MapStyle(ChipHandler handler, Chip chip);
    private static partial void MapTitleColor(ChipHandler handler, Chip chip);
    
    
    internal void OnChipTapped()
    {
        var wasToggled = (bool)VirtualView.IsToggled!;
        
        VirtualView.SendTapped();
        
        switch (wasToggled)
        {
            case true when !(bool)VirtualView.IsToggled!:
                VirtualView.Style = ToggleStyle.ToggledOff;
                break;
            case false when (bool)VirtualView.IsToggled!:
                VirtualView.Style = ToggleStyle.ToggledOn;
                break;
        }
    }

    internal void OnCloseTapped()
    {
        VirtualView.SendCloseTapped();
    }
    
}