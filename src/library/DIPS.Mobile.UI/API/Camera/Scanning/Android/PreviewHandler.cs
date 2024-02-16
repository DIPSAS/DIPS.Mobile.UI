using Android.Views;
using AndroidX.Camera.View;
using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.API.Camera.Scanning;

//Preview: https://developer.android.com/media/camera/camera2/camera-preview
public partial class PreviewHandler : ViewHandler<Preview, PreviewView>
{
    public PreviewHandler() : base(ViewMapper, ViewCommandMapper)
    {
    }

    protected override PreviewView CreatePlatformView()
    {
        var previewView = new PreviewView(Context);
        previewView.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent,
            ViewGroup.LayoutParams.WrapContent);
        previewView.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
        return previewView;
    }
}