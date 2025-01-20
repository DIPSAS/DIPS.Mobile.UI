using DIPS.Mobile.UI.API.Camera.Gallery.BottomSheet;
using DIPS.Mobile.UI.API.Camera.ImageCapturing;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Microsoft.Maui.Controls.Handlers.Items;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.API.Camera.Gallery;

public partial class GalleryThumbnails : Grid
{
    private readonly CollectionView m_collectionView;

    public GalleryThumbnails()
    {
        HeightRequest = 100;
        
        VerticalOptions = LayoutOptions.Center;
        
        m_collectionView = new Components.Lists.CollectionView
        {
            ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal),
            ItemTemplate = new DataTemplate(() => new ImageThumbnailView.ImageThumbnailView(OnRemoveImage, OnTappedImage)),
            HasAdditionalSpaceAtTheEnd = false
        };
        
        AddColumnDefinition(new ColumnDefinition(GridLength.Auto));
        AddColumnDefinition(new ColumnDefinition(GridLength.Star));

        var cameraButton = new Button
        {
            ImageSource = Icons.GetIcon(IconName.camera),
            ImageTintColor = Colors.GetColor(ColorName.color_neutral_80),
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
            CornerRadius = Sizes.GetSize(SizeName.size_2),
            BackgroundColor = Colors.GetColor(ColorName.color_neutral_30),
            WidthRequest = Sizes.GetSize(SizeName.size_15),
            HeightRequest = Sizes.GetSize(SizeName.size_15),
            Command = new Command(Execute)
        };
        
        Add(cameraButton);
        this.Add(m_collectionView, 1);
    }

    private void Execute()
    {
        CameraButtonTappedCommand?.Execute(null);
        CameraButtonTapped?.Invoke(this, EventArgs.Empty);
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);
        
        if(args.NewHandler is not null)
            OnImagesChanged();
    }

    private void OnTappedImage(int imageIndex)
    {
        new GalleryBottomSheet(Images, imageIndex, OnRemoveImage, UpdateImages).Open();
    }

    private void UpdateImages()
    {
        var copyOfImages = Images.ToList();
        Images = copyOfImages;        
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
            Image = image.ThumbnailAsByteArray ?? image.AsByteArray,
            Index = index
        }).ToList();
        
        m_collectionView.ItemsSource = imagesAndCaptureButton;
    }

    /// <summary>
    /// Add a image to the gallery, the <see cref="Images"/> binding will be updated.
    /// </summary>
    /// <param name="capturedImage"></param>
    public void AddImage(CapturedImage capturedImage)
    {
        if (Images == null || Images.Count == 0)
        {
            Images = [capturedImage];
        }
        else
        {
            var newImages = Images.ToList();
            newImages.Add(capturedImage);
            Images = newImages;
        }
    }
}

internal class ImageThumbnailViewModel
{
    public byte[]? Image { get; init; }
    public int Index { get; init; }
}