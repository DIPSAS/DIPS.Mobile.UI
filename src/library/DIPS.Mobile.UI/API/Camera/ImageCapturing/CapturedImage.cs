namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public class CapturedImage
{
    public byte[] AsByteArray { get; }
    

#if __ANDROID__
    public CapturedImage(byte[] asByteArray, AndroidX.Camera.Core.IImageInfo imageImageInfo)
    {
        AsByteArray = asByteArray;
        ImageImageInfo = imageImageInfo;
    }
    
    public AndroidX.Camera.Core.IImageInfo ImageImageInfo { get; }
#elif __IOS__
    public CapturedImage(byte[] asByteArray, AVFoundation.AVCapturePhoto photo)
    {
        AsByteArray = asByteArray;
        Photo = photo;
    }

    public AVFoundation.AVCapturePhoto Photo { get; }
#endif
}