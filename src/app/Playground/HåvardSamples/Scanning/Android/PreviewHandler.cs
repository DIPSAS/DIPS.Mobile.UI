using Android.Views;
using AndroidX.Camera.View;
using Microsoft.Maui.Handlers;
using Playground.HåvardSamples.Scanning.Android;
using AbsoluteLayout = Android.Widget.AbsoluteLayout;
using Color = Android.Graphics.Color;

namespace Playground.HåvardSamples.Scanning;

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
        previewView.SetBackgroundColor(Color.Yellow);
        return previewView;
    }
}