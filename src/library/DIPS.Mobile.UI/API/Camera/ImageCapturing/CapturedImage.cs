namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public class CapturedImage(byte[] asByteArray)
{
    public byte[] AsByteArray { get; } = asByteArray;
}