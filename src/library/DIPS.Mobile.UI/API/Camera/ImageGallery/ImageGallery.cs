using DIPS.Mobile.UI.API.Camera.ImageGallery.BottomSheet;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.API.Camera.ImageGallery;

public partial class ImageGallery : Grid
{
    private readonly CollectionView m_collectionView;

    public ImageGallery()
    {
        m_collectionView = new Components.Lists.CollectionView
        {
            ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal),
            ItemTemplate = new DataTemplate(() => new ImageThumbnailView.ImageThumbnailView(OnRemoveImage, OnTappedImage)),
            HeightRequest = 100
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
            HeightRequest = Sizes.GetSize(SizeName.size_15)
        };
        
        cameraButton.SetBinding(Microsoft.Maui.Controls.Button.CommandProperty, new Binding(nameof(CameraButtonTappedCommand)));
        
        Add(cameraButton);
        this.Add(m_collectionView, 1);
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        OnImagesChanged();
    }

    private void OnTappedImage(int imageIndex)
    {
        new ImageGalleryBottomSheet(Images, imageIndex, OnRemoveImage).Open();
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
    public int Index { get; init; }
}