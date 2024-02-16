using Microsoft.Maui.Handlers;
using Playground.HåvardSamples.Scanning.Android;

namespace Playground.HåvardSamples.Scanning;


//Preview: https://developer.android.com/media/camera/camera2/camera-preview
public partial class PreviewHandler : ViewHandler<Preview, AutoFitTextureView>
{
    public PreviewHandler() : base(ViewMapper, ViewCommandMapper)
    {
        
    }

    protected override AutoFitTextureView CreatePlatformView()
    {
        var textureView = new AutoFitTextureView(Context);
        return textureView;
    }
}