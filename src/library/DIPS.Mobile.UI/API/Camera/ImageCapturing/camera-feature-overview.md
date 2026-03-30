# ImageCapture


### Modes

`StartSingleImageCapture` opens the camera. The user takes a photo and reviews it on the Confirm screen. "Use Photo" delivers the image and closes the camera. Retake returns to the live preview to try again.

`StartMultiImageCapture` keeps the camera open across multiple shots, ending when the user taps Finish. By default each photo is delivered the moment it's captured.

Setting `RequiresConfirmationOnEachImage = true` (multi mode only) routes each shot through the Confirm screen first. "Use Photo" then returns to the live preview rather than closing the camera, so the user can keep capturing.

Both take a `CameraOptions`. Multi mode also takes a `MultiImageCaptureOptions`.

### States and transitions

The camera moves between three states.

- **Streaming**: live preview with shutter, flash, settings, and zoom.
- **Confirm**: the captured photo is shown for review, with Use, Retake, and Edit.
- **Edit**: the photo can be rotated in 90° steps. Save or Cancel returns to Confirm.

The transitions between them:

**Single mode:**

- Image capture moves Streaming to Confirm.
- "Use Photo"-button closes the camera.
- Retake returns to Streaming.
- Save or Cancel in Edit returns to Confirm.

**Multi mode, with confirmation** (`RequiresConfirmationOnEachImage = true`):

- Image capture moves Streaming to Confirm.
- "Use Photo"-button returns to Streaming.
- Retake returns to Streaming.
- Save or Cancel in Edit returns to Confirm.
- The Finish button closes the camera.

The Cancel button is hidden during Confirm so the focus stays on Use or Retake.

**Multi mode, without confirmation** (`RequiresConfirmationOnEachImage = false`):

- Image capture delivers the image and stays in Streaming. Confirm is never entered.
- The Finish button closes the camera.

### Platform implementations

| Concern | Android | iOS |
|---|---|---|
| Camera API | AndroidX CameraX | AVFoundation |
| Capture callback | `OnImageCapturedCallback` | `AVCapturePhotoCaptureDelegate` |
| Orientation | EXIF via `ExifOrientationExtensions` | `CGImagePropertyOrientation` via `RotateCgImageToPortrait` |
| Thumbnail | EXIF embedded | `CGImageSource.CreateThumbnail` |
| Zoom | `CameraControl.SetZoomRatio` | session reconfiguration |
| Tap-to-focus | `StartFocusAndMetering` | not implemented |

### File map

- `ImageCapture.cs` plus `ImageCapture.{Streaming,Confirm,Edit}State.cs` drive transitions.
- `CaptureSession.cs` wraps options into a Single or Multi session record that the rest of the code branches on.
- `Views/TopToolbar/ImageCaptureTopToolbarView.cs` owns the per-mode button layout.
- `BottomSheets/ImageCaptureSettingsBottomSheet.cs` is the resolution sheet behind the Settings button.
