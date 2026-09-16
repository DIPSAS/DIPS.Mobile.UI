DIPS.Mobile.UI provides ways for you to use the camera of the phone with different APIs. The APIs require a `CameraPreview` which visually changes when using it with the different APIs.

# Camera prerequisites
## iOS
Add `NSCameraUsageDescription` to your Info.plist.
```xml
<key>NSCameraUsageDescription</key>
<string>This app uses the camera take pictures and scan barcodes, which is used for ..... </string>
```
> Make sure your description is clear and obvious as to what you are using it for. We've experienced that Apple denies uploading to App Store if this is not.

## Android
Add `android.permission.CAMERA` permission to your android manifest.
```xml
<uses-permission android:name="android.permission.CAMERA" />
```

# Preview the camera
To start off, add a `CameraPreview` to your page and give it a `x:Name`. This lets us refer it in the code behind.

```xml
<dui:ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
                 xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
                 ...>
    <dui:CameraPreview x:Name="CameraPreview"/>
</dui:ContentPage>
```

# Scan barcodes

DIPS.Mobile.UI provides a camera API for people to scan barcodes. The implementations are optimised and is inspired by [ML Kit Barcode scanning](https://developers.google.com/ml-kit/vision/barcode-scanning) for Android and [AVCamBarcode from Apple](https://developer.apple.com/documentation/avfoundation/capture_setup/avcambarcode_detecting_barcodes_and_faces) for iOS.

> **NOTE:** On iOS, devices with ultra-wide cameras can use the macro-capable lens for close-range barcode focus while starting the preview in normal wide framing. On those devices, the zoom controls can show `0.5x` for the ultra-wide lens.

## Start scanning
To get started, create an `BarcodeScanner` object in your code behind, and `Start()` it when it makes sense for people.

```csharp
public MyPage()
{
    InitializeComponent();
    m_scanner = new BarcodeScanner();
}
```

In many cases, starting it when page appear makes sense:
```csharp
protected override void OnAppearing()
{
    try
    {
        await m_scanner.Start(new BarcodeScannerStartOptions
        {
            Preview = CameraPreview,
            OnCameraFailed = CameraFailed,
            OnBarcodeAcceptedAsync = result =>
            {
                var theBarCode = result.Barcode.RawValue;
                return Task.CompletedTask;
            }
        });
    }
    catch (Exception e)
    {
        //Todo: catch exception
    }
    base.OnAppearing();
}
```

> Notice that `CameraPreview` is the code behind reference from the preview guide.

## Validate barcodes before accepting them
Use `ValidateBarcodeAsync` when the scanner should ask your app whether a detected barcode belongs in the current workflow before playing the success animation. If validation returns invalid, the scanner plays the failure animation, does not increment the progress counter, and resumes scanning after a short cooldown.

```csharp
private readonly BarcodeScanner m_scanner = new();

await m_scanner.Start(new BarcodeScannerStartOptions
{
    Preview = CameraPreview,
    OnCameraFailed = CameraFailed,
    Strategy = new ScanRectangleBarcodeScanStrategy
    {
        WidthFraction = 0.8f,
        HeightFraction = 0.25f
    },
    Completion = new BarcodeScanCompletionOptions
    {
        RequiredCount = 5,
        InitialCount = alreadyScannedCount,
        OnCompletedAsync = OnScanCountCompletedAsync
    },
    OnBarcodeAcceptedAsync = OnBarcodeAcceptedAsync,
    ValidateBarcodeAsync = ValidateBarcodeAsync,
    OnBarcodeRejectedAsync = OnBarcodeRejectedAsync
});

private Task OnBarcodeAcceptedAsync(BarcodeScanResult result)
{
    var acceptedBarcode = result.Barcode.RawValue;

    if (result.TryGetValidationState<BarcodeMatch>(out var match))
    {
        ViewModel.UseMatch(match);
    }

    return Task.CompletedTask;
}

private async Task<BarcodeScanValidationResult> ValidateBarcodeAsync(string barcode)
{
    var match = await ViewModel.FindBarcodeMatchAsync(barcode);

    return match is not null
        ? BarcodeScanValidationResult.Valid(match)
        : BarcodeScanValidationResult.Invalid("Barcode does not belong to this step.", "not-in-step");
}

private Task OnBarcodeRejectedAsync(BarcodeScanResult result, BarcodeScanValidationResult validationResult)
{
    return Task.CompletedTask;
}

private Task OnScanCountCompletedAsync()
{
    return Task.CompletedTask;
}
```

When `Completion.RequiredCount` is greater than zero, the scanner displays a simple counter at the top of the bottom toolbar, such as `3 av 5 skannet`. Set `Completion.InitialCount` when the session starts with already accepted barcodes; after that, the scanner owns the active count and increments it after each accepted scan. Rejected scans do not change the counter. When the active count reaches `Completion.RequiredCount`, scanner analysis stops and `Completion.OnCompletedAsync` is invoked.

If `ValidateBarcodeAsync` throws, the scanner treats the barcode as invalid, plays the failure animation, logs the exception, and resumes scanning.

## Duplicate suppression and repeated barcodes
The default behavior from 60.2.18 is preserved: `AllowRepeatedBarcodes` is `false`, and `DuplicateScanCooldown` remains a non-nullable `TimeSpan` with a default of 800 milliseconds. The effective cooldown is at least 500 milliseconds and blocks all detections after an accepted or rejected scan, not just the same value.

With the timer strategy, confirmed values are cleared when the cooldown expires. With the scan rectangle strategy, confirmed values remain suppressed while they keep being detected; they are cleared when the barcode is lost. Pausing and resuming scanning clears this suppression state.

Set `AllowRepeatedBarcodes = true` explicitly when your workflow must validate repeated values, for example when multiple required sample labels share a barcode:

```csharp
await m_scanner.Start(new BarcodeScannerStartOptions
{
    Preview = CameraPreview,
    OnCameraFailed = CameraFailed,
    Strategy = new ScanRectangleBarcodeScanStrategy(),
    AllowRepeatedBarcodes = true,
    ValidateBarcodeAsync = ValidateBarcodeAsync,
    OnBarcodeAcceptedAsync = OnBarcodeAcceptedAsync,
    OnBarcodeRejectedAsync = OnBarcodeRejectedAsync
});
```

This option bypasses both confirmed-value suppression and `DuplicateScanCooldown`, for either confirmation strategy. It does not bypass confirmation, validation, feedback animations, the failure reset delay, or completion handling. The same value can be confirmed again without first leaving the camera view. Each accepted scan still increments the configured completion count; rejected scans do not.

> **Important:** Your `ValidateBarcodeAsync` callback must still enforce the required count for each label, patient ownership, and all other domain rules. Enabling repeated detections does not mean that a repeated label should automatically be accepted. `Completion.RequiredCount` is a total count, not validation of individual labels.

## Scanner feedback
Accepted and rejected scans trigger success and error haptic feedback respectively. With `ScanRectangleBarcodeScanStrategy`, a non-empty `BarcodeScanValidationResult.ErrorMessage` also appears above the scan rectangle during failure feedback. Supply localized text through `BarcodeScanValidationResult.Invalid(errorMessage, reasonCode)`.

The message temporarily hides custom tooltip content and restores it when the message animation finishes. After a rejected scan, the scanner waits for the bracket reset animation to finish and then waits another 500 milliseconds before the rejection callback and resuming scanning. This failure delay also applies when `AllowRepeatedBarcodes` is `true`; with the default options, the normal duplicate cooldown follows it.

## Automatic zoom hint
Use `Hint` to show the camera zoom tip when scanning takes longer than expected. Automatic hints are disabled by default. When enabled with non-empty text, the default delay is 15 seconds; negative delays are treated as zero.

```csharp
await m_scanner.Start(new BarcodeScannerStartOptions
{
    Preview = CameraPreview,
    OnCameraFailed = CameraFailed,
    Strategy = new ScanRectangleBarcodeScanStrategy(),
    ValidateBarcodeAsync = ValidateBarcodeAsync,
    Hint = new BarcodeScannerHintOptions
    {
        ShowAutomaticHint = true,
        HintText = LocalizedStrings.BarcodeZoomHint,
        Delay = TimeSpan.FromSeconds(15),
        CanShowHintAsync = () => TipService.CanShow(BarcodeScanningZoomTip),
        OnHintShownAsync = () =>
        {
            TipService.DidShow(BarcodeScanningZoomTip);
            return Task.CompletedTask;
        }
    }
});
```

The localization resource and tip service above belong to your app. Both callbacks are optional and run on the main thread. A hint is attempted at most once per `Start()` session, even when `CanShowHintAsync` returns `false`. It is not shown after people have used camera zoom. Callback exceptions are logged without interrupting scanning.

Barcode detection cancels the pending hint. Its delay restarts when scanning resumes or a tracked barcode is lost. Pause, stop, disposal, and a new session cancel pending hints; old scan runs cannot show a delayed hint in a new session. Enabling hints does not change duplicate suppression.

Use `SetTooltipView` after `Start()` only when you need fully custom content above the scan rectangle.

## Stop scanning
Remember to release resources to make sure your page does not leak memory.

Use `PauseScanning()` when you only need to pause barcode processing temporarily, for example while a result bottom sheet is open. This keeps overlay views attached so the scan rectangle does not disappear behind the sheet. Call `ResumeScanning()` when the temporary UI is dismissed.

```csharp
m_scanner.PauseScanning(resetOverlay: false);
m_scanner.ResumeScanning();
```

In most cases it makes sense to do this when the page disappear. Other cases can be when people got the result.
```csharp
protected override void OnDisappearing()
{
    base.OnDisappearing();
    m_scanner.StopAndDispose();
}
```

## Debuggable result 
DIPS.Mobile.UI provides a tailored view that you can use where it makes sense. This is helpful when you need to debug the values of the barcode live in the app when an error occurs. 

```xml
<dui:BarcodeScanResultView BarcodeScanResult="{Binding BarcodeScanResult}" />
```

This gives you an overview of the current barcode along with all the barcodes that was detected during scanning.The barcode you get is the "most detected" barcode.