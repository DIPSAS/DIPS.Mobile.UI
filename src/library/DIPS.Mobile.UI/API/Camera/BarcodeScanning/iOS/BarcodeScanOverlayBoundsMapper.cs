using AVFoundation;
using CoreGraphics;
using DIPS.Mobile.UI.API.Camera.Preview;

namespace DIPS.Mobile.UI.API.Camera.BarcodeScanning;

internal static class BarcodeScanOverlayBoundsMapper
{
    public static RectF? TryGetBarcodeBoundsInOverlay(
        AVMetadataMachineReadableCodeObject readableCodeObject,
        AVCaptureVideoPreviewLayer? previewLayer,
        BarcodeScanRectangleOverlay? scanRectangleOverlay)
    {
        if (previewLayer is null || scanRectangleOverlay is null)
            return null;

        if (!TryGetBarcodeBoundsInPreviewLayer(readableCodeObject, previewLayer, out var previewLayerBounds))
            return null;

        var normalizedBounds = NormalizePreviewLayerBoundsToVisibleCameraFeed(previewLayerBounds, previewLayer.Bounds);
        return scanRectangleOverlay.NormalizedBoundsToOverlay(
            normalizedBounds.X,
            normalizedBounds.Y,
            normalizedBounds.Width,
            normalizedBounds.Height);
    }

    private static bool TryGetBarcodeBoundsInPreviewLayer(
        AVMetadataMachineReadableCodeObject readableCodeObject,
        AVCaptureVideoPreviewLayer previewLayer,
        out CGRect previewLayerBounds)
    {
        var transformedMetadataObject = previewLayer.GetTransformedMetadataObject(readableCodeObject);
        if (transformedMetadataObject is null)
        {
            previewLayerBounds = default;
            return false;
        }

        previewLayerBounds = transformedMetadataObject.Bounds;
        return true;
    }

    private static RectF NormalizePreviewLayerBoundsToVisibleCameraFeed(CGRect previewLayerBounds, CGRect previewLayerFrame)
    {
        var visibleCameraFeedFrame = GetVisibleCameraFeedFrame(previewLayerFrame);

        return new RectF(
            (float)previewLayerBounds.X / visibleCameraFeedFrame.Width,
            ((float)previewLayerBounds.Y - visibleCameraFeedFrame.Y) / visibleCameraFeedFrame.Height,
            (float)previewLayerBounds.Width / visibleCameraFeedFrame.Width,
            (float)previewLayerBounds.Height / visibleCameraFeedFrame.Height);
    }

    private static RectF GetVisibleCameraFeedFrame(CGRect previewLayerFrame)
    {
        var layerWidth = (float)previewLayerFrame.Width;
        var layerHeight = (float)previewLayerFrame.Height;
        var feedHeight = Math.Min(layerWidth / CameraPreview.ThreeFourRatio, layerHeight);
        var feedTop = (layerHeight - feedHeight) / 2f;

        return new RectF(0, feedTop, layerWidth, feedHeight);
    }
}