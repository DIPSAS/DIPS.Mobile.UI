# Image Capture

The `ImageCapture` component allows you to capture images using the device's camera. This documentation provides an example of how to use the `ImageCapture` component in your application.

## Usage

In this example, the `ImageCapture` component is initialized and used to capture images, and then be stored.

### Initialization

First, put `CameraPreview` component into your page:

```xaml
<dui:CameraPreview x:Name="CameraPreview" />
```

Next, initialize the `ImageCapture` component and a list to store the captured images:

```csharp
private readonly List<CapturedImage> m_images;
private readonly ImageCapture m_imageCapture;

public ImageCaptureSample()
{
    InitializeComponent();

    m_images = new List<CapturedImage>();
    m_imageCapture = new ImageCapture();
}
```

### Starting Image Capture

To start capturing images, call the `StartImageCapture` method on the `ImageCapture` object you created, and feed in the `CameraPreview`, delegate for when an image is captured, and delegate for when something went wrong. This method also allows you to configure your desired settings.

> We encourage to start the camera in the `OnAppearing` function.

```csharp
private async Task StartImageCapture()
{
    await m_imageCapture.Start(CameraPreview, OnImageCaptured, OnCameraFailed,
        settings =>
        {
            settings.PostCaptureAction = PostCaptureAction.Close;
            settings.MaxHeightOrWidth = 2560;
            settings.CanChangeMaxHeightOrWidth = true;
            settings.DoneButtonCommand = new Command(Close);
        });
}
```

> If the `DoneButtonCommand` is `null`, the `Cancel` button in the `CameraPreview` is hidden

### Handling Captured Images

When an image is captured, the `OnImageCaptured` method is called. This method allows you to for example add the captured image to a list:

```csharp
private async void OnImageCaptured(CapturedImage capturedImage)
{
    m_images.Add(capturedImage);
}
```

### Handling Camera Failures

If the camera fails, the `OnCameraFailed` method is called, here you can for instance display an error message:

```csharp
private void OnCameraFailed(CameraException e)
{
    App.Current.MainPage.DisplayAlert("Something failed!", e.Message, "Ok");
}
```

### Closing the Camera

The `Close` method stops the camera. The stopping of the camera is handled when the `Cancel` button is tapped or when an image is captured. However, if you are not using our automatic memory leak tooling, you need to stop the camera yourself when the user is navigating away from the page:

```csharp
protected override void OnDisappearing()
{
    m_imageCapture.Stop();
}
```
