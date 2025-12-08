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
    private readonly List<ImageSource> m_pageImages = new();
    
    private int m_totalPages;
    private int m_currentPage;
    
    private Label m_pageLabel;
    
    private CancellationTokenSource? m_loadAllPagesCancellationTokenSource = new();

    public TiffViewer()
    {
        RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        
        Add(new Components.Loading.ActivityIndicator
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            IsRunning = true
        });
        
        m_gallery = [];
        Add(m_gallery);
        
        /*var navigationBar = CreateNavigationBar();
        this.Add(navigationBar, 0, 1);*/
    }

    
    /*private HorizontalStackLayout CreateNavigationBar()
    {
        var horizontalStackLayout = new HorizontalStackLayout
        {
            Spacing = 8,
            Padding = new Thickness(16, 8),
            HorizontalOptions = LayoutOptions.Center
        };

        var previousButton = new Button
        {
            Text = "◀",
            Command = new Command(() => Navigate(false))
        };
        
        previousButton.SetBinding(IsEnabledProperty, new Binding(nameof(CanGoPrevious), source: this));
        horizontalStackLayout.Children.Add(previousButton);

        m_pageLabel = new Label
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            MinimumWidthRequest = 80
        };
        horizontalStackLayout.Children.Add(m_pageLabel);

        var nextButton = new Buttons.Button 
        { 
            Text = "▶",
            Command = new Command(() => Navigate(true))
        };
        horizontalStackLayout.Children.Add(nextButton);

        return horizontalStackLayout;
    }*/

    private void UpdatePageLabel()
    {
         m_pageLabel.Text = $"{m_currentPage} / {m_totalPages}";
    }

    private async Task LoadTiffPages()
    {
        m_loadAllPagesCancellationTokenSource?.Cancel();
        m_loadAllPagesCancellationTokenSource?.Dispose();
        m_loadAllPagesCancellationTokenSource = new CancellationTokenSource();
        
        m_pageImages.Clear();
        
        var pageCount = await LoadTiffPagesAsync(m_loadAllPagesCancellationTokenSource?.Token);
        m_totalPages = pageCount;
        m_currentPage = 1;
        
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
        UpdatePageLabel();
    }

    private partial Task<int> LoadTiffPagesAsync(CancellationToken? cancellationToken = null);
    private partial Task<ImageSource?> GetPageImageSourceAsync(int pageIndex, CancellationToken? cancellationToken = null);
}
