using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.StreamingState;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing;

public partial class ImageCapture
{
    private readonly List<CapturedImage> m_capturedImages = [];
    private CapturedImagesGalleryButton? m_capturedImagesGalleryButton;
    private CapturedImagesGalleryOverlay? m_galleryOverlay;

    private bool HasReachedImageLimit =>
        m_cameraSession is MultiCaptureSession multiCaptureSession
        && m_capturedImages.Count >= multiCaptureSession.MultiImageCaptureOptions.MaxImageCount;

    /// <summary>
    /// Only show optimistic UI when no per-image confirmation is needed. With per-image confirmation, the image is not
    /// committed until the user accepts it, and should not update UI optimistically.
    /// </summary>
    private bool ShouldUpdateImagePreviewImmediately =>
        m_cameraSession is MultiCaptureSession { MultiImageCaptureOptions.RequiresConfirmationOnEachImage: false };
    
    private void OptimisticallyUpdateGalleryButton()
    {
        if (!ShouldUpdateImagePreviewImmediately)
            return;

        var pendingImageCount = m_capturedImages.Count + 1;
        m_capturedImagesGalleryButton?.ShowPendingCapture(pendingImageCount);
    }

    private CapturedImagesGalleryButton? CreateCapturedImagesGalleryButtonForMultiCapture()
    {
        if (m_cameraSession is not MultiCaptureSession)
        {
            m_capturedImagesGalleryButton = null;
            return null;
        }

        m_capturedImagesGalleryButton = new CapturedImagesGalleryButton(OpenCapturedImagesGallery);
        
        m_capturedImagesGalleryButton.ShowMostRecentImageAndCount(m_capturedImages);
        
        return m_capturedImagesGalleryButton;
    }

    private void AddImageAndUpdateImagePreview(CapturedImage capturedImage)
    {
        m_capturedImages.Add(capturedImage);
        
        m_capturedImagesGalleryButton?.ShowMostRecentImageAndCount(m_capturedImages);

        if (HasReachedImageLimit)
        {
            ShowMaxImagesReachedMessage();
        }
    }

    private void ShowMaxImagesReachedMessage()
    {
        if (m_cameraSession is not MultiCaptureSession multiCaptureSession)
            throw new InvalidOperationException($"{nameof(ShowMaxImagesReachedMessage)} is only valid during a multi capture session, but the session was {m_cameraSession?.GetType().Name ?? "null"}.");

        var maxImageCount = multiCaptureSession.MultiImageCaptureOptions.MaxImageCount;
        _ = DialogService.ShowMessage(configurator => configurator
            .SetTitle(DUILocalizedStrings.MaxImagesReachedTitle)
            .SetDescription(string.Format(DUILocalizedStrings.MaxImagesReachedMessage, maxImageCount))
            .SetActionTitle(DUILocalizedStrings.Ok));
    }

    private void OpenCapturedImagesGallery()
    {
        if (m_capturedImages.Count == 0)
            throw new InvalidOperationException("Captured-images gallery opened with no images. The preview is hidden when empty, so this should be unreachable.");

        if (m_cameraPreview is null)
            throw new InvalidOperationException("Captured-images gallery opened after the camera preview was torn down.");

        // Guard against repeat taps
        if (m_galleryOverlay is not null)
            return;
        
        m_cameraPreview.HidePreview();

        var startingIndex = m_capturedImages.Count - 1;
        
        m_galleryOverlay = new CapturedImagesGalleryOverlay(
            m_capturedImages, 
            startingIndex, 
            onRemoveImageAtAction: RemoveImageAndUpdateGalleryButton, 
            onCloseAction: CloseCapturedImagesGallery);
        
        m_cameraPreview.AddViewToRoot(m_galleryOverlay);
    }

    private async void CloseCapturedImagesGallery()
    {
        try
        {
            ArgumentNullException.ThrowIfNull(m_cameraPreview);
            ArgumentNullException.ThrowIfNull(m_galleryOverlay);
            
            var overlay = m_galleryOverlay;
            m_galleryOverlay = null;
            
            m_cameraPreview.ShowPreview();
            
            await overlay.FadeOutAsync();

            // The user can navigate away from the page during the fade, which nulls the preview.
            if (m_cameraPreview is null)
                return;

            m_cameraPreview.RemoveViewFromRoot(overlay);

            m_capturedImagesGalleryButton?.ShowMostRecentImageAndCount(m_capturedImages);
            m_bottomToolbarView?.SetShutterButtonEnabled(true);
        }
        catch (Exception e)
        {
            Log(e.Message);
        }
    }

    private void TearDownGalleryOverlay()
    {
        m_cameraPreview?.RemoveViewFromRoot(m_galleryOverlay);
        m_galleryOverlay = null;
    }

    private void RemoveImageAndUpdateGalleryButton(int index)
    {
        if (index < 0 || index >= m_capturedImages.Count)
            throw new ArgumentOutOfRangeException(nameof(index), index, $"The gallery asked to remove an image at an index outside the captured set (count {m_capturedImages.Count}).");

        var removedImage = m_capturedImages[index];
        m_capturedImages.RemoveAt(index);

        if (m_cameraSession is MultiCaptureSession multiCaptureSession)
        {
            multiCaptureSession.MultiImageCaptureOptions.OnImageRemoved?.Invoke(removedImage);
        }

        m_capturedImagesGalleryButton?.ShowMostRecentImageAndCount(m_capturedImages);
    }
}
