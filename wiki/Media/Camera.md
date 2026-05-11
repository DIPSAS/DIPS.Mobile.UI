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
        await m_scanner.Start(CameraPreview, CameraFailed, settings =>
        {
            settings.OnValidBarcodeScanned = result =>
            {
                var theBarCode = result.Barcode.RawValue;
            };
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
private readonly BarcodeScanningSettings m_barcodeScanningSettings = new()
{
    RequiredScanCount = 5,
    ShowScanRectangle = true,
    OnValidBarcodeScanned = OnValidBarcodeScanned,
    ValidateBarcodeAsync = ValidateBarcodeAsync,
    OnInvalidBarcodeScannedAsync = OnInvalidBarcodeScannedAsync,
    OnRequiredScanCountCompletedAsync = OnRequiredScanCountCompletedAsync
};

private void OnValidBarcodeScanned(BarcodeScanResult result)
{
    var acceptedBarcode = result.Barcode.RawValue;
}

private async Task<BarcodeScanValidationResult> ValidateBarcodeAsync(string barcode)
{
    var isValid = await ViewModel.ValidateBarcodeAsync(barcode);

    return isValid
        ? BarcodeScanValidationResult.Valid()
        : BarcodeScanValidationResult.Invalid();
}

private Task OnInvalidBarcodeScannedAsync(BarcodeScanResult result, BarcodeScanValidationResult validationResult)
{
    return Task.CompletedTask;
}

private Task OnRequiredScanCountCompletedAsync()
{
    return Task.CompletedTask;
}
```

```csharp
await m_scanner.Start(CameraPreview, CameraFailed, m_barcodeScanningSettings);
```

When `RequiredScanCount` is greater than zero, the scanner displays a simple counter at the top of the bottom toolbar, such as `3 av 5 skannet`. Accepted scans animate a barcode token into the counter label, animate the counter text, and increment `CurrentScanCount`; rejected scans do not change the counter. When `CurrentScanCount` reaches `RequiredScanCount`, `OnRequiredScanCountCompletedAsync` is invoked and scanning stops by default. Set `StopScanningWhenCompleted` to `false` if the consumer should decide when to stop the camera.

If `ValidateBarcodeAsync` throws, the scanner treats the barcode as invalid, plays the failure animation, logs the exception, and resumes scanning.

## Stop scanning
Remember to release resources to make sure your page does not leak memory.

Use `StopScanning()` when you only need to pause camera analysis temporarily, for example while a result bottom sheet is open. This keeps overlay views attached so the scan rectangle does not disappear behind the sheet.

```csharp
m_scanner.StopScanning(resetOverlay: false);
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