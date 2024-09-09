namespace DIPS.Mobile.UI.API.Camera.ImageGallery.ImageGalleryThumbnails;

public partial class ImageGalleryThumbnails : Grid
{
    private readonly CollectionView m_collectionView;

    public ImageGalleryThumbnails()
    {
        m_collectionView = new Components.Lists.CollectionView
        {
            ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal),
            ItemTemplate = new DataTemplate(() => new ImageThumbnail.ImageThumbnail(OnRemoveImage, OnTappedImage)),
            HeightRequest = 100,
            
        };
        
        AddColumnDefinition(new ColumnDefinition(GridLength.Auto));
        AddColumnDefinition(new ColumnDefinition(GridLength.Star));
        
        Add(new CaptureButton());
        this.Add(m_collectionView, 1);
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        OnImagesChanged();
    }

    private void OnTappedImage(int imageIndex)
    {
        OnTappedImageCommand.Execute(imageIndex);
    }
    
    private void OnRemoveImage(int imageIndex)
    {
        var copyOfImages = Images.ToList();
        copyOfImages.RemoveAt(imageIndex);
        Images = copyOfImages;
    }

    private void OnImagesChanged()
    {
        var imagesAndCaptureButton = Images.Select((image, index) => new ImageThumbnailViewModel()
        {
            Image = image,
            Index = index
        }).ToList();
        
        m_collectionView.ItemsSource = imagesAndCaptureButton;
    }
}

internal class ImageThumbnailViewModel
{
    public byte[]? Image { get; init; }
    public int Index { get; set; }
}