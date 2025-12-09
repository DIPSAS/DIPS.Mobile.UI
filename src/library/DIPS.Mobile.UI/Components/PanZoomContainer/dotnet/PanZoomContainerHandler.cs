using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.PanZoomContainer;

public partial class PanZoomContainerHandler : ViewHandler<PanZoomContainer, object>
{
    protected override object CreatePlatformView()
    {
        return new object();
    }

    private void UpdateContent() {}
}
