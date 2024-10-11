using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.API.Camera.Preview;

internal partial class PreviewViewHandler() : ViewHandler<PreviewView, iOS.PreviewView>(ViewMapper)
{
    protected override iOS.PreviewView CreatePlatformView()
    {
        return new iOS.PreviewView();
    }
}