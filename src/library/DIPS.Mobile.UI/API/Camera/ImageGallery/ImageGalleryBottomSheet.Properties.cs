using System.Windows.Input;

namespace DIPS.Mobile.UI.API.Camera.ImageGallery;

internal partial class ImageGalleryBottomSheet
{
    /// <summary>
    /// The images to be displayed
    /// <remarks>The default bindingmode is <see cref="BindingMode.TwoWay"/></remarks>
    /// </summary>
    public List<byte[]> Images
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
    public int StartingIndex
    {
        get => m_startingIndex;
        set
        {
            m_startingIndex = value;
            OnStartingIndexChanged();
        }
    }

    public static readonly BindableProperty ImagesProperty = BindableProperty.Create(
        nameof(Images),
        typeof(List<byte[]>),
        typeof(ImageGalleryBottomSheet),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, _, _) => ((ImageGalleryBottomSheet)bindable).OnImagesChanged());
    
    public static readonly BindableProperty StartingIndexProperty = BindableProperty.Create(
        nameof(StartingIndex),
        typeof(int),
        typeof(ImageGalleryBottomSheet));

    private List<byte[]> m_images;
    private int m_startingIndex;
}