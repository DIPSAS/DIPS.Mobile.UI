using Android.Graphics;
using Android.Views;

namespace Playground.HåvardSamples.Scanning.Android;

public class SurfaceTextureListener : Java.Lang.Object, TextureView.ISurfaceTextureListener 
{
    public void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height)
    {
            
    }

    public bool OnSurfaceTextureDestroyed(SurfaceTexture surface)
    {
        return true;
    }

    public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
    {
    }

    public void OnSurfaceTextureUpdated(SurfaceTexture surface)
    {
    }
}