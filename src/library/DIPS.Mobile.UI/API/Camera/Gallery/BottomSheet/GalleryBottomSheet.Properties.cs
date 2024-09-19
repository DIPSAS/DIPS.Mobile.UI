using DIPS.Mobile.UI.API.Camera.ImageCapturing;

namespace DIPS.Mobile.UI.API.Camera.Gallery.BottomSheet;

internal partial class GalleryBottomSheet
{
    private List<CapturedImage> m_images;
    private readonly int m_startingIndex;
    
    /// <summary>
    /// The images to be displayed
    /// </summary>
    private List<CapturedImage> Images
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