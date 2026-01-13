using DIPS.Mobile.UI.Internal.Logging;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.TiffViewer;

/// <summary>
/// A viewer component for multi-page TIFF images with zoom and pan support.
/// </summary>
[ContentProperty(nameof(Source))]
public partial class TiffViewer : Grid
{
    private readonly Gallery.Gallery m_gallery;
    private readonly List<ImageSource> m_pageImages = [];
    
    private CancellationTokenSource? m_loadAllPagesCancellationTokenSource = new();

    public TiffViewer()
    {
        RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        
        m_gallery = [];
        Add(m_gallery);
        
        Add(new Components.Loading.ActivityIndicator
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            IsRunning = true
        });
        
        m_gallery.SetBinding(Gallery.Gallery.GalleryCustomizerProperty, static (TiffViewer tiffViewer) => tiffViewer.GalleryCustomizer, source: this);
        m_gallery.SetBinding(BackgroundColorProperty, static (TiffViewer tiffViewer) => tiffViewer.BackgroundColor, source: this);
    }

    private async Task LoadTiffPages()
    {
        m_loadAllPagesCancellationTokenSource?.Cancel();
        m_loadAllPagesCancellationTokenSource?.Dispose();
        m_loadAllPagesCancellationTokenSource = new CancellationTokenSource();
        
        m_pageImages.Clear();
        
        var pageCount = await LoadTiffPagesAsync(m_loadAllPagesCancellationTokenSource?.Token);
        
        // Load all pages into the gallery
        for (var i = 0; i < pageCount; i++)
        {
            try
            {
                var imageSource = await GetPageImageSourceAsync(i, m_loadAllPagesCancellationTokenSource?.Token);
                if (imageSource != null)
                {
                    m_pageImages.Add(imageSource);
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception e)
            {
                DUILogService.LogError<TiffViewer>($"Error loading page {i + 1}: {e.Message}");
            }
        }
        
        m_gallery.Images = m_pageImages;
        // Remove spinner
        RemoveAt(1);
    }

    private partial Task<int> LoadTiffPagesAsync(CancellationToken? cancellationToken = null);
    private partial Task<ImageSource?> GetPageImageSourceAsync(int pageIndex, CancellationToken? cancellationToken = null);
}
