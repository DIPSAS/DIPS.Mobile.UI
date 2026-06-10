# Image Capture

The `ImageCapture` component lets you capture images using the device's camera. It supports two modes:

- **Single image:** the user takes one photo, confirms it, and the camera closes.
- **Multiple images:** the user takes several photos in one session and ends the session with the finish button. Per-image confirmation is optional. The user can also optionally review and/or delete the photos in a gallery view before ending the session.

## Setup

First, put a `CameraPreview` into your page:

```xaml
<dui:CameraPreview x:Name="CameraPreview" />
```

Then create an `ImageCapture` instance and a list to hold the results:

```csharp
private readonly List<CapturedImage> m_images = [];
private readonly ImageCapture m_imageCapture = new();
```

> We recommend starting the camera in `OnHandlerChanged`.

> **NOTE:** On iOS, devices with ultra-wide cameras can use the ultra-wide lens through the shared zoom controls while starting the preview in normal wide framing. On those devices, the zoom controls can show `0.5x` for the ultra-wide lens.

## Capturing a single image

Call `StartSingleImageCapture` with the preview, your "image captured" delegate, your "camera failed" delegate, and a `CameraOptions` object:

```csharp
private async Task StartSingleImageCapture()
{
    var cameraOptions = new CameraOptions
    {
        MaxHeightOrWidth = 2560,
        CanChangeMaxHeightOrWidth = true,
        CancelButtonCommand = new Command(Close),
    };

    await m_imageCapture.StartSingleImageCapture(
        CameraPreview, OnImageCaptured, OnCameraFailed, cameraOptions);
}
```

The delegate fires once, after the user accepts the image on the confirm screen. The camera then closes.

## Capturing multiple images

Call `StartMultiImageCapture` with an extra `MultiImageCaptureOptions` argument:

```csharp
private async Task StartMultiImageCapture()
{
    var cameraOptions = new CameraOptions
    {
        CancelButtonCommand = new Command(OnCancelled),
    };

    var multiOptions = new MultiImageCaptureOptions
    {
        RequiresConfirmationOnEachImage = false,
        MaxImageCount = 10,
        FinishedButtonCommand = new Command(OnFinished),
        OnImageRemoved = OnImageRemoved,
    };

    await m_imageCapture.StartMultiImageCapture(
        CameraPreview, OnImageCaptured, OnCameraFailed,
        cameraOptions, multiOptions);
}
```

`RequiresConfirmationOnEachImage` controls the per-image flow:

- **`false`** (default): each image is delivered to your delegate as soon as it is captured, and the camera stays ready for the next shot. Faster for batch capture.
- **`true`**: each image first goes through the confirm screen. The delegate fires only after the user accepts it.

`MaxImageCount` caps how many photos one session can hold. It defaults to 15 and must be greater than zero. Captured photos stay in memory for the whole session, so this cap is what keeps memory bounded. When the user reaches the cap, the camera shows a message and ignores the shutter until they remove a photo.

`FinishedButtonCommand` runs when the user taps the finish button. The finish button sits next to the shutter at the bottom of the screen, and its label defaults to "Done". Use this command to close the page or commit the captured list.

## Reviewing and removing photos

During multi-capture, a thumbnail of the most recent photo appears in the bottom-left corner of the camera, with a badge showing how many photos the session holds. The thumbnail updates after every shot.

Tapping the thumbnail opens a full-screen gallery. The user can swipe through every photo taken so far. The Delete button removes the photo on screen after a confirmation prompt. The Done button closes the gallery and returns to the camera.

When the user deletes a photo in the gallery, `OnImageRemoved` fires with that `CapturedImage`. Handle it to keep your own list matched to what the user kept:

```csharp
private void OnImageRemoved(CapturedImage capturedImage)
{
    m_images.Remove(capturedImage);
}
```

## Handling captured images

```csharp
private void OnImageCaptured(CapturedImage capturedImage)
{
    m_images.Add(capturedImage);
}
```

In multi-capture this method is called once per image, in capture order. Pair it with `OnImageRemoved` so your list reflects every add and every removal the user makes.

## Handling camera failures

```csharp
private void OnCameraFailed(CameraException e)
{
    DisplayAlert("Something failed!", e.Message, "Ok");
}
```

## Closing the camera

Tapping the cancel button closes the camera and invokes `CancelButtonCommand` if you set one. Capturing an image in single mode also closes the camera. If you are not using our automatic memory leak tooling, stop the camera explicitly when the user leaves the page:

```csharp
protected override void OnHandlerChanging(HandlerChangingEventArgs args)
{
    base.OnHandlerChanging(args);

    if (args.NewHandler is not null)
        return;

    m_imageCapture.Stop();
}
```

Use `OnHandlerChanging` rather than `OnDisappearing`. A null new handler means the page itself is being removed, so the camera stops only then. `OnDisappearing` also fires when a sheet or dialog covers the page, which would stop the camera while the user is still capturing.
</content>
</invoke>
