using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.Chips;

public partial class ChipHandler : ViewHandler<Chip, ViewHandler>
{
    private static partial void MapTitle(ChipHandler handler, Chip chip)
    {
    }

    protected override ViewHandler CreatePlatformView()
    {
        throw new NotImplementedException();
    }
    
}