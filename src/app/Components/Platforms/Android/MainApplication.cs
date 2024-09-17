using Android.App;
using Android.Runtime;
using AndroidX.Camera.Camera2;
using AndroidX.Camera.Core;

namespace Components;

[Application]
public class MainApplication : MauiApplication , CameraXConfig.IProvider 
{
    
    public CameraXConfig CameraXConfig =>
        CameraXConfig.Builder.FromConfig(Camera2Config.DefaultConfig())
            .SetAvailableCamerasLimiter(CameraSelector.DefaultBackCamera)
            .Build(); //To speed up initialization of CameraX when the camera API is started. Taken from:  https://developer.android.com/media/camera/camerax/configuration#camera-limiter
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}