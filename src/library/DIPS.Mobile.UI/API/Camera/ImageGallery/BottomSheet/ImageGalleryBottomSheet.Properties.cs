namespace DIPS.Mobile.UI.API.Camera.ImageGallery.BottomSheet;

internal partial class ImageGalleryBottomSheet
{
    private List<byte[]> m_images;
    private readonly int m_startingIndex;
    
    /// <summary>
    /// The images to be displayed
    /// </summary>
    private List<byte[]> Images
    {
        get => m_images;
        set
        {
            m_images = value;
            OnImagesChanged();
        }
    }

    /// <summary>
    /// What index the <see cref="CarouselView"/> should start at
    /// </summary>
    private int StartingIndex
    {
        get => m_startingIndex;
        init
        {
            m_startingIndex = value;
            OnStartingIndexChanged();
        }
    }
}