using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.API.Camera.Preview;

internal partial class PreviewViewHandler() : ViewHandler<PreviewView, AndroidX.Camera.View.PreviewView>(ViewMapper)
{
    protected override AndroidX.Camera.View.PreviewView CreatePlatformView()
    {
        return new AndroidX.Camera.View.PreviewView(Context);
    }
}