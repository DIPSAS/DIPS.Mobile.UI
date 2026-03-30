# Image Capture

The `ImageCapture` component lets you capture images using the device's camera. It supports two modes:

- **Single image:** the user takes one photo, confirms it, and the camera closes.
- **Multiple images:** the user takes several photos in one session, and the session ends when they tap the finish button.

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

> We recommend starting the camera in `OnAppearing`.

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
        FinishedButtonCommand = new Command(OnFinished),
    };

    await m_imageCapture.StartMultiImageCapture(
        CameraPreview, OnImageCaptured, OnCameraFailed,
        cameraOptions, multiOptions);
}
```

`RequiresConfirmationOnEachImage` controls the per-image flow:

- **`false`** (default): each image is delivered to your delegate as soon as it is captured, and the camera stays ready for the next shot. Faster for batch capture.
- **`true`**: each image first goes through the confirm screen. The delegate fires only after the user accepts it.

`FinishedButtonCommand` is invoked when the user taps the finish button in the top toolbar. Use it to close the page or commit the captured list.

## Handling captured images

```csharp
private void OnImageCaptured(CapturedImage capturedImage)
{
    m_images.Add(capturedImage);
}
```

In multi-capture this method is called once per image, in capture order.

## Handling camera failures

```csharp
private void OnCameraFailed(CameraException e)
{
    DisplayAlert("Something failed!", e.Message, "Ok");
}
```

## Closing the camera

Tapping the cancel button closes the camera and invokes `CancelButtonCommand` if you set one. Capturing an image in single mode also closes the camera. If you are not using our automatic memory leak tooling, stop the camera explicitly when the user navigates away:

```csharp
protected override void OnDisappearing()
{
    base.OnDisappearing();
    m_imageCapture.Stop();
}
```
