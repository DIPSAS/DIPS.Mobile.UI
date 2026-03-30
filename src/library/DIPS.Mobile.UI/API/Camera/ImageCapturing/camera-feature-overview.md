# ImageCapture — Comprehensive Overview

## Entry Point

The top-level API is `Camera.StartImageCapture()`, which internally creates an `ImageCapture` instance, wires it to a `CameraPreview`, and manages the full lifecycle.

```csharp
await camera.StartImageCapture(
    didCaptureImageDelegate,    // DidCaptureImage callback
    cameraFailedDelegate        // CameraFailed callback
);
```

---

## Core Classes & Their APIs

### `ImageCapture` — Orchestrator (`ICameraUseCase`)
```
Start(cameraPreview, onCaptured, onFailed, configure?)
Stop()
StopAndDispose()
```
Uses **partial methods** for platform dispatch: `PlatformStart()`, `PlatformCapturePhoto()`, `PlatformStop()`.

### `CapturedImage` — Output model
| Member | Description |
|---|---|
| `AsByteArray` | JPEG bytes |
| `ThumbnailAsByteArray` | Embedded EXIF thumbnail |
| `AsByte64String` | Base64 full image |
| `Size` (ImageSize) | Width, Height, SizeInMegaBytes |
| `Transformation` | OrientationDegree (0/90/180/270) |
| `Rotate(clockwise)` | Returns a new rotated CapturedImage |
| Platform: `ImageBitmap` (Android), `Photo` (iOS) | Native objects |

### `ImageCaptureSettings` — Configuration
| Setting | Default | Purpose |
|---|---|---|
| `PostCaptureAction` | Close | Close camera or Continue capturing |
| `CancelButtonText` | "Cancel" | Top toolbar button text |
| `DoneButtonCommand` | null | Command on cancel/done tap |
| `MaxHeightOrWidth` | null (device max) | Resolution constraint in px |
| `CanChangeMaxHeightOrWidth` | false | Allow user to change resolution |

---

## State Machine

ImageCapture is a 3-state machine. Each state has a corresponding observer interface, view, and toolbar:

```
┌──────────────────┐     shutter tap     ┌──────────────────┐
│  StreamingState   │ ─────────────────→  │   ConfirmState    │
│  (live preview)   │ ←───────────────── │  (review photo)   │
│                   │    retake           │                   │
│  • ShutterButton  │                     │  • UsePhotoButton │
│  • FlashToggle    │                     │  • RetakeButton   │
│  • SettingsButton │                     │  • EditButton     │
│  • ZoomControls   │                     │  • InfoButton     │
└──────────────────┘                     └────────┬─────────┘
                                            edit  │  ↑ save/cancel
                                                  ↓  │
                                         ┌──────────────────┐
                                         │    EditState      │
                                         │  (rotate image)   │
                                         │                   │
                                         │  • RotateButtons  │
                                         │  • Save / Cancel  │
                                         └──────────────────┘
```

**Observer interfaces** (internal):
- `IStreamingStateObserver` — `OnTappedShutterButton()`, `OnTappedFlashButton()`, `OnSettingsChanged()`
- `IConfirmStateObserver` — `OnEditButtonTapped()`, `OnUsePhotoButtonTapped()`, `OnRetakePhotoButtonTapped()`
- `IImageEditStateObserver` — `OnSaveButtonTapped()`, `OnCancelButtonTapped()`, `OnRotateButtonTapped(clockwise)`

---

## Use Cases

1. **Single capture** — `PostCaptureAction.Close`: Stream → Capture → Confirm → delegate fires → camera closes
2. **Continuous capture** — `PostCaptureAction.Continue`: Stream → Capture → Confirm → delegate fires → back to Stream (repeat N times)
3. **Capture + Edit** — Confirm → Edit (rotate 90° increments) → Save → back to Confirm
4. **Resolution-constrained capture** — `MaxHeightOrWidth = 1024` limits output dimensions
5. **Zoom** — pinch gesture (Android) / slider (iOS) via `CameraZoomView`
6. **Flash control** — toggle in StreamingState, applied at capture time
7. **Tap-to-focus** — Android only, via `CameraControl.StartFocusAndMetering()`

---

## Platform Implementations

| Concern | Android | iOS |
|---|---|---|
| Camera API | AndroidX CameraX (`ImageCapture` use case) | AVFoundation (`AVCapturePhotoOutput`) |
| Session | `CameraFragment` (Fragment lifecycle) | `CameraSession` (AVCaptureSession) |
| Capture callback | `OnImageCapturedCallback` inner class | `AVCapturePhotoCaptureDelegate` inner class |
| Orientation | EXIF tags → `ExifOrientationExtensions` | `CGImagePropertyOrientation` → `RotateCgImageToPortrait()` |
| Thumbnail | EXIF embedded thumbnail extraction | `CGImageSource.CreateThumbnail()` |
| Flash | `ImageCapture.FlashMode` enum | `AVCapturePhotoSettings.FlashMode` |
| Zoom | `CameraControl.SetZoomRatio()` | Via session configuration |
| Focus | `StartFocusAndMetering()` with tap coordinates | Not implemented |

---

## View Hierarchy

```
CameraPreview (ContentView)
├── PreviewView (native camera feed)
├── TopToolbar
│   └── ImageCaptureTopToolbarView
│       ├── Cancel/Done button
│       ├── Settings button (streaming)
│       ├── Edit button (confirm)
│       └── Info button (confirm)
├── BottomToolbar (swapped per state)
│   ├── StreamingStateView → ShutterButton + FlashToggle
│   ├── ConfirmStateView → Retake + UsePhoto
│   └── EditStateBottomView → Rotate + Save/Cancel
├── CameraZoomView (slider + buttons)
├── ConfirmImage overlay (full-screen captured photo)
└── ActivityIndicator (processing)
```

---

## Error Handling

Errors flow through the `CameraFailed` delegate with a `CameraException` containing `Context` (where it failed), the inner `Exception`, and optional user-facing `Title`/`Description`.

---

## Data Flow Summary

```
Camera.StartImageCapture()
  → ImageCapture.Start(settings)
    → PlatformStart() [Android: CameraX bind, iOS: AVCaptureSession]
      → StreamingState (live preview)
        → User taps shutter
          → OnBeforeCapture() [flash animation, haptic]
          → PlatformCapturePhoto()
            → Platform callback processes raw data → CapturedImage
              → ConfirmState (shows photo)
                → "Use Photo" → DidCaptureImage delegate invoked
                  → PostCaptureAction.Close: StopAndDispose()
                  → PostCaptureAction.Continue: back to StreamingState
```
