using CoreGraphics;
using ImageIO;
using UIKit;

namespace DIPS.Mobile.UI.Components.TiffViewer;

public partial class TiffViewer
{
    private CGImageSource? m_imageSource;

    private partial async Task<int> LoadTiffPagesAsync(CancellationToken? cancellationToken = null)
    {
        await Task.CompletedTask;

        if (Source == null || Source.Length == 0)
        {
            m_imageSource?.Dispose();
            m_imageSource = null;
            return 1;
        }

        try
        {
            var data = Foundation.NSData.FromArray(Source);
            
            m_imageSource?.Dispose();
            m_imageSource = CGImageSource.FromData(data);

            if (m_imageSource == null)
            {
                return 1;
            }

            return (int)m_imageSource.ImageCount;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading TIFF on iOS: {ex.Message}");
            m_imageSource?.Dispose();
            m_imageSource = null;
            return 1;
        }
    }

    private partial async Task<ImageSource?> GetPageImageSourceAsync(int pageIndex, CancellationToken? cancellationToken = null)
    {
        await Task.CompletedTask;

        if (m_imageSource == null || pageIndex < 0 || pageIndex >= m_imageSource.ImageCount)
        {
            return null;
        }

        try
        {
            using var cgImage = m_imageSource.CreateImage(pageIndex, null);
            
            if (cgImage == null)
            {
                return null;
            }

            var uiImage = UIImage.FromImage(cgImage);
            
            // Convert UIImage to PNG data
            using var pngData = uiImage.AsPNG();
            
            if (pngData == null)
            {
                return null;
            }

            var bytes = pngData.ToArray();

            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting page {pageIndex} on iOS: {ex.Message}");
            return null;
        }
    }
}
