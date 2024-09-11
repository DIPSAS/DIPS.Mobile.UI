using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.API.Camera.Preview.Android.PreviewView;

internal class PreviewViewHandler() : ViewHandler<PreviewView, AndroidX.Camera.View.PreviewView>(ViewMapper)
{
    protected override AndroidX.Camera.View.PreviewView CreatePlatformView()
    {
        var test = new AndroidX.Camera.View.PreviewView(Context);
        test.SetImplementationMode(AndroidX.Camera.View.PreviewView.ImplementationMode.Compatible);
        return test;
    }
}