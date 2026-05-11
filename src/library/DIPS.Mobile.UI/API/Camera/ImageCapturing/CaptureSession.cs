using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

internal abstract record CaptureSession(CameraOptions CameraOptions);

internal sealed record SingleCaptureSession(CameraOptions CameraOptions)
    : CaptureSession(CameraOptions);

internal sealed record MultiCaptureSession(CameraOptions CameraOptions, 
    MultiImageCaptureOptions MultiImageCaptureOptions)
    : CaptureSession(CameraOptions);