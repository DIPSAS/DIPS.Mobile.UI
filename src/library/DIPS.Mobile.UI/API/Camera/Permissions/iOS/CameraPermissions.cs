using AVFoundation;

namespace DIPS.Mobile.UI.API.Camera.Permissions;

internal static partial class CameraPermissions
{
    //https://developer.apple.com/documentation/avfoundation/capture_setup/requesting_authorization_to_capture_and_save_media#2958841
    internal static partial async Task<bool> CanUseCamera()
    {
        return await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVAuthorizationMediaType.Video);
    }
}