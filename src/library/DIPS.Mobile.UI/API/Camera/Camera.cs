using DIPS.Mobile.UI.API.Camera.BarcodeScanning;
using DIPS.Mobile.UI.API.Camera.ImageCapturing;
using DIPS.Mobile.UI.API.Camera.Preview;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;

namespace DIPS.Mobile.UI.API.Camera;

public class Camera
{

    private ImageCapture? m_imageCapture;
    private BarcodeScanner? m_barCodeScanner;

    public async Task StartImageCapture(DidCaptureImage didCaptureImageDelegate, CameraFailed cameraFailedDelegate)
    {
        try
        {
            var cameraPreview = await OpenAndSetCameraPreview();
            if (cameraPreview == null) return;
            m_imageCapture ??= new ImageCapture();
            await m_imageCapture.Start(cameraPreview, didCaptureImageDelegate, cameraFailedDelegate);
        }
        catch (Exception e)
        {
            await Stop();
            throw;
        }
    }

    public async Task StartBarcodeScanning(DidFindBarcodeCallback didFindBarcodeCallback,
        CameraFailed cameraFailedDelegate)
    {
        try
        {
            var cameraPreview = await OpenAndSetCameraPreview();
            if (cameraPreview == null) return;
            m_barCodeScanner ??= new BarcodeScanner();
            await m_barCodeScanner.Start(cameraPreview, didFindBarcodeCallback, cameraFailedDelegate);
        }
        catch (Exception e)
        {
            await Stop();
            throw;
        }
    }

    private async Task<CameraPreview?> OpenAndSetCameraPreview()
    {
        if (Application.Current == null || Application.Current.MainPage == null)
        {
            return null;
        }

        var (page, cameraPreview ) = CreatePageAndCameraPreview();
        await Application.Current.MainPage.Navigation.PushModalAsync(page, true);
        return cameraPreview;

    }
    
    private Tuple<ContentPage, CameraPreview> CreatePageAndCameraPreview()
    {
        var cameraPreview = new CameraPreview();
        var page = new ContentPage(){ AutomationId = $"{nameof(Camera)}ModalPage", Content = cameraPreview, ToolbarItems = { new ToolbarItem {Text = DUILocalizedStrings.Finished, Command = new Command(() => _ = Stop())} }};
        return new Tuple<ContentPage, CameraPreview>(page, cameraPreview);
    }

    public async Task Stop()
    {
        if (Application.Current != null && Application.Current.MainPage != null)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync(animated: true);
        }
    }

}